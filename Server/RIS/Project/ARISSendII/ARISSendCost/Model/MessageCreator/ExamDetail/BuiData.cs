using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARISSend.Model.MessageCreator.ExamDetail
{
	internal class BuiData
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
		public string NO
		{
			get;
			set;
		}

		/// <summary>
		/// 部位ID
		/// </summary>
		public string BUI_ID
		{
			get;
			set;
		}

		/// <summary>
		/// 方向ID
		/// </summary>
		public string HOUKOU_ID
		{
			get;
			set;
		}

		/// <summary>
		/// 検査方法ID
		/// </summary>
		public string KENSAHOUHOU_ID
		{
			get;
			set;
		}

		/// <summary>
		/// 撮影進捗
		/// </summary>
		/// <remarks>
		/// 0：未　1：済　2：中止
		/// </remarks>
		public string SATUEISTATUS
		{
			get;
			set;
		}

		/// <summary>
		/// 部位分類ID
		/// </summary>
		public string BUIBUNRUI_ID
		{
			get;
			set;
		}

	}
}
