using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoReports;
using StatisticsPrint.Model.Print.Geppo.Data;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Geppo
{
	/// <summary>
	/// 帳票分類部
	/// </summary>
	public class GeppoWriterPartBunrui : GeppoWriterPart
	{
		#region 定数

		/// <summary>
		/// 対象COReportsリスト
		/// </summary>
		private const string COREPORTS_LIST ="Data_2";
		
		/// <summary>
		/// 対象COReportsリスト(前年比)
		/// </summary>
		private const string COREPORTS_LIST_PREV ="Data_3";

		#endregion 定数

		#region フィールド

		#endregion フィールド

		#region プロパティ

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GeppoWriterPartBunrui()
		{

		}

		#endregion

		#region メソッド

		public override void Write(GeppoItem item, CrForm f)
		{
			for (int colIndex = 0; colIndex < GeppoDefine.Instance.WriterItemsBunrui.Count(); colIndex++)
			{
				GeppoWriterItem writerItem = GeppoDefine.Instance.WriterItemsBunrui[colIndex];

				GeppoItemBunrui bunruiItem = item.GeppoItemBunruiList.FirstOrDefault(
											b => b.BUNRUI_ID == writerItem.BunruiID);
				if (bunruiItem == null)
				{
					f.CrObjects[COREPORTS_LIST].get_CrListCell(colIndex, 0).Text = "0";
					continue;
				}
				f.CrObjects[COREPORTS_LIST].get_CrListCell(colIndex, 0).Text = bunruiItem.Total;
				f.CrObjects[COREPORTS_LIST_PREV].get_CrListCell(colIndex, 0).Text = bunruiItem.RatioPreviousYear;
			}
		}

		public override void ClearDetail(CrForm f)
		{
			ClearCOReportsList(COREPORTS_LIST, f);
			ClearCOReportsList(COREPORTS_LIST_PREV, f);
		}

		#region private


		#endregion

		#endregion

	}
}
