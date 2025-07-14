using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsPrint.Model.Print.Bui.Data
{
	/// <summary>
	/// 合計フィールド名取得
	/// </summary>
	public class TotalFieldName
	{
		public virtual string TotalName
		{
			get
			{
				return "小計(検査装置+検査種別)";
			}
		}

		public virtual string TotalNameDay
		{
			get
			{
				return "小計(検査装置+検査種別+日付)";
			}
		}

		public virtual string TotalNameBuiBunrui
		{
			get
			{
				return "小計(検査装置+検査種別+部位分類)";
			}
		}
	}
}
