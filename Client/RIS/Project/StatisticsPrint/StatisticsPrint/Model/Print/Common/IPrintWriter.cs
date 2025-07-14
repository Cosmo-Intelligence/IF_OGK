using System;
using StatisticsPrint.Data;

namespace StatisticsPrint.Model.Print.Common
{
	/// <summary>
	/// 印刷時データ書き込みインターフェース
	/// </summary>
	public interface IPrintWriter
	{
		ConditionPrint Condition
		{
			get;
			set;
		}
		void Write(object sender, StatisticsPrint.Model.COReports.PrintEventArgs e);
	}
}
