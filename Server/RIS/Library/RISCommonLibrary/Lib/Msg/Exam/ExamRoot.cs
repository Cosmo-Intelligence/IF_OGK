using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Msg.Common.Exam;

namespace RISCommonLibrary.Lib.Msg.Exam
{
	/// <summary>
	/// 実施情報メッセージルート
	/// </summary>
	public class ExamRoot : CommonRootNode, IExam
	{
		#region field
		
		#endregion

		#region property

		/// <summary>
		/// 実施情報部
		/// </summary>
		public ExamAggregate Exam
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ExamRoot()
			: base(ExamRootNodeInfo.EXAM_ROOT)
		{

			Exam = new ExamAggregate();
			Add(Exam);
		}
		#endregion

	}
}
