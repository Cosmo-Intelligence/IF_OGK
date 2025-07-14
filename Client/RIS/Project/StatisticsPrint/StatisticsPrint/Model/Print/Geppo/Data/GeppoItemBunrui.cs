using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;
using RISCommonLibrary.Lib.Utils;

namespace StatisticsPrint.Model.Print.Geppo.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	/// <remarks>
	/// 分類の情報を保持
	/// </remarks>
	public class GeppoItemBunrui
	{
		#region フィールド

		#endregion

		#region プロパティ

		/// <summary>
		/// 分類ID
		/// </summary>
		/// <remarks>statistics_monthly_dat.class_keyと同期</remarks>
		public string BUNRUI_ID
		{
			get;
			set;
		}

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

		public List<GeppoItemKubun> GeppoItemKubunList
		{
			get;
			set;
		}

		#region 合計項目

		/// <summary>
		/// 今年度小計(分類)
		/// </summary>
		public string Total
		{
			get;
			set;
		}

		/// <summary>
		/// 前年度小計(分類)
		/// </summary>
		public string TotalPrevious
		{
			get;
			set;
		}

		/// <summary>
		/// 前年比率
		/// </summary>
		/// <remarks>小数点第2位で四捨五入する</remarks>
		public string RatioPreviousYear
		{
			get
			{
				decimal totaInt =  StringUtils.StringToDecimal(Total);
				decimal previousTotalInt = StringUtils.StringToDecimal(TotalPrevious);
				if (previousTotalInt == 0)
				{
					return " "; //前年度データなし、もしくはNULL、0の場合は半角スペースを返す
				}
				const int PRECISION = 1;
				decimal ratio = (totaInt / previousTotalInt) * 100;
				ratio = Math.Round(ratio, PRECISION, MidpointRounding.AwayFromZero);
				return ratio.ToString("0.0");
			}
		}
		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </GeppoItemKensasitu>
		public GeppoItemBunrui()
		{
			GeppoItemKubunList = new List<GeppoItemKubun>();
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
			GeppoItemKubunList.Add(new GeppoItemKubun()
								{
									KBN_NAME = kbnName,
									KBN_SHOWORDER = kbnShoworder
								});
		}

		#endregion
	}
}
