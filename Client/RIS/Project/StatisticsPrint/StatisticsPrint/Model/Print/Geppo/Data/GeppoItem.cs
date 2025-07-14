using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Geppo.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	public class GeppoItem
	{
		#region フィールド

		#endregion

		#region プロパティ

		public int Count
		{
			get;
			set;
		}

		public int CountPrev
		{
			get;
			set;
		}

		/// <summary>
		/// Geppo分類リスト
		/// </summary>
		public List<GeppoItemBunrui> GeppoItemBunruiList
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
		public GeppoItem()
		{
			GeppoItemBunruiList = new List<GeppoItemBunrui>();
		}

		#endregion

		#region メソッド

		#endregion
	}
}
