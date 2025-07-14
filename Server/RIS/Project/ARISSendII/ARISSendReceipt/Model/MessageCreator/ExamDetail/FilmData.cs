using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARISSend.Model.MessageCreator.ExamDetail
{
	internal class FilmData
	{
		/// <summary>
		/// RIS識別ID
		/// </summary>
		public string RIS_ID
		{
			get;
			set;
		}

		/// <summary>
		/// 部位情報連番
		/// </summary>
		public string BUI_NO
		{
			get;
			set;
		}

		/// <summary>
		/// フィルム連番
		/// </summary>
		public string NO
		{
			get;
			set;
		}

		/// <summary>
		/// フィルムID
		/// </summary>
		public string FILM_ID
		{
			get;
			set;
		}

		/// <summary>
		/// 枚数
		/// </summary>
		public string USED
		{
			get;
			set;
		}

		/// <summary>
		/// ロス
		/// </summary>
		public string LOSS
		{
			get;
			set;
		}

		/// <summary>
		/// 分割　CR,DRのみ
		/// </summary>
		public string PARTITION
		{
			get;
			set;
		}

	}
}
