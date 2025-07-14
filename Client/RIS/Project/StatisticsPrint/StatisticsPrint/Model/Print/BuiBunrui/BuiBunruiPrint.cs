using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;
using StatisticsPrint.Data;
using StatisticsPrint.Model.COReports;
using StatisticsPrint.Properties;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.BuiBunrui.Data;
using StatisticsPrint.Model.Print.Common.Data;


namespace StatisticsPrint.Model.Print.BuiBunrui
{
	/// <summary>
	/// 撮影検査数一覧（部位分類）印刷
	/// </summary>
	internal class BuiBunruiPrint : PrintBase
	{
		#region プロパティ
		/// <summary>
		/// ドキュメント名
		/// </summary>
		public override string DocumentName
		{
			get
			{
				return "撮影検査数一覧（部位分類）";
			}
		}

		/// <summary>
		/// フォームファイルパス
		/// </summary>
		public override string FormFilePath
		{
			get
			{
				return ConfigurationManager.AppSettings["FormFileBUIBUNRUI"].StringToString();
			}
		}
		#endregion

		#region メソッド

		#region public

		/// <summary>
		/// 印刷する
		/// </summary>
		/// <param name="condition"></param>
		public override void Print(ConditionPrint condition)
		{
			if (condition == null)
			{
				return;
			}

			BuiBunruiItem data = GetResultData(condition);
			List<BuiBunruiItemKensaType> list = data.KensaTypeItemList;
			if (list.Count < 1)
			{
				_log.Debug("出力データがありません");
				MessageBox.Show("出力データがありません。", "情報", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				return;
			}

			using (COReportsHelper helper = new COReportsHelper())
			{
				IPrintWriter writer = new BuiBunruiWriter()
				{
					Condition = condition,
					Data = data
				};

				PrintExecute(condition, helper, writer);
			}
		}

		#endregion

		#region private
		
		private BuiBunruiItem GetResultData(ConditionPrint condition)
		{
			ConditionPrintPlace conditionPlace = condition as ConditionPrintPlace;
			IDbConnection cn = ODPConnectionUtils.Connect(Settings.Default.ConnectionString);
			try
			{
				using (IDbCommand command = cn.CreateCommand())
				{
					return GetResultDataByCommand(conditionPlace, command);
				}
			}
			finally
			{
				cn.Close();
			}
		}

		private BuiBunruiItem GetResultDataByCommand(ConditionPrintPlace conditionPlace, IDbCommand command)
		{
			command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
			command.CommandText = "STATISTICS_PRINT_PACKAGE.GetBuiBunruiData";
			command.CommandType = CommandType.StoredProcedure;

			#region パラメータ設定
			IDataParameter p = command.CreateParameter();
			p.SetInputDateTime("P_EXAMENDDATE", conditionPlace.Nengetsu);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			p.SetInputString("P_PLACE_ID", conditionPlace.PlaceID);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_BUIBUNRUI_CURSOR");
			command.Parameters.Add(p);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);

			_log.Debug("プロシージャ実行します");
			command.ExecuteNonQuery();
			_log.Debug("プロシージャ実行しました");
			OracleCommand cmd = command as OracleCommand;
			using (OracleRefCursor cur1 = (OracleRefCursor)cmd.Parameters["P_BUIBUNRUI_CURSOR"].Value)
			{
				using (OracleDataReader dr1 = cur1.GetDataReader())
				{
					BuiBunruiItem data = new BuiBunruiItem();

					int count = 0;
					while (dr1.Read())
					{
						count++;
						AddResultData(dr1, data);
					}
					_log.DebugFormat("{0}件取得しました", count);
					return data;
				}
			}

		}

		private void AddResultData(OracleDataReader dr1, BuiBunruiItem data)
		{
			//MiscUtils.WriteDataReaderLogForLog4net(dr1, _log);

			string gyoSyosai = dr1.GetStringByDB("GYO_SYOSAI");

			#region 帳票単位
			
			const string GYO_SYOSAI_TOTAL = "総計";
			if (GYO_SYOSAI_TOTAL == gyoSyosai)
			{
				data.Total = dr1.GetStringByDB("CNT");
				return;
			}

			const string GYO_SYOSAI_TOTAL_DAY = "小計(日付)";
			if (GYO_SYOSAI_TOTAL_DAY == gyoSyosai)
			{
				data.AddDay(dr1.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr1.GetStringByDB("CNT"));
				return;
			}
			#endregion

			#region 検査種別ごと
			
			string kensaTypeID = dr1.GetStringByDB("KENSATYPE_ID");
			List<BuiBunruiItemKensaType> list = data.KensaTypeItemList;

			BuiBunruiItemKensaType kensaTypeItem = list.FirstOrDefault(k => k.KENSATYPE_ID == kensaTypeID);
			if (kensaTypeItem == null)
			{
				kensaTypeItem = new BuiBunruiItemKensaType();
				kensaTypeItem.KENSATYPE_ID = dr1.GetStringByDB("KENSATYPE_ID");
				kensaTypeItem.KENSATYPE_NAME = dr1.GetStringByDB("KENSATYPE_NAME");
				kensaTypeItem.KENSATYPE_SHOWORDER = dr1.GetStringByDB("KENSATYPE_SHOWORDER");
				list.Add(kensaTypeItem);
			}

			const string GYO_SYOSAI_TOTAL_KENSA = "小計(検査種別)";
			if (GYO_SYOSAI_TOTAL_KENSA == gyoSyosai)
			{
				kensaTypeItem.Total = dr1.GetStringByDB("CNT");
				return;
			}

			const string GYO_SYOSAI_TOTAL_KENSA_DAY = "小計(検査種別+日付)";
			if (GYO_SYOSAI_TOTAL_KENSA_DAY == gyoSyosai)
			{
				kensaTypeItem.AddDay(dr1.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr1.GetStringByDB("CNT"));
				return;
			}

			#endregion

			AddBuiBunruiList(dr1, kensaTypeItem.BuiBunruiItemList);
		}

		private void AddBuiBunruiList(OracleDataReader dr1, List<BuiBunruiItemBuiBunrui> buiBunruiList)
		{
			string buiBunruiID = dr1.GetStringByDB("BUIBUNRUI_ID");
			BuiBunruiItemBuiBunrui buiBunruiitem = buiBunruiList.FirstOrDefault(b => b.BUIBUNRUI_ID == buiBunruiID);
			if (buiBunruiitem == null)
			{
				buiBunruiitem = new BuiBunruiItemBuiBunrui();
				buiBunruiitem.BUIBUNRUI_ID = dr1.GetStringByDB("BUIBUNRUI_ID");
				buiBunruiitem.BUIBUNRUI_NAME = dr1.GetStringByDB("BUIBUNRUI_NAME");
				buiBunruiitem.BUI_SHOWORDER = dr1.GetStringByDB("BUI_SHOWORDER");
				buiBunruiList.Add(buiBunruiitem);
			}

			string gyoSyosai = dr1.GetStringByDB("GYO_SYOSAI");
			const string GYO_SYOSAI_TOTAL_KUBUN = "小計(検査種別+部位分類)";
			if (GYO_SYOSAI_TOTAL_KUBUN == gyoSyosai)
			{
				buiBunruiitem.Total = dr1.GetStringByDB("CNT");
				return;
			}
	
			buiBunruiitem.AddDay(dr1.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr1.GetStringByDB("CNT"));
		}

		#endregion

		#endregion メソッド

	}
}
