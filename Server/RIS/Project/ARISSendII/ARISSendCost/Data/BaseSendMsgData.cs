using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISBizLibrary.Data;

namespace ARISSend.Data
{
	public class BaseSendMsgData : BaseMsgData
	{
		#region property

		#region 外からもらってくるもの

		/// <summary>
		/// データ元ソース
		/// </summary>
		public ToHisInfo DataSource
		{
			get;
			set;
		}

		#endregion

		#endregion

	}
}
