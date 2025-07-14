using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Response
{
	/// <summary>
	/// 応答電文のノード定義
	/// </summary>
	class ResponseRootNodeInfo
	{
		public static NodeInfo RESPONSE_ROOT = new NodeInfo("RESPONSE_ROOT", "応答", NodeTypeEnum.ntAggregate, -1);
	}
}
