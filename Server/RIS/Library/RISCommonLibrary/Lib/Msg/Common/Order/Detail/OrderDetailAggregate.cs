using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Order.Detail
{
	/// <summary>
	/// オーダ情報部繰り返し明細部
	/// </summary>
	public class OrderDetailAggregate : AggregateNode
	{
		#region property
		/// <summary>
		/// 項目区分
		/// </summary>
		public DataNode ITEM_KIND
		{
			get;
			set;
		}

		/// <summary>
		/// コメント区分
		/// </summary>
		public DataNode COMMENT_KIND
		{
			get;
			set;
		}

		/// <summary>
		/// 項目コード
		/// </summary>
		public DataNode ITEM_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 行為コメント
		/// </summary>
		public DataNode ACTING_COMMENT
		{
			get;
			set;
		}

		#endregion	

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OrderDetailAggregate()
			: base(OrderNodeInfo.ORDER_ORDER_DETAIL_LIST)
		{
			ITEM_KIND = AddChildNode(new DataNode(OrderNodeInfo.ORDER_ITEM_KIND));
			COMMENT_KIND = AddChildNode(new DataNode(OrderNodeInfo.ORDER_COMMENT_KIND));
			ITEM_CODE = AddChildNode(new DataNode(OrderNodeInfo.ORDER_ITEM_CODE));
			ACTING_COMMENT = AddChildNode(new DataNode(OrderNodeInfo.ORDER_ACTING_COMMENT));
		}

	}
}
