using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using RISCommonLibrary.Lib.Utils;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Geppo.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	/// <remarks>
	/// 区分ごとの情報を保持
	/// </remarks>
	public class GeppoItemKubun
	{
		#region フィールド
		#endregion

		#region プロパティ

		public string KBN_NAME
		{
			get;
			set;
		}

		public string KBN_SHOWORDER
		{
			get;
			set;
		}

		/// <summary>
		/// 数
		/// </summary>
		public string CNT
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GeppoItemKubun()
		{
		}
		#endregion

		#region メソッド
		
		#endregion
	}
}
