using System.Configuration;
using ARISSend.Data;
using RISBizLibrary.Data;
using RISBizLibrary.Utils;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Msg.RequestPatient;
using RISCommonLibrary.Lib.Utils;

namespace ARISSend.Model.MessageCreator
{
	internal class PatientMsgCreator : BaseMsgCreator
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
				return "患者要求";
			}
		}

		/// <summary>
		/// REQUESTTYPEごとの定義
		/// </summary>
		private readonly RequestTypeData[] DefineByRequestType = 
		{
			new RequestTypeData()
			{
				RequestType = RQRISDBConst.TOHISINFO_REQUESTTYPE_PATIENT,
				ProcessingType = MsgConst.PROCESSING_TYPE_PATIENT
			}
		};

		#region ISocketParameter メンバ

		public override string Host
		{
			get
			{
				return ConfigurationManager.AppSettings["PatientHost"].StringToString();
			}
		}

		public override int Port
		{
			get
			{
				return ConfigurationManager.AppSettings["PatientPort"].StringToInt32();
			}
		}

		public override int ConnectionTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["PatientConnectionTimeout"].StringToInt32();
			}
		}

		public override int SendTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["PatientSendTimeout"].StringToInt32();
			}
		}

		public override int ReceiveTimeout
		{
			get
			{
				return ConfigurationManager.AppSettings["PatientReceiveTimeout"].StringToInt32();
			}
		}

		#endregion

		#endregion

		#region constractor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PatientMsgCreator()
			: base()
		{
		}
		#endregion

		#region method
		
		#region IMessageCreator メンバ

		public override string[] GetRequestTypes()
		{
			return new[]
			{
				RQRISDBConst.TOHISINFO_REQUESTTYPE_PATIENT
			};
		}

		public override BaseMsg CreateMsg(ARISSend.Data.ToHisInfo toHisInfo, System.Data.IDbConnection cn)
		{
			_log.Info("患者情報を取得します");

			RequestPatientMsg msg = new RequestPatientMsg();
			#region 通信制御部
			CommunicationControlAggregate cc = msg.MsgBody.CommunicationControl;
			cc.CreateCommunicationControlAgg();
			cc.TELEGRAPH_KIND.Data = MsgConst.TELEGRAPH_KIND_PATIENT_INFO;
			cc.ERR_STATUS.Data = MsgConst.ERR_STATUS_NORMAL;
			#endregion

			#region 受付(進捗)情報部
			PatientAttributeAggregate p = msg.MsgBody.PatientAttribute;
			p.PROCESSING_TYPE.Data = MsgConst.PROCESSING_TYPE_PATIENT;
			p.PATIENT_NO.Data = toHisInfo.MessageID1;
			#endregion

			return msg;
		}

		public override BaseMsgData CreateMsgData()
		{
			return new PatientMsgData();
		}

		#endregion

		#endregion
    }
}
