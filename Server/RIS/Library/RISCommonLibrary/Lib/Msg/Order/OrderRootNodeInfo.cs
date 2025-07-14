using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Order
{
	/// <summary>
	/// オーダ情報のノード定義
	/// </summary>
	class OrderRootNodeInfo
	{
		public static NodeInfo ORDER_ROOT = new NodeInfo("ORDER_ROOT", "オーダ情報", NodeTypeEnum.ntAggregate, -1);
	}
}
