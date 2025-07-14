using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsPrint.Model.Print.Common.Data
{
	/// <summary>
	/// 帳票明細部日付部分
	/// </summary>
	public class DetailDayItem
	{
		/// <summary>
		/// 対象日付
		/// </summary>
		public string EXAMENDDATE_YYYYMMDD10
		{
			get;
			set;
		}

		/// <summary>
		/// 数
		/// </summary>
		public string CNT
		{
			get;
			set;
		}
	}
}
