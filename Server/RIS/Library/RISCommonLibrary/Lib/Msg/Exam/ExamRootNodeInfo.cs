using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Exam
{
	/// <summary>
	/// 実施情報のノード定義
	/// </summary>
	class ExamRootNodeInfo
	{
		public static NodeInfo EXAM_ROOT = new NodeInfo("EXAM_ROOT", "実施情報", NodeTypeEnum.ntAggregate, -1);
	}
}
