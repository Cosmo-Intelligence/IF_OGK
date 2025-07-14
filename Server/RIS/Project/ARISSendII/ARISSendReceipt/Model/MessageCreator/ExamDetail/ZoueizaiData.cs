using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARISSend.Model.MessageCreator.ExamDetail
{
	internal class ZoueizaiData
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
		/// 器材連番　->同一部位で使用した器材の連番
		/// </summary>
		public string NO
		{
			get;
			set;
		}

		/// <summary>
		/// 器材ID（PARTSMASTER.ID）
		/// </summary>
		public string PARTS_ID
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

		/// <summary>
		/// 医事数量単位
		/// </summary>
		public string ZOUEIZAIIJITANNI_ID
		{
			get;
			set;
		}

	}
}
