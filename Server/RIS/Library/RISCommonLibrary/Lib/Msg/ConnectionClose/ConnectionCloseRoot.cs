using RISCommonLibrary.Lib.Msg.Common;

namespace RISCommonLibrary.Lib.Msg.ConnectionClose
{
	/// <summary>
	/// 接続要求メッセージルート
	/// </summary>
	public class ConnectionCloseRoot : CommonRootNode
	{
		#region field
		
		#endregion

		#region property

		#endregion

		#region コンストラクタ
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ConnectionCloseRoot()
			: base(ConnectionCloseNodeInfo.COMMUNICATION_CLOSE_ROOT)
		{
		}
		#endregion

	}
}
