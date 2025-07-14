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
using StatisticsPrint.Model.Print.CDFilm.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.CDFilm
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class CDFilmWriter : PrintWriterBase
	{
		#region 定数

		#region COReprtsオブジェクト
		
		/// <summary>
		/// 区分名リスト
		/// </summary>
		private const string COREPORTS_KBN_NAME_LIST = "KBNName";

		#region 区分名

		/// <summary>
		/// 区分名Film取込
		/// </summary>
		private const string KBN_NAME_FILM_READ = "Film取込";

		/// <summary>
		/// 区分名Film出力
		/// </summary>
		private const string KBN_NAME_FILM_WRITE = "Film出力";

		/// <summary>
		/// 区分名CD取込
		/// </summary>
		private const string KBN_NAME_CD_READ = "CD取込";

		/// <summary>
		/// 区分名CD出力
		/// </summary>
		private const string KBN_NAME_CD_WRITE = "CD出力";

		/// <summary>
		/// 区分名日別集計
		/// </summary>
		private const string KBN_NAME_TOTAL = "日別集計";
		#endregion

		#region 区分名インデックス

		/// <summary>
		/// 区分名インデックスFilm取込
		/// </summary>
		private const int KBN_NAME_FILM_READ_ROW = 0;

		/// <summary>
		/// 区分名インデックスFilm出力
		/// </summary>
		private const int KBN_NAME_FILM_WRITE_ROW = KBN_NAME_FILM_READ_ROW + 1;
		/// <summary>
		/// 区分名インデックスCD取込
		/// </summary>
		private const int KBN_NAME_CD_READ_ROW = KBN_NAME_FILM_WRITE_ROW + 1;

		/// <summary>
		/// 区分名インデックスCD出力
		/// </summary>
		private const int KBN_NAME_CD_WRITE_ROW = KBN_NAME_CD_READ_ROW + 1;

		/// <summary>
		/// 区分名インデックス合計
		/// </summary>
		private const int KBN_NAME_TOTAL_ROW = KBN_NAME_CD_WRITE_ROW + 1;

		#endregion

		#endregion

		#endregion

		#region フィールド
		#endregion

		#region プロパティ

		/// <summary>
		/// データクラス
		/// </summary>
		public CDFilmItem Data
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
				CDFilmItemCDFilm emergencyitem = Data.CDFilmItemList.FirstOrDefault(
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
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_FILM_READ_ROW).Text = KBN_NAME_FILM_READ;
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_FILM_WRITE_ROW).Text = KBN_NAME_FILM_WRITE;
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_CD_READ_ROW).Text = KBN_NAME_CD_READ;
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_CD_WRITE_ROW).Text = KBN_NAME_CD_WRITE;
			f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, KBN_NAME_TOTAL_ROW).Text = KBN_NAME_TOTAL;
		}

		/// <summary>
		/// 明細行書き込み
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rowIndex"></param>
		/// <param name="emergencyItem"></param>
		private void WriteDetail(CrForm f, int rowIndex, CDFilmItemCDFilm emergencyItem)
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
		private void WriteTotalAndDay(CDFilmItem data, int rowIndex, CrForm f)
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
