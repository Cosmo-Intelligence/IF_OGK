using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg
{
	class MsgFormat
	{
		private List<NodeInfo> _NodeInfos = new List<NodeInfo>();

		public MsgFormat(NodeInfo[] msgInfo)
		{
			_NodeInfos.AddRange(msgInfo);
		}
	}
}
