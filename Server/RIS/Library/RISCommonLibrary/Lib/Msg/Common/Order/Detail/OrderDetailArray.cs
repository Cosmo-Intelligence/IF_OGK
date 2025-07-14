using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Order.Detail
{
	/// <summary>
	/// オーダ情報明細
	/// </summary>
	public class OrderDetailArray : ArrayNode
	{

		#region field
		#endregion

		#region property

		/// <summary>
		/// インデクサ
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public new OrderDetailAggregate this[int index]
		{
			get
			{
				return (OrderDetailAggregate)base[index];
			}
		}

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OrderDetailArray()
			: base(OrderNodeInfo.ORDER_ORDER_DETAIL_SUMM)
		{

		}
		#endregion

		#region method
		/// <summary>
		/// 子クラスの型
		/// </summary>
		/// <returns></returns>
		public override Type GetElementClass()
		{
			return typeof(OrderDetailAggregate);
		}

		/// <summary>
		/// ヘルパー
		/// </summary>
		public OrderDetailArrayHelper GetHelper()
		{
			return new OrderDetailArrayHelper(this);
		}
		#endregion

		#endregion

	}
}
