using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace RISCommonLibrary.Lib.Utils
{
	public static class DateTimeUtils
    {
		/// <summary>
		/// 日付型データを書式化する
		/// データがNULLの場合には指定された値を返す
		/// </summary>
		/// <param name="src">日付型データ</param>
		/// <param name="format">書式文字列</param>
		/// <param name="nullValue">日付がNULLの場合に返す文字列</param>
		/// <returns></returns>
		public static string GetDateTimeString(string srcDateTimeString, string format, string defaultValue)
		{
			DateTime srcDateTime;
			if (!DateTime.TryParse(srcDateTimeString, out srcDateTime))
			{
				return defaultValue;
			}
			return srcDateTime.ToString(format);
		}

		/// <summary>
		/// 日付型データを書式化する
		/// データがNULLの場合には指定された値を返す
		/// </summary>
		/// <param name="src">日付型データ</param>
		/// <param name="format">入力書式文字列</param>
		/// <param name="dstformat">出力書式文字列</param>
		/// <returns></returns>
		public static string GetDateTimeStringExact(string srcDateTimeString, string srcFormat,
			string dstFormat)
		{
			return GetDateTimeStringExact(srcDateTimeString, srcFormat, dstFormat, "");
		}

		/// 日付型データを書式化する
		/// データがNULLの場合には指定された値を返す
		/// </summary>
		/// <param name="src">日付型データ</param>
		/// <param name="format">入力書式文字列</param>
		/// <param name="dstformat">出力書式文字列</param>
		/// <param name="defaultValue">日付がNULLの場合に返す文字列</param>
		/// <returns></returns>
		public static string GetDateTimeStringExact(string srcDateTimeString, string srcFormat, 
			string dstFormat, string defaultValue)
		{
			DateTime srcDateTime;
			if (!DateTime.TryParseExact(srcDateTimeString, srcFormat, 
				System.Globalization.DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None,
					out srcDateTime))
			{
				return defaultValue;
			}
			return srcDateTime.ToString(dstFormat);
		}

		public static DateTime GetDateTimeDef(string srcDateTimeString, DateTime defaultValue)
        {
            DateTime srcDateTime;
            if (!DateTime.TryParse(srcDateTimeString, out srcDateTime))
            {
                return defaultValue;
            }
            return srcDateTime;
        }

        /// <summary>
        /// 文字列が指定のフォーマットの日時文字列かチェックする
        /// </summary>
        /// <returns></returns>
        public static bool IsStrDateTime(string format, string src)
        {
            DateTime srcDateTime;
            return DateTime.TryParseExact(src, format, null, DateTimeStyles.None, out srcDateTime);
        }

		/// <summary>
		/// 月初日を取得する拡張メソッド
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		/// <remarks>
		/// http://xlwahito.blogspot.com/2010/12/blog-post.html
		/// </remarks>
		public static DateTime BeginOfMonth(this DateTime source)
		{
			return new DateTime(source.Year, source.Month, 1);
		}

		/// <summary>
		/// 月末日を取得する拡張メソッド
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		/// <remarks>
		/// http://xlwahito.blogspot.com/2010/12/blog-post.html
		/// </remarks>
		public static DateTime EndOfMonth(this DateTime source)
		{
			var day = DateTime.DaysInMonth(source.Year, source.Month);
			return new DateTime(source.Year, source.Month, day);
		}
    }
}
