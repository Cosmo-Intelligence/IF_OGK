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
using StatisticsPrint.Model.Print.RI.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.RI
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class RIWriter : PrintWriterBase
	{
		#region 定数

		#region COReprtsオブジェクト
		
		/// <summary>
		/// 区分名リスト
		/// </summary>
		private const string COREPORTS_KBN_NAME_LIST = "KBNName";

		#region 区分名
		
		/// <summary>
		/// 区分名SPECT
		/// </summary>
		private const string KBN_NAME_SPECT = "SPECT";
		
		/// <summary>
		/// 区分名部分(動態)
		/// </summary>
		private const string KBN_NAME_BUBUN_DOU = "部分(動態)";
		
		/// <summary>
		/// 区分名部分(静態)
		/// </summary>
		private const string KBN_NAME_BUBUN_SEI = "部分(静態)";
		
		/// <summary>
		/// 区分名全身
		/// </summary>
		private const string KBN_NAME_ZENSHIN = "全身";
		
		/// <summary>
		/// 区分名その他
		/// </summary>
		private const string KBN_NAME_SONOTA = "その他";

		#endregion

		#region 区分名インデックス

		/// <summary>
		/// 区分名インデックスSPECT
		/// </summary>
		private const int KBN_NAME_SPECT_ROW = 0;

		/// <summary>
		/// 区分名インデックス部分(動態)
		/// </summary>
		private const int KBN_NAME_BUBUN_DOU_ROW = KBN_NAME_SPECT_ROW + 1;

		/// <summary>
		/// 区分名インデックス部分(静態)
		/// </summary>
		private const int KBN_NAME_BUBUN_SEI_ROW = KBN_NAME_BUBUN_DOU_ROW + 1;

		/// <summary>
		/// 区分名インデックス全身
		/// </summary>
		private const int KBN_NAME_ZENSHIN_ROW = KBN_NAME_BUBUN_SEI_ROW + 1;

		/// <summary>
		/// 区分名インデックスその他
		/// </summary>
		private const int KBN_NAME_SONOTA_ROW = KBN_NAME_ZENSHIN_ROW + 1;

		#endregion

		#endregion

		#endregion

		#region フィールド
		#endregion

		#region プロパティ

		/// <summary>
		/// データクラス
		/// </summary>
		public RIItem Data
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
				return KBN_NAME_SONOTA_ROW + 1;
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
				string kbnName = f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, rowIndex).Text;
				if (string.IsNullOrEmpty(kbnName))
				{
					continue; //帳票側に区分名が無いときは空行
				}
				//名称で付き合わせる
				RIItemRI kbnItem = Data.RIItemList.FirstOrDefault(
															em => em.KBN_NAME == kbnName);
				if (kbnItem == null)
				{
					f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = "0";
					continue;
				}
				WriteDetail(f, rowIndex, kbnItem);
				f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = kbnItem.Total; //小計(区分)
			}
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
			CrObject list = f.CrObjects[COREPORTS_KBN_NAME_LIST];
			list.get_CrListCell(0, KBN_NAME_SPECT_ROW).Text = KBN_NAME_SPECT;
			list.get_CrListCell(0, KBN_NAME_BUBUN_DOU_ROW).Text = KBN_NAME_BUBUN_DOU;
			list.get_CrListCell(0, KBN_NAME_BUBUN_SEI_ROW).Text = KBN_NAME_BUBUN_SEI;
			list.get_CrListCell(0, KBN_NAME_ZENSHIN_ROW).Text = KBN_NAME_ZENSHIN;
			list.get_CrListCell(0, KBN_NAME_SONOTA_ROW).Text = KBN_NAME_SONOTA;
		}

		/// <summary>
		/// 明細行書き込み
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rowIndex"></param>
		/// <param name="emergencyItem"></param>
		private void WriteDetail(CrForm f, int rowIndex, RIItemRI emergencyItem)
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
			string objectName = string.Format(COREPORTS_DAY_DETAIL_NAME_FORMAT, day);
			crForm.CrObjects[objectName].get_CrListCell(0, rowIndex).Text = value;
		}

		#endregion

		#endregion
	}
}
