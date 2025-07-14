using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using StatisticsPrint.Model.COReports;
using StatisticsPrint.Model.Print.Common;
using RISCommonLibrary.Lib.Utils;
using CoReports;

namespace StatisticsPrint.Model.Print.Common
{
	/// <summary>
	/// フォームファイル書き込み
	/// </summary>
	public abstract class PrintWriterBase : IPrintWriter
	{
		#region 定数

		#region COReprtsオブジェクト

		/// <summary>
		/// 年月
		/// </summary>
		public const string COREPORTS_MONTH = "Month";

		/// <summary>
		/// 日付ラベル
		/// </summary>
		public const string COREPORTS_DAY_LABEL = "DayLABEL";

		/// <summary>
		/// 実施場所
		/// </summary>
		public const string COREPORTS_PLACE_NAME = "PlaceName";

		/// <summary>
		/// 検査室
		/// </summary>
		public const string COREPORTS_EXAM_ROOM_NAME = "ExamRoomName";

		/// <summary>
		/// ページ
		/// </summary>
		public const string COREPORTS_PAGE = "Page";

		/// <summary>
		/// 明細行リスト
		/// </summary>
		public const string COREPORTS_MONTH_TOTAL = "MonthTotal";

		/// <summary>
		/// 日付名細部フォーマット
		/// </summary>
		public const string COREPORTS_DAY_DETAIL_NAME_FORMAT = "Day{0}";

		#endregion

		#endregion

		#region デリゲート
		/// <summary>
		/// 日付ループデリゲート
		/// </summary>
		/// <param name="currentDate"></param>
		/// <remarks>TimeEventArgs型のオブジェクトを返すようにする</remarks>
		public delegate void DayLoopFunc(DateTime currentDate);

		#endregion

		#region フィールド

		/// <summary>
		/// ページ数
		/// </summary>
		private int _pageCount = 0;

		#endregion

		#region プロパティ

		/// <summary>
		/// 検索条件
		/// </summary>
		public ConditionPrint Condition
		{
			get;
			set;
		}

		public int PageCount
		{
			get
			{
				return _pageCount;
			}
			set
			{
				_pageCount = value;
			}
		}

		/// <summary>
		/// 最大行
		/// </summary>
		protected virtual int MaxRowCount
		{
			get
			{
				const int MAX_ROW_COUNT = 40;
				return MAX_ROW_COUNT;
			}

		}
		#endregion

		#region メソッド

		/// <summary>
		/// 書き込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public abstract void Write(object sender, PrintEventArgs e);

		/// <summary>
		/// 日付でループする
		/// </summary>
		public void DayLoopExecuter(DayLoopFunc func)
		{
			DateTime currentDate = Condition.Nengetsu;
			DateTime endDate = Condition.Nengetsu.EndOfMonth();
			while (currentDate <= endDate)
			{
				func(currentDate);
				currentDate = currentDate.AddDays(1);
			}
		}


		#region ヘッダ
		
		/// <summary>
		/// ヘッダ書き込み
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="crForm"></param>
		protected virtual void WriteHeader(ConditionPrint condition, CrForm crForm)
		{
			WriteHeaderMonth(condition, crForm);
		}

		/// <summary>
		/// 月ヘッダ書き込み
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="crForm"></param>
		protected virtual void WriteHeaderMonth(ConditionPrint condition, CrForm crForm)
		{
			crForm.CrObjects[COREPORTS_MONTH].Text = condition.Nengetsu.ToString("yyyy 年 MM 月");

			DayLoopExecuter(delegate(DateTime currentDate)
			{
				crForm.CrObjects[COREPORTS_DAY_LABEL].get_CrListCell(currentDate.Day -1, 0).Text = currentDate.Day.ToString();
			});
		}

		#endregion

		/// <summary>
		/// 明細行クリア
		/// </summary>
		/// <param name="f"></param>
		protected abstract void ClearDetailFields(CrForm f);

		/// <summary>
		/// 行インクリメント
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="f"></param>
		/// <returns></returns>
		protected int IncRow(int rowIndex, CrForm f)
		{
			int newIndex = rowIndex;
			newIndex++;
			if (newIndex < MaxRowCount)
			{
				return newIndex;
			}
			return NewPage(f);
		}

		/// <summary>
		/// 改ページ
		/// </summary>
		/// <param name="f"></param>
		/// <returns></returns>
		protected int NewPage(CrForm f)
		{
			_pageCount = PrintoutCurrentPage(f, _pageCount);

			ClearDetailFields(f);
			return 0;
		}

		/// <summary>
		/// 1ページ印刷する
		/// </summary>
		/// <param name="f"></param>
		protected int PrintoutCurrentPage(CrForm f, int pageCount)
		{
			int newPageCount = pageCount;
			newPageCount++;
			WritePageCount(newPageCount, f);

			int retPrintout = f.PrintOut();
			if (retPrintout != (int)corError.corNoError)
			{
				throw new COReportsException(string.Format("PrintOutで例外が発生しました={0}:{1}", retPrintout,
					COReportsUtils.GetErrMessage(retPrintout)));
			}
			return newPageCount;
		}

		/// <summary>
		/// ページ数書き込み
		/// </summary>
		/// <param name="pageCount"></param>
		protected virtual void WritePageCount(int pageCount, CrForm f)
		{
			f.CrObjects[COREPORTS_PAGE].Text = string.Format("{0} Page", pageCount);
		}

		#endregion
	}
}
