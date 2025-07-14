using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StatisticsPrint.Controller;

namespace StatisticsPrint.View
{
	/// <summary>
	/// メニュー
	/// </summary>
	public partial class menuForm : Form
	{
		/// <summary>
		/// コントローラ
		/// </summary>
		private StatisticsController _controller = new StatisticsController();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public menuForm()
		{
			InitializeComponent();
		}

		#region Events
		
		/// <summary>
		/// フォーム表示イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuForm_Shown(object sender, EventArgs e)
		{
			this.ActiveControl = buibunruiButton;
		}

		/// <summary>
		/// 終了ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void exitButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// 撮影検査数一覧（部位分類）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buibunruiButton_Click(object sender, EventArgs e)
		{
			_controller.PrintBuibunrui();
		}

		/// <summary>
		/// 撮影検査数一覧（主行為）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buiButton_Click(object sender, EventArgs e)
		{
			_controller.PrintBui();
		}


		/// <summary>
		/// フィルム一覧
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void filmButton_Click(object sender, EventArgs e)
		{
			_controller.PrintFilm();
		}

		/// <summary>
		/// 造影・薬剤一覧
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zoueizaiButton_Click(object sender, EventArgs e)
		{
			_controller.PrintZoueizai();
		}

		/// <summary>
		/// 救命救急センター　集計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void emergencyButton_Click(object sender, EventArgs e)
		{
			_controller.PrintEmergency();
		}

		/// <summary>
		/// 医療技術部診療検査科　月報
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void geppoButton_Click(object sender, EventArgs e)
		{
			_controller.PrintGeppo();
		}


		/// <summary>
		/// 一般撮影　分類別集計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ippansatsueiButton_Click(object sender, EventArgs e)
		{
			_controller.PrintIppan();
		}

		/// <summary>
		/// ＣＴ　分類別集計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ctButton_Click(object sender, EventArgs e)
		{
			_controller.PrintCT();
		}

		/// <summary>
		/// ＲＩ　分類別集計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void riButton_Click(object sender, EventArgs e)
		{
			_controller.PrintRI();
		}

		/// <summary>
		/// 透視造影　分類別集計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void drButton_Click(object sender, EventArgs e)
		{
			_controller.PrintDR();
		}

		/// <summary>
		/// ＵＳ　分類別集計表
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void usButton_Click(object sender, EventArgs e)
		{
			_controller.PrintUS();
		}

		/// <summary>
		/// Film読込・書出/CD読込・書出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cdFilmButton_Click(object sender, EventArgs e)
		{
			_controller.PrintCDFilm();
		}

		#endregion
	}
}
