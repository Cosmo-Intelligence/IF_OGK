using System;
using System.Data;

namespace RISCommonLibrary.Lib.Utils
{
	public static class DataReaderUtils
    {
        /// <summary>
        /// IDataReaderからフィールド名で値を取得する
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetStringByDB(this IDataReader dr, string fieldName)
        {
            return GetStringByDB(dr, fieldName, string.Empty);
        }

        /// <summary>
        /// IDataReaderからフィールド名で値を取得する
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetStringByDB(this IDataReader dr, string fieldName, string defaultValue)
        {
            int idx = dr.GetOrdinal(fieldName);
            if (dr.IsDBNull(idx))
            {
                return defaultValue;
            }
            string name = dr.GetFieldType(idx).Name;
            if (string.Compare(name, "string", true) == 0)
            {
                return DataReaderUtils.GetStringByDBString(dr, fieldName, defaultValue);
            }
            if (string.Compare(name, "Int16", true) == 0)
            {
                return DataReaderUtils.GetStringByDBInt16(dr, fieldName, defaultValue);
            }
            if (string.Compare(name, "Int32", true) == 0)
            {
                return DataReaderUtils.GetStringByDBInt32(dr, fieldName, defaultValue);
            }
            if (string.Compare(name, "Decimal", true) == 0)
            {
                return DataReaderUtils.GetStringByDBDecimal(dr, fieldName, defaultValue);
            }
            if (string.Compare(name, "Double", true) == 0)
            {
                return DataReaderUtils.GetStringByDBDouble(dr, fieldName, defaultValue);
            }
            if (string.Compare(name, "Datetime", true) == 0)
            {
                return DataReaderUtils.GetStringByDBDatetime(dr, fieldName, defaultValue);
            }
            throw new NotImplementedException(string.Format("未実装の型が実行されました={0}", dr.GetDataTypeName(idx)));

        }

        /// <summary>
        /// IDataReaderからフィールド名で文字列方の値を取得する
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <returns>DBNullだった場合は、string.Emptyを返す</returns>
        public static string GetStringByDBString(this IDataReader dr, string fieldName)
        {
            return GetStringByDBString(dr, fieldName, string.Empty);
        }

        /// <summary>
        /// IDataReaderからフィールド名で文字列方の値を取得する
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="fieldName"></param>
        /// <param name="defaultValue">DBNullだった場合の返す値</param>
        /// <returns></returns>
        public static string GetStringByDBString(this IDataReader dr, string fieldName, string defaultValue)
        {
            int idx = dr.GetOrdinal(fieldName);
            if (dr.IsDBNull(idx))
            {
                return defaultValue;
            }
            return dr.GetString(idx);
        }

        public static string GetStringByDBInt32(this IDataReader dr, string fieldName)
        {
            return GetStringByDBInt32(dr, fieldName, string.Empty);
        }

        public static string GetStringByDBInt32(this IDataReader dr, string fieldName, string defaultValue)
        {
            int idx = dr.GetOrdinal(fieldName);
            if (dr.IsDBNull(idx))
            {
                return defaultValue;
            }
            return Convert.ToString(dr.GetInt32(idx));
        }

        public static string GetStringByDBInt16(this IDataReader dr, string fieldName)
        {
            return GetStringByDBInt16(dr, fieldName, string.Empty);
        }

        public static string GetStringByDBInt16(this IDataReader dr, string fieldName, string defaultValue)
        {
            int idx = dr.GetOrdinal(fieldName);
            if (dr.IsDBNull(idx))
            {
                return defaultValue;
            }
            return Convert.ToString(dr.GetInt16(idx));
        }

        public static string GetStringByDBDecimal(this IDataReader dr, string fieldName)
        {
            return GetStringByDBDecimal(dr, fieldName, string.Empty);
        }

        public static string GetStringByDBDecimal(this IDataReader dr, string fieldName, string defaultValue)
        {
            int idx = dr.GetOrdinal(fieldName);
            if (dr.IsDBNull(idx))
            {
                return defaultValue;
            }
            return Convert.ToString(dr.GetDecimal(idx));
        }

        public static string GetStringByDBDouble(this IDataReader dr, string fieldName)
        {
            return GetStringByDBDouble(dr, fieldName, string.Empty);
        }

        public static string GetStringByDBDouble(this IDataReader dr, string fieldName, string defaultValue)
        {
            int idx = dr.GetOrdinal(fieldName);
            if (dr.IsDBNull(idx))
            {
                return defaultValue;
            }
            return Convert.ToString(dr.GetDouble(idx));
        }

        public static string GetStringByDBDatetime(this IDataReader dr, string fieldName)
        {
            return GetStringByDBDatetime(dr, fieldName, string.Empty);
        }

        public static string GetStringByDBDatetime(this IDataReader dr, string fieldName, string defaultValue)
        {
            const string FORMAT_DATE_TIME = "yyyy/MM/dd HH:mm:ss";
            return GetStringByDBDatetime(dr, fieldName, FORMAT_DATE_TIME, string.Empty);
        }

        public static string GetStringByDBDatetime(this IDataReader dr, string fieldName, string format, string defaultValue)
        {
            int idx = dr.GetOrdinal(fieldName);
            if (dr.IsDBNull(idx))
            {
                return defaultValue;
            }
            return dr.GetDateTime(idx).ToString(format);
        }
    }
}
