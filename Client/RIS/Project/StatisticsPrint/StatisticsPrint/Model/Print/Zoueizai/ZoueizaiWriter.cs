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
using StatisticsPrint.Model.Print.Zoueizai.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Zoueizai
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class ZoueizaiWriter : PrintWriterBase
	{
		#region 定数

		#region COReprtsオブジェクト

		/// <summary>
		/// 造影剤リスト
		/// </summary>
		private const string COREPORTS_ZOUEIZAI_NAME = "ZoueizaiName";

		/// <summary>
		/// 単位リスト
		/// </summary>
		private const string COREPORTS_UNIT = "Unit";

		#endregion

		#endregion

		#region フィールド
		#endregion

		#region プロパティ

		/// <summary>
		/// 造影剤データクラス
		/// </summary>
		public ZoueizaiItem Data
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
			foreach (ZoueizaiItemZoueizai zoueizaiItem in Data.ZoueizaiItemList)
			{
				WriteDetail(f, rowIndex, zoueizaiItem);

				f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = zoueizaiItem.Total; //小計
				rowIndex = IncRow(rowIndex, f);
			}
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
			CrObject crNameList = f.CrObjects[COREPORTS_ZOUEIZAI_NAME];
			for (int rowIndex = 0; rowIndex <= crNameList.ListRows -1; rowIndex++)
			{
				f.CrObjects[COREPORTS_ZOUEIZAI_NAME].get_CrListCell(0, rowIndex).Text = "";
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
		/// <param name="zoueizaiItem"></param>
		private void WriteDetail(CrForm f, int rowIndex, ZoueizaiItemZoueizai zoueizaiItem)
		{
			f.CrObjects[COREPORTS_ZOUEIZAI_NAME].get_CrListCell(0, rowIndex).Text = zoueizaiItem.ZOUEIZAI_NAME;
			f.CrObjects[COREPORTS_UNIT].get_CrListCell(0, rowIndex).Text = zoueizaiItem.ZOUEIZAITANNI_NAME;

			DayLoopExecuter(delegate(DateTime currentDate)
			{
				DetailDayItem dayitem = zoueizaiItem.DetailDayList.FirstOrDefault(
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
		#endregion

		#endregion
	}
}
