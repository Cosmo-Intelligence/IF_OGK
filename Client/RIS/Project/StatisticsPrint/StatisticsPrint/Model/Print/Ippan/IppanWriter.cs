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
using StatisticsPrint.Model.Print.Ippan.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Ippan
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class IppanWriter : PrintWriterBase
	{
		#region 定数

		#region COReprtsオブジェクト
		
		/// <summary>
		/// 区分名リスト
		/// </summary>
		private const string COREPORTS_KBN_NAME_LIST = "KBNName";

		#region 区分名
		
		/// <summary>
		/// 区分名胸腹部撮影
		/// </summary>
		private const string KBN_NAME_KYOFUKUBU = "胸腹部撮影";
		
		/// <summary>
		/// 区分名骨撮影他
		/// </summary>
		private const string KBN_NAME_HONE = "骨撮影他";
		
		/// <summary>
		/// 区分名断層撮影
		/// </summary>
		private const string KBN_NAME_DANSO = "断層撮影";
		
		/// <summary>
		/// 区分名乳房撮影
		/// </summary>
		private const string KBN_NAME_NYUBO = "乳房撮影";
		
		/// <summary>
		/// 区分名健診業務
		/// </summary>
		private const string KBN_NAME_KENSHIN = "健診業務";

		/// <summary>
		/// 区分名ポータブル
		/// </summary>
		private const string KBN_NAME_PORTABLE = "ポータブル";

		/// <summary>
		/// 区分名ﾗｲﾅｯｸｸﾞﾗﾌｨｰ
		/// </summary>
		private const string KBN_NAME_LINAC = "ﾗｲﾅｯｸｸﾞﾗﾌｨｰ";

		/// <summary>
		/// 区分名ｼｭﾐﾚｰｼｮﾝ
		/// </summary>
		/// <remarks>
		/// シミュレーション
		/// </remarks>
		private const string KBN_NAME_SIMU = "ｼｭﾐﾚｰｼｮﾝ";

		/// <summary>
		/// 区分名骨塩定量
		/// </summary>
		private const string KBN_NAME_KOTSUEN = "骨塩定量";

		/// <summary>
		/// 区分名透視造影
		/// </summary>
		private const string KBN_NAME_DR = "透視造影";

		/// <summary>
		/// 患者数(救急含)
		/// </summary>
		private const string KBN_NAME_PATIENT_COUNT = "患者数(救急含)";
		#endregion

		#region 区分名インデックス

		/// <summary>
		/// 区分名インデックス胸腹部撮影
		/// </summary>
		private const int KBN_NAME_KYOFUKUBU_ROW = 0;

		/// <summary>
		/// 区分名インデックス骨撮影他
		/// </summary>
		private const int KBN_NAME_HONE_ROW = KBN_NAME_KYOFUKUBU_ROW + 1;

		/// <summary>
		/// 区分名インデックス断層撮影
		/// </summary>
		private const int KBN_NAME_DANSO_ROW = KBN_NAME_HONE_ROW + 1;

		/// <summary>
		/// 区分名インデックス乳房撮影
		/// </summary>
		private const int KBN_NAME_NYUBO_ROW = KBN_NAME_DANSO_ROW + 1;

		/// <summary>
		/// 区分名インデックス健診業務
		/// </summary>
		private const int KBN_NAME_KENSHIN_ROW = KBN_NAME_NYUBO_ROW + 1;

		/// <summary>
		/// 区分名インデックスポータブル
		/// </summary>
		private const int KBN_NAME_PORTABLE_ROW = KBN_NAME_KENSHIN_ROW + 1;

		/// <summary>
		/// 区分名インデックスﾗｲﾅｯｸｸﾞﾗﾌｨｰ
		/// </summary>
		private const int KBN_NAME_LINAC_ROW = KBN_NAME_PORTABLE_ROW + 2; //一行あける

		/// <summary>
		/// 区分名インデックスｼｭﾐﾚｰｼｮﾝ
		/// </summary>
		private const int KBN_NAME_SIMU_ROW = KBN_NAME_LINAC_ROW + 1;

		/// <summary>
		/// 区分名インデックス骨塩定量
		/// </summary>
		private const int KBN_NAME_KOTSUEN_ROW = KBN_NAME_SIMU_ROW + 2; //一行あける

		/// <summary>
		/// 区分名インデックス透視造影
		/// </summary>
		private const int KBN_NAME_DR_ROW = KBN_NAME_KOTSUEN_ROW + 1;

		/// <summary>
		/// 区分名インデックス患者数(救急含)
		/// </summary>
		private const int KBN_NAME_PATIENT_COUNT_ROW = KBN_NAME_DR_ROW + 2; //一行あける

		#endregion

		#endregion

		#endregion

		#region フィールド
		#endregion

		#region プロパティ

		/// <summary>
		/// データクラス
		/// </summary>
		public IppanItem Data
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
				return KBN_NAME_PATIENT_COUNT_ROW + 1;
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
				IppanItemIppan kbnItem = Data.IppanItemList.FirstOrDefault(
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
			list.get_CrListCell(0, KBN_NAME_KYOFUKUBU_ROW).Text = KBN_NAME_KYOFUKUBU;
			list.get_CrListCell(0, KBN_NAME_HONE_ROW).Text = KBN_NAME_HONE;
			list.get_CrListCell(0, KBN_NAME_DANSO_ROW).Text = KBN_NAME_DANSO;
			list.get_CrListCell(0, KBN_NAME_NYUBO_ROW).Text = KBN_NAME_NYUBO;
			list.get_CrListCell(0, KBN_NAME_KENSHIN_ROW).Text = KBN_NAME_KENSHIN;
			list.get_CrListCell(0, KBN_NAME_PORTABLE_ROW).Text = KBN_NAME_PORTABLE;
			list.get_CrListCell(0, KBN_NAME_LINAC_ROW).Text = KBN_NAME_LINAC;
			list.get_CrListCell(0, KBN_NAME_SIMU_ROW).Text = KBN_NAME_SIMU;
			list.get_CrListCell(0, KBN_NAME_KOTSUEN_ROW).Text = KBN_NAME_KOTSUEN;
			list.get_CrListCell(0, KBN_NAME_DR_ROW).Text = KBN_NAME_DR;
			list.get_CrListCell(0, KBN_NAME_PATIENT_COUNT_ROW).Text = KBN_NAME_PATIENT_COUNT;
		}

		/// <summary>
		/// 明細行書き込み
		/// </summary>
		/// <param name="f"></param>
		/// <param name="rowIndex"></param>
		/// <param name="emergencyItem"></param>
		private void WriteDetail(CrForm f, int rowIndex, IppanItemIppan emergencyItem)
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
