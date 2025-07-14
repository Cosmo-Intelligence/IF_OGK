using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using RISCommonLibrary.Lib.MessageLog;
using RISCommonLibrary.Lib.Utils;
using RISBizLibrary.Utils;

namespace RISBizLibrary.Receive.Model
{
	/// <summary>
	/// クライアント接続処理クラス
	/// </summary>
	public abstract class TcpClientHandler: IDisposable
	{
		#region field
		
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// 同期オブジェクト
		/// </summary>
		private object _syncObject = new object();

		/// <summary>
		/// クライアントソケット接続
		/// </summary>
		private TcpClient _tcpClient;

		/// <summary>
		/// 処理中イベント
		/// </summary>
		private ManualResetEvent _doingEvent = new ManualResetEvent(true);

		/// <summary>
		/// サーバソケットで使用しているパラメータ
		/// </summary>
		private ServerSocketParameter _param;

		/// <summary>
		/// メッセージ処理クラス
		/// </summary>
		private MessageHandler _messageHandler;

		/// <summary>
		/// 古いログ削除ヘルパー
		/// </summary>
		private DeleteOldLogHelper _deleteOldLogHelper = new DeleteOldLogHelper();

		#endregion

		#region property
		
		/// <summary>
		/// クライアントソケット接続
		/// </summary>
		public TcpClient Client
		{
			get
			{
				lock (_syncObject)
				{
					return _tcpClient;
				}
			}
			set
			{
				lock (_syncObject)
				{
					_tcpClient = value;
				}
			}
		}

		/// <summary>
		/// 接続中か？
		/// </summary>
		public bool Connected
		{
			get
			{
				lock (_syncObject)
				{
					if (Client == null)
					{
						return false;
					}
					return Client.Connected;
				}
			}
		}
		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="tcpClient"></param>
		public TcpClientHandler(TcpClient tcpClient)
		{
			Client = tcpClient;
			_messageHandler = CreateMessageHandler();
		}

		#endregion

		#region method
		
		/// <summary>
		/// クライアントから読み込み
		/// </summary>
		/// <param name="state"></param>
		public void ReadClient(object state)
		{
			_log.DebugFormat("ReadClientThreadId={0}", Thread.CurrentThread.ManagedThreadId);
			try
			{
				#region パラメータ設定
				_param = state as ServerSocketParameter;
				if (_param == null)
				{
					_log.Debug("パラメータ設定されていないので処理中断");
					return;
				}
				_log.DebugFormat("ReceiveTimeout={0}, SendTimeout={1}", _param.ReceiveTimeout,
					_param.SendTimeout);
				Client.ReceiveTimeout = _param.ReceiveTimeout;
				Client.SendTimeout = _param.SendTimeout;
				#endregion

				_log.Info("クライアント接続しました");

				using (NetworkStream stream = Client.GetStream())
				{
					#region CanReadCanWrite
					if (!stream.CanRead)
					{
						_log.Error("NetworkStreamが読み取りをサポートしていません");
						return;
					}
					if (!stream.CanWrite)
					{
						_log.Error("NetworkStreamが書き込みをサポートしていません");
						return;
					}
					#endregion

					ReceivedByteInfo rbi = new ReceivedByteInfo();
					int readByteCount; //読み込みバイト数
					Byte[] bufferBytes = new Byte[Client.ReceiveBufferSize]; //Client.ReceiveBufferSizeはデフォルトの8192
					#region 終端文字まで読み込み処理の場合
					#endregion
					while ((readByteCount = stream.Read(bufferBytes, 0, bufferBytes.Length)) != 0)
					{
						if (rbi.LengthReceived == 0)
						{
							_log.Info("読み込み開始");
							_log.Debug("処理中イベントリセット");
							_doingEvent.Reset();
							_log.Debug("処理中イベントリセット完了");
						}
						_log.DebugFormat("Read={0}バイト", readByteCount);

						if (readByteCount == 0)
						{
							_log.Info("クライアント接続が切断されています");
							break;
						}
						ReadSend(readByteCount, bufferBytes, rbi, stream);
					}
					_log.Info("クライアント接続終了します");
				}
			}
			catch (System.IO.IOException eIO)
			{
				string errMessage = MiscUtils.BuildErrMessage(eIO, null);
				_log.InfoFormat("クライアント接続が切断されました。{0}", errMessage);
			}
			catch (ThreadAbortException)
			{
				_log.Warn("クライアント通信スレッドがAbortされました");
				Thread.ResetAbort();
			}
			catch (Exception e)
			{
				string errMessage = MiscUtils.BuildErrMessage(e, null);
				_log.ErrorFormat("クライアント通信スレッドで未補足の例外が発生しました。{0}", errMessage);
			}
			finally
			{
				_log.Info("クライアント接続終了処理");
				if (Client != null)
				{
					_log.Info("クライアント接続切断します");
					ReadAll(Client);
					Client.Close(); // Shutdown and end connection
					Client = null;
				}
				if (_doingEvent != null)
				{
					_log.Debug("処理中イベントセット");
					_doingEvent.Set();
					_log.Debug("処理中イベントセット完了");
					_doingEvent = null;
				}
			}
		}

		#region IDisposable メンバ

		public void Dispose()
		{
			_log.Debug("Dispose");
			lock (_syncObject)
			{

				if (_doingEvent != null)
				{
					_log.DebugFormat("クライアント通信終了を待ちます={0}", _param.ClientCloseTimeout);
					if (!_doingEvent.WaitOne(_param.ClientCloseTimeout))
					{
						_log.Debug("クライアント通信終了待ちタイムアウトしました");
					}
					_log.Debug("クライアント通信待ち終了");
					_doingEvent.Close();
					_doingEvent = null;
				}

				if (Client != null)
				{
					_log.Debug("クライアント接続クローズ開始");
					ReadAll(Client);
					Client.Close();
					Client = null;
					_log.Debug("クライアント接続クローズ終了");
				}
			}
		}

		#endregion

		/// <summary>
		/// メッセージハンドラ生成
		/// </summary>
		/// <returns></returns>
		protected abstract MessageHandler CreateMessageHandler();

		/// <summary>
		/// 電文ログファイル名取得
		/// </summary>
		/// <param name="logDateTime"></param>
		/// <param name="telegraphKind"></param>
		/// <param name="sendOrRecv"></param>
		/// <returns></returns>
		private String GetFileName(DateTime logDateTime, String telegraphKind, String sendOrRecv)
		{
			const String FORMAT_FILENAME = "{0:yyyyMMddHHmmssfff}_{1}_{2}.log";
			return String.Format(FORMAT_FILENAME, logDateTime, telegraphKind, sendOrRecv);
		}
		
		/// <summary>
		/// 受信、送信
		/// </summary>
		/// <param name="readByteCount"></param>
		/// <param name="bufferBytes"></param>
		/// <param name="rbi"></param>
		/// <param name="stream"></param>
		private void ReadSend(int readByteCount, Byte[] bufferBytes, ReceivedByteInfo rbi, NetworkStream stream)
		{
			try
			{
				rbi.LengthReceived += readByteCount;
				rbi.DataReceived = rbi.DataReceived.Concat<Byte>(bufferBytes.Take<Byte>(readByteCount)).ToArray<Byte>(); //受信した分だけバイト配列をつなげる

				#region 終端文字まで読み込み処理の場合
				//if (bufferBytes[0] != 0x03)
				#endregion
				if (stream.DataAvailable)
				{
					_log.Debug("次の読み出しへ");
					return; //読み残しあり
				}
				_log.Info("読み込み終了");
				_log.DebugFormat("ReadEnd={0}バイト", rbi.LengthReceived);
				
				#region ログファイルを削除。実質クライアントは1台しか接続してこないので(マルチスレッドにはならない)、ここでOK
				_log.Debug("古いログファイルを削除します");
				_deleteOldLogHelper.DeleteOldLog();
				#endregion

				string receivedString = System.Text.Encoding.GetEncoding(932).GetString(rbi.DataReceived, 0, rbi.DataReceived.Length);
				DateTime logDateTime = DateTime.Now; //電文ログ日時

				#region 受信ログ電文ファイル作成
				MessageLogger.Instance.UpdateCurrentDir(logDateTime);

				string recvFileLogName = GetFileName(logDateTime,
					MessageHandler.GetTELEGRAPH_KIND(receivedString), "RECV");
				_log.InfoFormat("ソケット受信電文ログファイル名={0}", recvFileLogName);
				MessageLogger.Instance.WriteLog(recvFileLogName, receivedString);
				#endregion

				string sendString = _messageHandler.Execute(receivedString);

				#region 送信ログ電文ファイル作成
				string sendFileLogName = GetFileName(logDateTime,
					MessageHandler.GetTELEGRAPH_KIND(sendString), "SEND");
				_log.InfoFormat("ソケット送信電文ログファイル名={0}", sendFileLogName);
				MessageLogger.Instance.WriteLog(sendFileLogName, sendString);
				#endregion

				byte[] msg = System.Text.Encoding.GetEncoding(932).GetBytes(sendString);

				_log.Info("送信開始");
				stream.Write(msg, 0, msg.Length);
				_log.Info("送信終了");

				_log.DebugFormat("ストリーム中の読み取り残={0}", stream.DataAvailable);
	
				rbi.Clear();
				_log.Debug("処理中イベントセット");
				_doingEvent.Set();
				_log.Debug("処理中イベントセット完了");
			}
			catch (SocketException eSocket) 
			{
				if (eSocket.SocketErrorCode == SocketError.WouldBlock ||
					eSocket.SocketErrorCode == SocketError.IOPending ||
					eSocket.SocketErrorCode == SocketError.NoBufferSpaceAvailable)
				{
					_log.DebugFormat("ソケットバッファが空です。再読み込みします。{0}", eSocket);
					Thread.Sleep(30); // socket buffer is probably empty, wait and try again
					return;
				}
				throw;
			}
		}

		/// <summary>
		/// ネットワークの読み込みバッファ内に残っているデータをすべて読む
		/// </summary>
		/// <returns></returns>
		private void ReadAll(TcpClient client)
		{
			if (client == null)
			{
				return;
			}
			if (!client.Connected)
			{
				return;
			}

			using (NetworkStream stream = client.GetStream())
			{
				if (stream == null)
				{
					return;
				}

				if (!stream.DataAvailable)
				{
					return;
				}

				_log.Debug("Readします");
				byte[] completeBuffer = new byte[] { };
				const int READ_BUFFER_SIZE = 8192;
				byte[] readBuffer = new byte[READ_BUFFER_SIZE];
				int numberOfBytesRead = 0;
				do
				{
					_log.Debug("ReadStart");
					numberOfBytesRead = stream.Read(readBuffer, 0, readBuffer.Length);
					_log.DebugFormat("ReadEnd={0}バイト", numberOfBytesRead);
					if (numberOfBytesRead == 0)
					{
						break;
					}
					completeBuffer = completeBuffer.Concat<Byte>(
						readBuffer.Take<Byte>(numberOfBytesRead)).ToArray<Byte>(); //受信した分だけバイト配列をつなげる
				}
				while (stream.DataAvailable);

				_log.Debug("Readしました");

				_log.WarnFormat("切断時に未受信のバッファが見つかりました={0}",
					System.Text.Encoding.GetEncoding(932).GetString(completeBuffer, 0, completeBuffer.Length));
			}
		}

		#endregion

		/// <summary>
		/// 受信済み情報クラス
		/// </summary>
		private class ReceivedByteInfo
		{
			public Byte[] DataReceived
			{
				get;
				set;
			}
			
			public int LengthReceived
			{
				get;
				set;
			}

			public ReceivedByteInfo()
			{
				Clear();
			}

			public void Clear()
			{
				this.DataReceived = new Byte[] { };
				LengthReceived = 0;
			}
		}
	}
}
