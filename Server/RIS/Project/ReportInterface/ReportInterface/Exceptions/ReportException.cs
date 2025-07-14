using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Exceptions;

namespace ReportInterface.Exceptions
{
	internal class ReportException : RISIfExceptionUserException
	{
		public ReportException()
			: base()
		{

		}

		public ReportException(string message)
			: base(message)
		{


		}

		public ReportException(string message, Exception inner)
			: base(message, inner)
		{

		}

	}

	/// <summary>
	/// Config設定が
	/// </summary>
	internal class ConfigException : ReportException
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


	internal class ReportDataNotFoundException : ReportException
	{
		public ReportDataNotFoundException()
			: base()
		{

		}

		public ReportDataNotFoundException(string message)
			: base(message)
		{


		}

		public ReportDataNotFoundException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}

	/// <summary>
	/// 送信時に未定義のRequestTypeがあった
	/// </summary>
	internal class ReportRequestTypeNotDefineException : ReportException
	{
		public ReportRequestTypeNotDefineException()
			: base()
		{

		}

		public ReportRequestTypeNotDefineException(string message)
			: base(message)
		{


		}

		public ReportRequestTypeNotDefineException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}

	#region エラーにせず警告にする
	
	/// <summary>
	/// エラーにせず警告にする
	/// </summary>
	/// <remarks>
	/// 更新時、ステータスが確定、確定待ち
	/// </remarks>
	internal class ReportWarningException : ReportException
	{
		public ReportWarningException()
			: base()
		{

		}

		public ReportWarningException(string message)
			: base(message)
		{


		}

		public ReportWarningException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}


	/// <summary>
	/// 更新時、ステータスが確定、確定待ち
	/// </summary>
	/// <remarks>
	/// 
	/// </remarks>
	internal class ReportStatusException : ReportWarningException
	{
		public ReportStatusException()
			: base()
		{

		}

		public ReportStatusException(string message)
			: base(message)
		{


		}

		public ReportStatusException(string message, Exception inner)
			: base(message, inner)
		{

		}
	}
	#endregion

}
