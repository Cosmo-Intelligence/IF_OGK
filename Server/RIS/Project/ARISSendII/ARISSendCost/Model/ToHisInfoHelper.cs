using System;
using System.Configuration;
using System.Data;
using ARISSend.Data;
using RISBizLibrary.Data;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Response;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ARISSend.Model
{
	internal static class ToHisInfoHelper
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

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// TOHISINFO未送信1レコード取得
		/// </summary>
		/// <param name="cn"></param>
		/// <returns></returns>
		public static ToHisInfo GetDataFromDB(IDbConnection cn)
        {
			// 2018.05.14 Add S.Ichinose@COSMO Start HIS-RIS通信AP性能改善
			// 設定ファイルから対象処理種別を取得
			string inRequestType = ConfigurationManager.AppSettings["InRequestType"];
			// 2018.05.14 Add S.Ichinose@COSMO End   HIS-RIS通信AP性能改善

			string SQL_SELECT =
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
			// 2018.05.14 Mod S.Ichinose@COSMO Start HIS-RIS通信AP性能改善
				"SELECT /*+ INDEX(t IDX_TOHISINFO4) */ " +
			// 2018.05.14 Mod S.Ichinose@COSMO End   HIS-RIS通信AP性能改善
					"ROW_NUMBER() " +
						"OVER " +
						"( " +
							"ORDER BY " +
								"t.requestdate, " +
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
					"tohisinfo t " +
				"WHERE " +
					"t.transferstatus = '00' " +
					"AND " +
			// 2018.05.14 Mod S.Ichinose@COSMO Start HIS-RIS通信AP性能改善
			// 設定ファイルより取得した処理種別を条件にする
					"t.requesttype in (" + inRequestType + ") " +
			// 2018.05.14 Mod S.Ichinose@COSMO End   HIS-RIS通信AP性能改善
				") tf " +
			"WHERE " +
				"tf.rno = 1 ";

			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				#region コメントをループ時減らす
				//_log.Debug("TOHISINFOテーブル取得します");
				#endregion
				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						#region コメントをループ時減らす
						//_log.Debug("TOHISINFOに該当データは見つかりませんでした");
						#endregion
						return null;
					}

					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

					ToHisInfo target = new ToHisInfo();
					target.RequestID = reader.GetStringByDBInt32(FIELD_REQUESTID);
					target.RequestDate = reader.GetStringByDBDatetime(FIELD_REQUESTDATE);
					target.RIS_ID = reader.GetStringByDBString(FIELD_RIS_ID);
					target.RequestUser = reader.GetStringByDBString(FIELD_REQUESTUSER);
					target.RequestTerminalID = reader.GetStringByDBString(FIELD_REQUESTTERMINALID);
					target.RequestType = reader.GetStringByDBString(FIELD_REQUESTTYPE);
					target.MessageID1 = reader.GetStringByDBString(FIELD_MESSAGEID1);
					target.MessageID2 = reader.GetStringByDBString(FIELD_MESSAGEID1);
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

		public static void UpdateToDB(ToHisInfo target, IDbConnection cn, BaseSendMsgData msgData)
		{
			UpdateToDB(target, cn, null, msgData);
		}

		public static void UpdateToDB(ToHisInfo target, IDbConnection cn, IDbTransaction tr, BaseSendMsgData msgData)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
				command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				command.CommandText = 
				"UPDATE " +
					"tohisinfo " +
				"SET " +
					"transferstatus = :transferstatus, " +
					"transferdate = SYSDATE, " +
					"transferresult = :transferresult, " +
					"transfertext = :transfertext " +
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

				// 2020.04.22 Mod H.Taira@COSMO Start
				try
				{
					////4000バイト越えで全角を含むデータを更新しようとするとora-01461
					////NULLで更新する
					// バインドの場合、ora-01461は発生しない
					param = command.CreateParameter();
					if (msgData != null && msgData.Request != null)
					{
						param.SetInputString(PARAM_TRANSFERTEXT, msgData.Request.TextMessage);
					}
					else
					{
						param.SetInputString(PARAM_TRANSFERTEXT, "");
					}
					
				}
				catch (Exception e)
				{
					_log.ErrorFormat("エラーが発生しました。{0}", e.ToString());
					param.SetInputString(PARAM_TRANSFERTEXT, e.ToString());
				}
				// 2020.04.22 Mod H.Taira@COSMO End

				command.Parameters.Add(param);

				#endregion

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				_log.Debug("TOHISINFOテーブル更新します");

				command.ExecuteNonQuery();

				_log.Debug("TOHISINFOテーブル更新しました");
			}
		}

		/// <summary>
		/// レスポンスメッセージから設定する
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		public static void SetDataFromResponseMsg(ToHisInfo target, BaseMsgData msgData)
		{
			target.TransferStatus = RQRISDBConst.TOHISINFO_TRANSFERSTATUS_SENT;
			target.TransferResult = GetTransferResult(msgData);
			target.TransferText = msgData.Request.OriginalMessage; //未送信時は""
		}

		/// <summary>
		/// レスポンスメッセージから設定する
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msgData"></param>
		/// <remarks>例外時</remarks>
		public static void SetDataFromResponseMsgByException(ToHisInfo target, BaseMsgData msgData)
		{
			target.TransferStatus = RQRISDBConst.TOHISINFO_TRANSFERSTATUS_SENT;
			if ((msgData == null)||(msgData.Request == null))
			{ //送信メッセージ作成前
				target.TransferResult = RQRISDBConst.TOHISINFO_TRANSFERRESULT_ERROR;
				target.TransferText = "";
				return;
			}

			target.TransferText = msgData.Request.OriginalMessage;
			if (msgData.Response == null)
			{ //受信メッセージ解析前は解析失敗
				target.TransferResult = RQRISDBConst.TOHISINFO_TRANSFERRESULT_ERROR;
				return;
			}
			target.TransferResult = GetTransferResult(msgData);
		}

		private static string GetTransferResult(BaseMsgData msgData)
		{
			BaseResponseMsg rm = msgData.Response;
			if (rm == null) //未受信の場合
			{
				return RQRISDBConst.TOHISINFO_TRANSFERRESULT_ERROR;
			}

			CommunicationControlAggregate cc = rm.MsgBody.CommunicationControl;
			if (cc.TELEGRAPH_KIND.TrimData == MsgConst.TELEGRAPH_KIND_RESPONSE_OK)
			{
				return RQRISDBConst.TOHISINFO_TRANSFERRESULT_NORMAL;
			}
			return RQRISDBConst.TOHISINFO_TRANSFERRESULT_ERROR + cc.ERR_STATUS.TrimData;
		}

		#endregion

		#region transferstatusしか更新しない系

		public static void UpdateToDBRequestTypeNotDefine(ToHisInfo target, IDbConnection cn)
		{
			UpdateToDBRequestTypeNotDefine(target, cn, null);
		}

		/// <summary>
		/// 送信対象外の場合の更新
		/// </summary>
		/// <param name="target"></param>
		/// <param name="cn"></param>
		/// <param name="tr"></param>
		public static void UpdateToDBRequestTypeNotDefine(ToHisInfo target, IDbConnection cn, IDbTransaction tr)
		{
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				command.CommandText = 
				"UPDATE " +
					"tohisinfo " +
				"SET " +
					"transferstatus = :transferstatus " +
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
				#endregion

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				_log.Debug("TOHISINFOテーブル更新します");

				command.ExecuteNonQuery();

				_log.Debug("TOHISINFOテーブル更新しました");
			}
		}

		/// <summary>
		/// レスポンスメッセージから設定する
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		/// <remarks>TOHISINFO.RequestType未定義</remarks>
		public static void SetDataFromResponseMsgByRequestTypeNotDefine(ToHisInfo target)
		{
			target.TransferStatus = RQRISDBConst.TOHISINFO_TRANSFERSTATUS_SENT_EXEMPT;
			target.TransferResult = "";
			target.TransferText = "";
		}

		#endregion

		#endregion
	}
}
