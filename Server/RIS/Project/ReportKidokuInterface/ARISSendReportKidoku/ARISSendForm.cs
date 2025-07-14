using System;
using System.Configuration;
using System.Windows.Forms;
using ARISSend.Model;
using RISCommonLibrary.Lib.Debugger;
using RISCommonLibrary.Lib.Utils;

namespace ARISSend
{
    public partial class ARISSendForm : Form
    {
		#region field
		/// <summary>
		/// ログフォーム
		/// </summary>
		private LogForm _logForm;

		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// 
		/// </summary>
		private ServiceLooper _looper = new ServiceLooper();

		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ARISSendForm()
		{
			InitializeComponent();
		}

		#endregion

		#region event
		
		private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            Cursor preCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            executeLoopButton.Enabled = false;
            executeButton.Enabled = false;
            try
            {
				_looper.ExcecuteOne();
            }
            finally
            {
                executeLoopButton.Enabled = true;
                executeButton.Enabled = true;
                Cursor.Current = preCursor;
            }
        }

        private void executeLoopButton_Click(object sender, EventArgs e)
        {
            Cursor preCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            executeLoopButton.Enabled = false;
            executeButton.Enabled = false;
            try
            {
				_looper.Execute(this.LoopAction);
            }
            finally
            {
                executeLoopButton.Enabled = true;
                executeButton.Enabled = true;
                Cursor.Current = preCursor;
            }
        }

        private void loopStopButton_Click(object sender, EventArgs e)
        {
			_looper.StopLoop();
        }

		private void ArisSendForm_Load(object sender, EventArgs e)
		{
			_logForm = new LogForm();

			#region App.Configから画面へ反映

			#region 既読
			reportDokueiHostTextBox.Text = ConfigurationManager.AppSettings["ReportDokueiHost"].StringToString();
			reportDokueiPortTextBox.Text = ConfigurationManager.AppSettings["ReportDokueiPort"].StringToString();
			reportDokueiSendTimeoutTextBox.Text = ConfigurationManager.AppSettings["ReportDokueiTimeout"].StringToString();
			reportDokueiReceiveTimeoutTextBox.Text = ConfigurationManager.AppSettings["ReportDokueiReceiveTimeout"].StringToString();
			#endregion

			#endregion
		}

		private void ArisSendForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_looper != null)
			{
				_looper.Dispose();
			}
			
			if (_logForm != null)
			{
				_logForm.Close();
			}
		}

		private void logButton_Click(object sender, EventArgs e)
		{
			_logForm.Show();
			_logForm.BringToFront();
		}
		#endregion

		#region method
		
		private void LoopAction()
		{
			Application.DoEvents();
		}
		#endregion

		private void reportDokueiConnectTcpCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (reportDokueiConnectTcpCheckBox.Checked)
			{
				_log.InfoFormat("既読ソケット接続を開始します");
				_looper.ConnectTcpReportDokuei();
				SetConnectTcpText(reportDokueiConnectTcpCheckBox);
				_log.InfoFormat("既読ソケット接続を開始しました");
				return;
			}
			_log.InfoFormat("既読ソケット接続を切断します");
			_looper.DisConnectTcpReportDokuei();
			SetConnectTcpText(reportDokueiConnectTcpCheckBox);
			reportDokueiRequestCheckBox.Checked = false;
			_log.InfoFormat("既読ソケット接続を切断しました");
		}

		private void reportDokueiRequestCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (reportDokueiRequestCheckBox.Checked)
			{
				_log.InfoFormat("既読接続開始要求を開始します");
				_looper.RequestOpenReportDokuei();
				SetRequestText(reportDokueiRequestCheckBox);
				_log.InfoFormat("既読接続開始要求を終了しました");
				return;
			}
			_log.InfoFormat("既読切断開始要求を開始します");
			_looper.RequestCloseReportDokuei();
			SetRequestText(reportDokueiRequestCheckBox);
			_log.InfoFormat("既読切断開始要求を終了しました");
		}

		private void SetConnectTcpText(CheckBox target)
		{
			if (target.Checked)
			{
				target.Text = "ソケット接続を閉じる";
				return;
			}
			target.Text = "ソケット接続を開く";
		}

		private void SetRequestText(CheckBox target)
		{
			if (target.Checked)
			{
				target.Text = "切断要求を送信";
				return;
			}
			target.Text = "接続要求を送信";
		}
	}
}
