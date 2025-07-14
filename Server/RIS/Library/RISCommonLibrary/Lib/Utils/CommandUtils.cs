using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RISCommonLibrary.Lib.Utils
{
	/// <summary>
	/// Commandユーティリティ
	/// </summary>
	public static class CommandUtils
	{
		/// <summary>
		/// コマンドの共通初期化
		/// </summary>
		/// <param name="command"></param>
		public static void InitCommand(this IDbCommand command, int commandTimeout)
		{
			command.CommandType = CommandType.Text;
			command.CommandTimeout = commandTimeout;
		}

		/// <summary>
		/// 文字列値をコマンドに設定する
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="value"></param>
		/// <param name="command"></param>
		public static void SetStringToCommand(this IDbCommand command, string paramName, string value)
		{
			IDataParameter param = command.CreateParameter();
			param.SetInputString(paramName, value);
			command.Parameters.Add(param);
		}

		/// <summary>
		/// 数値をコマンドに設定する
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="value"></param>
		/// <param name="command"></param>
		public static void SetIntToCommand(this IDbCommand command, string paramName, string value)
		{
			IDataParameter param = command.CreateParameter();
			param.SetInputInt32FromString(paramName, value);
			command.Parameters.Add(param);
		}

		/// <summary>
		/// Decimalをコマンドに設定する
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="value"></param>
		/// <param name="command"></param>
		public static void SetDecimalToCommand(this IDbCommand command, string paramName, string value)
		{
			IDataParameter param = command.CreateParameter();
			param.SetInputDecimalFromString(paramName, value);
			command.Parameters.Add(param);
		}

		/// <summary>
		/// 日時をコマンドに設定する
		/// </summary>
		/// <param name="paramName"></param>
		/// <param name="value"></param>
		/// <param name="command"></param>
		public static void SetDateTimeToCommand(this IDbCommand command, string paramName, string value)
		{
			IDataParameter param = command.CreateParameter();
			param.SetInputDateTimeFromString(paramName, value);
			command.Parameters.Add(param);
		}

	}
}
