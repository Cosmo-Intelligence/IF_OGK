using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using RISBizLibrary.Receive.Model;

namespace ARISReceive.Model
{
	class TcpClientHandlerHospital : TcpClientHandler
	{

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="tcpClient"></param>
		public TcpClientHandlerHospital(TcpClient tcpClient)
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
			return new MessageHandlerHospital();
		}


	}
}
