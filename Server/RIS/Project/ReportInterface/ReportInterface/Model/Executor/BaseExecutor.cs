using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportInterface.Model.Executor
{
	internal class BaseExecutor
	{
		#region field

		/// <summary>
		/// ログ
		/// </summary>
		protected static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#endregion
	}
}
