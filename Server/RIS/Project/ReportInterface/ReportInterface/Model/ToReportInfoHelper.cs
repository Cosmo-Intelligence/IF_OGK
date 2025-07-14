using System;
using System.Configuration;
using System.Data;
using ReportInterface.Data;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ReportInterface.Model
{
	internal static class ToReportInfoHelper
	{
		private const string FIELD_REQUESTID = "requestid";
		private const string FIELD_REQUESTDATE = "requestdate";
		private const string FIELD_RIS_ID = "ris_id";
		private const string FIELD_REQUESTUSER = "requestuser";
		private const string FIELD_REQUESTTERMINALID = "requestterminalid";
		private const string FIELD_REQUESTTYPE = "requesttype";
		private const string FIELD_MESSAGEID1 = "messageid1";
		private const string FIELD_MESSAGEID2 = "messageid2";
		private const string FIELD_TRANSFERSTATUS = "transferstatus";
		private const string FIELD_TRANSFERDATE = "transferdate";
		private const string FIELD_TRANSFERRESULT = "transferresult";
		private const string FIELD_TRANSFERTEXT = "transfertext";

		private const string PARAM_REQUESTID = FIELD_REQUESTID;
		private const string PARAM_TRANSFERSTATUS = FIELD_TRANSFERSTATUS;
		private const string PARAM_TRANSFERRESULT = FIELD_TRANSFERRESULT;
		private const string PARAM_TRANSFERTEXT = FIELD_TRANSFERTEXT;

		private const string TOREPORTINFO_TRANSFERSTATUS_NORMAL = "01";
		private const string TOREPORTINFO_TRANSFERSTATUS_ERR = "02";
		private const string TOREPORTINFO_TRANSFERRESULT_NORMAL = "OK";
		private const string TOREPORTINFO_TRANSFERRESULT_ERR = "NG";

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// ToReportInfo未送信レコード取得
		/// </summary>
		/// <param name="sentList">送信済みリスト</param>
		/// <param name="cn"></param>
		/// <returns></returns>
		public static ToReportInfo GetDataFromDB(IDbConnection cn)
		{
			const string SQL_SELECT =
			"SELECT " +
				"tf.requestid, " +
				"tf.requestdate, " +
				"tf.ris_id, " +
				"tf.requestuser, " +
				"tf.requestterminalid, " +
				"tf.requesttype, " +
				"tf.messageid1, " +
				"tf.messageid2, " +
				"tf.transferstatus, " +
				"tf.transferdate, " +
				"tf.transferresult, " +
				"tf.transfertext " +
			"FROM " +
				"( " +
					"SELECT " +
						"ROW_NUMBER() " +
						"OVER " +
						"( " +
							"ORDER BY " +
								"t.requestid " +
						") rno, " +
						"t.requestid, " +
						"t.requestdate, " +
						"t.ris_id, " +
						"t.requestuser, " +
						"t.requestterminalid, " +
						"t.requesttype, " +
						"t.messageid1, " +
						"t.messageid2, " +
						"t.transferstatus, " +
						"t.transferdate, " +
						"t.transferresult, " +
						"t.transfertext " +
					"FROM " +
						"toreportinfo t " +
					"WHERE " +
						"t.transferstatus = '00' " +
				") tf " +
			"WHERE " +
				"tf.rno = 1 ";

			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						return null;
					}

					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

					ToReportInfo target = new ToReportInfo();
					target.RequestID = reader.GetStringByDBInt32(FIELD_REQUESTID);
					target.RequestDate = reader.GetStringByDBDatetime(FIELD_REQUESTDATE);
					target.RIS_ID = reader.GetStringByDBString(FIELD_RIS_ID);
					target.RequestUser = reader.GetStringByDBString(FIELD_REQUESTUSER);
					target.RequestTerminalID = reader.GetStringByDBString(FIELD_REQUESTTERMINALID);
					target.RequestType = reader.GetStringByDBString(FIELD_REQUESTTYPE);

                    // ■y_higuchi 2019.03 - 仕様変更 - ACNOへODRIDと同じ値を入れる - del -->> ここから
                    // target.MessageID1 = "00" + reader.GetStringByDBString(FIELD_MESSAGEID1);
                    // ■y_higuchi 2019.03 - 仕様変更 - ACNOへODRIDと同じ値を入れる <<-- del - add -->> 
                    target.MessageID1 = reader.GetStringByDBString(FIELD_MESSAGEID1);
                    // ■y_higuchi 2019.03 - 仕様変更 - ACNOへODRIDと同じ値を入れる - add - ここまで

                    target.MessageID2 = reader.GetStringByDBString(FIELD_MESSAGEID2);
					target.TransferStatus = reader.GetStringByDBString(FIELD_TRANSFERSTATUS);
					target.TransferDate = reader.GetStringByDBDatetime(FIELD_TRANSFERDATE);
					target.TransferResult = reader.GetStringByDBString(FIELD_TRANSFERRESULT);
					target.TransferText = reader.GetStringByDBString(FIELD_TRANSFERTEXT);

					return target;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		#region 更新系

		#region transferstatus、transferdate、transferresult、transfertext更新系

		public static void UpdateToDB(ToReportInfo target, IDbConnection cn)
		{
			UpdateToDB(target, cn, null);
		}

		public static void UpdateToDB(ToReportInfo target, IDbConnection cn, IDbTransaction tr)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				command.CommandText = 
				"UPDATE " +
					"toreportinfo " +
				"SET " +
					"transferstatus = :transferstatus, " +
					"transferdate = SYSDATE, " +
					"transferresult = :transferresult " +
				"WHERE " +
					"requestid = :requestid ";

				#region パラメータ設定
				command.Parameters.Clear();
				IDataParameter param = command.CreateParameter();
				param.SetInputInt32(FIELD_REQUESTID, Convert.ToInt32(target.RequestID));
				command.Parameters.Add(param);

				param = command.CreateParameter();
				param.SetInputString(PARAM_TRANSFERSTATUS, target.TransferStatus);
				command.Parameters.Add(param);

				param = command.CreateParameter();
				param.SetInputString(PARAM_TRANSFERRESULT, target.TransferResult);
				command.Parameters.Add(param);

				#endregion

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				_log.Debug("TOREPORTINFOテーブル更新します");

				int cnt = command.ExecuteNonQuery();

				_log.DebugFormat("TOREPORTINFOテーブル更新しました={0}件", cnt);
			}
		}


		/// <summary>
		/// レスポンスメッセージから設定する
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		public static void SetDataFromResponseMsg(ToReportInfo target)
		{
			target.TransferStatus = TOREPORTINFO_TRANSFERSTATUS_NORMAL;
			target.TransferResult = TOREPORTINFO_TRANSFERRESULT_NORMAL;
		}

		/// <summary>
		/// レスポンスメッセージから設定する
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		public static void SetDataFromResponseMsgByException(ToReportInfo target)
		{
			target.TransferStatus = TOREPORTINFO_TRANSFERSTATUS_ERR;
			target.TransferResult = TOREPORTINFO_TRANSFERRESULT_ERR;
		}

		#endregion

		#endregion
	}
}
