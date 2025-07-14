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
	/// 帳票メイン部
	/// </summary>
	public class USWriterPartMain : USWriterPart
	{
		#region 定数

		/// <summary>
		/// 区分名 総件数
		/// </summary>
		private const string KUBUN_TOTAL = "総件数";
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
			new USWriterItem(){ Bunrui = "①心臓領域総件数", Kubun = KUBUN_TOTAL},
			new USWriterItem(){ Bunrui = "①心臓領域総件数", Kubun = "成人"},
			new USWriterItem(){ Bunrui = "①心臓領域総件数", Kubun = "小児"},
			new USWriterItem(){ Bunrui = "①心臓領域総件数", Kubun = "造影"},
			new USWriterItem(){ Bunrui = "①心臓領域総件数", Kubun = "特殊"},
			new USWriterItem(){ Bunrui = "①心臓領域総件数", Kubun = "経食道"},
			new USWriterItem(){ Bunrui = "①心臓領域総件数", Kubun = "ポータブル"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = KUBUN_TOTAL},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "腹部"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "泌尿器系"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "産科"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "その他"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "経動脈造影"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "経静脈造影"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "ＰＦＡ／ＰＥＩＴ"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "生検・穿刺"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "ドプラ"},
			new USWriterItem(){ Bunrui = "②腹部等領域総件数", Kubun = "ポータブル"},
			new USWriterItem(){ Bunrui = "③体表等領域総件数", Kubun = KUBUN_TOTAL},
			new USWriterItem(){ Bunrui = "③体表等領域総件数", Kubun = "乳腺"},
			new USWriterItem(){ Bunrui = "③体表等領域総件数", Kubun = "甲状腺"},
			new USWriterItem(){ Bunrui = "③体表等領域総件数", Kubun = "血管系"},
			new USWriterItem(){ Bunrui = "③体表等領域総件数", Kubun = "その他"},
			new USWriterItem(){ Bunrui = "③体表等領域総件数", Kubun = "ドプラ"}
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
				return PrintWriterBase.COREPORTS_MONTH_TOTAL;
			}
		}

		/// <summary>
		/// COReortsオブジェクト日付フォーーマット
		/// </summary>
		protected override string COReportsDayDetailNameFormat
		{
			get
			{
				return PrintWriterBase.COREPORTS_DAY_DETAIL_NAME_FORMAT;
			}
		}

		#endregion

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public USWriterPartMain(PrintWriterBase writer)
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

				USItemBunrui bunruiItem = GetBunruiItem(writerItem, item);
				if (bunruiItem == null)
				{
					f.CrObjects[COReportsMonthTotal].get_CrListCell(0, rowIndex).Text = "0";
					continue;
				}

				string kbnName = writerItem.Kubun;
				if (KUBUN_TOTAL == kbnName)
				{
					WriteDetail(f, rowIndex, bunruiItem.DetailDayList);
					f.CrObjects[COReportsMonthTotal].get_CrListCell(0, rowIndex).Text = bunruiItem.Total; //小計(区分)
					continue;
				}

				USItemKubun kbnItem = bunruiItem.USItemKubunList.FirstOrDefault(
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

		/// <summary>
		/// 分類項目取得
		/// </summary>
		/// <param name="writerItem"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		/// <remarks>区分の存在までチェックする</remarks>
		private USItemBunrui GetBunruiItem(USWriterItem writerItem, USItem item)
		{
			USItemBunrui bunruiItemUS = item.USItemBunruiListUS.FirstOrDefault(
										b => b.BUNRUI_NAME == writerItem.Bunrui);
			if (KUBUN_TOTAL == writerItem.Kubun)
			{
				return bunruiItemUS; //合計は固定で超音波
			}
			if (bunruiItemUS == null)
			{
				return GetBunruiItemDoppler(writerItem, item); //超音波リストになかったらドップラリストを探しにいく
			}

			if (!bunruiItemUS.USItemKubunList.Exists(
									k => k.KBN_NAME == writerItem.Kubun))
			{
				return GetBunruiItemDoppler(writerItem, item);
			}
			return bunruiItemUS;
		}

		/// <summary>
		/// ドップラ分類項目取得
		/// </summary>
		/// <param name="writerItem"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		private USItemBunrui GetBunruiItemDoppler(USWriterItem writerItem, USItem item)
		{
			return item.USItemBunruiListDoppler.FirstOrDefault(
										b => b.BUNRUI_NAME == writerItem.Bunrui);
		}

		#endregion



		#endregion

	}
}
