using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoReports;
using CoReportsCore;
using StatisticsPrint.Model.COReports;
using StatisticsPrint.Data;
using RISCommonLibrary.Lib.Utils;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.BuiBunrui.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.BuiBunrui
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class BuiBunruiWriter : PrintWriterBase
	{
		#region 定数

		#region COReprtsオブジェクト

		/// <summary>
		/// アイテムリスト
		/// </summary>
		private const string COREPORTS_EXAM_KIND_LIST = "ExamKindList";

		/// <summary>
		/// 部位分類リスト
		/// </summary>
		private const string COREPORTS_BUIBUNRUI_NAME = "BuiBunruiName";

		#endregion

		#endregion

		#region フィールド
		#endregion

		#region プロパティ

		/// <summary>
		/// 部位分類データクラス
		/// </summary>
		public BuiBunruiItem Data
		{
			get;
			set;
		}

		#endregion

		#region メソッド

		#region public
		
		/// <summary>
		/// 書き込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void Write(object sender, PrintEventArgs e)
		{
			CrForm f = e.crForm;
			//フィールドを全てクリアします
			f.ClearAllFields();

			WriteHeader(Condition, f);

			int rowIndex = 0;
			foreach (BuiBunruiItemKensaType kensaTypeItem in Data.KensaTypeItemList)
			{
				foreach (BuiBunruiItemBuiBunrui buiBunruiitem in kensaTypeItem.BuiBunruiItemList)
				{
					WriteDetail(f, rowIndex, kensaTypeItem, buiBunruiitem);
					f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = buiBunruiitem.Total; //小計(検査種別+部位分類)
					rowIndex = IncRow(rowIndex, f);
				}
				WriteSubtotalKensaTypeAndDay(kensaTypeItem, rowIndex, f); //小計(検査種別+日付)を書き込み

				rowIndex = IncRow(rowIndex, f);
				rowIndex = IncRow(rowIndex, f); //小計(検査種別+日付)のあとは1行あける
			}

			WriteTotalAndDay(Data, rowIndex, f); //小計(日付)、総計を書き込み
			PageCount = PrintoutCurrentPage(f, PageCount);
		}

		#endregion

		#region protected
		
		/// <summary>
		/// ヘッダ書き込み
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="crForm"></param>
		protected override void WriteHeader(ConditionPrint condition, CrForm crForm)
		{
			base.WriteHeader(condition, crForm);
			WriteHeaderPlace(condition, crForm);
		}

		/// <summary>
		/// 明細行クリア
		/// </summary>
		/// <param name="f"></param>
		protected override void ClearDetailFields(CrForm f)
		{
			CrObject crExamKindList = f.CrObjects[COREPORTS_EXAM_KIND_LIST];
			for (int rowIndex = 0; rowIndex <= crExamKindList.ListRows -1; rowIndex++)
			{
				f.CrObjects[COREPORTS_EXAM_KIND_LIST].get_CrListCell(0, rowIndex).Text = "";
				f.CrObjects[COREPORTS_BUIBUNRUI_NAME].get_CrListCell(0, rowIndex).Text = "";
				f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = "";

				DayLoopExecuter(delegate(DateTime currentDate)
				{

					SetDetailByDay(currentDate.Day, rowIndex, "", f);
				});
			}
		}
		#endregion

		#region private

		/// <summary>
		/// 明細行書き込み
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rowIndex"></param>
		/// <param name="kensaTypeItem"></param>
		/// <param name="buiBunruiitem"></param>
		private void WriteDetail(CrForm f, int rowIndex, BuiBunruiItemKensaType kensaTypeItem,
			BuiBunruiItemBuiBunrui buiBunruiitem)
		{
			f.CrObjects[COREPORTS_EXAM_KIND_LIST].get_CrListCell(0, rowIndex).Text = kensaTypeItem.KENSATYPE_NAME;
			f.CrObjects[COREPORTS_BUIBUNRUI_NAME].get_CrListCell(0, rowIndex).Text = buiBunruiitem.BUIBUNRUI_NAME;

			DayLoopExecuter(delegate(DateTime currentDate)
			{
				DetailDayItem dayitem = buiBunruiitem.DetailDayList.FirstOrDefault(
					d => d.EXAMENDDATE_YYYYMMDD10 == currentDate.ToString("yyyy/MM/dd"));
				if (dayitem == null)
				{
					return;
				}
				SetDetailByDay(currentDate.Day, rowIndex, dayitem.CNT, f);
			});
		}

		/// <summary>
		/// ヘッダ場所書き込み
		/// </summary>
		/// <param name="condtion"></param>
		/// <param name="crForm"></param>
		private void WriteHeaderPlace(ConditionPrint condtion, CrForm crForm)
		{
			ConditionPrintPlace conditionPlace = (ConditionPrintPlace)condtion;
			crForm.CrObjects[COREPORTS_PLACE_NAME].Text = conditionPlace.PlaceName;
		}

		/// <summary>
		/// 日付毎詳細設定
		/// </summary>
		/// <param name="day"></param>
		/// <param name="rowIndex"></param>
		/// <param name="value"></param>
		/// <param name="crForm"></param>
		private void SetDetailByDay(int day, int rowIndex, string value, CrForm crForm)
		{
			string objectName = string.Format(COREPORTS_DAY_DETAIL_NAME_FORMAT, day);
			crForm.CrObjects[objectName].get_CrListCell(0, rowIndex).Text = value;
		}

		/// <summary>
		/// 小計(検査種別+日付)小計を書き込み
		/// </summary>
		/// <param name="kensaTypeItem"></param>
		/// <param name="rowIndex"></param>
		/// <param name="f"></param>
		private void WriteSubtotalKensaTypeAndDay(BuiBunruiItemKensaType kensaTypeItem, int rowIndex, CrForm f)
		{
			f.CrObjects[COREPORTS_BUIBUNRUI_NAME].get_CrListCell(0, rowIndex).Text = "小計";

			DayLoopExecuter(delegate(DateTime currentDate)
			{
				DetailDayItem dayitem = kensaTypeItem.DetailDayList.FirstOrDefault(
					d => d.EXAMENDDATE_YYYYMMDD10 == currentDate.ToString("yyyy/MM/dd"));
				if (dayitem == null)
				{
					SetDetailByDay(currentDate.Day, rowIndex, "0", f);
					return;
				}
				SetDetailByDay(currentDate.Day, rowIndex, dayitem.CNT, f);
			});
			f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = kensaTypeItem.Total;
		}

		/// <summary>
		/// 小計(日付)、総計を書き込み
		/// </summary>
		/// <param name="data"></param>
		/// <param name="rowIndex"></param>
		/// <param name="f"></param>
		private void WriteTotalAndDay(BuiBunruiItem data, int rowIndex, CrForm f)
		{
			f.CrObjects[COREPORTS_BUIBUNRUI_NAME].get_CrListCell(0, rowIndex).Text = "<日別合計>";
			DayLoopExecuter(delegate(DateTime currentDate)
			{
				DetailDayItem dayitem = data.DetailDayList.FirstOrDefault(
					d => d.EXAMENDDATE_YYYYMMDD10 == currentDate.ToString("yyyy/MM/dd"));
				if (dayitem == null)
				{
					SetDetailByDay(currentDate.Day, rowIndex, "0", f);
					return;
				}
				SetDetailByDay(currentDate.Day, rowIndex, dayitem.CNT, f);
			});
			f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = data.Total;
		}
		#endregion

		#endregion
	}
}
