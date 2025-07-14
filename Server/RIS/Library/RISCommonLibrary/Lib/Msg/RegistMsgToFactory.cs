using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.ConnectionOpen;
using RISCommonLibrary.Lib.Msg.ConnectionClose;
using RISCommonLibrary.Lib.Msg.Order;
using RISCommonLibrary.Lib.Msg.Receipt;
using RISCommonLibrary.Lib.Msg.Exam;
using RISCommonLibrary.Lib.Msg.RequestPatient;
using RISCommonLibrary.Lib.Msg.Hospitalize;
using RISCommonLibrary.Lib.Msg.Response;
using RISCommonLibrary.Lib.Msg.ResponseWithPatient;

namespace RISCommonLibrary.Lib.Msg
{
	/// <summary>
	/// 電文ファクトリに登録
	/// </summary>
	public class RegistMsgToFactory
	{
		/// <summary>
		/// 電文ファクトリに登録
		/// </summary>
		public static void Regist()
		{
			MsgFactory.Instance.RegistMsg(typeof(ConnectionOpenMsg));
			MsgFactory.Instance.RegistMsg(typeof(ConnectionCloseMsg));
			MsgFactory.Instance.RegistMsg(typeof(OrderMsg));
			MsgFactory.Instance.RegistMsg(typeof(ReceiptMsg));
			MsgFactory.Instance.RegistMsg(typeof(ExamMsg));
			MsgFactory.Instance.RegistMsg(typeof(RequestPatientMsg));
			MsgFactory.Instance.RegistMsg(typeof(HospitalizeMsg));
			MsgFactory.Instance.RegistMsg(typeof(ResponseMsg));
			MsgFactory.Instance.RegistMsg(typeof(ResponseWithPatientMsg));
		}
	}
}
