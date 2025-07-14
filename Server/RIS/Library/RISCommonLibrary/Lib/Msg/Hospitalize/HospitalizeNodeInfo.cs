using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Hospitalize
{
	/// <summary>
	/// 入退院情報のノード定義
	/// </summary>
	class HospitalizeNodeInfo
	{
		public static NodeInfo HOSPITALIZE_ROOT = new NodeInfo("HOSPITALIZE_ROOT", "入退院情報", NodeTypeEnum.ntAggregate, -1);
	}
}
