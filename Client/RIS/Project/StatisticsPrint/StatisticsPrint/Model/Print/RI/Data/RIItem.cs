using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.RI.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	public class RIItem
	{
		#region フィールド

		#endregion

		#region プロパティ

		/// <summary>
		/// アイテムリスト
		/// </summary>
		public List<RIItemRI> RIItemList
		{
			get;
			set;
		}

		#region 合計項目

		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RIItem()
		{
			RIItemList = new List<RIItemRI>();
		}

		#endregion

		#region メソッド
		#endregion
	}
}
