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
	/// オーダ明細ヘルパー
	/// </summary>
	public class OrderDetailArrayHelper : IEnumerable<OrderDetailArrayHelperItem>
	{
		#region field
		/// <summary>
		/// 明細配列
		/// </summary>
		private OrderDetailArray _orderDetailArray;

		/// <summary>
		/// JAのリスト
		/// </summary>
		private List<BaseNode> _jaList;

		/// <summary>
		/// 明細行のリスト
		/// </summary>
		private List<OrderDetailArrayHelperItem> _itemList;

		#endregion

		#region prop

		/// <summary>
		/// インデクサ
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public OrderDetailArrayHelperItem this[int index]
		{
			get
			{
				return _itemList[index];
			}
		}

		/// <summary>
		/// JAのカウント=明細行のカウント
		/// </summary>
		public int Count
		{
			get
			{
				return _jaList.Count();
			}
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="ary"></param>
		public OrderDetailArrayHelper(OrderDetailArray ary)
		{
			_orderDetailArray = ary;

			#region JAのリスト作成
			
			IEnumerable<BaseNode> jas = from l in _orderDetailArray
											where ((OrderDetailAggregate)l).ITEM_KIND.TrimData == MsgConst.ORDER_ITEM_KIND_ACT
											select l;
			_jaList = jas.ToList();
			#endregion

			#region 明細行のリスト作成

			_itemList = new List<OrderDetailArrayHelperItem>();
			for (int jaIndex = 0; jaIndex < _jaList.Count; jaIndex++)
			{
				BaseNode jaItem = _jaList[jaIndex];
				int startIndex = jaItem.Index;

				int lastIndex;
				#region 明細行の最終Index取得
				
				int nextJAIndex = jaIndex + 1; //次のJA項目

				if (_jaList.Count -1 < nextJAIndex)
				{
					lastIndex = _orderDetailArray.Count -1; //自分が最後のJAだったら
				}
				else
				{
					lastIndex = _jaList[nextJAIndex].Index -1;
				}
				#endregion

				List<BaseNode> list = new List<BaseNode>();
				while (startIndex <= lastIndex)
				{
					OrderDetailAggregate detailAgg = _orderDetailArray[startIndex];
					list.Add(detailAgg);
					startIndex++;
				}
				_itemList.Add(new OrderDetailArrayHelperItem(list));
			}
			#endregion
		}

		/// <summary>
		/// 指定された番目のJAIndexを返す
		/// </summary>
		/// <param name="jaIndex">何番目のJA。0オリジン</param>
		/// <returns></returns>
		public int IndexOfByJANo(int jaIndex)
		{
			if (_jaList.Count -1 < jaIndex)
			{
				return -1;
			}

			BaseNode jaNode = _jaList[jaIndex];
			if (jaNode == null)
			{
				return -1;
			}
			return jaNode.Index;
		}

		#endregion

		#region IEnumerable<OrderDetailArrayHelperItem> メンバ

		public IEnumerator<OrderDetailArrayHelperItem> GetEnumerator()
		{
			return _itemList.GetEnumerator();
		}

		#endregion

		#region IEnumerable メンバ

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}
