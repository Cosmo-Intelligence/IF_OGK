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
using StatisticsPrint.Model.Print.DR.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.DR
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class DRWriter : PrintWriterBase
	{
		#region 定数

		#region COReprtsオブジェクト
		
		/// <summary>
		/// 区分名リスト
		/// </summary>
		private const string COREPORTS_KBN_NAME_LIST = "KBNName";

		#region 区分名

		/// <summary>
		/// 区分名胃透視
		/// </summary>
		private const string KBN_NAME_I_TOSHI = "胃透視";

		/// <summary>
		/// 区分名注腸
		/// </summary>
		private const string KBN_NAME_CHOKUCHO = "注腸";

		/// <summary>
		/// 区分名食道透視
		/// </summary>
		private const string KBN_NAME_SHOKUDO_TOSHI = "食道透視";

		/// <summary>
		/// 区分名小腸透視
		/// </summary>
		private const string KBN_NAME_SHOCHO_TOSHI = "小腸透視";

		/// <summary>
		/// 区分名DIC
		/// </summary>
		private const string KBN_NAME_DIC = "DIC";

		/// <summary>
		/// 区分名ERCP
		/// </summary>
		private const string KBN_NAME_ERCP = "ERCP";

		/// <summary>
		/// 区分名PTCD・膿瘍ドレナージ
		/// </summary>
		private const string KBN_NAME_PTCD = "PTCD・膿瘍ドレナージ";

		/// <summary>
		/// 区分名チューブ造影・交換
		/// </summary>
		private const string KBN_NAME_TUBE_ZOEI = "チューブ造影・交換";

		/// <summary>
		/// 区分名IVH挿入・交換
		/// </summary>
		private const string KBN_NAME_IVH = "IVH挿入・交換";

		/// <summary>
		/// 区分名婦人科
		/// </summary>
		private const string KBN_NAME_FUJINKA = "婦人科";

		/// <summary>
		/// 区分名ESWL
		/// </summary>
		private const string KBN_NAME_ESWL = "ESWL";

		/// <summary>
		/// 区分名その他の造影
		/// </summary>
		private const string KBN_NAME_SONOTA_ZOEI = "その他の造影";

		/// <summary>
		/// 区分名内視鏡的治療
		/// </summary>
		private const string KBN_NAME_NAISHIKYO = "内視鏡的治療";

		/// <summary>
		/// 区分名日別集計
		/// </summary>
		private const string KBN_NAME_TOTAL = "日別集計";
		#endregion

		#region 区分名インデックス

		/// <summary>
		/// 区分名インデックス胃透視
		/// </summary>
		private const int KBN_NAME_I_TOSHI_ROW = 0;

		/// <summary>
		/// 区分名インデックス注腸
		/// </summary>
		private const int KBN_NAME_CHOKUCHO_ROW = KBN_NAME_I_TOSHI_ROW + 1;

		/// <summary>
		/// 区分名インデックス食道透視
		/// </summary>
		private const int KBN_NAME_SHOKUDO_TOSHI_ROW = KBN_NAME_CHOKUCHO_ROW + 1;

		/// <summary>
		/// 区分名インデックス小腸透視
		/// </summary>
		private const int KBN_NAME_SHOCHO_TOSHI_ROW = KBN_NAME_SHOKUDO_TOSHI_ROW + 1;

		/// <summary>
		/// 区分名インデックスDIC
		/// </summary>
		private const int KBN_NAME_DIC_ROW = KBN_NAME_SHOCHO_TOSHI_ROW + 1;

		/// <summary>
		/// 区分名インデックスERCP
		/// </summary>
		private const int KBN_NAME_ERCP_ROW = KBN_NAME_DIC_ROW + 1;

		/// <summary>
		/// 区分名インデックスPTCD・膿瘍ドレナージ
		/// </summary>
		private const int KBN_NAME_PTCD_ROW = KBN_NAME_ERCP_ROW + 1;

		/// <summary>
		/// 区分名インデックスチューブ造影・交換
		/// </summary>
		private const int KBN_NAME_TUBE_ZOEI_ROW = KBN_NAME_PTCD_ROW + 1;

		/// <summary>
		/// 区分名インデックスIVH挿入・交換
		/// </summary>
		private const int KBN_NAME_IVH_ROW = KBN_NAME_TUBE_ZOEI_ROW + 1;

		/// <summary>
		/// 区分名インデックス婦人科
		/// </summary>
		private const int KBN_NAME_FUJINKA_ROW = KBN_NAME_IVH_ROW + 1;

		/// <summary>
		/// 区分名インデックスESWL
		/// </summary>
		private const int KBN_NAME_ESWL_ROW = KBN_NAME_FUJINKA_ROW + 1;

		/// <summary>
		/// 区分名インデックスその他の造影
		/// </summary>
		private const int KBN_NAME_SONOTA_ZOEI_ROW = KBN_NAME_ESWL_ROW + 1;

		/// <summary>
		/// 区分名インデックス内視鏡的治療
		/// </summary>
		private const int KBN_NAME_NAISHIKYO_ROW = KBN_NAME_SONOTA_ZOEI_ROW + 1;

		/// <summary>
		/// 区分名インデックス日別集計
		/// </summary>
		private const int KBN_NAME_TOTAL_ROW = KBN_NAME_NAISHIKYO_ROW + 1;

		#endregion

		#endregion

		#endregion

		#region フィールド
		#endregion

		#region プロパティ

		/// <summary>
		/// データクラス
		/// </summary>
		public DRItem Data
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
				string kbnName = f.CrObjects[COREPORTS_KBN_NAME_LIST].get_CrListCell(0, rowIndex).Text;
				//名称で付き合わせる
				DRItemDR kbnItem = Data.DRItemList.FirstOrDefault(
															em => em.KBN_NAME == kbnName);
				if (kbnItem == null)
				{
					f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = "0";
					continue;
				}
				WriteDetail(f, rowIndex, kbnItem);
				f.CrObjects[COREPORTS_MONTH_TOTAL].get_CrListCell(0, rowIndex).Text = kbnItem.Total; //小計(緊急種別)
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
			CrObject list = f.CrObjects[COREPORTS_KBN_NAME_LIST];
			list.get_CrListCell(0, KBN_NAME_I_TOSHI_ROW).Text = KBN_NAME_I_TOSHI;
			list.get_CrListCell(0, KBN_NAME_CHOKUCHO_ROW).Text = KBN_NAME_CHOKUCHO;
			list.get_CrListCell(0, KBN_NAME_SHOKUDO_TOSHI_ROW).Text = KBN_NAME_SHOKUDO_TOSHI;
			list.get_CrListCell(0, KBN_NAME_SHOCHO_TOSHI_ROW).Text = KBN_NAME_SHOCHO_TOSHI;
			list.get_CrListCell(0, KBN_NAME_DIC_ROW).Text = KBN_NAME_DIC;
			list.get_CrListCell(0, KBN_NAME_ERCP_ROW).Text = KBN_NAME_ERCP;
			list.get_CrListCell(0, KBN_NAME_PTCD_ROW).Text = KBN_NAME_PTCD;
			list.get_CrListCell(0, KBN_NAME_TUBE_ZOEI_ROW).Text = KBN_NAME_TUBE_ZOEI;
			list.get_CrListCell(0, KBN_NAME_IVH_ROW).Text = KBN_NAME_IVH;
			list.get_CrListCell(0, KBN_NAME_FUJINKA_ROW).Text = KBN_NAME_FUJINKA;
			list.get_CrListCell(0, KBN_NAME_ESWL_ROW).Text = KBN_NAME_ESWL;
			list.get_CrListCell(0, KBN_NAME_SONOTA_ZOEI_ROW).Text = KBN_NAME_SONOTA_ZOEI;
			list.get_CrListCell(0, KBN_NAME_NAISHIKYO_ROW).Text = KBN_NAME_NAISHIKYO;
			list.get_CrListCell(0, KBN_NAME_TOTAL_ROW).Text = KBN_NAME_TOTAL;
		}

		/// <summary>
		/// 明細行書き込み
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rowIndex"></param>
		/// <param name="kbnItem"></param>
		private void WriteDetail(CrForm f, int rowIndex, DRItemDR kbnItem)
		{
			DayLoopExecuter(delegate(DateTime currentDate)
			{
				DetailDayItem dayitem = kbnItem.DetailDayList.FirstOrDefault(
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

		/// <summary>
		/// 日別合計、総計を書き込み
		/// </summary>
		/// <param name="data"></param>
		/// <param name="rowIndex"></param>
		/// <param name="f"></param>
		/// <remarks>
		/// 部位分類と日別合計の使い方が違う
		/// </remarks>
		private void WriteTotalAndDay(DRItem data, int rowIndex, CrForm f)
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
