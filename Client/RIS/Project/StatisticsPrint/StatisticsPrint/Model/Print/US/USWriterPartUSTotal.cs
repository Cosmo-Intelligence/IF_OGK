using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.US.Data;
using CoReports;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.US
{
	/// <summary>
	/// 超音波総件数
	/// </summary>
	public class USWriterPartUSTotal : USWriterPart
	{
		#region 定数

		/// <summary>
		/// 超音波総件数名細部
		/// </summary>
		public const string COREPORTS_DAY_TOTAL = "DayTotal";

		/// <summary>
		/// 超音波総件数総計
		/// </summary>
		private const string COREPORTS_MONTH_TOTAL_US = "USMonthTotal";


		#endregion 定数


		#region プロパティ

		/// <summary>
		/// 最大行カウント
		/// </summary>
		protected override int MaxRowCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		#region COReortsオブジェクト

		/// <summary>
		/// COReortsオブジェクト分類別計
		/// </summary>
		protected override string COReportsMonthTotal
		{
			get
			{
				return COREPORTS_MONTH_TOTAL_US;
			}
		}

		/// <summary>
		/// COReortsオブジェクト日付フォーーマット
		/// </summary>
		protected override string COReportsDayDetailNameFormat
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public USWriterPartUSTotal(PrintWriterBase writer)
			: base(writer)
		{

		}

		#endregion

		#region メソッド

		public override void Write(USItem item, CrForm f)
		{
			int rowIndex = 0;
			WriteDetail(f, rowIndex, item.DetailDayList);
			f.CrObjects[COReportsMonthTotal].get_CrListCell(0, rowIndex).Text = item.Total; //総計
		}

		#region protected

		/// <summary>
		/// COReportsの詳細部リストオブジェクト名
		/// </summary>
		protected override string GetDetailListObjectName(int day)
		{
			return COREPORTS_DAY_TOTAL;
		}
		
		#endregion

		#region private

		/// <summary>
		/// 明細行書き込み
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rowIndex"></param>
		/// <param name="detailDayList"></param>
		protected override void WriteDetail(CrForm f, int rowIndex, List<DetailDayItem> detailDayList)
		{
			Writer.DayLoopExecuter(delegate(DateTime currentDate)
			{
				DetailDayItem dayitem = detailDayList.FirstOrDefault(
					d => d.EXAMENDDATE_YYYYMMDD10 == currentDate.ToString("yyyy/MM/dd"));
				if (dayitem == null)
				{
					SetDetailByDay(currentDate.Day, rowIndex, "0", f);
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
		protected override void SetDetailByDay(int day, int rowIndex, string value, CrForm crForm)
		{
			string objectName = GetDetailListObjectName(day);
			crForm.CrObjects[objectName].get_CrListCell(day - 1, rowIndex).Text = value;
		}

		#endregion

		#endregion


	}
}
