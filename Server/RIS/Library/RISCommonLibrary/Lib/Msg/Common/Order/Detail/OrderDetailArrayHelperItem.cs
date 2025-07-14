using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common.Order;
using RISCommonLibrary.Lib.Msg.Common.Order.Detail;

namespace RISCommonLibrary.Lib.Msg.Common.Order.Detail
{
	/// <summary>
	/// オーダ明細ヘルパーアイテム
	/// </summary>
	public class OrderDetailArrayHelperItem
	{

		#region field

		/// <summary>
		/// 対象ノードリスト
		/// </summary>
		private List<BaseNode> _nodeList;

		#endregion

		#region property

		/// <summary>
		/// JAノード
		/// </summary>
		public OrderDetailAggregate JA
		{
			get
			{
				return GetNodeByItemKind(MsgConst.ORDER_ITEM_KIND_ACT);
			}
		}

		/// <summary>
		/// JBノード
		/// </summary>
		public OrderDetailAggregate JB
		{
			get
			{
				return GetNodeByItemKind(MsgConst.ORDER_ITEM_KIND_BUI);
			}
		}

		/// <summary>
		/// JHノード
		/// </summary>
		public OrderDetailAggregate JH
		{
			get
			{
				return GetNodeByItemKind(MsgConst.ORDER_ITEM_KIND_HOUKOU);
			}
		}

		/// <summary>
		/// JTノード
		/// </summary>
		public OrderDetailAggregate JT
		{
			get
			{
				return GetNodeByItemKind(MsgConst.ORDER_ITEM_KIND_TAII);
			}
		}

		/// <summary>
		/// JVリスト
		/// </summary>
		public List<OrderDetailAggregate> JVList
		{
			get
			{
				return GetListByItemKind(MsgConst.ORDER_ITEM_KIND_ACT_COMMENT);
			}
		}

		#endregion

		#region constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OrderDetailArrayHelperItem(List<BaseNode> nodeList)
		{
			this._nodeList = nodeList;
		}
		#endregion

		#region method
		
		/// <summary>
		/// ITEM_KINDの値によりOrderDetailListを返す
		/// </summary>
		/// <param name="itemKind"></param>
		/// <returns></returns>
		private OrderDetailAggregate GetNodeByItemKind(string itemKind)
		{
			IEnumerable<OrderDetailAggregate> nList = from n in _nodeList
										 where ((OrderDetailAggregate)n).ITEM_KIND.TrimData == itemKind
										 select ((OrderDetailAggregate)n);
			return nList.FirstOrDefault();
		}

		/// <summary>
		/// ITEM_KINDの値によりList&lt;OrderDetailAgg&gt;を返す
		/// </summary>
		/// <param name="itemKind"></param>
		/// <returns></returns>
		private List<OrderDetailAggregate> GetListByItemKind(string itemKind)
		{
			IEnumerable<OrderDetailAggregate> nList = from n in _nodeList
												 where ((OrderDetailAggregate)n).ITEM_KIND.TrimData == itemKind
												 select ((OrderDetailAggregate)n);
			return nList.ToList();
		}
		#endregion

	}
}
