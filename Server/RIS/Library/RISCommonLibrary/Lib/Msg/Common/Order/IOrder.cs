using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Order
{
	public interface IOrder
	{
		/// <summary>
		/// オーダ情報部
		/// </summary>
		OrderAggregate Order
		{
			get;
			set;
		}
	}
}
