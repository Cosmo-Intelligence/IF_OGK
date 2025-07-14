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

			#region 受付
			receiptHostTextBox.Text = ConfigurationManager.AppSettings["ReceiptHost"].StringToString();
			receiptPortTextBox.Text = ConfigurationManager.AppSettings["ReceiptPort"].StringToString();
			receiptSendTimeoutTextBox.Text = ConfigurationManager.AppSettings["ReceiptSendTimeout"].StringToString();
			receiptReceiveTimeoutTextBox.Text = ConfigurationManager.AppSettings["ReceiptReceiveTimeout"].StringToString();
			#endregion
	
			#region 実績
			examHostTextBox.Text = ConfigurationManager.AppSettings["ExamHost"].StringToString();
			examPortTextBox.Text = ConfigurationManager.AppSettings["ExamPort"].StringToString();
			examSendTimeoutTextBox.Text = ConfigurationManager.AppSettings["ExamSendTimeout"].StringToString();
			examReceiveTimeoutTextBox.Text = ConfigurationManager.AppSettings["ExamReceiveTimeout"].StringToString();
			#endregion

			#region 患者
			patientHostTextBox.Text = ConfigurationManager.AppSettings["PatientHost"].StringToString();
			patientPortTextBox.Text = ConfigurationManager.AppSettings["PatientPort"].StringToString();
			patientSendTimeoutTextBox.Text = ConfigurationManager.AppSettings["PatientSendTimeout"].StringToString();
			patientReceiveTimeoutTextBox.Text = ConfigurationManager.AppSettings["PatientReceiveTimeout"].StringToString();
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

		private void receiptConnectTcpCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (receiptConnectTcpCheckBox.Checked)
			{
				_log.InfoFormat("受付ソケット接続を開始します");
				_looper.ConnectTcpReceipt();
				SetConnectTcpText(receiptConnectTcpCheckBox);
				_log.InfoFormat("受付ソケット接続を開始しました");
				return;
			}
			_log.InfoFormat("受付ソケット接続を切断します");
			_looper.DisConnectTcpReceipt();
			SetConnectTcpText(receiptConnectTcpCheckBox);
			receiptRequestCheckBox.Checked = false;
			_log.InfoFormat("受付ソケット接続を切断しました");
		}

		private void receiptRequestCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (receiptRequestCheckBox.Checked)
			{
				_log.InfoFormat("受付接続開始要求を開始します");
				_looper.RequestOpenReceipt();
				SetRequestText(receiptRequestCheckBox);
				_log.InfoFormat("受付接続開始要求を終了しました");
				return;
			}
			_log.InfoFormat("受付切断開始要求を開始します");
			_looper.RequestCloseReceipt();
			SetRequestText(receiptRequestCheckBox);
			_log.InfoFormat("受付切断開始要求を終了しました");
		}

		private void examConnectTcpCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (examConnectTcpCheckBox.Checked)
			{
				_log.InfoFormat("実施ソケット接続を開始します");
				_looper.ConnectTcpExam();
				SetConnectTcpText(examConnectTcpCheckBox);
				_log.InfoFormat("実施ソケット接続を開始しました");
				return;
			}
			_log.InfoFormat("実施ソケット接続を切断します");
			_looper.DisConnectTcpExam();
			SetConnectTcpText(examConnectTcpCheckBox);
			examRequestCheckBox.Checked = false;
			_log.InfoFormat("実施ソケット接続を切断しました");
		}

		private void examRequestCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (examRequestCheckBox.Checked)
			{
				_log.InfoFormat("実施接続開始要求を開始します");
				_looper.RequestOpenExam();
				SetRequestText(examRequestCheckBox);
				_log.InfoFormat("実施接続開始要求を終了しました");
				return;
			}
			_log.InfoFormat("実施切断開始要求を開始します");
			_looper.RequestCloseExam();
			SetRequestText(examRequestCheckBox);
			_log.InfoFormat("実施切断開始要求を終了しました");
		}

		private void patientConnectTcpCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (patientConnectTcpCheckBox.Checked)
			{
				_log.InfoFormat("患者ソケット接続を開始します");
				_looper.ConnectTcpPatient();
				SetConnectTcpText(patientConnectTcpCheckBox);
				_log.InfoFormat("患者ソケット接続を開始しました");
				return;
			}
			_log.InfoFormat("患者ソケット接続を切断します");
			_looper.DisConnectTcpPatient();
			SetConnectTcpText(patientConnectTcpCheckBox);
			patientRequestCheckBox.Checked = false;
			_log.InfoFormat("患者ソケット接続を切断しました");
		}

		private void patientRequestCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (patientRequestCheckBox.Checked)
			{
				_log.InfoFormat("患者接続開始要求を開始します");
				_looper.RequestOpenPatient();
				SetRequestText(patientRequestCheckBox);
				_log.InfoFormat("患者接続開始要求を終了しました");
				return;
			}
			_log.InfoFormat("患者切断開始要求を開始します");
			_looper.RequestClosePatient();
			SetRequestText(patientRequestCheckBox);
			_log.InfoFormat("患者切断開始要求を終了しました");
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
