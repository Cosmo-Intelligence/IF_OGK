using System;
using System.Configuration;
using System.Windows.Forms;
using RISCommonLibrary.Lib.Debugger;
using RISCommonLibrary.Lib.Utils;
using AddToHisInfoFromSQDB.Model;

namespace AddToHisInfoFromSQDB
{
    public partial class AddToHisInfoFromSQDBForm : Form
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
		private MasterStudyWatcher _watcher = new MasterStudyWatcher();

		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public AddToHisInfoFromSQDBForm()
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
				_watcher.ExcecuteOne();
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
				_watcher.Execute(this.LoopAction);
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
			_watcher.StopLoop();
        }

		private void ArisSendForm_Load(object sender, EventArgs e)
		{
			_logForm = new LogForm();
		}

		private void ArisSendForm_FormClosed(object sender, FormClosedEventArgs e)
		{

			
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

	}
}
