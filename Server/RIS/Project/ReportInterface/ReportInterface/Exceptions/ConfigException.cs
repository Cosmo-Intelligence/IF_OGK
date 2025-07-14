using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Exceptions;

namespace ReportInterface.Exceptions
{
	/// <summary>
	/// Config設定が
	/// </summary>
	internal class ConfigException : RISIfExceptionUserException
	{
		public ConfigException()
			: base()
		{

		}

		public ConfigException(string message)
			: base(message)
		{


		}

		public ConfigException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}
}
