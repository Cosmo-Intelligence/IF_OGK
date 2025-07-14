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
using StatisticsPrint.Model.Print.US.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.US
{
	/// <summary>
	/// ＵＳ　分類別集計表印刷
	/// </summary>
	internal class USPrint : PrintBase
	{
		#region プロパティ
		/// <summary>
		/// ドキュメント名
		/// </summary>
		public override string DocumentName
		{
			get
			{
				return "ＵＳ　分類別集計表";
			}
		}

		/// <summary>
		/// フォームファイルパス
		/// </summary>
		public override string FormFilePath
		{
			get
			{
				return ConfigurationManager.AppSettings["FormFileUS"].StringToString();
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

			USItem data = GetResultData(condition);
			if (data.Count < 1)
			{
				_log.Debug("出力データがありません");
				MessageBox.Show("出力データがありません。", "情報", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				return;
			}

			using (COReportsHelper helper = new COReportsHelper())
			{
				IPrintWriter writer = new USWriter()
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
		private USItem GetResultData(ConditionPrint condition)
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
		private USItem GetResultDataByCommand(ConditionPrint condition, IDbCommand command)
		{
			command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
			command.CommandText = "STATISTICS_PRINT_PACKAGE.GetUSData";
			command.CommandType = CommandType.StoredProcedure;

			#region パラメータ設定
			IDataParameter p = command.CreateParameter();
			p.SetInputDateTime("P_EXAMENDDATE", condition.Nengetsu);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_US_CURSOR");
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_US_DOPPLER_CURSOR");
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_US_ZOUEIZAI_CURSOR");
			command.Parameters.Add(p);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);

			_log.Debug("プロシージャ実行します");
			command.ExecuteNonQuery();
			_log.Debug("プロシージャ実行しました");
			OracleCommand cmd = command as OracleCommand;

			USItem data = new USItem();
			#region US
			
			using (OracleRefCursor cur = (OracleRefCursor)cmd.Parameters["P_US_CURSOR"].Value)
			{
				using (OracleDataReader dr = cur.GetDataReader())
				{

					int count = 0;
					while (dr.Read())
					{
						count++;
						AddResultDataUS(dr, data);
					}
					_log.DebugFormat("US{0}件取得しました", count);
				}
			}
			#endregion

			#region Doppler

			using (OracleRefCursor cur = (OracleRefCursor)cmd.Parameters["P_US_DOPPLER_CURSOR"].Value)
			{
				using (OracleDataReader dr = cur.GetDataReader())
				{
					int count = 0;
					while (dr.Read())
					{
						count++;
						AddResultDataDoppler(dr, data);
					}
					_log.DebugFormat("Doppler{0}件取得しました", count);
				}
			}
			#endregion

			#region Zoueizai

			using (OracleRefCursor cur = (OracleRefCursor)cmd.Parameters["P_US_ZOUEIZAI_CURSOR"].Value)
			{
				using (OracleDataReader dr = cur.GetDataReader())
				{
					int count = 0;
					while (dr.Read())
					{
						count++;
						AddResultDataZoueizai(dr, data);
					}
					_log.DebugFormat("Zoueizai{0}件取得しました", count);
				}
			}
			#endregion

			return data;
		}

		private void AddResultDataUS(OracleDataReader dr, USItem data)
		{
			//MiscUtils.WriteDataReaderLogForLog4net(dr, _log);

			string gyoSyosai = dr.GetStringByDB("GYO_SYOSAI");

			#region 帳票単位
			const string GYO_SYOSAI_TOTAL = "総計";
			if (GYO_SYOSAI_TOTAL == gyoSyosai)
			{
				data.Total = dr.GetStringByDB("CNT");
				return;
			}

			const string GYO_SYOSAI_TOTAL_DAY = "小計(日付)";
			if (GYO_SYOSAI_TOTAL_DAY == gyoSyosai)
			{
				data.AddDay(dr.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr.GetStringByDB("CNT"));
				return;
			}

			#endregion

			AddBunruiList(dr, data.USItemBunruiListUS);
		}

		private void AddResultDataDoppler(OracleDataReader dr, USItem data)
		{
			//MiscUtils.WriteDataReaderLogForLog4net(dr, _log);

			#region 帳票単位

			#endregion

			AddBunruiList(dr, data.USItemBunruiListDoppler);
		}

		private void AddResultDataZoueizai(OracleDataReader dr, USItem data)
		{
			//MiscUtils.WriteDataReaderLogForLog4net(dr, _log);

			#region 帳票単位

			#endregion

			AddKubunList(dr, data.USItemZoueizaiList);
		}

		private void AddBunruiList(OracleDataReader dr, List<USItemBunrui> list)
		{
			#region 分類ごと

			string bunruiName = dr.GetStringByDB("BUNRUI_NAME");
			USItemBunrui bunruiItem = list.FirstOrDefault(b => b.BUNRUI_NAME == bunruiName);
			if (bunruiItem == null)
			{
				bunruiItem = new USItemBunrui();
				bunruiItem.BUNRUI_NAME = dr.GetStringByDB("BUNRUI_NAME");
				bunruiItem.BUNRUI_SHOWORDER = dr.GetStringByDB("BUNRUI_SHOWORDER");
				list.Add(bunruiItem);
			}

			string gyoSyosai = dr.GetStringByDB("GYO_SYOSAI");
			const string GYO_SYOSAI_TOTAL_KUBUN = "小計(分類)";
			if (GYO_SYOSAI_TOTAL_KUBUN == gyoSyosai)
			{
				bunruiItem.Total = dr.GetStringByDB("CNT");
				return;
			}

			const string GYO_SYOSAI_TOTAL_BUNRUI_DAY = "小計(分類+日付)";
			if (GYO_SYOSAI_TOTAL_BUNRUI_DAY == gyoSyosai)
			{
				bunruiItem.AddDay(dr.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr.GetStringByDB("CNT"));
				return;
			}

	
			#endregion

			AddKubunList(dr, bunruiItem.USItemKubunList);
		}

		private void AddKubunList(OracleDataReader dr, List<USItemKubun> list)
		{
			#region 区分ごと

			string kbnName = dr.GetStringByDB("KBN_NAME");
			USItemKubun kbnItem = list.FirstOrDefault(k => k.KBN_NAME == kbnName);
			if (kbnItem == null)
			{
				kbnItem = new USItemKubun();
				kbnItem.KBN_NAME = dr.GetStringByDB("KBN_NAME");
				kbnItem.KBN_SHOWORDER = dr.GetStringByDB("KBN_SHOWORDER");
				list.Add(kbnItem);
			}

			string gyoSyosai = dr.GetStringByDB("GYO_SYOSAI");
			const string GYO_SYOSAI_TOTAL_KENSA_BUI = "小計(区分)";
			if (GYO_SYOSAI_TOTAL_KENSA_BUI == gyoSyosai)
			{
				kbnItem.Total = dr.GetStringByDB("CNT");
				return;
			}

			#endregion
			kbnItem.AddDay(dr.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr.GetStringByDB("CNT"));
		}

		#endregion

		#endregion メソッド

	}
}
