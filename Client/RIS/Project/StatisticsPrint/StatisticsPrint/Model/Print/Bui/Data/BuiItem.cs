using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;
using StatisticsPrint.Model.Print.Bui.Data.ALL;

namespace StatisticsPrint.Model.Print.Bui.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	public class BuiItem
	{
		#region フィールド

		#endregion

		#region プロパティ

		/// <summary>
		/// アイテムリスト
		/// </summary>
		public List<IBuiItemChild> ChildItemList
		{
			get;
			set;
		}

		#region 合計項目
		
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
		public BuiItem()
		{
			ChildItemList = new List<IBuiItemChild>();
		}

		#endregion

		#region メソッド

		/// <summary>
		/// 検査種別追加
		/// </summary>
		/// <param name="kensasituID"></param>
		/// <param name="kensasituName"></param>
		/// <param name="kensasituShoworder"></param>
		/// <returns></returns>
		public BuiItemExamRoom Add(string kensasituID, string kensasituName, string kensasituShoworder)
		{
			if (string.IsNullOrEmpty(kensasituID))
			{
				return null;
			}
			BuiItemExamRoom item = new BuiItemExamRoom()
										{
											KENSASITU_ID = kensasituID,
											KENSASITU_NAME = kensasituName,
											KENSASITU_SHOWORDER = kensasituShoworder
										};
			ChildItemList.Add(item);
			return item;
		}

		/// <summary>
		/// 検査種別追加
		/// </summary>
		/// <param name="placeID"></param>
		/// <param name="placeName"></param>
		/// <param name="placeShoworder"></param>
		/// <returns></returns>
		public BuiItemPlaceAll AddForAll(string placeID, string placeName, string placeShoworder)
		{
			if (string.IsNullOrEmpty(placeID))
			{
				return null;
			}
			BuiItemPlaceAll item = new BuiItemPlaceAll()
			{
				PLACE_ID = placeID,
				PLACE_NAME = placeName,
				PLACE_SHOWORDER = placeShoworder
			};
			ChildItemList.Add(item);
			return item;
		}
		#endregion

	}
}
