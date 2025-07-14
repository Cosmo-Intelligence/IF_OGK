using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsPrint.Data
{
	/// <summary>
	/// 
	/// </summary>
	internal class ConditionPrintPlace : ConditionPrint
	{
		/// <summary>
		/// 実施場所ID
		/// </summary>
		public string PlaceID
		{
			get;
			set;
		}

		/// <summary>
		/// 実施場所名
		/// </summary>
		public string PlaceName
		{
			get;
			set;
		}
	}
}
