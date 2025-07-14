using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReportInterface.Data;
using System.Data;

namespace ReportInterface.Model.Executor
{
	interface IExecutor
	{
		void Execute(ToReportInfo t, IDbConnection cn);
		bool CanExecute(string requestType);
	}
}
