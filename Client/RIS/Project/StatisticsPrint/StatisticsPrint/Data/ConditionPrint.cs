using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsPrint.Data
{
	/// <summary>
	/// 印刷条件クラス
	/// </summary>
	public class ConditionPrint
	{
		/// <summary>
		/// 年月
		/// </summary>
		/// <remarks>
		/// 日付は月初に設定される
		/// </remarks>
		public DateTime Nengetsu
		{
			get;
			set;
		}

		/// <summary>
		/// プリンタ名
		/// </summary>
		public string PrinterName
		{
			get;
			set;
		}

		/// <summary>
		/// 部数
		/// </summary>
		public int Copies
		{
			get;
			set;
		}

	}
}
