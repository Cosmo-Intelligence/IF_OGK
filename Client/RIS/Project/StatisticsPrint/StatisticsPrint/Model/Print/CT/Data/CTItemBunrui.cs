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
	/// 分類の情報を保持
	/// </remarks>
	public class CTItemBunrui
	{
		#region フィールド

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

		public List<CTItemKubun> CTItemKubunList
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </CTItemKensasitu>
		public CTItemBunrui()
		{
			CTItemKubunList = new List<CTItemKubun>();
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
			CTItemKubunList.Add(new CTItemKubun()
								{
									KBN_NAME = kbnName,
									KBN_SHOWORDER = kbnShoworder
								});
		}

		#endregion
	}
}
