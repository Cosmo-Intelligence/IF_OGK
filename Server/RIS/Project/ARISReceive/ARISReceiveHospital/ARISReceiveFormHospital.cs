using System;
using System.Threading;
using System.Windows.Forms;
using ARISReceive.Model;
using ARISReceive.Properties;
using RISBizLibrary.Receive.Model;
using RISCommonLibrary.Lib.Debugger;
using RISCommonLibrary.Lib.Utils;
using System.Configuration;

namespace ARISReceive
{
	public partial class ARISReceiveFormHospital : Form
	{
		#region field
		/// <summary>
		/// ログフォーム
		/// </summary>
		private LogForm logForm;

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
		public ARISReceiveFormHospital()
		{
			InitializeComponent();
		}

		#endregion

		private void exitButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void logButton_Click(object sender, EventArgs e)
		{
			logForm.Show();
			logForm.BringToFront();
		}

		private void ArisReceiveForm_Load(object sender, EventArgs e)
		{
			logForm = new LogForm();

			this.Text =  GetFormText();
			#region App.Configから画面へ反映
			portTextBox.Text = Convert.ToString(ConfigurationManager.AppSettings["Port"]);
			sendTimeoutTextBox.Text = Convert.ToString(ConfigurationManager.AppSettings["SendTimeout"]);
			receiveTimeoutTextBox.Text = Convert.ToString(ConfigurationManager.AppSettings["ReceiveTimeout"]);
			clientCloseTimeoutTextBox.Text = Convert.ToString(ConfigurationManager.AppSettings["ClientCloseTimeout"]);
			#endregion
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			System.Diagnostics.FileVersionInfo ver =
					System.Diagnostics.FileVersionInfo.GetVersionInfo(
				System.Reflection.Assembly.GetExecutingAssembly().Location);
			_log.InfoFormat("サービスを開始します。ファイルバージョン={0}", ver.FileVersion);

			Int32 port = -1;
			port = StringUtils.StringToInt32(portTextBox.Text, port);
			Int32 sendTimeout = 15000;
			sendTimeout = StringUtils.StringToInt32(sendTimeoutTextBox.Text, sendTimeout);
			Int32 receiveTimeout = 15000;
			receiveTimeout = StringUtils.StringToInt32(receiveTimeoutTextBox.Text, receiveTimeout);
			Int32 clientCloseTimeout = 90000;
			clientCloseTimeout = StringUtils.StringToInt32(clientCloseTimeoutTextBox.Text, clientCloseTimeout);

			_log.DebugFormat("リッスン開始します。Port={0}, SendTimeout={1}, ReceiveTimeout={2}, ClientCloseTimeout={3}",
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

			startButton.Enabled = false;
			stopButton.Enabled = !startButton.Enabled;
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			_log.Info("停止します");
			if (_listenerHandler == null)
			{
				_log.Debug("すでに停止しています");
				return;
			}
			_listenerHandler.StopListen();
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
			_log.Info("停止しました");

			startButton.Enabled = true;
			stopButton.Enabled = !startButton.Enabled;
		}

		private void ArisReceiveForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			stopButton_Click(this, null);
			if (logForm != null)
			{
				logForm.Close();
			}
		}

		/// <summary>
		/// フォーム表示キャプション
		/// </summary>
		/// <returns></returns>
		protected virtual string GetFormText()
		{
			return "入退院受信";
		}

		/// <summary>
		/// リスナハンドラ作成
		/// </summary>
		/// <returns></returns>
		protected TcpListenerHandler CreateTcpListenerHandler()
		{
			return new TcpListenerHandlerHospital();
		}

	}
}
