using System;
using System.Diagnostics;
using System.Data;

namespace RISCommonLibrary.Lib.Debugger
{
    public static class QueryDebugger
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        public static void WriteConsole(IDbCommand cmd)
        {
            Console.WriteLine(cmd.CommandText);

            Console.WriteLine(string.Format("パラメータカウント:{0}" , cmd.Parameters.Count.ToString()));
            for (int idxParam = 0; idxParam < cmd.Parameters.Count; idxParam++)
            {
                IDataParameter item = cmd.Parameters[idxParam] as IDataParameter;
                Console.WriteLine(string.Format("Index:{0},Direction:{1},IsNullable:{2},Name:{3},Type:{4},Value:{5}",
                        idxParam, item.Direction, item.IsNullable, item.ParameterName, item.DbType, item.Value));
            }
        }

		/// <summary>
		/// readerオブジェクトの現在行の中身をログに出力する
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="log"></param>
		public static void WriteDataReaderLogForLog4net(IDataReader reader)
		{
			Console.WriteLine("リーダオブジェクトをログ出力します");
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
				Console.WriteLine(string.Format("Index:{0}, Name:{1}, FieldType:{2}, IsDBNull:{3}, Value:{4}",
						fieldIndex, reader.GetName(fieldIndex), reader.GetFieldType(fieldIndex), reader.IsDBNull(fieldIndex), reader[fieldIndex]));
			}
		}

    }
}
