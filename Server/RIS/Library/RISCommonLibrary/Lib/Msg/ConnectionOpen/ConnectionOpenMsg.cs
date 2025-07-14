using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.ConnectionOpen
{
	/// <summary>
	/// 接続要求ハンドリングクラス
	/// </summary>
	public class ConnectionOpenMsg : BaseMsg
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
				return "回線接続要求";
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
				return MsgConst.MSG_LENGTH_COMMUNICATION_CONTROL_OPEN;
			}
		}

		/// <summary>
		/// 電文種別
		/// </summary>
		public override string[] TelegraphKinds
		{
			get
			{
				return new[] { MsgConst.TELEGRAPH_KIND_REQUEST_OPEN };
			}
		}

		/// <summary>
		/// メッセージツリールートクラス
		/// </summary>
		/// <remarks>キャストの手間を避けるために作成</remarks>
		public ConnectionOpenRoot MsgBody
		{
			get
			{
				return (ConnectionOpenRoot)Body;
			}
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ConnectionOpenMsg()
			: base()
		{
			Body = new ConnectionOpenRoot();
			RequestKind = RequestKindEnum.rkRequestOpen;
		}
		#endregion

		#region method

		#endregion

	}
}
