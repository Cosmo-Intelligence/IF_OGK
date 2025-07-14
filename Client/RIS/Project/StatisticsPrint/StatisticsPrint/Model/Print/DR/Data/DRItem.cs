using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.DR.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	public class DRItem : IHasDetailDayList
	{
		#region フィールド

		/// <summary>
		/// 日付保持クラス実装
		/// </summary>
		private HasDetailDayListImpl _hasDetailDayListImpl;
		#endregion

		#region プロパティ

		/// <summary>
		/// アイテムリスト
		/// </summary>
		public List<DRItemDR> DRItemList
		{
			get;
			set;
		}

		#region 合計項目

		/// <summary>
		/// 小計(日付)
		/// </summary>
		public List<DetailDayItem> DetailDayList
		{
			get
			{
				return _hasDetailDayListImpl.DetailDayList;
			}
		}

		/// <summary>
		/// 総計
		/// </summary>
		public string Total
		{
			get;
			set;
		}

		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DRItem()
		{
			DRItemList = new List<DRItemDR>();
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
