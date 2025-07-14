using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;
using StatisticsPrint.Model.Print.Geppo.Data;
using System.Collections;

namespace StatisticsPrint.Model.Print.Geppo
{
	public static class GeppoSTATISTICS_MONTHLY_DATAHelper
	{

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		#region 更新系

		public static void DeleteToDB(DateTime nenGetsu, GeppoItem data, IDbConnection cn, IDbTransaction tr)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				command.CommandText = 
					"DELETE FROM statistics_monthly_data s " +
					"WHERE " +
					"s.target_month = :target_month ";

				#region パラメータ設定
				IDataParameter param = command.CreateParameter();
				param.SetInputString("target_month", nenGetsu.ToString("yyyyMM"));
				command.Parameters.Add(param);
				#endregion

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				_log.Debug("STATISTICS_MONTHLY_DATAテーブル削除します");
				int cnt = command.ExecuteNonQuery();
				_log.DebugFormat("STATISTICS_MONTHLY_DATAテーブル削除しました={0}件", cnt);
			}
		}

		public static void InsertToDB(DateTime nenGetsu, GeppoItem data, IDbConnection cn, IDbTransaction tr)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				command.CommandText = 
				"INSERT INTO statistics_monthly_data  " +
				"( " +
					"target_month, " +
					"class_key, " +
					"exam_count " +
				") " +
				"VALUES  " +
				"( " +
					":target_month, " +
					":class_key, " +
					":exam_count " +
				") ";

				IEnumerable<GeppoItemBunrui> konnendoList = from b in data.GeppoItemBunruiList
															where !string.IsNullOrEmpty(b.Total)
															select b; //今年度のデータ

				foreach (var item in konnendoList)
				{
					InsertToDB(nenGetsu, item, command);
				}
			}
		}

		private static void InsertToDB(DateTime nenGetsu, GeppoItemBunrui item, IDbCommand command)
		{
			#region パラメータ設定
			command.Parameters.Clear();
			IDataParameter param = command.CreateParameter();
			param.SetInputString("target_month", nenGetsu.ToString("yyyyMM"));
			command.Parameters.Add(param);

			param = command.CreateParameter();
			param.SetInputString("class_key", item.BUNRUI_ID);
			command.Parameters.Add(param);

			param = command.CreateParameter();
			param.SetInputInt32FromString("exam_count", item.Total);
			command.Parameters.Add(param);

			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.Debug("STATISTICS_MONTHLY_DATA新規作成します");

			int cnt = command.ExecuteNonQuery();

			_log.DebugFormat("STATISTICS_MONTHLY_DATA新規作成しました={0}件", cnt);
		}

		#endregion

	}
}
