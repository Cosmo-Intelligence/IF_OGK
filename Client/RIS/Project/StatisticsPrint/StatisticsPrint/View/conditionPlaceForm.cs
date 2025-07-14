using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StatisticsPrint.Properties;
using RISODPLibrary.Lib.Utils;
using StatisticsPrint.Model;
using StatisticsPrint.Data;
using StatisticsPrint.Model.FormHelper;

namespace StatisticsPrint.View
{
	/// <summary>
	/// 抽出(実施場所)
	/// </summary>
	public partial class conditionPlaceForm : conditionForm
	{
		#region プロパティ

		/// <summary>
		/// 実施場所コンボにALLを追加するか
		/// </summary>
		public bool IsAddAll
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public conditionPlaceForm()
		{
			InitializeComponent();
			IsAddAll = false;
		}
		#endregion

		#region イベント
		
		/// <summary>
		/// 実施場所の評価後
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void placeComboBox_Validated(object sender, EventArgs e)
		{
			this.conditionErrorProvider.SetError((ComboBox)sender, null);
		}

		/// <summary>
		/// 実施場所の評価
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void placeComboBox_Validating(object sender, CancelEventArgs e)
		{
			if (placeComboBox.SelectedIndex < 0)
			{
				this.conditionErrorProvider.SetError((ComboBox)sender, "出力する実施場所を選択してください");
				e.Cancel = true;
				return;
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
			ConditionPrintPlace condition = new ConditionPrintPlace();
			base.SetCondition(condition);

			condition.PlaceID = (string)placeComboBox.SelectedValue;
			condition.PlaceName = (string)placeComboBox.Text;
			return condition;
		}
		#endregion


		protected override void Init()
		{
			base.Init();
			ConditionPlaceFormHelper helper = new ConditionPlaceFormHelper();

			#region 実施場所の設定

			placeComboBox.DisplayMember = "PLACE_NAME";
			placeComboBox.ValueMember = "PLACE_ID";
			IDbConnection cn = ODPConnectionUtils.Connect(Settings.Default.ConnectionString);
			try
			{
				placeComboBox.DataSource = helper.GetPlaceNameRoomList(cn, IsAddAll);
			}
			finally
			{
				cn.Close();
			}
			if (placeComboBox.Items.Count > 0)
			{
				placeComboBox.SelectedIndex = 0;
			}
			#endregion
		}
		#endregion メソッド

	}
}
