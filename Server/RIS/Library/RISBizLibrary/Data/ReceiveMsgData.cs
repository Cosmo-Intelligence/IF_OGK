using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISBizLibrary.Data
{
	public abstract class ReceiveMsgData : BaseMsgData
	{
		#region メッセージから取得

		/// <summary>
		/// 患者ID
		/// </summary>
		public abstract string KanjaID
		{
			get;
		}

		#endregion
	}
}
