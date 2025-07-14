using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using RISCommonLibrary.Lib.Utils;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Zoueizai.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	/// <remarks>
	/// フィルムごとの情報を保持
	/// </remarks>
	public class ZoueizaiItemZoueizai : IHasDetailDayList
	{
		#region フィールド

		/// <summary>
		/// 日付保持クラス実装
		/// </summary>
		private HasDetailDayListImpl _hasDetailDayListImpl;
		#endregion

		#region プロパティ

		public string ZOUEIZAI_ID
		{
			get;
			set;
		}

		public string ZOUEIZAI_NAME
		{
			get;
			set;
		}

		public string ZOUEIZAI_SHOWORDER
		{
			get;
			set;
		}

		public string ZOUEIZAITANNI_NAME
		{
			get;
			set;
		}

		#region 合計項目
		
		/// <summary>
		/// 小計(フィルム)
		/// </summary>
		public string Total
		{
			get;
			set;
		}

		/// <summary>
		/// 日付ごと明細
		/// </summary>
		public List<DetailDayItem> DetailDayList
		{
			get
			{
				return _hasDetailDayListImpl.DetailDayList;
			}
		}

		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ZoueizaiItemZoueizai()
		{
			_hasDetailDayListImpl = new HasDetailDayListImpl();
		}
		#endregion

		#region メソッド
		
		/// <summary>
		/// 日付追加
		/// </summary>
		/// <param name="examenddate_yyyymmdd10"></param>
		/// <param name="cnt"></param>
		/// <returns></returns>
		public void AddDay(string examenddate_yyyymmdd10, string cnt)
		{
			_hasDetailDayListImpl.AddDay(examenddate_yyyymmdd10, cnt);
		}

		#endregion
	}
}
