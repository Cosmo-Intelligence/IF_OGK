using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Receipt
{
	/// <summary>
	/// 受付(進捗)情報部
	/// </summary>
	public class ReceiptAggregate : AggregateNode
	{
		#region property

		/// <summary>
		/// 処理タイプ
		/// </summary>
		public DataNode PROCESSING_TYPE
		{
			get;
			set;
		}

		/// <summary>
		/// 患者番号
		/// </summary>
		public DataNode PATIENT_NO
		{
			get;
			set;
		}

		/// <summary>
		/// 受付日
		/// </summary>
		public DataNode RECEIPT_DATE
		{
			get;
			set;
		}

		/// <summary>
		/// 受付時間
		/// </summary>
		public DataNode RECEIPT_TIME
		{
			get;
			set;
		}

		/// <summary>
		/// 受付者コード
		/// </summary>
		public DataNode RECEIPTOR_CODE
		{
			get;
			set;
		}

		/// <summary>
		/// 予備
		/// </summary>
		public DataNode RESERVE
		{
			get;
			set;
		}
		
		#endregion

		#region constructor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReceiptAggregate()
			: base(ReceiptNodeInfo.RECEIPT_ROOT)
		{
			PROCESSING_TYPE = AddChildNode(new DataNode(ReceiptNodeInfo.RECEIPT_PROCESSING_TYPE));
			PATIENT_NO = AddChildNode(new DataNode(ReceiptNodeInfo.RECEIPT_PATIENT_NO));
			RECEIPT_DATE = AddChildNode(new DataNode(ReceiptNodeInfo.RECEIPT_RECEIPT_DATE));
			RECEIPT_TIME = AddChildNode(new DataNode(ReceiptNodeInfo.RECEIPT_RECEIPT_TIME));
			RECEIPTOR_CODE = AddChildNode(new DataNode(ReceiptNodeInfo.RECEIPT_RECEIPTOR_CODE));
			RESERVE = AddChildNode(new DataNode(ReceiptNodeInfo.RECEIPT_RESERVE));
		}
		#endregion
	}
}
