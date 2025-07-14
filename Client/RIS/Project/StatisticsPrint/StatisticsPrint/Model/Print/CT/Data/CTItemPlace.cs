using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.CT.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	/// <remarks>
	/// 実施場所の情報を保持
	/// </remarks>
	public class CTItemPlace
	{
		#region フィールド
		#endregion

		#region プロパティ

		public string PLACE_ID
		{
			get;
			set;
		}

		public string PLACE_NAME
		{
			get;
			set;
		}

		public string PLACE_SHOWORDER
		{
			get;
			set;
		}

		public List<CTItemBunrui> CTItemBunruiList
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </CTItemKensasitu>
		public CTItemPlace()
		{
			CTItemBunruiList = new List<CTItemBunrui>();
		}
		#endregion

		#region メソッド

		/// <summary>
		/// 分類追加
		/// </summary>
		/// <param name="bunruiName"></param>
		/// <param name="bunruiShoworder"></param>
		/// <returns></returns>
		public void Add(string bunruiName, string bunruiShoworder)
		{
			if (string.IsNullOrEmpty(bunruiName))
			{
				return;
			}
			CTItemBunruiList.Add(new CTItemBunrui()
								{
									BUNRUI_NAME = bunruiName,
									BUNRUI_SHOWORDER = bunruiShoworder
								});

		}

		#endregion
	}
}
