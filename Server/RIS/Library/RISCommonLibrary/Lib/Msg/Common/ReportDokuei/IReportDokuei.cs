using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.ReportDokuei
{
	public interface IReportDokuei
	{
		/// <summary>
		/// 患者属性部
		/// </summary>
		ReportDokueiAggregate ReportDokuei
		{
			get;
			set;
		}
	}
}
