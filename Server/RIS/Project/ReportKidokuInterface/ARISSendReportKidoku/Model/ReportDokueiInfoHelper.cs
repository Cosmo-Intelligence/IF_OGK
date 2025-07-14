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
	internal static class ReportDokueiInfoHelper
    {

		private const string FIELD_ID= "id";
		private const string FIELD_REVISION= "revision";
		private const string FIELD_SUBREVISION= "subrevision";
		private const string FIELD_DRAWDATE= "drawdate";
		private const string FIELD_DRAWDOCTOR= "drawdoctor";
		private const string FIELD_FIXDATE= "fixdate";
		private const string FIELD_FIXDOCTOR= "fixdoctor";
		private const string FIELD_OMITDATE= "omitdate";
		private const string FIELD_OMITDOCTOR= "omitdoctor";
		private const string FIELD_FINDINGS= "findings";
		private const string FIELD_IMPRESSION= "impression";
		private const string FIELD_REMARKS= "remarks";
		private const string FIELD_STATUS= "status";
		private const string FIELD_LASTSTATUS= "laststatus";
		private const string FIELD_TRANSCRIBEDATE= "transcribedate";
		private const string FIELD_TRANSCRIBER= "transcriber";
		private const string FIELD_TRANSLANGUAGE= "translanguage";
		private const string FIELD_TRANSCHECKFLAG= "transcheckflag";
		private const string FIELD_TRANSQAFLAG= "transqaflag";
		private const string FIELD_TRANSQACOMMENT= "transqacomment";
		private const string FIELD_FILEPATH= "filepath";
		private const string FIELD_FILENAME= "filename";
		private const string FIELD_PRINTFORM= "printform";
		private const string FIELD_RPCOMMENT= "rpcomment";
		private const string FIELD_PRINTED= "printed";
		private const string FIELD_FINDINGS_RICH= "findings_rich";
		private const string FIELD_FINDINGS_HTML= "findings_html";
		private const string FIELD_IMPRESSION_RICH= "impression_rich";
		private const string FIELD_IMPRESSION_HTML= "impression_html";
		private const string FIELD_DRAWDOCTORCODE= "drawdoctorcode";
		private const string FIELD_FIXDOCTORCODE= "fixdoctorcode";
		private const string FIELD_OMITDOCTORCODE= "omitdoctorcode";
		private const string FIELD_CHECKFLG= "checkflg";
		private const string FIELD_CHECKDATE= "checkdate";
		private const string FIELD_CHECKDOCTOR= "checkdoctor";
		private const string FIELD_FIRSTFIXDATE= "firstfixdate";
		private const string FIELD_FIXCONFIRMFLG= "fixconfirmflg";
		private const string FIELD_DOCUMENTHTML= "documenthtml";
		private const string FIELD_DOCUMENTXML= "documentxml";
		private const string FIELD_PRINTHTML= "printhtml";
		private const string FIELD_HTMLTEMPLATEID= "htmltemplateid";
		private const string FIELD_HTMLFORMID= "htmlformid";
		private const string FIELD_PREFIXDOCTOR= "prefixdoctor";
		private const string FIELD_PREFIXDOCTORCODE= "prefixdoctorcode";
		private const string FIELD_DOCUMENTHTMLCSS= "documenthtmlcss";
		private const string FIELD_FINDINGS_C  = "findings_c";

		//private const string FIELD_REQUESTID = "requestid";
		//private const string FIELD_REQUESTDATE = "requestdate";
		//private const string FIELD_RIS_ID = "ris_id";
		//private const string FIELD_REQUESTUSER = "requestuser";
		//private const string FIELD_REQUESTTERMINALID = "requestterminalid";
		//private const string FIELD_REQUESTTYPE = "requesttype";
		//private const string FIELD_MESSAGEID1 = "messageid1";
		//private const string FIELD_MESSAGEID2 = "messageid2";
		//private const string FIELD_TRANSFERSTATUS = "transferstatus";
		//private const string FIELD_TRANSFERDATE = "transferdate";
		//private const string FIELD_TRANSFERRESULT = "transferresult";
		private const string FIELD_TRANSFERTEXT = "transfertext";

		//private const string PARAM_REQUESTID = FIELD_REQUESTID;
		//private const string PARAM_TRANSFERSTATUS = FIELD_TRANSFERSTATUS;
		//private const string PARAM_TRANSFERRESULT = FIELD_TRANSFERRESULT;
		private const string PARAM_ID = FIELD_ID;
		private const string PARAM_REVISION = FIELD_REVISION;
		private const string PARAM_TRANSFERTEXT = FIELD_TRANSFERTEXT;

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// REPORTINFO既読レコード取得
		/// </summary>
		/// <param name="cn"></param>
		/// <returns></returns>
		public static ReportDokueiInfo GetDataFromDB(IDbConnection cn)
        {

			string SQL_SELECT =
			"SELECT " +
				"RI.ID, " +
				"RI.REVISION " +
			"FROM " +
				"REPORTINFO RI " +
				"LEFT JOIN " +
					"REPORTDOKUEIINFO RD " +
				"ON  RI.ID = RD.ID " +
			"WHERE " +
				"RI.CHECKFLG = '1' "+
			"AND " +
				"RD.ID IS NULL";

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

					ReportDokueiInfo target = new ReportDokueiInfo();
					target.Id = reader.GetStringByDBString(FIELD_ID);
					target.Revision = reader.GetStringByDBInt32(FIELD_REVISION);
					//target.Revision = reader.GetStringByDBString(FIELD_REVISION);
					//target.SubRevision = reader.GetStringByDBString(FIELD_SUBREVISION);
					//target.DrawDate = reader.GetStringByDBString(FIELD_DRAWDATE);
					//target.DrawDoctor = reader.GetStringByDBString(FIELD_DRAWDOCTOR);
					//target.FirstFixDate = reader.GetStringByDBString(FIELD_FIXDATE);
					//target.FixDoctor = reader.GetStringByDBString(FIELD_FIXDOCTOR);
					//target.OmitDate = reader.GetStringByDBString(FIELD_OMITDATE);
					//target.OmitDoctor = reader.GetStringByDBString(FIELD_OMITDOCTOR);
					//target.Findings = reader.GetStringByDBString(FIELD_FINDINGS);
					//target.Impression = reader.GetStringByDBString(FIELD_IMPRESSION);
					//target.Remarks = reader.GetStringByDBString(FIELD_REMARKS);
					//target.Status = reader.GetStringByDBString(FIELD_STATUS);
					//target.LastStatus = reader.GetStringByDBString(FIELD_LASTSTATUS);
					//target.TranscribeDate = reader.GetStringByDBString(FIELD_TRANSCRIBEDATE);
					//target.Transcriber = reader.GetStringByDBString(FIELD_TRANSCRIBER);
					//target.TransLanguage = reader.GetStringByDBString(FIELD_TRANSLANGUAGE);
					//target.TranscheckFlag = reader.GetStringByDBString(FIELD_TRANSCHECKFLAG);
					//target.TransqaFlag = reader.GetStringByDBString(FIELD_TRANSQAFLAG);
					//target.TransqaComment = reader.GetStringByDBString(FIELD_TRANSQACOMMENT);
					//target.Filepath = reader.GetStringByDBString(FIELD_FILEPATH);
					//target.Filename = reader.GetStringByDBString(FIELD_FILENAME);
					//target.Printform = reader.GetStringByDBString(FIELD_PRINTFORM);
					//target.Rpcomment = reader.GetStringByDBString(FIELD_RPCOMMENT);
					//target.Printed = reader.GetStringByDBString(FIELD_PRINTED);
					//target.FindingsRich = reader.GetStringByDBString(FIELD_FINDINGS_RICH);
					//target.FindingsHtml = reader.GetStringByDBString(FIELD_FINDINGS_HTML);
					//target.ImpressionRich = reader.GetStringByDBString(FIELD_IMPRESSION_RICH);
					//target.ImpressionHtml = reader.GetStringByDBString(FIELD_IMPRESSION_HTML);
					//target.DrawDoctorcode = reader.GetStringByDBString(FIELD_DRAWDOCTORCODE);
					//target.FixDoctorCode = reader.GetStringByDBString(FIELD_FIXDOCTORCODE);
					//target.OmitDoctorCode = reader.GetStringByDBString(FIELD_OMITDOCTORCODE);
					//target.CheckFlg = reader.GetStringByDBString(FIELD_CHECKFLG);
					//target.CheckDate = reader.GetStringByDBString(FIELD_CHECKDATE);
					//target.CheckDoctor = reader.GetStringByDBString(FIELD_CHECKDOCTOR);
					//target.FirstFixDate = reader.GetStringByDBString(FIELD_FIRSTFIXDATE);
					//target.FixConfirmFlg = reader.GetStringByDBString(FIELD_FIXCONFIRMFLG);
					//target.DocumentHtml = reader.GetStringByDBString(FIELD_DOCUMENTHTML);
					//target.DocumentXml = reader.GetStringByDBString(FIELD_DOCUMENTXML);
					//target.PrintHtml = reader.GetStringByDBString(FIELD_PRINTHTML);
					//target.HtmlTemplateId = reader.GetStringByDBString(FIELD_HTMLTEMPLATEID);
					//target.HtmlFormId = reader.GetStringByDBString(FIELD_HTMLFORMID);
					//target.PrefixDoctor = reader.GetStringByDBString(FIELD_PREFIXDOCTOR);
					//target.PrefixDoctorCode = reader.GetStringByDBString(FIELD_PREFIXDOCTORCODE);
					//target.DocumentHtmlCss = reader.GetStringByDBString(FIELD_DOCUMENTHTMLCSS);
					//target.FindingsC   = reader.GetStringByDBString(FIELD_FINDINGS_C);

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

		public static void InsertToDB(ReportDokueiInfo target, IDbConnection cn, BaseSendMsgData msgData)
		{
			InsertToDB(target, cn, null, msgData);
		}

		public static void InsertToDB(ReportDokueiInfo target, IDbConnection cn, IDbTransaction tr, BaseSendMsgData msgData)
		{
			using (IDbCommand command = cn.CreateCommand())
			{

				command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.Transaction = tr;
				command.CommandText =
				"insert into reportdokueiinfo( " +
					"ID, " +
					"REVISION, " +
					"TRANSFERDATE, " +
					"TRANSFERTEXT " +
				") " +
				"values(" +
					":id, " +
					":revision, " +
					"SYSDATE, " +
					":transfertext " +
				") ";

				#region パラメータ設定
				command.Parameters.Clear();
				IDataParameter param = command.CreateParameter();
				param.SetInputString(FIELD_ID, target.Id);
				command.Parameters.Add(param);

				param = command.CreateParameter();
				param.SetInputString(PARAM_REVISION, target.Revision);
				command.Parameters.Add(param);

				param = command.CreateParameter();
				param.SetInputString(PARAM_TRANSFERTEXT, target.Transfertext);
				command.Parameters.Add(param);

				#endregion

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);
				_log.Debug("REPORTDOKUEIINFOテーブルを更新します");

				command.ExecuteNonQuery();

				_log.Debug("REPORTDOKUEIINFOテーブルを更新しました");
			}
		}

		/// <summary>
		/// レスポンスメッセージから設定する
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		public static void SetDataFromResponseMsg(ReportDokueiInfo target, BaseMsgData msgData)
		{
			target.Transfertext = GetTransferResult(msgData);
		}

		/// <summary>
		/// レスポンスメッセージから設定する
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msgData"></param>
		/// <remarks>例外時</remarks>
		//public static void SetDataFromResponseMsgByException(ToHisInfo target, BaseMsgData msgData)
		//{
		//	target.TransferStatus = RQRISDBConst.TOHISINFO_TRANSFERSTATUS_SENT;
		//	if ((msgData == null)||(msgData.Request == null))
		//	{ //送信メッセージ作成前
		//		target.TransferResult = RQRISDBConst.TOHISINFO_TRANSFERRESULT_ERROR;
		//		target.TransferText = "";
		//		return;
		//	}

		//	target.TransferText = msgData.Request.OriginalMessage;
		//	if (msgData.Response == null)
		//	{ //受信メッセージ解析前は解析失敗
		//		target.TransferResult = RQRISDBConst.TOHISINFO_TRANSFERRESULT_ERROR;
		//		return;
		//	}
		//	target.TransferResult = GetTransferResult(msgData);
		//}

		private static string GetTransferResult(BaseMsgData msgData)
		{
			BaseResponseMsg rm = msgData.Response;
			if (rm == null) //未受信の場合
			{
				return RQRISDBConst.TOHISINFO_TRANSFERRESULT_ERROR;
			}

			CommunicationControlAggregate cc = rm.MsgBody.CommunicationControl;
			//if (cc.TELEGRAPH_KIND.TrimData == MsgConst.TELEGRAPH_KIND_RESPONSE_OK)
			//{
			//	return RQRISDBConst.TOHISINFO_TRANSFERRESULT_NORMAL;
			//}
			//return RQRISDBConst.TOHISINFO_TRANSFERRESULT_ERROR + cc.ERR_STATUS.TrimData;
			return cc.ERR_STATUS.TrimData;
		}

		#endregion

		#region transferstatusしか更新しない系

		//public static void UpdateToDBRequestTypeNotDefine(ToHisInfo target, IDbConnection cn)
		//{
		//	UpdateToDBRequestTypeNotDefine(target, cn, null);
		//}

		/// <summary>
		/// 送信対象外の場合の更新
		/// </summary>
		/// <param name="target"></param>
		/// <param name="cn"></param>
		/// <param name="tr"></param>
		//public static void UpdateToDBRequestTypeNotDefine(ToHisInfo target, IDbConnection cn, IDbTransaction tr)
		//{
			//using (IDbCommand command = cn.CreateCommand())
			//{
   //             command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
			//	command.Transaction = tr;
			//	command.CommandText = 
			//	"UPDATE " +
			//		"tohisinfo " +
			//	"SET " +
			//		"transferstatus = :transferstatus " +
			//	"WHERE " +
			//		"requestid = :requestid ";

			//	#region パラメータ設定

			//	command.Parameters.Clear();

			//	IDataParameter param = command.CreateParameter();
			//	//param.SetInputInt32(FIELD_REQUESTID, Convert.ToInt32(target.RequestID));
			//	command.Parameters.Add(param);

			//	param = command.CreateParameter();
			//	//param.SetInputString(PARAM_TRANSFERSTATUS, target.TransferStatus);
			//	command.Parameters.Add(param);
			//	#endregion

			//	MiscUtils.WriteDbCommandLogForLog4net(command, _log);
			//	_log.Debug("TOHISINFOテーブル更新します");

			//	command.ExecuteNonQuery();

			//	_log.Debug("TOHISINFOテーブル更新しました");
			//}
		//}

		/// <summary>
		/// レスポンスメッセージから設定する
		/// </summary>
		/// <param name="target"></param>
		/// <param name="msg"></param>
		/// <remarks>TOHISINFO.RequestType未定義</remarks>
		//public static void SetDataFromResponseMsgByRequestTypeNotDefine(ToHisInfo target)
		//{
		//	target.TransferStatus = RQRISDBConst.TOHISINFO_TRANSFERSTATUS_SENT_EXEMPT;
		//	target.TransferResult = "";
		//	target.TransferText = "";
		//}

		#endregion

		#endregion
	}
}
