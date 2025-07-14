using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.BuiBunrui.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	/// <remarks>
	/// 検査種別レベルの情報をまとめたもの
	/// </remarks>
	public class BuiBunruiItemKensaType : IHasDetailDayList
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

		public List<BuiBunruiItemBuiBunrui> BuiBunruiItemList
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
		public BuiBunruiItemKensaType()
		{
			BuiBunruiItemList = new List<BuiBunruiItemBuiBunrui>();
			_hasDetailDayListImpl = new HasDetailDayListImpl();
		}
		#endregion

		#region メソッド

		/// <summary>
		/// 部位分類追加
		/// </summary>
		/// <param name="buiBunruiID"></param>
		/// <param name="buiBunruiName"></param>
		/// <param name="buiShoworder"></param>
		/// <returns></returns>
		public void Add(string buiBunruiID, string buiBunruiName, string buiShoworder)
		{
			if (string.IsNullOrEmpty(buiBunruiID))
			{
				return;
			}
			BuiBunruiItemList.Add(new BuiBunruiItemBuiBunrui()
								{
									BUIBUNRUI_ID = buiBunruiID,
									BUIBUNRUI_NAME = buiBunruiName,
									BUI_SHOWORDER = buiShoworder
								});

		}

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
