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
using StatisticsPrint.Model.Print.Ippan.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Ippan
{
	/// <summary>
	/// 一般撮影　分類別集計表印刷
	/// </summary>
	internal class IppanPrint : PrintBase
	{
		#region プロパティ
		/// <summary>
		/// ドキュメント名
		/// </summary>
		public override string DocumentName
		{
			get
			{
				return "一般撮影　分類別集計表";
			}
		}

		/// <summary>
		/// フォームファイルパス
		/// </summary>
		public override string FormFilePath
		{
			get
			{
				return ConfigurationManager.AppSettings["FormFileIppan"].StringToString();
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

			IppanItem data = GetResultData(condition);
			List<IppanItemIppan> list = data.IppanItemList;
			if (list.Count < 1)
			{
				_log.Debug("出力データがありません");
				MessageBox.Show("出力データがありません。", "情報", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				return;
			}

			using (COReportsHelper helper = new COReportsHelper())
			{
				IPrintWriter writer = new IppanWriter()
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
		private IppanItem GetResultData(ConditionPrint condition)
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
		private IppanItem GetResultDataByCommand(ConditionPrint condition, IDbCommand command)
		{
			command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
			command.CommandText = "STATISTICS_PRINT_PACKAGE.GetIppanData";
			command.CommandType = CommandType.StoredProcedure;

			#region パラメータ設定
			IDataParameter p = command.CreateParameter();
			p.SetInputDateTime("P_EXAMENDDATE", condition.Nengetsu);
			command.Parameters.Add(p);

			p = command.CreateParameter();
			((OracleParameter)p).SetOututRefCursor("P_IPPAN_CURSOR");
			command.Parameters.Add(p);
			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);

			_log.Debug("プロシージャ実行します");
			command.ExecuteNonQuery();
			_log.Debug("プロシージャ実行しました");
			OracleCommand cmd = command as OracleCommand;
			using (OracleRefCursor cur1 = (OracleRefCursor)cmd.Parameters["P_IPPAN_CURSOR"].Value)
			{
				using (OracleDataReader dr1 = cur1.GetDataReader())
				{
					IppanItem data = new IppanItem();

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

		private void AddResultData(OracleDataReader dr1, IppanItem data)
		{
			//MiscUtils.WriteDataReaderLogForLog4net(dr1, _log);

			#region 帳票単位

			#endregion

			AddIppanList(dr1, data.IppanItemList);
		}

		private void AddIppanList(OracleDataReader dr1, List<IppanItemIppan> kbnList)
		{
			string kbnName = dr1.GetStringByDB("KBN_NAME");
			IppanItemIppan kbnItem = kbnList.FirstOrDefault(b => b.KBN_NAME == kbnName);
			if (kbnItem == null)
			{
				kbnItem = new IppanItemIppan();
				kbnItem.KBN_NAME = dr1.GetStringByDB("KBN_NAME");
				kbnItem.KBN_SHOWORDER = dr1.GetStringByDB("KBN_SHOWORDER");
				kbnList.Add(kbnItem);
			}

			string gyoSyosai = dr1.GetStringByDB("GYO_SYOSAI");
			const string GYO_SYOSAI_TOTAL_KUBUN = "小計(区分)";
			if (GYO_SYOSAI_TOTAL_KUBUN == gyoSyosai)
			{
				kbnItem.Total = dr1.GetStringByDB("CNT");
				return;
			}

			kbnItem.AddDay(dr1.GetStringByDB("EXAMENDDATE_YYYYMMDD10"), dr1.GetStringByDB("CNT"));
		}

		#endregion

		#endregion メソッド

	}
}
