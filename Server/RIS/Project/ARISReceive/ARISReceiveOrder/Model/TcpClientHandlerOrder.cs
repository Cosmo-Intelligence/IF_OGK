using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using RISBizLibrary.Receive.Model;

namespace ARISReceive.Model
{
	class TcpClientHandlerOrder : TcpClientHandler
	{
		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="tcpClient"></param>
		public TcpClientHandlerOrder(TcpClient tcpClient)
			: base(tcpClient)
		{

		}

		#endregion
		/// <summary>
		/// メッセージハンドラ生成
		/// </summary>
		/// <returns></returns>
		protected override MessageHandler CreateMessageHandler()
		{
			return new MessageHandlerOrder();
		}

	}
}
