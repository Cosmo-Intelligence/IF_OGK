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
using StatisticsPrint.Model.Print.Emergency.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Emergency
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class EmergencyWriter : PrintWriterBase
	{
		#region 定数

		#region COReprtsオブジェクト
		
		/// <summary>
		/// 区分名リスト
		/// </summary>
		private const string COREPORTS_KBN_NAME_LIST = "KBNName";

		#region 区分名
		
		/// <summary>
		/// 区分名一般撮影
		/// </summary>
		private const string KBN_NAME_IPPAN = "一般撮影";
		
		/// <summary>
		/// 区分名ポータブル
		/// </summary>
		private const string KBN_NAME_PORTABLE = "ポータブル";
		
		/// <summary>
		/// 区分名Ｘ線透視
		/// </summary>
		private const string KBN_NAME_X_DR = "Ｘ線透視";
		
		/// <summary>
		/// 区分名ＣＴ
		/// </summary>
		private const string KBN_NAME_CT = "ＣＴ";
		
		/// <summary>
		/// 区分名ＵＳ
		/// </summary>
		private const string KBN_NAME_US = "ＵＳ";

		/// <summary>
		/// 区分名ＵＣＴ
		/// </summary>
		private const string KBN_NAME_UCT = "ＵＣＴ";

		/// <summary>
		/// 区分名日別計
		/// </summary>
		private const string KBN_NAME_TOTAL = "日別計";
		#endregion

		#region 区分名インデックス

		/// <summary>
		/// 区分名インデックス一般撮影
		/// </summary>
		private const int KBN_NAME_IPPAN_ROW = 0;

		/// <summary>
		/// 区分名インデックスポータブル
		/// </summary>
		private const int KBN_NAME_PORTABLE_ROW = KBN_NAME_IPPAN_ROW + 1;

		/// <summary>
		/// 区分名インデックスＸ線透視
		/// </summary>
		private const int KBN_NAME_X_DR_ROW = KBN_NAME_PORTABLE_ROW + 1;

		/// <summary>
		/// 区分名インデックスＣＴ
		/// </summary>
		private const int KBN_NAME_CT_ROW = KBN_NAME_X_DR_ROW + 1;

		/// <summary>
		/// 区分名インデックスＵＳ
		/// </summary>
		private const int KBN_NAME_US_ROW = KBN_NAME_CT_ROW + 1;

		/// <summary>
		/// 区分名インデックスＵＣＴ
		/// </summary>
		private const int KBN_NAME_UCT_ROW = KBN_NAME_US_ROW + 1;

		/// <summary>
		/// 区分名インデックス合計
		/// </summary>
		private const int KBN_NAME_TOTAL_ROW = KBN_NAME_UCT_ROW + 1;

		#endregion

		#endregion

		#endregion

		#region フィールド
		#endregion

		#region プロパティ

		/// <summary>
		/// データクラス
		/// </summary>
		public EmergencyItem Data
		{
			get;
			set;
		}

		/// <summary>
		/// 最大行
		/// </summary>
		protected override int MaxRowCount
		{
			get
			{
				return KBN_NAME_TOTAL_ROW + 1;
			}

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
			WriteConst(f);

			for (int rowIndex = 0; rowIndex < MaxRowCount; rowIndex++)
			{
				string emergencyName = f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, rowIndex).Text;
				//名称で付き合わせる
				EmergencyItemEmergency emergencyitem = Data.EmergencyItemList.FirstOrDefault(
															em => em.KBN_NAME == emergencyName);
				if (emergencyitem == null)
				{
					f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = "0";
					continue;
				}
				WriteDetail(f, rowIndex, emergencyitem);
				f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = emergencyitem.Total; //小計(緊急種別)
			}
			WriteTotalAndDay(Data, KBN_NAME_TOTAL_ROW, f); //小計(日付)、総計を書き込み
			PageCount = PrintoutCurrentPage(f, PageCount);
		}

		#endregion

		#region protected
		
		/// <summary>
		/// 明細行クリア
		/// </summary>
		/// <param name="f"></param>
		protected override void ClearDetailFields(CrForm f)
		{
			CrObject crExamKindList = f.CrObjects[COREPORTS_KBN_NAME_LIST];
			for (int rowIndex = 0; rowIndex <= crExamKindList.ListRows -1; rowIndex++)
			{
				f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, rowIndex).Text = "";
				f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = "";

				DayLoopExecuter(delegate(DateTime currentDate)
				{

					SetDetailByDay(currentDate.Day, rowIndex, "", f);
				});
			}
		}

		/// <summary>
		/// ページ数書き込み
		/// </summary>
		/// <param name="pageCount"></param>
		protected override void WritePageCount(int pageCount, CrForm f)
		{
			//ページ数なし
		}
		#endregion

		#region private

		/// <summary>
		/// 定数書き込み
		/// </summary>
		/// <param name="f"></param>
		private void WriteConst(CrForm f)
		{
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_IPPAN_ROW).Text = KBN_NAME_IPPAN;
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_PORTABLE_ROW).Text = KBN_NAME_PORTABLE;
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_X_DR_ROW).Text = KBN_NAME_X_DR;
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_CT_ROW).Text = KBN_NAME_CT;
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_US_ROW).Text = KBN_NAME_US;
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_UCT_ROW).Text = KBN_NAME_UCT;
		}

		/// <summary>
		/// 明細行書き込み
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rowIndex"></param>
		/// <param name="emergencyItem"></param>
		private void WriteDetail(CrForm f, int rowIndex, EmergencyItemEmergency emergencyItem)
		{
			DayLoopExecuter(delegate(DateTime currentDate)
			{
				DetailDayItem dayitem = emergencyItem.DetailDayList.FirstOrDefault(
					d => d.EXAMENDDATE_YYYYMMDD10 == currentDate.ToString("yyyy/MM/dd"));
				if (dayitem == null)
				{
					return;
				}
				SetDetailByDay(currentDate.Day, rowIndex, dayitem.CNT, f);
			});
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
			string objectName = string.Format(PrintWriterBase.COREPORTS_DAY_DETAIL_NAME_FORMAT, day);
			crForm.CrObjects[objectName].get_CrListCell(0, rowIndex).Text = value;
		}

		/// <summary>
		/// 日別合計、総計を書き込み
		/// </summary>
		/// <param name="data"></param>
		/// <param name="rowIndex"></param>
		/// <param name="f"></param>
		/// <remarks>
		/// 部位分類と日別合計の使い方が違う
		/// </remarks>
		private void WriteTotalAndDay(EmergencyItem data, int rowIndex, CrForm f)
		{
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, rowIndex).Text = KBN_NAME_TOTAL;
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
