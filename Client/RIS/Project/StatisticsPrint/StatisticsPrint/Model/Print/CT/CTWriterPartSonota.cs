using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using CoReports;
using StatisticsPrint.Model.Print.CT.Data;

namespace StatisticsPrint.Model.Print.CT
{
	/// <summary>
	/// 帳票その他部
	/// </summary>
	public class CTWriterPartSonota : CTWriterPart
	{
		#region 定数

		#region COReprtsオブジェクト

		#region タイトル

		#region 分類

		#endregion

		#region 区分
		/// <summary>
		/// 区分名 その他
		/// </summary>
		public const string COREPORTS_KBN_NAME_LIST_SONOTA = "KBNName_3";

		#endregion

		#endregion

		#region 分類別計

		/// <summary>
		/// その他 分類別計リスト
		/// </summary>
		public const string COREPORTS_MONTH_TOTAL_SONOTA = "MonthTotal_3";
		#endregion
		
		#region 分類名

		/// <summary>
		/// 分類名 その他
		/// </summary>
		private const string BUNRUI_NAME_SONOTA = "その他";

		#endregion

		#region 区分名

		/// <summary>
		/// 区分名 RTP
		/// </summary>
		public const string KBN_NAME_RTP = "RTP";

		/// <summary>
		/// 区分名 生検
		/// </summary>
		public const string KBN_NAME_SEIKEN = "生検";

		/// <summary>
		/// 区分名 IVR-CT(腹部)
		/// </summary>
		public const string KBN_NAME_IVR_CT_FUKUBU = "IVR-CT(腹部)";

		/// <summary>
		/// 区分名 IVR-CT(骨盤)
		/// </summary>
		public const string KBN_NAME_IVR_CT_KOTSUBAN = "IVR-CT(骨盤)";

		#endregion

		#region 区分名インデックス

		/// <summary>
		/// 区分名インデックス RTP
		/// </summary>
		public const int KBN_NAME_RTP_ROW = 0;

		/// <summary>
		/// 区分名インデックス 生検
		/// </summary>
		public const int KBN_NAME_SEIKEN_ROW = KBN_NAME_RTP_ROW + 1;

		/// <summary>
		/// 区分名インデックス IVR-CT(腹部)
		/// </summary>
		public const int KBN_NAME_IVR_CT_FUKUBU_ROW = KBN_NAME_SEIKEN_ROW + 1;

		/// <summary>
		/// 区分名インデックス IVR-CT(骨盤)
		/// </summary>
		public const int KBN_NAME_IVR_CT_KOTSUBAN_ROW = KBN_NAME_IVR_CT_FUKUBU_ROW + 1;

		#endregion

		#region 明細
		/// <summary>
		/// 日付名細部フォーマット
		/// </summary>
		protected const string COREPORTS_DAY_DETAIL_NAME_FORMAT_SONOTA = "Day3_{0}";

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
				return KBN_NAME_IVR_CT_KOTSUBAN_ROW + 1;
			}
		}

		/// <summary>
		/// 分類名取得
		/// </summary>
		protected override string BunruiName
		{
			get
			{
				return BUNRUI_NAME_SONOTA;
			}
		}

		#region COReortsオブジェクト
		/// <summary>
		/// COReortsオブジェクト分類名
		/// </summary>
		/// <remarks>
		/// その他の分類名は帳票上に出てこない
		/// </remarks>
		protected override string COReportsBunruiName
		{
			get
			{
				return "";
			}
		}

		/// <summary>
		/// COReortsオブジェクト区分名
		/// </summary>
		protected override string COReportsKubunName
		{
			get
			{
				return COREPORTS_KBN_NAME_LIST_SONOTA;
			}
		}

		/// <summary>
		/// COReortsオブジェクト分類別計
		/// </summary>
		protected override string COReportsMonthTotal
		{
			get
			{
				return COREPORTS_MONTH_TOTAL_SONOTA;
			}
		}

		/// <summary>
		/// COReortsオブジェクト日付フォーーマット
		/// </summary>
		protected override string COReportsDayDetailNameFormat
		{
			get
			{
				return COREPORTS_DAY_DETAIL_NAME_FORMAT_SONOTA;
			}
		}

		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CTWriterPartSonota(PrintWriterBase writer)
			: base(writer)
		{

		}

		#endregion

		#region メソッド

		/// <summary>
		/// 定数書き込み
		/// </summary>
		/// <param name="f"></param>
		public override void WriteConst(CrForm f)
		{
			//区分名設定
			CrObject list = f.CrObjects[COReportsKubunName];
			list.get_CrListCell(0, KBN_NAME_RTP_ROW).Text = KBN_NAME_RTP;
			list.get_CrListCell(0, KBN_NAME_SEIKEN_ROW).Text = KBN_NAME_SEIKEN;
			list.get_CrListCell(0, KBN_NAME_IVR_CT_FUKUBU_ROW).Text = KBN_NAME_IVR_CT_FUKUBU;
			list.get_CrListCell(0, KBN_NAME_IVR_CT_KOTSUBAN_ROW).Text = KBN_NAME_IVR_CT_KOTSUBAN;
		}

		#endregion
	}
}
