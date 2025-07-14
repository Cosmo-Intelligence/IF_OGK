using System;
using System.Data;

namespace RISCommonLibrary.Lib.Utils
{
	/// <summary>
	/// パラメータ補助
	/// </summary>
	public static class ParameterUtils
	{
		/// <summary>
		/// 入力文字列パラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetInputString(this IDataParameter param, String name, String value)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.String;
			param.ParameterName = name;
			param.Value = value;
		}

		/// <summary>
		/// 入力数値パラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetInputInt32(this IDataParameter param, String name, Int32 value)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.Int32;
			param.ParameterName = name;
			param.Value = value;
		}

		/// <summary>
		/// 入力数値パラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetInputInt32Nullable(this IDataParameter param, String name, Nullable<Int32> value)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.Int32;
			param.ParameterName = name;
			if (!value.HasValue)
			{
				param.Value = DBNull.Value;
				return;
			}
			param.Value = value;
		}

		/// <summary>
		/// 入力数値パラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetInputInt32FromString(this IDataParameter param, String name, String value)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.Int32;
			param.ParameterName = name;
			int dstValue;
			if (!Int32.TryParse(value, out dstValue))
			{
				param.Value = DBNull.Value;
				return;
			}
			param.Value = dstValue;
		}

		/// <summary>
		/// 入力Decimalパラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetInputDecimalFromString(this IDataParameter param, String name, String value)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.Decimal;
			param.ParameterName = name;
			decimal dstValue;
			if (!Decimal.TryParse(value, out dstValue))
			{
				param.Value = DBNull.Value;
				return;
			}
			param.Value = dstValue;
		}

		/// <summary>
		/// 入力64Bit数値パラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetInputInt64(this IDataParameter param, String name, Int64 value)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.Int64;
			param.ParameterName = name;
			param.Value = value;
		}

		/// <summary>
		/// 入力日時パラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetInputDateTime(this IDataParameter param, String name, DateTime value)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.DateTime;
			param.ParameterName = name;
			param.Value = value;
		}

		/// <summary>
		/// 入力日時パラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetInputDateTimeNullable(this IDataParameter param, String name, Nullable<DateTime> value)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.DateTime;
			param.ParameterName = name;
			if (!value.HasValue)
			{
				param.Value = DBNull.Value;
				return;
			}
			param.Value = value;
		}

		/// <summary>
		/// 入力日時パラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetInputDateTimeFromString(this IDataParameter param, String name, String value)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.DateTime;
			param.ParameterName = name;
			DateTime dstValue;
			if (!DateTime.TryParse(value, out dstValue))
			{
				param.Value = DBNull.Value;
				return;
			}
			param.Value = dstValue;
		}

		/// <summary>
		/// 入力数値パラメータNull設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		public static void SetInputInt32Null(this IDataParameter param, String name)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.Int32;
			param.ParameterName = name;
			param.Value = DBNull.Value;
		}

		/// <summary>
		/// 入力日時パラメータNull設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		public static void SetInputDateTimeNull(this IDataParameter param, String name)
		{
			param.Direction = ParameterDirection.Input;
			param.DbType = DbType.DateTime;
			param.ParameterName = name;
			param.Value = DBNull.Value;
		}
	}
}
