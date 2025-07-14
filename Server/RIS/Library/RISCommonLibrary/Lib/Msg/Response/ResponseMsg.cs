using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Response
{
	/// <summary>
	/// 応答電文ハンドリングクラス
	/// </summary>
	public class ResponseMsg : BaseResponseMsg
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
				return "応答";
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
				return MsgConst.MSG_LENGTH_RESPONSE;
			}
		}

		/// <summary>
		/// メッセージツリールートクラス
		/// </summary>
		/// <remarks>キャストの手間を避けるために作成</remarks>
		public new ResponseRoot MsgBody
		{
			get
			{
				return (ResponseRoot)Body;
			}
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ResponseMsg()
			: base()
		{
			Body = new ResponseRoot();
			RequestKind = RequestKindEnum.rkResponse;
		}
		#endregion

		#region method

		#endregion
	}
}
