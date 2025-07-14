using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.ReportDokuei
{
	/// <summary>
	/// 既読情報のノード定義
	/// </summary>
	class ReportDokueiRootNodeInfo
	{
		public static NodeInfo REPORTDOKUEI_ROOT = new NodeInfo("REPORTDOKUEI_ROOT", "既読情報", NodeTypeEnum.ntAggregate, -1);
	}
}
