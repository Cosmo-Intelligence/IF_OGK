using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using RISBizLibrary.Receive.Model;

namespace ARISReceive.Model
{
	public class TcpListenerHandlerHospital : TcpListenerHandler
	{
		#region コンストラクタ

		/// <summary>
		/// 
		/// </summary>
		public TcpListenerHandlerHospital(): base()
		{

		}

		#endregion

		#region method

		/// <summary>
		/// ソケットクライアント作成
		/// </summary>
		/// <returns></returns>
		protected override TcpClientHandler CreateTcpClientHandler(TcpClient client)
		{
			return new TcpClientHandlerHospital(client);
		}

		#endregion
	}
}
