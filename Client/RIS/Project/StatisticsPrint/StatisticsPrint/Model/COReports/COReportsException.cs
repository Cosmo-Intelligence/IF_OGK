using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsPrint.Model.COReports
{
	public class COReportsException : System.Exception
	{
		public COReportsException()
			: base()
		{

		}

		public COReportsException(string message)
			: base(message)
		{


		}

		public COReportsException(string message, Exception inner)
			: base(message, inner)
		{

		}

	}
}
