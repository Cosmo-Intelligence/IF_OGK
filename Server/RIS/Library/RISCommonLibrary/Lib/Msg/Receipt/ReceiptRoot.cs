using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Msg.Common.Receipt;
using RISCommonLibrary.Lib.Msg.Common.PatientAttribute;

namespace RISCommonLibrary.Lib.Msg.Receipt
{
	/// <summary>
	/// 受付情報メッセージルート
	/// </summary>
	public class ReceiptRoot : CommonRootNode, IReceipt
	{
		#region field
		
		#endregion

		#region property
		
		/// <summary>
		/// 患者属性部
		/// </summary>
		public ReceiptAggregate Receipt
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReceiptRoot()
			: base(ReceiptRootNodeInfo.RECEIPT_ROOT)
		{

			Receipt = new ReceiptAggregate();
			Add(Receipt);
		}
		#endregion

	}
}
