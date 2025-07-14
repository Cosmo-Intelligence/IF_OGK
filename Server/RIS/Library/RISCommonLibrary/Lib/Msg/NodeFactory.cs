using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg
{
	class NodeFactory
	{
		public static BaseNode CreateNode(NodeInfo define)
		{

			if (define == null)
			{
				return null;
			}
			if (define.NodeType == NodeTypeEnum.ntData)
			{
				return new DataNode(define);
			}
			if (define.NodeType == NodeTypeEnum.ntArray)
			{
				return new ArrayNode(define);
			}
			if (define.NodeType == NodeTypeEnum.ntAggregate)
			{
				return new AggregateNode(define);
			}
			return null;
		}
	}
}
