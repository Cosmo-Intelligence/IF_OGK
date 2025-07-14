using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Receipt
{
	/// <summary>
	/// 入退院情報のノード定義
	/// </summary>
	class ReceiptRootNodeInfo
	{
		public static NodeInfo RECEIPT_ROOT = new NodeInfo("RECEIPT_ROOT", "受付（進捗）情報", NodeTypeEnum.ntAggregate, -1);
	}
}
