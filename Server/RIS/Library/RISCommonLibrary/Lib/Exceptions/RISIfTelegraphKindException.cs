using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Exceptions
{
	/// <summary>
	/// 電文種別異常応答
	/// </summary>
	/// <remarks>システム例外</remarks>
	public class RISIfTelegraphKindException : RISIfExceptionFatalException
	{
		/// <summary>
		/// 電文種別エラー
		/// </summary>
		public RISIfTelegraphKindException()
			: base()
		{

		}

		public RISIfTelegraphKindException(string message)
			: base(message)
		{


		}

		public RISIfTelegraphKindException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}
}
