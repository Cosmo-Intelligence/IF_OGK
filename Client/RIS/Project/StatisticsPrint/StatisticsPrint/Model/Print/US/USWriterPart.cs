using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using CoReports;
using StatisticsPrint.Model.Print.US.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.US
{
	/// <summary>
	/// 帳票書き込み部分クラス基底
	/// </summary>
	public abstract class USWriterPart
	{
		#region 定数

		#region 区分名

		/// <summary>
		/// 区分名 頭頸部単純
		/// </summary>
		protected const string KBN_NAME_TOKEIBU_TANJUN = "頭頸部単純";

		/// <summary>
		/// 区分名 頭頸部造影
		/// </summary>
		protected const string KBN_NAME_TOKEIBU_ZOEI = "頭頸部造影";

		/// <summary>
		/// 区分名 躯幹部単純
		/// </summary>
		protected const string KBN_NAME_KUKANBU_TANJUN = "躯幹部単純";

		/// <summary>
		/// 区分名 躯幹部造影
		/// </summary>
		protected const string KBN_NAME_KUKANBU_ZOEI = "躯幹部造影";

		/// <summary>
		/// 区分名 四肢単純
		/// </summary>
		protected const string KBN_NAME_SHISHI_TANJUN = "四肢単純";

		/// <summary>
		/// 区分名 四肢造影
		/// </summary>
		protected const string KBN_NAME_SHISHI_ZOEI = "四肢造影";

		#endregion

		#region 区分名インデックス

		#region 一般、特殊
		/// <summary>
		/// 区分名インデックス 頭頸部単純
		/// </summary>
		protected const int KBN_NAME_TOKEIBU_TANJUN_ROW = 0;

		/// <summary>
		/// 区分名インデックス 頭頸部造影
		/// </summary>
		protected const int KBN_NAME_TOKEIBU_ZOEI_ROW = KBN_NAME_TOKEIBU_TANJUN_ROW + 1;

		/// <summary>
		/// 区分名インデックス 躯幹部単純
		/// </summary>
		protected const int KBN_NAME_KUKANBU_TANJUN_ROW = KBN_NAME_TOKEIBU_ZOEI_ROW + 1;

		/// <summary>
		/// 区分名インデックス 躯幹部造影
		/// </summary>
		protected const int KBN_NAME_KUKANBU_ZOEI_ROW = KBN_NAME_KUKANBU_TANJUN_ROW + 1;

		/// <summary>
		/// 区分名インデックス 四肢単純
		/// </summary>
		protected const int KBN_NAME_SHISHI_TANJUN_ROW = KBN_NAME_KUKANBU_ZOEI_ROW + 1;

		/// <summary>
		/// 区分名インデックス 四肢造影
		/// </summary>
		protected const int KBN_NAME_SHISHI_ZOEI_ROW = KBN_NAME_SHISHI_TANJUN_ROW + 1;
		#endregion

		#endregion


		#endregion

		#region フィールド

		/// <summary>
		/// 書き込みクラス
		/// </summary>
		private PrintWriterBase _writer;
		#endregion

		#region プロパティ

		protected PrintWriterBase Writer
		{
			get
			{
				return _writer;
			}
		}

		/// <summary>
		/// 最大行カウント
		/// </summary>
		protected abstract int MaxRowCount
		{
			get;
		}

		#region COReortsオブジェクト
		
		/// <summary>
		/// COReortsオブジェクト分類別計
		/// </summary>
		protected abstract string COReportsMonthTotal
		{
			get;
		}

		/// <summary>
		/// COReortsオブジェクト日付フォーーマット
		/// </summary>
		protected abstract string COReportsDayDetailNameFormat
		{
			get;
		}

		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public USWriterPart(PrintWriterBase writer)
		{
			_writer = writer;
		}

		#endregion

		#region メソッド

		#region public

		public abstract void Write(USItem item, CrForm f);

		public void ClearDetail(CrForm f)
		{
			string objectName = GetDetailListObjectName(1); //便宜的に初日指定
			CrObject crKbnList = f.CrObjects[objectName];
			for (int rowIndex = 0; rowIndex <= crKbnList.ListRows -1; rowIndex++)
			{
				f.CrObjects[COReportsMonthTotal].get_CrListCell(0, rowIndex).Text = "0"; //分類別計
				_writer.DayLoopExecuter(delegate(DateTime currentDate)
				{
					SetDetailByDay(currentDate.Day, rowIndex, "", f);
				});
			}
		}
		#endregion

		#region protected

		/// <summary>
		/// 明細行書き込み
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rowIndex"></param>
		/// <param name="detailDayList"></param>
		protected virtual void WriteDetail(CrForm f, int rowIndex, List<DetailDayItem> detailDayList)
		{
			_writer.DayLoopExecuter(delegate(DateTime currentDate)
			{
				DetailDayItem dayitem = detailDayList.FirstOrDefault(
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
		protected virtual void SetDetailByDay(int day, int rowIndex, string value, CrForm crForm)
		{
			string objectName = GetDetailListObjectName(day);
			crForm.CrObjects[objectName].get_CrListCell(0, rowIndex).Text = value;
		}

		/// <summary>
		/// COReportsの詳細部リストオブジェクト名
		/// </summary>
		protected virtual string GetDetailListObjectName(int day)
		{
			return string.Format(COReportsDayDetailNameFormat, day);
		}


		#endregion

		#endregion
	}
}
