using System;
using System.Windows.Forms;
using StatisticsPrint.Data;
using StatisticsPrint.Model.FormHelper;

namespace StatisticsPrint.View
{
	public partial class conditionGeppoForm : conditionForm
	{
		private ConditionGeppoFormHelper _helper = new ConditionGeppoFormHelper();

		public conditionGeppoForm()
		{
			InitializeComponent();
		}

		#region イベント
		
		/// <summary>
		/// データ入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataInputButton_Click(object sender, EventArgs e)
		{
			using (dataInputForm f = new dataInputForm())
			{
				f.Monsu = monsuTextBox.Text;
				if (f.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				monsuTextBox.Text = f.Monsu;
			};
		}

		private void dataSelectButton_Click(object sender, EventArgs e)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				monsuTextBox.Text = _helper.GetMonsu(nengetsuDateTimePicker.Value);
			}
			finally
			{
				Cursor.Current = cursor;
			}
		}

		/// <summary>
		/// 年月値変更イベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void nengetsuDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				monsuTextBox.Text = _helper.GetMonsu(nengetsuDateTimePicker.Value);
			}
			finally
			{
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
		public override ConditionPrint GetCondition()
		{
			ConditionPrintGeppo condition = new ConditionPrintGeppo();
			base.SetCondition(condition);

			condition.Monsu = monsuTextBox.Text;
			return condition;
		}
		#endregion

		protected override void Init()
		{
			base.Init();
			#region 実施場所の設定
			Cursor cursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			this.Enabled = false;
			try
			{
				monsuTextBox.Text = _helper.GetMonsu(nengetsuDateTimePicker.Value);
			}
			finally
			{
				this.Enabled = true;
				Cursor.Current = cursor;
			}
			#endregion
		}
		#endregion メソッド

	}
}
