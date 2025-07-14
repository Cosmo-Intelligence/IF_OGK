using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARISSend.Model.MessageCreator.ExamDetail
{
	internal class InfuseData
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
		/// 手技連番
		/// </summary>
		public string NO
		{
			get;
			set;
		}

		/// <summary>
		/// 手技ID
		/// </summary>
		public string INFUSE_ID
		{
			get;
			set;
		}

		/// <summary>
		/// 医事数量
		/// </summary>
		public string SUURYOU_IJI
		{
			get;
			set;
		}

		/// <summary>
		/// 器材区分ID
		/// </summary>
		public string PARTSBUNRUI_ID
		{
			get;
			set;
		}

	}
}
