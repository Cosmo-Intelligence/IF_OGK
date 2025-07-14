using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Bui.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	/// <remarks>
	/// 検査種別の情報を保持
	/// </remarks>
	public class BuiItemKensaType : IHasDetailDayList
	{
		#region フィールド

		/// <summary>
		/// 日付保持クラス実装
		/// </summary>
		private HasDetailDayListImpl _hasDetailDayListImpl;
		#endregion

		#region プロパティ

		public string KENSATYPE_ID
		{
			get;
			set;
		}

		public string KENSATYPE_NAME
		{
			get;
			set;
		}

		public string KENSATYPE_SHOWORDER
		{
			get;
			set;
		}

		public List<BuiItemBui> BuiItemList
		{
			get;
			set;
		}

		#region 合計項目

		/// <summary>
		/// 日次小計リスト
		/// </summary>
		public List<DetailDayItem> DetailDayList
		{
			get
			{
				return _hasDetailDayListImpl.DetailDayList;
			}
			set
			{
				_hasDetailDayListImpl.DetailDayList = value;
			}
		}

		/// <summary>
		/// 小計(検査種別)
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
		public BuiItemKensaType()
		{
			BuiItemList = new List<BuiItemBui>();
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
