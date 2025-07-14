using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Response;

namespace RISCommonLibrary.Lib.Msg.ResponseWithPatient
{
	/// <summary>
	/// 応答電文患者情報ハンドリングクラス
	/// </summary>
	public class ResponseWithPatientMsg : BaseResponseMsg
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
				return "応答患者情報";
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
				return MsgConst.MSG_LENGTH_RESPONSE_WITH_PATIENT;
			}
		}

		/// <summary>
		/// メッセージツリールートクラス
		/// </summary>
		/// <remarks>キャストの手間を避けるために作成</remarks>
		public new ResponseWithPatientRoot MsgBody
		{
			get
			{
				return (ResponseWithPatientRoot)Body;
			}
		}

		/// <summary>
		/// 電文種別
		/// </summary>
		public override string[] TelegraphKinds
		{
			get
			{
				return new[] { MsgConst.TELEGRAPH_KIND_RESPONSE_OK,
								MsgConst.TELEGRAPH_KIND_RESPONSE_NG};
			}
		}
		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ResponseWithPatientMsg()
			: base()
		{
			Body = new ResponseWithPatientRoot();
			RequestKind = RequestKindEnum.rkResponseWithPatient;
		}
		#endregion

		#region method

		#endregion

	}
}
