using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;
using RISCommonLibrary.Lib.Msg.Common.Order;

namespace RISCommonLibrary.Lib.Msg.Order
{
	/// <summary>
	/// オーダ情報メッセージルート
	/// </summary>
	public class OrderRoot : CommonRootNode, IOrder, IPatientAttribute
	{
		#region field
		
		#endregion

		#region property

		/// <summary>
		/// 患者属性部
		/// </summary>
		public PatientAttributeAggregate PatientAttribute
		{
			get;
			set;
		}

		/// <summary>
		/// オーダ情報部
		/// </summary>
		public OrderAggregate Order
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OrderRoot()
			: base(OrderRootNodeInfo.ORDER_ROOT)
		{

			PatientAttribute = new PatientAttributeAggregate();
			Add(PatientAttribute);

			Order = new OrderAggregate();
			Add(Order);
		}
		#endregion

	}
}
