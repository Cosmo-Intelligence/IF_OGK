using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Receipt
{
	public interface IReceipt
	{
		/// <summary>
		/// 患者属性部
		/// </summary>
		ReceiptAggregate Receipt
		{
			get;
			set;
		}
	}
}
