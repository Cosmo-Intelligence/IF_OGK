using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.CommunicationControl
{
	public interface ICommunicationControl
	{
		/// <summary>
		/// 通信制御部
		/// </summary>
		CommunicationControlAggregate CommunicationControl
		{
			get;
			set;
		}
	}
}
