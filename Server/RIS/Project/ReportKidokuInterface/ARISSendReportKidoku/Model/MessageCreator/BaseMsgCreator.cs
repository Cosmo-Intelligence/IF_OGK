using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using RISCommonLibrary.Lib.Msg;
using RISBizLibrary.Data;
using System.Net.Sockets;
using RISCommonLibrary.Lib.Utils;
using ARISSend.Data;
using RISCommonLibrary.Lib.Msg.ConnectionOpen;
using RISBizLibrary.Utils;
using RISCommonLibrary.Lib.Exceptions;

namespace ARISSend.Model.MessageCreator
{
	internal abstract class BaseMsgCreator: IMessageCreator, ISocketParameter, IDisposable
	{
		#region field

		/// <summary>
		/// tcpクライアントクラス
		/// </summary>
		private ReportDokueiInfoTcpClient _reportDokueiInfoTcpClient;

		/// <summary>
		/// ログ
		/// </summary>
		protected static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#region delegate

		/// <summary>
		/// 接続・切断要求のためのメッセージデータ作成
		/// </summary>
		/// <returns></returns>
		public delegate BaseMsgData CreateMsgDataForConnection();
		
		#endregion

		#endregion

		#region property

		/// <summary>
		/// ログ出力用名前
		/// </summary>
		public abstract string NameForLog
		{
			get;
		}

		protected ILog Log4NetLog
		{
			get
			{
				return _log;
			}
		}

		#region IMessageCreator メンバ
		/// <summary>
		/// 
		/// </summary>
		public bool Connected
		{
			get
			{
				if (_reportDokueiInfoTcpClient == null)
				{
					return false;
				}
				return _reportDokueiInfoTcpClient.Connected;
			}
		}

		#endregion

		#endregion

		#region constractor

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BaseMsgCreator()
		{
			_reportDokueiInfoTcpClient = new ReportDokueiInfoTcpClient();
		}
		#endregion

		#region method
		
		#region IMessageCreator メンバ

		//public abstract string[] GetRequestTypes();

		public abstract BaseMsg CreateMsg(ARISSend.Data.ReportDokueiInfo reportInfo, System.Data.IDbConnection cn);

		public abstract BaseMsgData CreateMsgData();

		public void ConnectTcp()
		{
			_log.InfoFormat("{0}:接続します", this.NameForLog);
			if (_reportDokueiInfoTcpClient == null)
			{
				_reportDokueiInfoTcpClient = new ReportDokueiInfoTcpClient();
			}
			_reportDokueiInfoTcpClient.ConnectTcp(this);
		}

		public void DisConnectTcp()
		{
			_log.InfoFormat("{0}:切断します", this.NameForLog);
			if (_reportDokueiInfoTcpClient == null)
			{
				return;
			}
			_reportDokueiInfoTcpClient.DisConnectTcp();
			_reportDokueiInfoTcpClient = null;
		}

		public BaseMsgData RequestOpen()
		{
			_log.DebugFormat("{0}:回線接続要求を送信します", this.NameForLog);
			return RequestConnection(CreateMsgDataConnectionOpen);
		}

		public BaseMsgData RequestClose()
		{
			_log.DebugFormat("{0}:回線切断要求を送信します", this.NameForLog);
			return RequestConnection(CreateMsgDataConnectionClose);
		}

		public void SendSocket(BaseMsgData msgData)
		{
			if (_reportDokueiInfoTcpClient == null)
			{
				throw new RISIfSocketException("ソケット未接続です");
			}
			_reportDokueiInfoTcpClient.SendRecv(msgData);
		}

		#region private
		
		/// <summary>
		/// 接続用
		/// </summary>
		private BaseMsgData RequestConnection(CreateMsgDataForConnection func)
		{
			BaseMsgData msgData = func();
			_reportDokueiInfoTcpClient.SendRecv(msgData);
			return msgData;
		}

		private BaseMsgData CreateMsgDataConnectionOpen()
		{
			BaseMsgData msgData = new ConnectionMsgData();
			ConnectionOpenMsg msg = new ConnectionOpenMsg();
			msg.MsgBody.CommunicationControl.CreateForOpen();
			msgData.Request = msg;
			return msgData;
		}

		private BaseMsgData CreateMsgDataConnectionClose()
		{
			BaseMsgData msgData = new ConnectionMsgData();
			ConnectionOpenMsg msg = new ConnectionOpenMsg();
			msg.MsgBody.CommunicationControl.CreateForClose();
			msgData.Request = msg;
			return msgData;
		}

		#endregion

		#endregion

		#endregion

		#region ISocketParameter メンバ

		public abstract string Host
		{
			get;
		}

		public abstract int Port
		{
			get;
		}

		public abstract int ConnectionTimeout
		{
			get;
		}

		public abstract int SendTimeout
		{
			get;
		}

		public abstract int ReceiveTimeout
		{
			get;
		}

		#endregion

		#region IDisposable メンバ

		public void Dispose()
		{
			((IDisposable)this).Dispose();
		}

		#endregion

		#region IDisposable メンバ

		void IDisposable.Dispose()
		{
			this.DisConnectTcp();
		}

		#endregion
	}
}
