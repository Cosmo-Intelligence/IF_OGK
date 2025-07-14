using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARISSend.Model.MessageCreator.ExamDetail
{
	internal class OrderCommentData
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
		/// コメント連番
		/// </summary>
		public string COMMENT_SEQ
		{
			get;
			set;
		}

		/// <summary>
		/// コメント区分
		/// </summary>
		public string COMMENT_KBN
		{
			get;
			set;
		}

		/// <summary>
		/// コメントID
		/// </summary>
		public string COMMENT_ID
		{
			get;
			set;
		}

	}
}
