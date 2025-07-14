using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;

namespace StatisticsPrint.Model.Print.CT
{
	/// <summary>
	/// 帳票特殊部
	/// </summary>
	public class CTWriterPartTokushu : CTWriterPart
	{
		#region 定数

		#region COReprtsオブジェクト

		#region タイトル

		#region 分類
		/// <summary>
		/// 分類名 特殊
		/// </summary>
		private const string COREPORTS_BUNRUI_NAME_LIST_TOKUSHU = "BunruiName_2";

		#endregion

		#region 区分
		/// <summary>
		/// 区分名 特殊
		/// </summary>
		private const string COREPORTS_KBN_NAME_LIST_TOKUSHU = "KBNName_2";

		#endregion

		#endregion

		#region 分類別計

		/// <summary>
		/// 特殊 分類別計リスト
		/// </summary>
		private const string COREPORTS_MONTH_TOTAL_TOKUSHU = "MonthTotal_2";
		#endregion

		#region 分類名

		/// <summary>
		/// 分類名 特殊
		/// </summary>
		private const string BUNRUI_NAME_TOKUSHU = "特殊";

		#endregion

		#region 明細
		/// <summary>
		/// 日付名細部フォーマット
		/// </summary>
		protected const string COREPORTS_DAY_DETAIL_NAME_FORMAT_TOKUSHU = "Day2_{0}";

		#endregion

		#endregion COReprtsオブジェクト

		#endregion 定数

		#region プロパティ

		/// <summary>
		/// 最大行カウント
		/// </summary>
		protected override int MaxRowCount
		{
			get
			{
				return KBN_NAME_SHISHI_ZOEI_ROW + 1;
			}

		}

		/// <summary>
		/// 分類名取得
		/// </summary>
		protected override string BunruiName
		{
			get
			{
				return BUNRUI_NAME_TOKUSHU;
			}
		}

		#region COReortsオブジェクト
		/// <summary>
		/// COReortsオブジェクト分類名
		/// </summary>
		protected override string COReportsBunruiName
		{
			get
			{
				return COREPORTS_BUNRUI_NAME_LIST_TOKUSHU;
			}
		}

		/// <summary>
		/// COReortsオブジェクト区分名
		/// </summary>
		protected override string COReportsKubunName
		{
			get
			{
				return COREPORTS_KBN_NAME_LIST_TOKUSHU;
			}
		}

		/// <summary>
		/// COReortsオブジェクト分類別計
		/// </summary>
		protected override string COReportsMonthTotal
		{
			get
			{
				return COREPORTS_MONTH_TOTAL_TOKUSHU;
			}
		}

		/// <summary>
		/// COReortsオブジェクト日付フォーーマット
		/// </summary>
		protected override string COReportsDayDetailNameFormat
		{
			get
			{
				return COREPORTS_DAY_DETAIL_NAME_FORMAT_TOKUSHU;
			}
		}

		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CTWriterPartTokushu(PrintWriterBase writer)
			: base(writer)
		{

		}

		#endregion

		#region メソッド


		#endregion

	}
}
