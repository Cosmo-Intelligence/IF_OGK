using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoReports;
using StatisticsPrint.Model.Print.US.Data;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.US
{
	/// <summary>
	/// 帳票造影剤部
	/// </summary>
	public class USWriterPartZoueizai : USWriterPart
	{
		#region 定数

		#region COReprtsオブジェクト
		/// <summary>
		/// 明細行リスト
		/// </summary>
		private const string COREPORTS_MONTH_TOTAL_ZOUEIZAI = "MonthTotal_3";

		/// <summary>
		/// 日付名細部フォーマット
		/// </summary>
		private const string COREPORTS_DAY_DETAIL_NAME_FORMAT_ZOUEIZAI = "Day{0}_3";
		#endregion

		#endregion 定数

		#region フィールド

		/// <summary>
		/// 書き込む項目指定
		/// </summary>
		/// <remarks>
		/// 配列のインデックスとCOReportsの明細行をあわせること
		/// </remarks>
		private USWriterItem[] _writerItems = new []
		{
			new USWriterItem(){ Bunrui = "", Kubun = "レボビスト"},
			new USWriterItem(){ Bunrui = "", Kubun = "ソナゾイド"},
			new USWriterItem(){ Bunrui = "", Kubun = "生食１００ｍｌ　１Ｋ"},
			new USWriterItem(){ Bunrui = "", Kubun = "生理食塩水　１００ｍｌ"},
			new USWriterItem(){ Bunrui = "", Kubun = "生理食塩水　　２０ｍｌ"}
		};

		#endregion フィールド

		#region プロパティ

		/// <summary>
		/// 最大行カウント
		/// </summary>
		protected override int MaxRowCount
		{
			get
			{
				return _writerItems.Count();
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
				return COREPORTS_MONTH_TOTAL_ZOUEIZAI;
			}
		}

		/// <summary>
		/// COReortsオブジェクト日付フォーーマット
		/// </summary>
		protected override string COReportsDayDetailNameFormat
		{
			get
			{
				return COREPORTS_DAY_DETAIL_NAME_FORMAT_ZOUEIZAI;
			}
		}

		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public USWriterPartZoueizai(PrintWriterBase writer)
			: base(writer)
		{

		}

		#endregion

		#region メソッド

		public override void Write(USItem item, CrForm f)
		{
			for (int rowIndex = 0; rowIndex < MaxRowCount; rowIndex++)
			{
				USWriterItem writerItem = _writerItems[rowIndex];

				string kbnName = writerItem.Kubun;
				USItemKubun kbnItem = item.USItemZoueizaiList.FirstOrDefault(
											k => k.KBN_NAME == kbnName);
				if (kbnItem == null)
				{
					f.CrObjects[COReportsMonthTotal].get_CrListCell(0, rowIndex).Text = "0";
					continue;
				}
				WriteDetail(f, rowIndex, kbnItem.DetailDayList);
				f.CrObjects[COReportsMonthTotal].get_CrListCell(0, rowIndex).Text = kbnItem.Total; //小計(区分)
			}
		}

		#region private
		#endregion

		#endregion
	}
}
