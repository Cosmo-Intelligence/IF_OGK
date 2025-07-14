using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using ServiceWatcher.Properties;

namespace ServiceWatcher
{
	public partial class serviceWatcherForm : Form
	{
		#region field
		
		/// <summary>
		/// log4netインスタンス
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// サービスデータクラスのリスト
		/// </summary>
		private List<ServiceData> _listServiceData;

		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public serviceWatcherForm()
		{
			InitializeComponent();

			serviceDataGridView.AutoGenerateColumns = false;
			refleshTextBox.Text = Convert.ToString(Settings.Default.RefleshTime);
			
			#region サービスデータクラスのリスト生成
			_listServiceData = new List<ServiceData>();
			foreach (var item in Settings.Default.TargetServiceNames)
			{
				_listServiceData.Add(new ServiceData(item, Settings.Default.OperationTimeout));
			}
			#endregion

			serviceBindingSource.DataSource = _listServiceData;
			SetData();
		}

		#endregion

		#region イベント

		/// <summary>
		/// フォームロード
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void serviceWatcherForm_Load(object sender, EventArgs e)
		{
			refleshTimer.Interval = Settings.Default.RefleshTime;
			refleshTimer.Start();
		}

		/// <summary>
		/// 閉じるボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// フォームが閉じられた
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void serviceWatcherForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				DisposeListServiceData(_listServiceData);
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
				throw;
			}
		}

		/// <summary>
		/// リフレッシュタイマー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refleshTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				Cursor cursor = this.Cursor;
				this.Cursor = Cursors.WaitCursor;
				refleshTimer.Stop();
				try
				{
					SetData();
				}
				finally
				{
					refleshTimer.Start();
					this.Cursor = cursor;
				}
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
				throw;
			}
		}

		/// <summary>
		/// 再表示ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refleshButton_Click(object sender, EventArgs e)
		{
			try
			{
				Cursor cursor = this.Cursor;
				this.Cursor = Cursors.WaitCursor;
				refleshTimer.Stop();
				try
				{
					SetData();
				}
				finally
				{
					refleshTimer.Start();
					this.Cursor = cursor;
				}
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
				throw;
			}
		}

		/// <summary>
		/// サービス一覧グリッドマウスダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void serviceDataGridView_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button != MouseButtons.Right)
				{
					return;
				}
				int hitRowIndex = serviceDataGridView.HitTest(e.Location.X, e.Location.Y).RowIndex;
				if (hitRowIndex < 0)
				{
					return;
				}
				serviceDataGridView.CurrentCell = serviceDataGridView[
					serviceDataGridView.CurrentCell.ColumnIndex, hitRowIndex];//SelectionChangedイベントが呼ばれる
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
				throw;
			}
		}

		/// <summary>
		/// グリッド一覧ポップアップメニューオープン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void serviceContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			try
			{
				DataGridViewRow viewRow = serviceDataGridView.CurrentRow;
				startToolStripMenuItem.Enabled = false;
				stopToolStripMenuItem.Enabled = false;
				if (viewRow == null)
				{
					return;
				}

				ServiceData sd = viewRow.DataBoundItem as ServiceData;
				if (sd == null)
				{
					return;
				}
				startToolStripMenuItem.Enabled = sd.CanStart;
				stopToolStripMenuItem.Enabled = sd.CanStop;
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
				throw;
			}
		}

		/// <summary>
		/// 開始メニュークリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void startToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Cursor cursor = this.Cursor;
				this.Cursor = Cursors.WaitCursor;
				try
				{
					DataGridViewRow viewRow = serviceDataGridView.CurrentRow;
					startToolStripMenuItem.Enabled = false;
					stopToolStripMenuItem.Enabled = false;
					if (viewRow == null)
					{
						return;
					}
					ServiceData sd = viewRow.DataBoundItem as ServiceData;
					if (sd == null)
					{
						return;
					}
					sd.Start();
					serviceDataGridView.InvalidateRow(viewRow.Index);
				}
				finally
				{
					this.Cursor = cursor;
				}
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
				throw;
			}
		}

		/// <summary>
		/// 停止メニュークリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Cursor cursor = this.Cursor;
				this.Cursor = Cursors.WaitCursor;
				try
				{
					DataGridViewRow viewRow =serviceDataGridView.CurrentRow;
					startToolStripMenuItem.Enabled = false;
					stopToolStripMenuItem.Enabled = false;
					if (viewRow == null)
					{
						return;
					}
					ServiceData sd = viewRow.DataBoundItem as ServiceData;
					if (sd == null)
					{
						return;
					}
					sd.Stop();
					serviceDataGridView.InvalidateRow(viewRow.Index);
				}
				finally
				{
					this.Cursor = cursor;
				}
			}
			catch (Exception ex)
			{
				_log.ErrorFormat("未補足の例外が発生しました。{0}", ex);
				throw;
			}
		}

		#endregion

		#region メソッド
		
		/// <summary>
		/// データ設定
		/// </summary>
		private void SetData()
		{
			DisposeListServiceData(_listServiceData);
			#region サービスコントローラの割付
			ServiceController[] services = System.ServiceProcess.ServiceController.GetServices();
			foreach (var item in _listServiceData)
			{
				item.TargetServiceController = services.FirstOrDefault(
					s => string.Compare(s.ServiceName, item.ServiceName, true) == 0);
			}
			#endregion

			serviceDataGridView.Invalidate();
		}

		/// <summary>
		/// サービスデータリストの開放
		/// </summary>
		/// <param name="list"></param>
		/// <remarks>不要っぽい</remarks>
		private void DisposeListServiceData(List<ServiceData> list)
		{
			if (list == null)
			{
				return;
			}
			for (int idx = 0; idx < list.Count; idx++)
			{
				ServiceData item = list[idx];
				item.Dispose();
			}
		}
		#endregion

	}
}
