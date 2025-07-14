using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.ConnectionClose
{
	/// <summary>
	/// 回線接続要求のノード定義
	/// </summary>
	class ConnectionCloseNodeInfo
	{
		public static NodeInfo COMMUNICATION_CLOSE_ROOT = new NodeInfo("COMMUNICATION_CLOSE_ROOT", "回線切断要求", NodeTypeEnum.ntAggregate, -1);
	}
}
