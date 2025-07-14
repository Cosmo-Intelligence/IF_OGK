using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.Exam
{
	public interface IExam
	{
		/// <summary>
		/// 実施情報部
		/// </summary>
		ExamAggregate Exam
		{
			get;
			set;
		}
	}
}
