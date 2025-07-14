using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using RISBizLibrary.Receive.Model;

namespace ARISReceive.Model
{
	class TcpListenerHandlerOrder : TcpListenerHandler
	{
		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TcpListenerHandlerOrder() :base()
		{
		}
		#endregion

		/// <summary>
		/// ソケットクライアント作成
		/// </summary>
		/// <returns></returns>
		protected override TcpClientHandler CreateTcpClientHandler(TcpClient client)
		{
			return new TcpClientHandlerOrder(client);
		}

	}
}
