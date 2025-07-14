using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common.Order.Detail;

namespace RISCommonLibrary.Lib.Msg.Common.Order
{
	/// <summary>
	/// オーダ情報部
	/// </summary>
	public class OrderAggregate : AggregateNode
	{
		#region property

		/// <summary>
		/// 依頼科コード
		/// </summary>
		public DataNode REQUEST_SECTION_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 依頼医師コード
		/// </summary>
		public DataNode REQUEST_DOCTOR_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 依頼医師名
		/// </summary>
		public DataNode REQUEST_DOCTOR_NAME
		{
			get;
			set;
		}

		/// <summary>
		/// オーダ区分
		/// </summary>
		public DataNode ORDER_KIND
		{
			get;
			set;
		}

		/// <summary>
		/// オーダ登録日
		/// </summary>
		public DataNode ORDER_REGISTER_DATE
		{
			get;
			set;
		}

		/// <summary>
		/// オーダ登録時間
		/// </summary>
		public DataNode ORDER_REGISTER_TIME
		{
			get;
			set;
		}

		/// <summary>
		/// 実施開始予定日
		/// </summary>
		public DataNode EXAM_START_SCHEDULED_DATE
		{
			get;
			set;
		}

		/// <summary>
		/// 実施開始予定時間
		/// </summary>
		public DataNode EXAM_START_SCHEDULED_TIME
		{
			get;
			set;
		}

		/// <summary>
		/// 検査室
		/// </summary>
		public DataNode EXAM_ROOM
		{
			get;
			set;
		}

		/// <summary>
		/// 所見要否
		/// </summary>
		public DataNode REMARK_NECESSITY
		{
			get;
			set;
		}

		/// <summary>
		/// 予備1
		/// </summary>
		public DataNode RESERVE1
		{
			get;
			set;
		}

		/// <summary>
		/// 検査目的
		/// </summary>
		public DataNode EXAM_PURPOSE
		{
			get;
			set;
		}

		/// <summary>
		/// オーダー明細行繰返し回数
		/// </summary>
		public OrderDetailArray ORDER_DETAIL_SUMM
		{
			get;
			set;
		}

		/// <summary>
		/// 予備2
		/// </summary>
		public DataNode RESERVE2
		{
			get;
			set;
		}

		
		#endregion

		#region constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OrderAggregate()
			: base(OrderNodeInfo.ORDER_ROOT)
		{
			REQUEST_SECTION_CODE = AddChildNode(new DataNode(OrderNodeInfo.ORDER_REQUEST_SECTION_CODE));
			REQUEST_DOCTOR_CODE = AddChildNode(new DataNode(OrderNodeInfo.ORDER_REQUEST_DOCTOR_CODE));
			REQUEST_DOCTOR_NAME = AddChildNode(new DataNode(OrderNodeInfo.ORDER_REQUEST_DOCTOR_NAME));
			ORDER_KIND = AddChildNode(new DataNode(OrderNodeInfo.ORDER_ORDER_KIND));
			ORDER_REGISTER_DATE = AddChildNode(new DataNode(OrderNodeInfo.ORDER_ORDER_REGISTER_DATE));
			ORDER_REGISTER_TIME = AddChildNode(new DataNode(OrderNodeInfo.ORDER_ORDER_REGISTER_TIME));
			EXAM_START_SCHEDULED_DATE = AddChildNode(new DataNode(OrderNodeInfo.ORDER_EXAM_START_SCHEDULED_DATE));
			EXAM_START_SCHEDULED_TIME = AddChildNode(new DataNode(OrderNodeInfo.ORDER_EXAM_START_SCHEDULED_TIME));
			EXAM_ROOM = AddChildNode(new DataNode(OrderNodeInfo.ORDER_EXAM_ROOM));
			REMARK_NECESSITY = AddChildNode(new DataNode(OrderNodeInfo.ORDER_REMARK_NECESSITY));
			RESERVE1 = AddChildNode(new DataNode(OrderNodeInfo.ORDER_RESERVE1));
			EXAM_PURPOSE = AddChildNode(new DataNode(OrderNodeInfo.ORDER_EXAM_PURPOSE));
			ORDER_DETAIL_SUMM = new OrderDetailArray();
			Add(ORDER_DETAIL_SUMM);
			//なぜかログからとったものについて予備2があるとうまくいかない
			//RESERVE2 = AddChildNode(new DataNode(OrderNodeInfo.ORDER_RESERVE2));
		}
		#endregion
	}
}
