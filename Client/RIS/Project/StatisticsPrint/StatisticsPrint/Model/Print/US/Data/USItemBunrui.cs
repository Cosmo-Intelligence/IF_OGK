using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.US.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	/// <remarks>
	/// 分類の情報を保持
	/// </remarks>
	public class USItemBunrui : IHasDetailDayList
	{
		#region フィールド

		/// <summary>
		/// 日付保持クラス実装
		/// </summary>
		private HasDetailDayListImpl _hasDetailDayListImpl;

		#endregion

		#region プロパティ

		public string BUNRUI_NAME
		{
			get;
			set;
		}

		public string BUNRUI_SHOWORDER
		{
			get;
			set;
		}

		public List<USItemKubun> USItemKubunList
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
		/// 小計(分類)
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
		/// </USItemKensasitu>
		public USItemBunrui()
		{
			USItemKubunList = new List<USItemKubun>();
			_hasDetailDayListImpl = new HasDetailDayListImpl();
		}
		#endregion

		#region メソッド

		/// <summary>
		/// 区分追加
		/// </summary>
		/// <param name="kbnName"></param>
		/// <param name="kbnShoworder"></param>
		/// <returns></returns>
		public void Add(string kbnName, string kbnShoworder)
		{
			if (string.IsNullOrEmpty(kbnName))
			{
				return;
			}
			USItemKubunList.Add(new USItemKubun()
								{
									KBN_NAME = kbnName,
									KBN_SHOWORDER = kbnShoworder
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
