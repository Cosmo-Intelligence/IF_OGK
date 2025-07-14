using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using StatisticsPrint.Model;
using StatisticsPrint.Data;
using StatisticsPrint.Model.Print;
using RISCommonLibrary.Lib.Utils;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.FormHelper;

namespace StatisticsPrint.View
{
	/// <summary>
	/// 抽出条件
	/// </summary>
	public partial class conditionForm : Form
	{

		#region property

		public PrintBase PrintInstance
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public conditionForm()
		{
			InitializeComponent();

			//デフォルトだと現在日付を指定されるので、31とかだと月を変更した際に以下のエラーが出てしまう対応
			//System.ArgumentOutOfRangeException: Year、Month および Day パラメータが表現できない DateTime を示しています。
			nengetsuDateTimePicker.Value = DateTime.Now.BeginOfMonth();
		}
		#endregion

		#region イベント

		/// <summary>
		/// ロード
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void conditionForm_Load(object sender, EventArgs e)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			this.Enabled = false;
			try
			{
				Init();
			}
			finally
			{
				this.Enabled = true;
				Cursor.Current = cursor;
			}
		}

		/// <summary>
		/// 年月評価後
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nengetsuDateTimePicker_Validated(object sender, EventArgs e)
		{
			this.conditionErrorProvider.SetError((DateTimePicker)sender, null);
		}

		/// <summary>
		/// プリンタ名評価後
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printerNameComboBox_Validated(object sender, EventArgs e)
		{
			this.conditionErrorProvider.SetError((ComboBox)sender, null);
		}

		/// <summary>
		/// 部数評価後
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copiesTextBox_Validated(object sender, EventArgs e)
		{
			this.conditionErrorProvider.SetError((TextBox)sender, null);
		}

		/// <summary>
		/// 部数評価
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void copiesTextBox_Validating(object sender, CancelEventArgs e)
		{
			int copies;
			if (!int.TryParse(copiesTextBox.Text, out copies))
			{
				this.conditionErrorProvider.SetError((TextBox)sender, "印刷する部数を正しく設定してください");
				e.Cancel = true;
				return;
			}
		}

		/// <summary>
		/// プリンタ名評価
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printerNameComboBox_Validating(object sender, CancelEventArgs e)
		{
			if (printerNameComboBox.SelectedIndex < 0)
			{
				this.conditionErrorProvider.SetError((ComboBox)sender, "出力するプリンタを選択してください");
				e.Cancel = true;
				return;
			}
		}


		/// <summary>
		/// 印刷ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void printButton_Click(object sender, EventArgs e)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			this.Enabled = false;
			try
			{
				this.Invalidate();
				ConditionPrint condition = this.GetCondition();
				PrintInstance.Print(condition);
			}
			finally
			{
				this.Enabled = true;
				Cursor.Current = cursor;
			}
		}

		#endregion

		#region メソッド

		#region public
		
		/// <summary>
		/// 条件クラス取得
		/// </summary>
		/// <returns></returns>
		public virtual ConditionPrint GetCondition()
		{
			ConditionPrint condition = new ConditionPrint();
			SetCondition(condition);
			return condition;
		}

		#endregion public

		#region protected

		/// <summary>
		/// 条件クラス取得
		/// </summary>
		/// <returns></returns>
		protected virtual void SetCondition(ConditionPrint condition)
		{
			condition.Nengetsu = nengetsuDateTimePicker.Value.BeginOfMonth(); //月初にする
			condition.PrinterName = printerNameComboBox.SelectedValue as string;
			condition.Copies = copiesTextBox.Text.StringToInt32();
		}

		/// <summary>
		/// 初期化処理
		/// </summary>
		protected virtual void Init()
		{
			ConditionFormHelper helper = new ConditionFormHelper();
			printerNameComboBox.DataSource = helper.GetLocalPriterList();
			printerNameComboBox.SelectedItem = helper.GetDefaultPrinterName();
		}

		#endregion protected

		#endregion

	}
}
