using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using ARISSend.Data;
using RISBizLibrary.Data;
using RISBizLibrary.Utils;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.ReportDokuei;
using RISCommonLibrary.Lib.Msg.ReportDokuei;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace ARISSend.Model.MessageCreator
{
	internal class ReportDokueiMsgCreator : BaseMsgCreator
	{
		#region field

		#endregion

		#region property

		/// <summary>
		/// ログ出力用名前
		/// </summary>
		public override string NameForLog
		{
			get
			{
				return "既読";
			}
		}

		/// <summary>
		/// REQUESTTYPEごとの定義
		/// </summary>
		//private readonly RequestTypeData[] DefineByRequestType = 
		//{
		//	new RequestTypeData()
		//	{
		//		RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT,
		//		ProcessingType = MsgConst.PROCESSING_TYPE_NEW
		//	},
		//	new RequestTypeData()
		//	{
		//		RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT_CANCEL,
		//		ProcessingType = MsgConst.PROCESSING_TYPE_DELETE
		//	},
		//	new RequestTypeData()
		//	{
		//		RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_IMAGE,
		//		ProcessingType = MsgConst.PROCESSING_TYPE_IMAGE
		//	},
		//	new RequestTypeData()
		//	{
		//		RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_FIRST,
		//		ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_FIRST
		//	},
		//	new RequestTypeData()
		//	{
		//		RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_SECOND,
		//		ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_SECOND
		//	},
		//	new RequestTypeData()
		//	{
		//		RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_SECOND_CANCEL,
		//		ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_SECOND_CANCEL
		//	},
		//	new RequestTypeData()
		//	{
		//		RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION,
		//		ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_SECOND
		//	},
		//	new RequestTypeData()
		//	{
		//		RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION_CANCEL,
		//		ProcessingType = MsgConst.PROCESSING_TYPE_REMARK_SECOND_CANCEL
		//	}
		//};

		#region ISocketParameter メンバ

		public override string Host
		{
			get
			{
				return ConfigurationManager.AppSettings["ReportDokueiHost"].StringToString();
			}
		}

		public override int Port
		{
			get
			{
				return ConfigurationManager.AppSettings["ReportDokueiPort"].StringToInt32();
			}
		}

		public override int ConnectionTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["ReportDokueiConnectionTimeout"].StringToInt32();
			}
		}

		public override int SendTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["ReportDokueiSendTimeout"].StringToInt32();
			}
		}

		public override int ReceiveTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["ReportDokueiReceiveTimeout"].StringToInt32();
			}
		}

		#endregion

		#endregion

		#region constractor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReportDokueiMsgCreator() : base()
		{
		}
		#endregion

		#region method
		
		#region IMessageCreator メンバ

		//public override string[] GetRequestTypes()
		//{
		//	return new[]
		//	{
		//		RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT,
		//		RQRISDBConst.TOHISINFO_REQUESTTYPE_RECEIPT_CANCEL,
		//		RQRISDBConst.TOHISINFO_REQUESTTYPE_IMAGE,
		//		RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_FIRST,
		//		RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_SECOND,
		//		RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_SECOND_CANCEL,
		//		RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION,
		//		RQRISDBConst.TOHISINFO_REQUESTTYPE_REMARK_REQUEST_ADDITION_CANCEL
		//	};
		//}

		public override BaseMsg CreateMsg(ARISSend.Data.ReportDokueiInfo reportInfo, System.Data.IDbConnection cn)
		{
			_log.Info("既読情報を取得します");

			const string SQL_SELECT =

			"SELECT " +
				"EI.ODRID odrid, " +
				"EI.PATID patid, " +
				"TO_CHAR(RI.CHECKDATE, 'YYYYMMDD') checkdate_yyyymmdd, " +
				"TO_CHAR(RI.CHECKDATE, 'HH24MI') checkdate_hh24mi, " +
				"UM.USERID userid " +
			"FROM " +
				"EXAMINFO EI " +
				"JOIN " +
					"REPORTINFO RI " +
				"ON  EI.RPTID = RI.ID " +
				"JOIN " +
					"USERMANAGE UM " +
				"ON  RI.CHECKDOCTOR = UM.USERID " +
				"WHERE " +
					"EI.RPTID = :rptid ";
			const string FIELD_ODRID = "odrid";
			const string FIELD_PATID = "patid";
			const string FIELD_CHECKDATE_YYYYMMDD = "checkdate_yyyymmdd";
			const string FIELD_CHECKDATE_HH24MI = "checkdate_hh24mi";
			const string FIELD_PROCESSING_TYPE = MsgConst.PROCESSING_TYPE_REMARK_SECOND_READ_FIX;
			const string FIELD_USERID = "userid";

			const string PARAM_RPTID = "rptid";
			
			using (IDbCommand command = cn.CreateCommand())
			{
                command.InitCommandODP(ConfigurationManager.AppSettings["CommandTimeout"].StringToInt32());
				command.CommandText = SQL_SELECT;

				IDataParameter param = command.CreateParameter();
				param.SetInputString(PARAM_RPTID, reportInfo.Id);
				command.Parameters.Add(param);

				MiscUtils.WriteDbCommandLogForLog4net(command, _log);

				IDataReader reader = command.ExecuteReader();
				try
				{
					if (!reader.Read())
					{
						throw new DataNotFoundException(string.Format(
							"RIS_IDデータが見つかりませんでした。Id={0}", reportInfo.Id));
					}

					MiscUtils.WriteDataReaderLogForLog4net(reader, _log);

					ReportDokueiMsg msg = new ReportDokueiMsg();
					#region 通信制御部
					CommunicationControlAggregate cc = msg.MsgBody.CommunicationControl;
					cc.CreateCommunicationControlAgg();
					cc.TELEGRAPH_KIND.Data = MsgConst.TELEGRAPH_KIND_READ_INFO;
					cc.ERR_STATUS.Data = MsgConst.ERR_STATUS_NORMAL;
					if (reader.GetStringByDBString(FIELD_ODRID).Length > CommunicationControlNodeInfo.COMMUNICATION_CONTROL_ORDER_NO.FieldLength)
					{
						cc.ORDER_NO.Data = reader.GetStringByDBString(FIELD_ODRID).Substring(0, CommunicationControlNodeInfo.COMMUNICATION_CONTROL_ORDER_NO.FieldLength);
					}
					else
					{
						cc.ORDER_NO.Data = reader.GetStringByDBString(FIELD_ODRID);
					}
					#endregion

					#region 既読情報部
					ReportDokueiAggregate rcpt = msg.MsgBody.ReportDokuei;
					rcpt.PROCESSING_TYPE.Data = FIELD_PROCESSING_TYPE;
					
					if (reader.GetStringByDBString(FIELD_PATID).Length > ReportDokueiNodeInfo.REPORTDOKUEI_PATIENT_NO.FieldLength) {
						rcpt.PATIENT_NO.Data = reader.GetStringByDBString(FIELD_PATID).Substring(0, ReportDokueiNodeInfo.REPORTDOKUEI_PATIENT_NO.FieldLength);
					} else {
						rcpt.PATIENT_NO.Data = reader.GetStringByDBString(FIELD_PATID);
					}

					rcpt.REPORTDOKUEI_DATE.Data = reader.GetStringByDBString(FIELD_CHECKDATE_YYYYMMDD);
					rcpt.REPORTDOKUEI_TIME.Data = reader.GetStringByDBString(FIELD_CHECKDATE_HH24MI);

					if (reader.GetStringByDBString(FIELD_USERID).Length > ReportDokueiNodeInfo.REPORTDOKUEI_USERID.FieldLength)
					{
						rcpt.REPORTDOKUEI_CODE.Data = reader.GetStringByDBString(FIELD_USERID).Substring(0, ReportDokueiNodeInfo.REPORTDOKUEI_USERID.FieldLength);
					}
					else
					{
						rcpt.REPORTDOKUEI_CODE.Data = reader.GetStringByDBString(FIELD_USERID);
					}
					
					#endregion

					return msg;
				}
				finally
				{
					reader.Close();
				}
			}
		}

		public override BaseMsgData CreateMsgData()
		{

			return new ReportDokueiMsgData();
		}

		#endregion

		#endregion

	}
}
