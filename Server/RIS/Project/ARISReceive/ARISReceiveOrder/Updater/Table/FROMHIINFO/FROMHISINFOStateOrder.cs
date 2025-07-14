using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARISReceive.Data;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg.Common.CommunicationControl;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISBizLibrary.Updater.Table.FROMHIINFO;
using RISBizLibrary.Data;

namespace ARISReceive.Updater.Table.FROMHIINFO
{
	public class FROMHISINFOStateOrder : FROMHISINFOState
	{

		public new OrderMsgData Data
		{
			get
			{
				return base.Data as OrderMsgData;
			}
			set
			{
				base.Data = value;
			}
		}

		public FROMHISINFOStateOrder(ReceiveMsgData data)
			: base(data)
		{
		}

		public override string GetMESSAGETYPE()
		{
			CommunicationControlAggregate control = Data.Request.MsgBody.CommunicationControl;
			PatientAttributeAggregate patient = Data.Request.MsgBody.PatientAttribute;
			if (patient.PROCESSING_TYPE.TrimData == MsgConst.PROCESSING_TYPE_NEW)
			{
				return RQRISDBConst.FROMHISINFO_MESSAGETYPE_ORDER;
			}
			if (patient.PROCESSING_TYPE.TrimData == MsgConst.PROCESSING_TYPE_DELETE)
			{
				return RQRISDBConst.FROMHISINFO_MESSAGETYPE_ORDER_CANCEL;
			}
			throw new MsgAnomalyException(string.Format(
				"想定していない電文種別と処理タイプの組み合わせを受信しました。電文種別={0},処理タイプ={1}",
					control.TELEGRAPH_KIND.TrimData, patient.PROCESSING_TYPE.TrimData));
		}

		public override string GetMESSAGEID1()
		{
			return Data.OrderNoForRQRIS;
		}

		public override string GetMESSAGEID2()
		{
			return Data.KanjaID;
		}

		public override string GetRIS_ID()
		{
			return Data.RIS_ID;
		}
	}
}
