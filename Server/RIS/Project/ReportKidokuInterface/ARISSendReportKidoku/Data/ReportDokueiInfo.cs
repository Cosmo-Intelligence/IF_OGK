using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARISSend.Data
{
	public class ReportDokueiInfo
	{
		/// <summary>
		/// 所見ID
		/// </summary>
		public string Id
		{
			get;
			set;
		}

		/// <summary>
		/// 履歴番号
		/// </summary>
		public string Revision
		{
			get;
			set;
		}

		/// <summary>
		/// システム日時
		/// </summary>
		public string TranferDate
		{
			get;
			set;
		}

		/// <summary>
		/// 【電】エラーステータス
		/// </summary>
		public string Transfertext
		{
			get;
			set;
		}

		/// <summary>
		/// ログ出力用文字列取得
		/// </summary>
		/// <returns></returns>
		public String ToStringForLog()
		{
			const String FORMAT_LOG = "ID={0}, REVISION={1}, TRANFERDATE={2}, TRANSFERTEXT={3}";
			return String.Format(FORMAT_LOG, Id, Revision, TranferDate, Transfertext);
		}


	}
}
