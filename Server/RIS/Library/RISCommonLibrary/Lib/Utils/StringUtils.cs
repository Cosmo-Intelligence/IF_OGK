using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RISCommonLibrary.Lib.Utils
{
    public static class StringUtils
    {

		/// <summary>
		/// 固定長文字列(ex:整数部3桁+小数部2桁)からDecimal形式の文字列を返す
		/// </summary>
		/// <param name="fixedString"></param>
		/// <param name="indexPoint">小数点位置</param>
		/// <returns></returns>
		/// <remarks>マルチバイトには非対応</remarks>
		public static String GetDecimalStringByFixedString(String fixedString, Int32 indexPoint)
		{
			if (String.IsNullOrEmpty(fixedString))
			{
				return "";
			}

			//小数点
			const String POINT = ".";
			if (fixedString.Length -1 < indexPoint)
			{
				return fixedString;
			}
			const String ZERO_POINT = "0" + POINT;
			if (indexPoint < 1)
			{
				return ZERO_POINT + fixedString;
			}
			return fixedString.Insert(indexPoint, POINT);
		}
		
        /// <summary>
        /// 文字列数値項目を数値フォーマットを利用して表示する
        /// </summary>
        /// <param name="src"></param>
        /// <param name="numberFormat"></param>
        /// <returns></returns>
		public static String FormatStringLikeNumber(this String src, String numberFormat)
        {
            if (String.IsNullOrEmpty(src))
            {
                return String.Empty;
            }
            Int32 srcInt;
            if (!Int32.TryParse(src, out srcInt))
            {
                return String.Empty;
            }
            return srcInt.ToString(numberFormat);
        }

        /// <summary>
        /// 文字列を10進数の文字コードで表示する
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
		public static String StringToDecCode(this String src)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char item in src)
            {
                sb.Append((int)item);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 文字列を16進数の文字コードで表示する
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
		public static String StringToHexCode(this String src)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char item in src)
            {
                sb.AppendFormat("{0:X2}", (int)item);
            }
            return sb.ToString();
        }

		public static String SpaceSplit(this String pMsg)
        {
            pMsg = pMsg.Replace('　', ' ');
            return pMsg.Replace(' ', '^');
        }

        /// <summary>
        /// クォーテーションで囲う
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
		public static String SetQuote(this String src)
        {
			const String FORMAT_QUOTE = "'{0}'";
			return String.Format(FORMAT_QUOTE, src);
        }

        /// <summary>
        /// ''で囲われていないカンマ区切りの文字列をそれぞれ''で囲んで返す
        /// </summary>
        /// <param name="src">''で囲われていないカンマ区切りの文字列</param>
        /// <param name="addedString">追加対象文字列</param>
        /// <returns>''で囲われたカンマ区切りの文字列</returns>
		public static String AddCommaStrings(String src, String addedString)
        {
			if (String.IsNullOrEmpty(src))
            {
				return String.Empty;
            }
			const String FORMAT_TEXT = "'{0}'";
            const char DELIMITER = ',';
			String[] ccdAry = src.Split(DELIMITER);
            StringBuilder ccdStringBuilder = new StringBuilder(addedString);
			Array.ForEach<String>(ccdAry, delegate(String item)
            {
                if (ccdStringBuilder.Length > 0)
                {
                    ccdStringBuilder.Append(DELIMITER);
                }
                ccdStringBuilder.AppendFormat(FORMAT_TEXT, item);
            }
            );
            return ccdStringBuilder.ToString();
        }

		/// <summary>
		/// 文字列がInt32に変換できるかチェックする
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsStrInt32(this String s)
		{
			Int32 outInt;
			return Int32.TryParse(s, out outInt);
		}

		/// <summary>
		/// 文字列がUInt32に変換できるかチェックする
		/// </summary>
		/// <returns></returns>
		public static bool IsStrUInt32(this String s)
		{
			UInt32 outInt;
			return UInt32.TryParse(s, out outInt);
		}

		/// <summary>
		/// String→String変換
		/// </summary>
		/// <remarks>Nullの場合はデフォルト値を返す</remarks>
		public static String StringToString(this String s, String def)
		{
			if (String.IsNullOrEmpty(s))
			{
				return def;
			}
			return s;
		}

		/// <summary>
		/// String→String変換
		/// </summary>
		public static String StringToString(this String s)
		{
			return StringToString(s, String.Empty);
		}

		/// <summary>
		/// String→Int変換
		/// </summary>
		public static Int32 StringToInt32(this String s, Int32 def)
		{
			if (String.IsNullOrEmpty(s))
			{
				return def;
			}
			Int32 _Int;
			if (!Int32.TryParse(s, out _Int))
			{
				return def;
			}
			return _Int;
		}

		/// <summary>
		/// String→Int変換
		/// </summary>
		public static Int32 StringToInt32(this String s)
		{
			return StringToInt32(s, 0);
		}

		/// <summary>
		/// String→Decimal変換
		/// </summary>
		public static Decimal StringToDecimal(this String s, Decimal def)
		{
			if (String.IsNullOrEmpty(s))
			{
				return def;
			}
			Decimal _Decimal;
			if (!Decimal.TryParse(s, out _Decimal))
			{
				return def;
			}
			return _Decimal;
		}

		/// <summary>
		/// String→Decimal変換
		/// </summary>
		public static Decimal StringToDecimal(this String s)
		{
			return StringToDecimal(s, 0M);
		}

		/// <summary>
		/// 文字列を浮動小数点数値にする
		/// </summary>
		/// <param name="s"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		public static Double StringToDoubleDef(this String s, Double def)
		{
			if (String.IsNullOrEmpty(s))
			{
				return def;
			}
			Double value;
			if (!Double.TryParse(s, out value))
			{
				return def;
			}
			return value;
		}

		/// <summary>
		/// 文字列を浮動小数点数値にする
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static Double StringToDoubleDef(this String s)
		{
			return StringToDoubleDef(s, 0D);
		}

		/// <summary>
		/// String→Boolean変換
		/// </summary>
		public static Boolean StringToBoolean(this String s, String trueString)
		{
			if (String.IsNullOrEmpty(s))
			{
				return false;
			}
			return (s == trueString) ? true : false;
		}

		/// <summary>
        /// Boolean→String変換
        /// </summary>
		public static String BooleanToString(Boolean b, String trueString, String falseString)
        {
            return b ? trueString : falseString;
        }

        /// <summary>
        /// Boolean→Int変換
        /// </summary>
        public static Int32 BooleanToInt(Boolean b)
        {
            return b ? 1 : 0;
        }
    }
}
