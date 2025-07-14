using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Receipt
{
	/// <summary>
	/// 入退院情報ハンドリングクラス
	/// </summary>
	public class ReceiptMsg : BaseMsg
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
				return "受付（進捗）情報";
			}
		}

		/// <summary>
		/// 電文種別
		/// </summary>
		public override string[] TelegraphKinds
		{
			get
			{
				return new[] { MsgConst.TELEGRAPH_KIND_RECEIPT_INFO };
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
				return MsgConst.MSG_LENGTH_RECEIPT;
			}
		}

		/// <summary>
		/// メッセージツリールートクラス
		/// </summary>
		/// <remarks>キャストの手間を避けるために作成</remarks>
		public ReceiptRoot MsgBody
		{
			get
			{
				return (ReceiptRoot)Body;
			}
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReceiptMsg()
			: base()
		{
			Body = new ReceiptRoot();
			RequestKind = RequestKindEnum.rkReceipt;
		}
		#endregion
	}
}
