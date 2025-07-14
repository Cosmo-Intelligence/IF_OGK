using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RISCommonLibrary.Lib.Msg.Common;

namespace RISCommonLibrary.Lib.Msg.Response
{
	/// <summary>
	/// 応答電文メッセージルート
	/// </summary>
	public class ResponseRoot : CommonRootNode
	{
		#region field
		
		#endregion

		#region property

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ResponseRoot()
			: base(ResponseRootNodeInfo.RESPONSE_ROOT)
		{
		}
		#endregion
	}
}
