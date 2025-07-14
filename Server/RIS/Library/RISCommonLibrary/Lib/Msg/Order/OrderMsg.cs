using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Order
{
	/// <summary>
	/// オーダ情報ハンドリングクラス
	/// </summary>
	public class OrderMsg : BaseMsg
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
				return "オーダ情報";
			}
		}

		/// <summary>
		/// 電文種別
		/// </summary>
		public override string[] TelegraphKinds
		{
			get
			{
				return new[] { MsgConst.TELEGRAPH_KIND_ORDER_INFO };
			}
		}

		/// <summary>
		/// メッセージ長
		/// </summary>
		public override int MsgLength
		{
			get
			{
				return MsgConst.MSG_LENGTH_ORDER;
			}
		}

		/// <summary>
		/// メッセージツリールートクラス
		/// </summary>
		/// <remarks>キャストの手間を避けるために作成</remarks>
		public OrderRoot MsgBody
		{
			get
			{
				return (OrderRoot)Body;
			}
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OrderMsg()
			: base()
		{
			Body = new OrderRoot();
			RequestKind = RequestKindEnum.rkOrder;
		}
		#endregion
	}
}
