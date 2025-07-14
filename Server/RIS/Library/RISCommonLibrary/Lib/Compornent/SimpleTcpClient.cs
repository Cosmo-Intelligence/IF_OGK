using System;
using System.Net.Sockets;
using System.Text;
using System.Linq;

namespace RISCommonLibrary.Lib.Compornent
{
    public class SimpleTcpClient: IDisposable
    {
        #region プライベートフィールド
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;

        private static readonly Encoding _shiftJISEnc = Encoding.GetEncoding("Shift_JIS");

		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		#endregion

        #region パブリックプロパティ
        
        public Int32 Available {
            get
            {
				if (_tcpClient == null)
				{
					return 0;
				}
                return _tcpClient.Available;
            }
        }

        public bool Connected {
            get
            {
				if (_tcpClient == null)
				{
					return false;
				}
				return _tcpClient.Connected;
            }
        }

        #endregion

        #region コンストラクタ

        public SimpleTcpClient()
        {
        }
        #endregion

        #region パブリックメソッド

        public void Connect(String host, Int32 port, Int32 connectionTimeout,
            Int32 sendTimeout, Int32 receiveTimeout)
        {
			_log.Debug("Connectします");
			_tcpClient = new TcpClient();
            try
            {
                IAsyncResult iar = _tcpClient.BeginConnect(host, port, null, null);
                try
                {
                    if (!iar.CompletedSynchronously)
                    {
                        if (!iar.AsyncWaitHandle.WaitOne(connectionTimeout, false))
                        {
                            throw new TimeoutException("接続タイムアウトしました");
                        }
                    }
                }
                finally
                {
                    _tcpClient.EndConnect(iar);
                }

                _networkStream = _tcpClient.GetStream(); //NetworkStream設定
                _tcpClient.SendTimeout = sendTimeout;
                _tcpClient.ReceiveTimeout = receiveTimeout;
            }
            catch 
            {
                DisConnect();
                throw;
            }
			_log.Debug("Connectしました");
		}

        public void DisConnect()
        {
			_log.Debug("DisConnectします");
			CloseNetworkStream(_networkStream);
			_networkStream = null;
			CloseTcpClient(_tcpClient);
            _tcpClient = null;
			_log.Debug("DisConnectしました");
		}
        
        public void Send(String message)
        {
			_log.Info("Sendします");
			Byte[] data = _shiftJISEnc.GetBytes(message);
            _networkStream.Write(data, 0, data.Length);
			_log.Info("Sendしました");
		}

        public string Read()
        {
			_log.Info("Readします");
			byte[] completeBuffer = new byte[] { };
			byte[] readBuffer = new byte[_tcpClient.ReceiveBufferSize];
            int numberOfBytesRead = 0;
            do
            {
				_log.Debug("ReadStart");
				numberOfBytesRead = _networkStream.Read(readBuffer, 0, readBuffer.Length);
				_log.DebugFormat("ReadEnd={0}バイト", numberOfBytesRead);
				if (numberOfBytesRead == 0)
                {
                    break;
                }
				completeBuffer = completeBuffer.Concat<Byte>(
					readBuffer.Take<Byte>(numberOfBytesRead)).ToArray<Byte>(); //受信した分だけバイト配列をつなげる
            }
            while (_networkStream.DataAvailable);

			_log.Info("Readしました");
			return _shiftJISEnc.GetString(completeBuffer, 0, completeBuffer.Length);
        }

        #endregion

        #region プライベートメソッド

        private void CloseTcpClient(TcpClient client)
        {
			_log.Debug("CloseTcpClientします");
			if (client == null)
            {
				_log.DebugFormat("CloseTcpClientしました。{0}", "client == null");
				return;
            }

            client.Close();
			_log.Debug("CloseTcpClientしました");
		}

        private void CloseNetworkStream(NetworkStream stream)
        {
			_log.Debug("CloseNetworkStreamします");
			if (stream == null)
            {
				_log.DebugFormat("CloseNetworkStreamしました。{0}", "stream == null");
				return;
            }
			try
			{
				if (stream.DataAvailable)
				{
					string buffer = this.Read();
					_log.WarnFormat("切断時に未受信のバッファが見つかりました={0}", buffer);
				}
			}
			catch (Exception e)
			{//切断時は例外を握りつぶす
				_log.WarnFormat("切断時未受信チェック例外={0}", e.ToString());
			}
			stream.Close();
			_log.Debug("CloseNetworkStreamしました");
		}
        #endregion

        #region IDisposable メンバ

        public void Dispose()
        {
            DisConnect();
        }

        #endregion
    }
}
