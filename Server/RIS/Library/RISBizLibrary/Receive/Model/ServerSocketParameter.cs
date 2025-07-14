using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISBizLibrary.Receive.Model
{
	public class ServerSocketParameter
	{
		/// <summary>
		/// リッスンポート
		/// </summary>
		public int Port
		{
			get;
			set;
		}

		/// <summary>
		/// 送信タイムアウト(ミリ秒)
		/// </summary>
		public int SendTimeout
		{
			get;
			set;
		}

		/// <summary>
		/// 受信タイムアウト(ミリ秒)
		/// </summary>
		public int ReceiveTimeout
		{
			get;
			set;
		}

		/// <summary>
		/// クライアント接続停止待ちタイムアウトミリ秒
		/// </summary>
		public int ClientCloseTimeout
		{
			get;
			set;
		}

	}
}
