using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.RequestPatient
{
	/// <summary>
	/// 入退院情報ハンドリングクラス
	/// </summary>
	public class RequestPatientMsg : BaseMsg
	{
		#region field
		#endregion

		#region property

		/// <summary>
		/// 対象電文名
		/// </summary>
		public override string MessageNameJ
		{
			get
			{
				return "患者情報要求";
			}
		}

		/// <summary>
		/// 電文種別
		/// </summary>
		public override string[] TelegraphKinds
		{
			get
			{
				return new[] { MsgConst.TELEGRAPH_KIND_PATIENT_INFO };
			}
		}

		/// <summary>
		/// メッセージ長
		/// </summary>
		public override int MsgLength
		{
			get
			{
				//固定長
				return MsgConst.MSG_LENGTH_REQUEST_PATIENT;
			}
		}

		/// <summary>
		/// メッセージツリールートクラス
		/// </summary>
		/// <remarks>キャストの手間を避けるために作成</remarks>
		public RequestPatientRoot MsgBody
		{
			get
			{
				return (RequestPatientRoot)Body;
			}
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RequestPatientMsg()
			: base()
		{
			Body = new RequestPatientRoot();
			RequestKind = RequestKindEnum.rkPatient;
		}
		#endregion
	}
}
