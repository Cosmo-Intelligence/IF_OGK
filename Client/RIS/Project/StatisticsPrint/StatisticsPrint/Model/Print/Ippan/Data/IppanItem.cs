using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Ippan.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	public class IppanItem
	{
		#region フィールド

		#endregion

		#region プロパティ

		/// <summary>
		/// アイテムリスト
		/// </summary>
		public List<IppanItemIppan> IppanItemList
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
		public IppanItem()
		{
			IppanItemList = new List<IppanItemIppan>();
		}

		#endregion

		#region メソッド
		#endregion
	}
}
