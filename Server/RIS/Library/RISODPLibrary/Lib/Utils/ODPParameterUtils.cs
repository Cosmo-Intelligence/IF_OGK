using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace RISODPLibrary.Lib.Utils
{
	/// <summary>
	/// ODP用パラメータ補助
	/// </summary>
	public static class ODPParameterUtils
	{
		/// <summary>
		/// 入力文字列パラメータ設定
		/// </summary>
		/// <param name="param"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetOututRefCursor(this OracleParameter param, String name)
		{
			param.ParameterName = name;
			param.OracleDbType = OracleDbType.RefCursor;
			param.Direction = ParameterDirection.Output;
		}


	}
}
