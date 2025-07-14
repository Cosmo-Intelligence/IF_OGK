using System;
using System.ServiceProcess;
using System.Threading;
using ARISReceive.Model;
using ARISReceive.Properties;
using RISBizLibrary.Receive.Model;
using RISCommonLibrary.Lib.Utils;
using System.Configuration;

namespace ARISReceive
{
	partial class ARISReceiveServiceHospital : ServiceBase
	{
		#region field
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// リッスンスレッド
		/// </summary>
		private Thread _listenThread;

		/// <summary>
		/// リスナー操作クラス
		/// </summary>
		private TcpListenerHandler _listenerHandler;

		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ARISReceiveServiceHospital()
		{
			InitializeComponent();
		}
		#endregion

		#region event
		
		/// <summary>
		/// 開始されたよイベント
		/// </summary>
		/// <param name="args"></param>
		protected override void OnStart(string[] args)
		{
			System.Diagnostics.FileVersionInfo ver =
					System.Diagnostics.FileVersionInfo.GetVersionInfo(
				System.Reflection.Assembly.GetExecutingAssembly().Location);
			_log.InfoFormat("サービスを開始します。ファイルバージョン={0}", ver.FileVersion);

			int port = ConfigurationManager.AppSettings["Port"].StringToInt32();
			int sendTimeout = ConfigurationManager.AppSettings["SendTimeout"].StringToInt32();
			int receiveTimeout = ConfigurationManager.AppSettings["ReceiveTimeout"].StringToInt32();
			int clientCloseTimeout = ConfigurationManager.AppSettings["ClientCloseTimeout"].StringToInt32();
			_log.DebugFormat("Listener開始します。Port={0}, SendTimeout={1}, ReceiveTimeout={2}, ClientCloseTimeout={3}",
				port, sendTimeout, receiveTimeout, clientCloseTimeout);

			ServerSocketParameter p = new ServerSocketParameter()
			{
				Port = port,
				SendTimeout = sendTimeout,
				ReceiveTimeout = receiveTimeout,
				ClientCloseTimeout = clientCloseTimeout
			};
			_listenerHandler = CreateTcpListenerHandler();
			_listenThread = new Thread(new ParameterizedThreadStart(_listenerHandler.StartListen));
			_listenThread.Start(p);
			_log.Debug("ListenThread開始します");
			_log.Info("サービスを開始しました");
		}

		/// <summary>
		/// 停止されたよイベント
		/// </summary>
		protected override void OnStop()
		{
			_log.Info("サービスを停止します");
			if (_listenerHandler != null)
			{
				_listenerHandler.StopListen();
			}
			if (_listenThread != null && _listenThread.IsAlive)
			{
				_log.Debug("ListenThread停止開始");

				Int32 millisecondsTimeout = ConfigurationManager.AppSettings["ListenerThreadStopTimeout"].StringToInt32();
				_log.DebugFormat("ListenThreadJoinします。Timeout={0}", millisecondsTimeout);
				if (!_listenThread.Join(millisecondsTimeout))
				{
					_log.Warn("制限時間内に処理が終了しませんでした。強制終了します");
					_listenThread.Abort();
				}
				_listenThread = null;
				_log.Debug("ListenThread停止完了");
			}
			if (_listenerHandler != null)
			{
				_listenerHandler.Dispose();
				_listenerHandler = null;
			}
			_log.Info("サービスを停止しました");
		}
		#endregion

		#region method
		/// <summary>
		/// リスナハンドラ作成
		/// </summary>
		/// <returns></returns>
		protected TcpListenerHandler CreateTcpListenerHandler()
		{
			return new TcpListenerHandlerHospital();
		}
		
		#endregion
	}
}
