using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using AddToHisInfoFromSQDB.Data;
using AddToHisInfoFromSQDB.Exceptions;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace AddToHisInfoFromSQDB.Model
{
	internal static class MasterStudyHelper
	{
		private const string FIELD_STUDYINSTANCEUID = "studyinstanceuid";
		private const string FIELD_ACCESSIONNUMBER = "accessionnumber";
		private const string FIELD_INSERTDATETIME = "insertdatetime";

		private const string PARAM_INSERTDATETIME = FIELD_INSERTDATETIME;

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// MasterStudy未送信レコード取得
		/// </summary>
		/// <param name="sentList">送信済みリスト</param>
		/// <param name="cn"></param>
		/// <returns></returns>
		public static List<MasterStudy> GetDataFromDB(List<MasterStudy> sentList, IDbConnection cn)
		{
			const string SQL_SELECT =
			"SELECT " +
				"m.studyinstanceuid, " +
				"m.accessionnumber, " +
				"m.insertdatetime " +
			"FROM " +
				"masterstudy m " +
			"WHERE " +
				"m.modality IN ({0}) AND " +
				"m.insertdatetime >= :insertdatetime AND " +
				"m.accessionnumber IS NOT NULL " +
			"ORDER BY " +
				"m.insertdatetime, " +
				"m.studyinstanceuid ";

			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				string modalities = GetModalityStringForSQL(ConfigurationManager.AppSettings["ModalityList"].StringToString());
				if (string.IsNullOrEmpty(modalities))
				{
					throw new ConfigException("対象モダリティ設定が未設定です");
				}
				command.CommandText = string.Format(SQL_SELECT, modalities);

				#region パラメータ設定
				command.Parameters.Clear();
				IDataParameter param = command.CreateParameter();
				param.SetInputDateTimeFromString(FIELD_INSERTDATETIME,
					GetLastInsertDateTime(sentList));
				command.Parameters.Add(param);
				#endregion

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				List<MasterStudy> list = new List<MasterStudy>();
				IDataReader reader = command.ExecuteReader();
				try
				{
					while (reader.Read())
					{
						#region ログ出力削減
						//MiscUtils.WriteDataReaderLogForLog4net(reader, _log);
						#endregion

						MasterStudy target = new MasterStudy();
						target.StudyInstanceUID = reader.GetStringByDB(FIELD_STUDYINSTANCEUID);
						target.AccessionNumber = reader.GetStringByDB(FIELD_ACCESSIONNUMBER);
						target.InsertDatetime = reader.GetStringByDB(FIELD_INSERTDATETIME);
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

		/// <summary>
		/// SQL用カンマ区切りモダリティ文字列取得
		/// </summary>
		/// <param name="modalities">カンマ区切り文字列</param>
		/// <returns></returns>
		private static string GetModalityStringForSQL(string modalities)
		{
			if (string.IsNullOrEmpty(modalities))
			{
				return "";
			}
			string[] modalityAry = modalities.Split(',');

			StringBuilder sb = new StringBuilder();
			Array.ForEach<string>(modalityAry, 
				delegate(string item)
				{
					sb.AddWithSeparator(string.Format("'{0}'", item), ",");
				}
			);
			return sb.ToString();
		}

		/// <summary>
		/// 送信済みレコードの最終InsertDateTime文字列を取得する
		/// </summary>
		/// <param name="sendList"></param>
		/// <returns></returns>
		/// <remarks>
		/// 未設定の場合はDateTime.MinValue(0001 年 1 月 1 日の 00:00:00.0000000)
		/// オラクルは紀元前4712年1月1日まで表現できるから問題ない
		/// </remarks>
		private static string GetLastInsertDateTime(List<MasterStudy> sendList)
		{
			const string FORMAT_DATE_TIME = "yyyy/MM/dd HH:mm:ss";
			if (sendList == null)
			{
				return DateTime.MinValue.ToString(FORMAT_DATE_TIME);
			}
			if (sendList.Count < 1)
			{
				return DateTime.MinValue.ToString(FORMAT_DATE_TIME);
			}
			MasterStudy last = sendList.LastOrDefault<MasterStudy>();
			if (last == null)
			{
				return DateTime.MinValue.ToString(FORMAT_DATE_TIME);
			}
			return last.InsertDatetime;
		}
	}
}
