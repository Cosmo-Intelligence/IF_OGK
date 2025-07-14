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
using StatisticsPrint.Model.Print.CT.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.CT
{
	/// <summary>
	/// ＣＴ　分類別集計表印刷
	/// </summary>
	internal class CTPrint : PrintBase
	{
		#region プロパティ
		/// <summary>
		/// ドキュメント名
		/// </summary>
		public override string DocumentName
		{
			get
			{
				return "ＣＴ　分類別集計表";
			}
		}

		/// <summary>
		/// フォームファイルパス
		/// </summary>
		public override string FormFilePath
		{
			get
			{
				return ConfigurationManager.AppSettings["FormFileCT"].StringToString();
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

			CTItem data = GetResultData(condition);
			List<CTItemPlace> list = data.CTItemList;
			if (list.Count < 1)
			{
				_log.Debug("出力データがありません");
				MessageBox.Show("出力データがありません。", "情報", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				return;
			}

			using (COReportsHelper helper = new COReportsHelper())
			{
				IPrintWriter writer = new CTWriter()
				{
					Condition = condition,
					Data = data
				};

				PrintExecute(condition, helper, writer);
			}
		}

		#endregion

		#region private
		
		/// <summary>
		/// 取得結果
		/// </summary>
		/// <param name="condition"></param>
		/// <returns></returns>
		private CTItem GetResultData(ConditionPrint condition)
		{
			IDbConnection cn = ODPConnectionUtils.Connect(Settings.Default.ConnectionString);
			try
			{
				using (IDbCommand command = cn.CreateCommand())
				{
					return GetResultDataByCommand(condition, command);
				}
			}
			finally
			{
				cn.Close();
			}
		}

		/// <summary>
		/// コマンドオブジェクトから結果を取得する
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		private CTItem GetResultDataByCommand(ConditionPrint condition, IDbCommand command)
		{
			command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
			command.CommandText = "STATISTICS_PRINT_PACKAGE.GetCTData";
			command.CommandType = CommandType.StoredProcedure;

			#region パラメータ設定
			IDataParameter p = command.CreateParameter();
			p.SetInputDateTime("P_EXAMENDDATE", condition.Nengetsu);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_CT_CURSOR");
			command.Parameters.Add(p);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);

			_log.Debug("プロシージャ実行します");
			command.ExecuteNonQuery();
			_log.Debug("プロシージャ実行しました");
			OracleCommand cmd = command as OracleCommand;
			using (OracleRefCursor cur1 = (OracleRefCursor)cmd.Parameters["P_CT_CURSOR"].Value)
			{
				using (OracleDataReader dr1 = cur1.GetDataReader())
				{
					CTItem data = new CTItem();

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

		private void AddResultData(OracleDataReader dr1, CTItem data)
		{
			//MiscUtils.WriteDataReaderLogForLog4net(dr1, _log);

			#region 帳票単位

			#endregion

			AddCTList(dr1, data);
		}

		private void AddCTList(OracleDataReader dr1, CTItem data)
		{

			#region 実施場所ごと

			string placeID = dr1.GetStringByDB("PLACE_ID");
			List<CTItemPlace> list = data.CTItemList;
			CTItemPlace placeItem = list.FirstOrDefault(k => k.PLACE_ID == placeID);
			if (placeItem == null)
			{
				placeItem = new CTItemPlace();
				placeItem.PLACE_ID = dr1.GetStringByDB("PLACE_ID");
				placeItem.PLACE_NAME = dr1.GetStringByDB("PLACE_NAME");
				placeItem.PLACE_SHOWORDER = dr1.GetStringByDB("PLACE_SHOWORDER");
				list.Add(placeItem);
			}

			#endregion

			AddBunuiList(dr1, placeItem);
		}

		private void AddBunuiList(OracleDataReader dr1, CTItemPlace placeItem)
		{
			#region 分類ごと

			string bunruiName = dr1.GetStringByDB("BUNRUI_NAME");
			List<CTItemBunrui> list = placeItem.CTItemBunruiList;
			CTItemBunrui bunruiItem = list.FirstOrDefault(b => b.BUNRUI_NAME == bunruiName);
			if (bunruiItem == null)
			{
				bunruiItem = new CTItemBunrui();
				bunruiItem.BUNRUI_NAME = dr1.GetStringByDB("BUNRUI_NAME");
				bunruiItem.BUNRUI_SHOWORDER = dr1.GetStringByDB("BUNRUI_SHOWORDER");
				list.Add(bunruiItem);
			}

			#endregion

			AddKubunList(dr1, bunruiItem);
		}

		private void AddKubunList(OracleDataReader dr1, CTItemBunrui bunruiItem)
		{
			#region 区分ごと

			string kbnName = dr1.GetStringByDB("KBN_NAME");
			List<CTItemKubun> list = bunruiItem.CTItemKubunList;
			CTItemKubun kbnItem = list.FirstOrDefault(k => k.KBN_NAME == kbnName);
			if (kbnItem == null)
			{
				kbnItem = new CTItemKubun();
				kbnItem.KBN_NAME = dr1.GetStringByDB("KBN_NAME");
				kbnItem.KBN_SHOWORDER = dr1.GetStringByDB("KBN_SHOWORDER");
				list.Add(kbnItem);
			}

			string gyoSyosai = dr1.GetStringByDB("GYO_SYOSAI");
			const string GYO_SYOSAI_TOTAL_KENSA_BUI = "小計(区分)";
			if (GYO_SYOSAI_TOTAL_KENSA_BUI == gyoSyosai)
			{
				kbnItem.Total = dr1.GetStringByDB("CNT");
				return;
			}

			#endregion

			kbnItem.AddDay(dr1.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr1.GetStringByDB("CNT"));
		}

		#endregion

		#endregion メソッド

	}
}
