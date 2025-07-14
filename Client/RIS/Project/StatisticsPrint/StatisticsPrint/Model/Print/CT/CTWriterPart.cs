using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using CoReports;
using StatisticsPrint.Model.Print.CT.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.CT
{
	/// <summary>
	/// 帳票書き込み部分クラス基底
	/// </summary>
	public abstract class CTWriterPart
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

			/// <summary>
		/// 分類名取得
		/// </summary>
		protected abstract string BunruiName
		{
			get;
		}

		#region COReortsオブジェクト
		
		/// <summary>
		/// COReortsオブジェクト分類名
		/// </summary>
		protected abstract string COReportsBunruiName
		{
			get;
		}

		/// <summary>
		/// COReortsオブジェクト区分名
		/// </summary>
		protected abstract string COReportsKubunName
		{
			get;
		}

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
		public CTWriterPart(PrintWriterBase writer)
		{
			_writer = writer;
		}

		#endregion

		#region メソッド

		#region public

		/// <summary>
		/// 定数書き込み
		/// </summary>
		/// <param name="f"></param>
		public virtual void WriteConst(CrForm f)
		{
			//分類名設定
			f.CrObjects[COReportsBunruiName].get_CrListCell(0, 0).Text = BunruiName;
			//区分名設定
			CrObject list = f.CrObjects[COReportsKubunName];
			list.get_CrListCell(0, CTWriterPart.KBN_NAME_TOKEIBU_TANJUN_ROW).Text = CTWriterPart.KBN_NAME_TOKEIBU_TANJUN;
			list.get_CrListCell(0, CTWriterPart.KBN_NAME_TOKEIBU_ZOEI_ROW).Text = CTWriterPart.KBN_NAME_TOKEIBU_ZOEI;
			list.get_CrListCell(0, CTWriterPart.KBN_NAME_KUKANBU_TANJUN_ROW).Text = CTWriterPart.KBN_NAME_KUKANBU_TANJUN;
			list.get_CrListCell(0, CTWriterPart.KBN_NAME_KUKANBU_ZOEI_ROW).Text = CTWriterPart.KBN_NAME_KUKANBU_ZOEI;
			list.get_CrListCell(0, CTWriterPart.KBN_NAME_SHISHI_TANJUN_ROW).Text = CTWriterPart.KBN_NAME_SHISHI_TANJUN;
			list.get_CrListCell(0, CTWriterPart.KBN_NAME_SHISHI_ZOEI_ROW).Text = CTWriterPart.KBN_NAME_SHISHI_ZOEI;
		}

		public virtual void Write(CTItemPlace placeItem, CrForm f)
		{
			for (int rowIndex = 0; rowIndex < MaxRowCount; rowIndex++)
			{
				string kbnName = f.CrObjects[COReportsKubunName].get_CrListCell(0, rowIndex).Text;
				if (string.IsNullOrEmpty(kbnName))
				{
					continue; //帳票側に区分名が無いときは空行
				}

				//名称で付き合わせる
				CTItemBunrui bunruiItem = placeItem.CTItemBunruiList.FirstOrDefault(
											b => b.BUNRUI_NAME == BunruiName);
				if (bunruiItem == null)
				{
					f.CrObjects[COReportsMonthTotal].get_CrListCell(0, rowIndex).Text = "0";
					continue;
				}
				CTItemKubun kbnItem = bunruiItem.CTItemKubunList.FirstOrDefault(
											k => k.KBN_NAME == kbnName);
				if (kbnItem == null)
				{
					f.CrObjects[COReportsMonthTotal].get_CrListCell(0, rowIndex).Text = "0";
					continue;
				}
				WriteDetail(f, rowIndex, kbnItem);
				f.CrObjects[COReportsMonthTotal].get_CrListCell(0, rowIndex).Text = kbnItem.Total; //小計(区分)
			}
		}

		public void ClearDetail(CrForm f)
		{
			CrObject crKbnList = f.CrObjects[COReportsKubunName];
			for (int rowIndex = 0; rowIndex <= crKbnList.ListRows -1; rowIndex++)
			{
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
		/// <param name="emergencyItem"></param>
		protected void WriteDetail(CrForm f, int rowIndex, CTItemKubun kbnItem)
		{
			_writer.DayLoopExecuter(delegate(DateTime currentDate)
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
		protected virtual void SetDetailByDay(int day, int rowIndex, string value, CrForm crForm)
		{
			string objectName = string.Format(COReportsDayDetailNameFormat, day);
			crForm.CrObjects[objectName].get_CrListCell(0, rowIndex).Text = value;
		}

		#endregion

		#endregion
	}
}
