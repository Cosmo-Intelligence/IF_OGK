using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsPrint.Model.Print.Common.Data
{
	/// <summary>
	/// 帳票明細部日付部分のリストを持つ
	/// </summary>
	interface IHasDetailDayList
	{
		/// <summary>
		/// 明細部日付リスト
		/// </summary>
		List<DetailDayItem> DetailDayList
		{
			get;
		}

		/// <summary>
		/// 日付追加
		/// </summary>
		/// <param name="examenddate_yyyymmdd10"></param>
		/// <param name="cnt"></param>
		/// <returns></returns>
		void AddDay(string examenddate_yyyymmdd10, string cnt);


	}
}
