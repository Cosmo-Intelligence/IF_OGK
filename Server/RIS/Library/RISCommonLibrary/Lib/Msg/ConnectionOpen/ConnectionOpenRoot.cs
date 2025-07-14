using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;

namespace RISCommonLibrary.Lib.Msg.ConnectionOpen
{
	/// <summary>
	/// 接続要求メッセージルート
	/// </summary>
	public class ConnectionOpenRoot : CommonRootNode
	{
		#region field
		
		#endregion

		#region property

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ConnectionOpenRoot()
			: base(ConnectionOpenNodeInfo.COMMUNICATION_OPEN_ROOT)
		{
		}
		#endregion

	}
}
