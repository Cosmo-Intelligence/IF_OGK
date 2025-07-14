using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using RISCommonLibrary.Lib.MessageLog;
using RISCommonLibrary.Lib.Utils;
using System.Configuration;

namespace RISBizLibrary.Receive.Model
{
	/// <summary>
	/// ソケットリスナー操作クラス
	/// </summary>
	public abstract class TcpListenerHandler: IDisposable
	{
		#region field
		
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private object _syncObject = new object();

		private volatile bool _isContinueAccept;

		private TcpListener _listener;
		
		private List<TcpClientHandler> _listClientHandler = new List<TcpClientHandler>();

		#endregion

		#region props
		
		private TcpListener Listener
		{
			get
			{
				lock (_syncObject)
				{
					return _listener;
				}
			}
			set
			{
				lock (_syncObject)
				{
					_listener = value;
				}
			}
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TcpListenerHandler()
		{
		}
		#endregion

		#region method

		public void StartListen(object obj)
		{
			try
			{
				_log.DebugFormat("ListenerThreadId={0}", Thread.CurrentThread.ManagedThreadId);
				ServerSocketParameter param = obj as ServerSocketParameter;
				if (param == null)
				{
					_log.Debug("パラメータ設定されていないので処理中断");
					return;
				}

				_log.Debug("電文ログクラスの初期化");
				InitMessageLogger(); //電文ログクラスを初期化しておく

				IPEndPoint ipLocalEndPoint = new IPEndPoint(IPAddress.Any, param.Port);
				Listener = new System.Net.Sockets.TcpListener(ipLocalEndPoint);

				if (!Listener.Server.Connected)
				{
					Listener.Start();
					_log.InfoFormat("Listenを開始しました。Port:{0}",
						((IPEndPoint)Listener.LocalEndpoint).Port);
				}

				try
				{
					_isContinueAccept = true;
					while (_isContinueAccept)
					{
						try
						{
							_log.Debug("不要アイテム削除します");
							RemoveAndDispose(_listClientHandler);

							#region ブロックする
							_log.Debug("クライアント接続待ち... ");
							TcpClient client = Listener.AcceptTcpClient();
							#endregion

							#region ブロックしない
							//TcpClient client = null;
							//if (!this.Listener.Pending())
							//{
								//Thread.Sleep(1000);
							//    continue;
							//}
							//client = Listener.AcceptTcpClient();
							//if (!client.Connected)
							//{
							//    _log.Debug(">>Server: AcceptSocket Failed");
							//    continue;
							//}
							#endregion

							TcpClientHandler clientHandler = CreateTcpClientHandler(client);
							_listClientHandler.Add(clientHandler);
							_log.InfoFormat("接続開始:{0} Port:{1}",
								IPAddress.Parse(((IPEndPoint)Listener.LocalEndpoint).Address.ToString()),
									((IPEndPoint)Listener.LocalEndpoint).Port.ToString()
									);
							ThreadPool.QueueUserWorkItem(clientHandler.ReadClient, param);
						}
						catch (SocketException eSocket)
						{
							if (eSocket.SocketErrorCode == SocketError.Interrupted)
							{
								string errMessage = MiscUtils.BuildErrMessage(eSocket, null);
								_log.InfoFormat("リスナ停止します。{0}", errMessage);
								Listener = null;
								_isContinueAccept = false;
								return;
							}
							throw;
						}
					}
				}
				finally
				{
					_listClientHandler.ForEach(item => item.Dispose());
				}
				Listener = null;
				_log.Info("Listenを停止しました");
			}
			catch (ThreadAbortException)
			{
				_log.Warn("リスナースレッドがAbortされました");
				Thread.ResetAbort();
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
				throw;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>メインスレッド側から呼ばれる</remarks>
		public void StopListen()
		{
			_isContinueAccept = false;
			if (Listener != null)
			{
				Listener.Stop();
				Listener = null;
				_log.Info("Listenerを閉じました。");
			}
		}

		/// <summary>
		/// ソケットクライアント作成
		/// </summary>
		/// <returns></returns>
		protected abstract TcpClientHandler CreateTcpClientHandler(TcpClient client);

		private void RemoveAndDispose(List<TcpClientHandler> list)
		{
			for (int idx = list.Count -1; idx >= 0; idx--)
			{
				TcpClientHandler item = list[idx];
				if (item.Connected)
				{
					continue;
				}
				list.Remove(item);
				item.Dispose();
			}
		}

		/// <summary>
		/// 電文ログクラス初期化
		/// </summary>
		private void InitMessageLogger()
		{
			MessageLogger.Instance.RootDir = ConfigurationManager.AppSettings["MessageLogRootDir"].StringToString();
			MessageLogger.Instance.TargetDirFormat = ConfigurationManager.AppSettings["MessageLogWriteDirDateFormat"].StringToString();
			MessageLogger.Instance.Enc = Encoding.GetEncoding(ConfigurationManager.AppSettings["MessageLogEncode"].StringToString());
		}

		#region IDisposable メンバ

		public void Dispose()
		{
			StopListen();
		}

		#endregion

		#endregion
	}
}
