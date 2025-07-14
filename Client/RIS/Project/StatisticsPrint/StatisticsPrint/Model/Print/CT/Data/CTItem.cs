using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.CT.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	public class CTItem
	{
		#region フィールド

		#endregion

		#region プロパティ

		/// <summary>
		/// 実施場所リスト
		/// </summary>
		public List<CTItemPlace> CTItemList
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CTItem()
		{
			CTItemList = new List<CTItemPlace>();
		}

		#endregion

		#region メソッド

		#endregion
	}
}
