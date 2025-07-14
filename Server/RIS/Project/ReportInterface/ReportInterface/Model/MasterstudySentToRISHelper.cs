using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportInterface.Data;
using System.Data;
using RISCommonLibrary.Lib.Utils;
using System.Configuration;

namespace ReportInterface.Model
{
	internal static class MasterstudySentToRISHelper
	{
		private const string FIELD_STUDYINSTANCEUID = "studyinstanceuid";
		private const string FIELD_ACCESSIONNUMBER = "accessionnumber";
		private const string FIELD_INSERTDATETIME = "insertdatetime";

		private const string PARAM_STUDYINSTANCEUID = FIELD_STUDYINSTANCEUID;
		private const string PARAM_ACCESSIONNUMBER = FIELD_ACCESSIONNUMBER;
		private const string PARAM_INSERTDATETIME = FIELD_INSERTDATETIME;

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#region Select系
		
		/// <summary>
		/// MasterStudy送信ずみレコード取得
		/// </summary>
		/// <param name="cn"></param>
		/// <returns></returns>
		public static List<ToReportInfo> GetDataFromDB(IDbConnection cn)
		{
			const string SQL_SELECT =
			"SELECT " +
				"t.studyinstanceuid, " +
				"t.accessionnumber, " +
				"t.insertdatetime " +
			"FROM " +
				"masterstudy_sent_to_ris t " +
			"ORDER BY " +
				"t.insertdatetime, " +
				"t.studyinstanceuid ";

			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommand(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				List<ToReportInfo> list = new List<ToReportInfo>();
				IDataReader reader = command.ExecuteReader();
				try
				{
					while (reader.Read())
					{
						#region ログ出力削減
						//MiscUtils.WriteDataReaderLogForLog4net(reader, _log);
						#endregion

						ToReportInfo target = new ToReportInfo();
						
						//TODO: なおす
						//target.StudyInstanceUID = reader.GetStringByDB(FIELD_STUDYINSTANCEUID);
						//target.AccessionNumber = reader.GetStringByDB(FIELD_ACCESSIONNUMBER);
						//target.InsertDatetime = reader.GetStringByDB(FIELD_INSERTDATETIME);
						list.Add(target);
					}
					return list;
				}
				finally
				{
					reader.Close();
				}
			}
		}
		#endregion

		#region 更新系

		public static void DeleteToDB(ToReportInfo lastMasterStudy, IDbConnection cn, IDbTransaction tr)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommand(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				command.CommandText = 
					"DELETE masterstudy_sent_to_ris t " +
					"WHERE " +
					"t.insertdatetime < :insertdatetime ";

				#region パラメータ設定
				IDataParameter param = command.CreateParameter();

				const string FORMAT_DATE_TIME = "yyyy/MM/dd HH:mm:ss";

				string dateTimeString = DateTime.MinValue.ToString(FORMAT_DATE_TIME);
				if (lastMasterStudy != null)
				{
					dateTimeString = lastMasterStudy.InsertDatetime;
				}
				param.SetInputDateTimeFromString(PARAM_INSERTDATETIME, dateTimeString);
				command.Parameters.Add(param);
				#endregion


				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				_log.Debug("MASTERSTUDY_SENT_TO_RISテーブル削除します");
				int cnt = command.ExecuteNonQuery();
				_log.DebugFormat("MASTERSTUDY_SENT_TO_RISテーブル削除しました={0}件", cnt);
			}
		}

		public static void InsertToDB(List<ToReportInfo> list, IDbConnection cn, IDbTransaction tr)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommand(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				command.CommandText = 
				"INSERT INTO masterstudy_sent_to_ris " +
				"( " +
					"studyinstanceuid,  " +
					"accessionnumber,  " +
					"insertdatetime " +
				") " +
				"VALUES " +
				"( " +
					":studyinstanceuid,  " +
					":accessionnumber,  " +
					":insertdatetime " +
				") ";

				foreach (var item in list)
				{
					InsertToDB(item, command);
				}
			}
		}

		private static void InsertToDB(ToReportInfo masterStudy, IDbCommand command)
		{

			#region パラメータ設定
			command.Parameters.Clear();
			IDataParameter param = command.CreateParameter();
			param.SetInputString(PARAM_STUDYINSTANCEUID, masterStudy.StudyInstanceUID);
			command.Parameters.Add(param);

			param = command.CreateParameter();
			param.SetInputString(PARAM_ACCESSIONNUMBER, masterStudy.AccessionNumber);
			command.Parameters.Add(param);

			param = command.CreateParameter();
			param.SetInputDateTimeFromString(PARAM_INSERTDATETIME, masterStudy.InsertDatetime);
			command.Parameters.Add(param);

			#endregion

			MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			_log.Debug("MASTERSTUDY_SENT_TO_RIS新規作成します");

			int cnt = command.ExecuteNonQuery();

			_log.DebugFormat("MASTERSTUDY_SENT_TO_RIS新規作成しました={0}件", cnt);
		}

		#endregion

	}
}
