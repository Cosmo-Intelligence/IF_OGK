using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net.Sockets;
using System.Data;
using System.IO;

namespace RISCommonLibrary.Lib.Utils
{
    public static class MiscUtils
    {

		/// <summary>
		/// Commandオブジェクトの中身をログに出力する
		/// </summary>
		/// <param name="cmd">コマンドオブジェクト</param>
		/// <param name="log">log4net.ILogのインスタンス</param>
		public static void WriteDbCommandLogForLog4net(IDbCommand cmd, log4net.ILog log)
		{
			log.Debug(cmd.CommandText);

			for (int idxParam = 0; idxParam < cmd.Parameters.Count; idxParam++)
			{
				IDataParameter item = cmd.Parameters[idxParam] as IDataParameter;
				log.DebugFormat("Index:{0},Direction:{1},IsNullable:{2},Name:{3},Type:{4},Value:{5}",
						idxParam, item.Direction, item.IsNullable, item.ParameterName, item.DbType, item.Value);
			}
		}

		/// <summary>
		/// readerオブジェクトの現在行の中身をログに出力する
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="log"></param>
		public static void WriteDataReaderLogForLog4net(IDataReader reader, log4net.ILog log)
		{
			log.Debug("リーダオブジェクトをログ出力します");
			if (reader == null)
			{
				return;
			}
			if (reader.IsClosed)
			{
				return;
			}
			for (int fieldIndex = 0; fieldIndex < reader.FieldCount; fieldIndex++)
			{
				log.DebugFormat("Index:{0}, Name:{1}, FieldType:{2}, IsDBNull:{3}, Value:{4}",
						fieldIndex, reader.GetName(fieldIndex), reader.GetFieldType(fieldIndex), reader.IsDBNull(fieldIndex), reader[fieldIndex]);
			}
		}

		/// <summary>
        /// ジェネリック型の最初の型引数クラス名を取得する
        /// </summary>
        /// <param name="t"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static string GetGenericArgumentsClassNameFirst(Type t, string def)
        {
            foreach (Type arg in t.GetGenericArguments())
            {
                return arg.Name;
            }
            return def;
        }

        /// <summary>
        /// 秒をあらわす文字列からミリ秒の数値を返す
        /// </summary>
        /// <param name="secondString"></param>
        /// <returns></returns>
        public static int SecondStringToMillisecond(string secondString)
        {
            return MiscUtils.SecondStringToMillisecond(secondString, 0);
        }

        /// <summary>
        /// 秒をあらわす文字列からミリ秒の数値を返す
        /// </summary>
        /// <param name="secondString"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static int SecondStringToMillisecond(string secondString, int def)
        {
            int second;
            if (!int.TryParse(secondString, out second))
            {
                return def;
            }

            return MiscUtils.SecondToMillisecond(second);
        }

        /// <summary>
        /// 秒からミリ秒の数値を返す
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int SecondToMillisecond(int second)
        {
            return second * 1000;
        }

		/// <summary>
		/// エラーメッセージ生成
		/// </summary>
		/// <param name="ex"></param>
		/// <param name="sb"></param>
		/// <returns></returns>
		public static String BuildErrMessage(Exception ex, StringBuilder sb)
		{
			StringBuilder sbLocal = sb;
			if (sbLocal == null)
			{
				sbLocal = new StringBuilder();
			}
			if (ex is SocketException)
			{
				SocketException socketEx = ex as SocketException;
				if (socketEx != null)
				{
					const string ERR_SOCKET_FORMAT = "{0}, ErrorCode={1}, SocketErrorCode={2}";
					sbLocal.AppendFormat(ERR_SOCKET_FORMAT, socketEx.ToString(),
						socketEx.ErrorCode, socketEx.SocketErrorCode);
					return sbLocal.ToString();
				}
			}
			const string ERR_FORMAT = "{0}";
			sbLocal.AppendFormat(ERR_FORMAT, ex.ToString());
			if (ex.InnerException != null)
			{
				sbLocal.AppendFormat(" Inner:{0}", BuildErrMessage(ex.InnerException, sbLocal));
			}
			return sbLocal.ToString();
		}

        /// <summary>
        /// XElementからDateTimeを取り出す
        /// </summary>
        /// <param name="targetElementName"></param>
        /// <param name="parentElement"></param>
        /// <returns>
        /// 指定されたエレメントが見つからない:null
        /// 指定されたエレメントの値が空:null
        /// 指定されたエレメントの値がDateTimeにパースできない:null
        /// 指定されたエレメントの値をDateTimeにパースできる:指定されたエレメントの値をDateTime変換
        /// </returns>
        public static Nullable<DateTime> GetDateTimeFromXElement(string targetElementName, XElement parentElement)
        {
            XElement targetElement = parentElement.Element(targetElementName);
            if (targetElement == null)
            {
                return null;
            }
            if (targetElement.Value == "")
            {
                return null;
            }
            DateTime dt;
            if (!DateTime.TryParse(targetElement.Value, out dt))
            {
                return null;
            }
            return dt;
        }

        /// <summary>
        /// DateTime日付をXElementに設定する
        /// </summary>
        /// <param name="setDate"></param>
        /// <param name="targetElementName"></param>
        /// <param name="parentElement"></param>
        public static void SetDateTimeToXElement(Nullable<DateTime> setDate, string targetElementName, XElement parentElement)
        {
            XElement targetElement = parentElement.Element(targetElementName);
            if (targetElement == null)
            {
                targetElement = new XElement(targetElementName);
                parentElement.Add(targetElement);
            }
            if (!setDate.HasValue)
            {
                targetElement.Value = "";
                return;
            }
            const string FORMAT_UPDATE_DATE = "yyy/MM/dd";
            targetElement.Value = setDate.Value.ToString(FORMAT_UPDATE_DATE);
        }

		public static String GetCommaTextWithQuote(IEnumerable<XElement> xElements)
		{
			StringBuilder commaTextWithQuote = new StringBuilder();
			foreach (XElement xElement in xElements)
			{
				if (commaTextWithQuote.Length != 0)
				{
					commaTextWithQuote.Append(",");
				}
				commaTextWithQuote.AppendFormat("'{0}'", xElement.Value);
			}
			return commaTextWithQuote.ToString();
		}

		/// <summary>
        /// DataTableに格納されている値が同じか？
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns></returns>
        /// <remarks>DBNullと""とnullを同一視する</remarks>
        public static bool SameDataTableValue(DataTable src, DataTable dst)
        {
            DataRowCollection srcRows = src.Rows;
            DataRowCollection dstRows = dst.Rows;
            DataColumnCollection srcCols = src.Columns;
            DataColumnCollection dstCols = dst.Columns;
            if (srcRows.Count != dstRows.Count)
            {
                return false;
            }
            if (srcCols.Count != dstCols.Count)
            {
                return false;
            }
            for (int idxRow = 0; idxRow < srcRows.Count; idxRow++)
            {
                for (int idxCol = 0; idxCol < srcCols.Count; idxCol++)
                {
                    if (Convert.ToString(srcRows[idxRow][idxCol]) == Convert.ToString(dstRows[idxRow][idxCol]))
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// DataRowのデータを入れ替える
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void SwapDataRow(DataRow src, DataRow dst)
        {
            object[] rowArrayDst = dst.ItemArray;
            dst.ItemArray = src.ItemArray;
            src.ItemArray = rowArrayDst;
        }

        /// <summary>
        /// データ行を移動する
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fromIndex"></param>
        /// <param name="toIndex"></param>
        public static void MoveRowOnTopForDatatable(DataTable dt, int fromIndex, int toIndex)
        {

            if (dt.Rows.Count < fromIndex)
            {
                return;
            }
            if (dt.Rows.Count < toIndex)
            {
                return;
            }
            DataRow fromDataRow = dt.Rows[fromIndex];
            DataRow fromDataRowCopy = dt.NewRow();
            fromDataRowCopy.ItemArray = fromDataRow.ItemArray;
            dt.Rows.InsertAt(fromDataRowCopy, toIndex);
            dt.Rows.Remove(fromDataRow);
            fromDataRowCopy.AcceptChanges();
            fromDataRowCopy.SetModified();
        }

        /// <summary>
        /// データ行を移動する
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fromDataRow"></param>
        /// <param name="toIndex"></param>
        public static void MoveRowOnTopForDatatable(DataTable dt, DataRow fromDataRow, int toIndex)
        {

            if (dt.Rows.Count < toIndex)
            {
                return;
            }

            DataRow fromDataRowCopy = dt.NewRow();
            fromDataRowCopy.ItemArray = fromDataRow.ItemArray;
            dt.Rows.InsertAt(fromDataRowCopy, toIndex);
            dt.Rows.Remove(fromDataRow);
            fromDataRowCopy.AcceptChanges();
            fromDataRowCopy.SetModified();
        }

		/// <summary>
		/// Convert a path into a fully qualified local file path.
		/// </summary>
		/// <param name="path">The path to convert.</param>
		/// <returns>The fully qualified path.</returns>
		/// <remarks>
		/// <para>
		/// Converts the path specified to a fully
		/// qualified path. If the path is relative it is
		/// taken as relative from the application base 
		/// directory.
		/// </para>
		/// <para>
		/// The path specified must be a local file path, a URI is not supported.
		/// </para>
		/// log4netから拝借
		/// </remarks>
		public static string ConvertToFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}

			string baseDirectory = "";
			try
			{
				string applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				if (applicationBaseDirectory != null)
				{
					// applicationBaseDirectory may be a URI not a local file path
					Uri applicationBaseDirectoryUri = new Uri(applicationBaseDirectory);
					if (applicationBaseDirectoryUri.IsFile)
					{
						baseDirectory = applicationBaseDirectoryUri.LocalPath;
					}
				}
			}
			catch
			{
				// Ignore URI exceptions & SecurityExceptions from SystemInfo.ApplicationBaseDirectory
			}

			if (baseDirectory != null && baseDirectory.Length > 0)
			{
				// Note that Path.Combine will return the second path if it is rooted
				return Path.GetFullPath(Path.Combine(baseDirectory, path));
			}
			return Path.GetFullPath(path);
		}

    }
}
