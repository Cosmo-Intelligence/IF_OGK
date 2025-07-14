using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using RISCommonLibrary.Lib.Utils;
using StatisticsPrint.Data;
using StatisticsPrint.Model.COReports;
using StatisticsPrint.Model.Print.Bui.All;
using StatisticsPrint.Model.Print.Bui.Data;
using StatisticsPrint.Model.Print.Common;


namespace StatisticsPrint.Model.Print.Bui
{
	/// <summary>
	/// 撮影検査数一覧（部位分類）印刷
	/// </summary>
	internal class BuiPrint : PrintBase
	{
		#region フィールド名

		private BuiPrintExecutor _executor;
		#endregion

		#region プロパティ
		/// <summary>
		/// ドキュメント名
		/// </summary>
		public override string DocumentName
		{
			get
			{
				return "撮影検査数一覧（主行為）";
			}
		}

		/// <summary>
		/// フォームファイルパス
		/// </summary>
		public override string FormFilePath
		{
			get
			{
				return ConfigurationManager.AppSettings["FormFileBUI"].StringToString();
			}
		}
		#endregion

		#region メソッド

		#region public
		
		/// <summary>
		/// 印刷する
		/// </summary>
		/// <param name="condition"></param>
		public override void Print(ConditionPrint condition)
		{
			ConditionPrintPlace conditionPlace = condition as ConditionPrintPlace;
			if (conditionPlace == null)
			{
				return ;
			}

			_executor = GetExecutor(conditionPlace.PlaceID);
			if (_executor == null)
			{
				throw new NotImplementedException(
					string.Format("未定義の印刷実行クラスです。{0}", conditionPlace.PlaceID));
			}
			BuiItem data = _executor.GetResultData(condition);
			List<IBuiItemChild> list = data.ChildItemList;
			if (list.Count < 1)
			{
				_log.Debug("出力データがありません");
				MessageBox.Show("出力データがありません。", "情報", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
				return;
			}

			using (COReportsHelper helper = new COReportsHelper())
			{
				BuiWriter writer = _executor.CreateBuiWriter();
				writer.Condition = conditionPlace;
				writer.Data = data;

				PrintExecute(conditionPlace, helper, writer);
			}
		}

		#endregion

		#region private

		private BuiPrintExecutor GetExecutor(string placeID)
		{
			if (placeID == PlaceData.PLACE_ID_ALL)
			{
				return new BuiPrintExecutorAll();
			}
			return new BuiPrintExecutor();
		}
		#endregion

		#endregion メソッド
	}
}
