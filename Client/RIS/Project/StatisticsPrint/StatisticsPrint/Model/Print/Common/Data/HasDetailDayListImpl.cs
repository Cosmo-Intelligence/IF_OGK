using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsPrint.Model.Print.Common.Data
{
	/// <summary>
	/// 帳票明細部日付部分のリストを持つ実装
	/// </summary>
	public class HasDetailDayListImpl : IHasDetailDayList
	{
		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HasDetailDayListImpl()
		{
			DetailDayList = new List<DetailDayItem>();
		}
		#endregion

		#region IHasDetailDayList メンバ

		/// <summary>
		/// 明細部日付リスト
		/// </summary>
		public List<DetailDayItem> DetailDayList
		{
			get;
			set;
		}

		/// <summary>
		/// 日付追加
		/// </summary>
		/// <param name="examenddate_yyyymmdd10"></param>
		/// <param name="cnt"></param>
		/// <returns></returns>
		public void AddDay(string examenddate_yyyymmdd10, string cnt)
		{
			if (string.IsNullOrEmpty(examenddate_yyyymmdd10))
			{
				return;
			}
			DetailDayList.Add(new DetailDayItem()
			{
				EXAMENDDATE_YYYYMMDD10 = examenddate_yyyymmdd10,
				CNT= cnt
			});
		}

		#endregion
	}
}
