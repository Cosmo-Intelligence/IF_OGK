using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.ConnectionOpen
{
	/// <summary>
	/// 回線接続要求のノード定義
	/// </summary>
	class ConnectionOpenNodeInfo
	{
		public static NodeInfo COMMUNICATION_OPEN_ROOT = new NodeInfo("COMMUNICATION_OPEN_ROOT", "回線接続要求", NodeTypeEnum.ntAggregate, -1);
	}
}
