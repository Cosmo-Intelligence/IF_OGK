using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Utils
{
	public static class StringBuilderUtils
	{
		/// <summary>
		/// StringBuilderに値を追加する。すでに値が設定されている場合はセパレータが挿入される
		/// </summary>
		/// <param name="sb"></param>
		/// <param name="addValue"></param>
		/// <param name="separator"></param>
		public static void AddWithSeparator(this StringBuilder sb, string addValue, string separator)
		{
			if (sb.Length > 0)
			{
				sb.Append(separator);
			}
			sb.Append(addValue);
		}

	}
}
