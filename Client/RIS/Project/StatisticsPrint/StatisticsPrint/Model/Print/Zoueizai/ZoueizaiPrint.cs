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
using StatisticsPrint.Model.Print.Bui.Data;
using StatisticsPrint.Model.Print.Common.Data;
using StatisticsPrint.Model.Print.Zoueizai.Data;

namespace StatisticsPrint.Model.Print.Zoueizai
{
	/// <summary>
	/// 造影剤・薬剤一覧印刷
	/// </summary>
	internal class ZoueizaiPrint : PrintBase
	{
		#region プロパティ
		/// <summary>
		/// ドキュメント名
		/// </summary>
		public override string DocumentName
		{
			get
			{
				return "造影・薬剤一覧";
			}
		}

		/// <summary>
		/// フォームファイルパス
		/// </summary>
		public override string FormFilePath
		{
			get
			{
				return ConfigurationManager.AppSettings["FormFileZoueizai"].StringToString();
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

			ZoueizaiItem data = GetResultData(condition);
			List<ZoueizaiItemZoueizai> list = data.ZoueizaiItemList;
			if (list.Count < 1)
			{
				_log.Debug("出力データがありません");
				MessageBox.Show("出力データがありません。", "情報", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				return;
			}

			using (COReportsHelper helper = new COReportsHelper())
			{
				IPrintWriter writer = new ZoueizaiWriter()
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
		private ZoueizaiItem GetResultData(ConditionPrint condition)
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

		/// <summary>
		/// コマンドオブジェクトから結果を取得する
		/// </summary>
		/// <param name="conditionPlace"></param>
		/// <param name="command"></param>
		/// <returns></returns>
		private ZoueizaiItem GetResultDataByCommand(ConditionPrintPlace conditionPlace, IDbCommand command)
		{
			command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
			command.CommandText = "STATISTICS_PRINT_PACKAGE.GetZoueizaiData";
			command.CommandType = CommandType.StoredProcedure;

			#region パラメータ設定
			IDataParameter p = command.CreateParameter();
			p.SetInputDateTime("P_EXAMENDDATE", conditionPlace.Nengetsu);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			p.SetInputString("P_PLACE_ID", conditionPlace.PlaceID);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_ZOUEIZAI_CURSOR");
			command.Parameters.Add(p);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);

			_log.Debug("プロシージャ実行します");
			command.ExecuteNonQuery();
			_log.Debug("プロシージャ実行しました");
			OracleCommand cmd = command as OracleCommand;
			using (OracleRefCursor cur1 = (OracleRefCursor)cmd.Parameters["P_ZOUEIZAI_CURSOR"].Value)
			{
				using (OracleDataReader dr1 = cur1.GetDataReader())
				{
					ZoueizaiItem data = new ZoueizaiItem();

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

		private void AddResultData(OracleDataReader dr1, ZoueizaiItem data)
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

			AddZoueizaiList(dr1, data.ZoueizaiItemList);
		}

		private void AddZoueizaiList(OracleDataReader dr1, List<ZoueizaiItemZoueizai> zoueizaiList)
		{
			string zoueizaiID = dr1.GetStringByDB("ZOUEIZAI_ID");
			ZoueizaiItemZoueizai zoueizaiItem = zoueizaiList.FirstOrDefault(b => b.ZOUEIZAI_ID == zoueizaiID);
			if (zoueizaiItem == null)
			{
				zoueizaiItem = new ZoueizaiItemZoueizai();
				zoueizaiItem.ZOUEIZAI_ID = dr1.GetStringByDB("ZOUEIZAI_ID");
				zoueizaiItem.ZOUEIZAI_NAME = dr1.GetStringByDB("ZOUEIZAI_NAME");
				zoueizaiItem.ZOUEIZAI_SHOWORDER = dr1.GetStringByDB("ZOUEIZAI_SHOWORDER");
				zoueizaiItem.ZOUEIZAITANNI_NAME = dr1.GetStringByDB("ZOUEIZAITANNI_NAME");
				zoueizaiList.Add(zoueizaiItem);
			}

			string gyoSyosai = dr1.GetStringByDB("GYO_SYOSAI");
			const string GYO_SYOSAI_TOTAL_KUBUN = "小計(造影剤)";
			if (GYO_SYOSAI_TOTAL_KUBUN == gyoSyosai)
			{
				zoueizaiItem.Total = dr1.GetStringByDB("CNT");
				return;
			}

			zoueizaiItem.AddDay(dr1.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr1.GetStringByDB("CNT"));
		}
		#endregion

		#endregion メソッド

	}
}
