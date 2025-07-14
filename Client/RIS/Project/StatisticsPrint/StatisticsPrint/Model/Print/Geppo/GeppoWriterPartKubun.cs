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
	/// 帳票区分部
	/// </summary>
	public class GeppoWriterPartKubun : GeppoWriterPart
	{
		#region 定数

		/// <summary>
		/// 対象COReportsリスト
		/// </summary>
		private const string COREPORTS_LIST ="Data_1";

		#endregion 定数

		#region フィールド

		/// <summary>
		/// 書き込む項目指定
		/// </summary>
		/// <remarks>
		/// 配列のインデックスとCOReportsの明細列をあわせること
		/// </remarks>
		private GeppoWriterItem[] _writerItems = new []
		{
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_IPPAN, KubunName = "一般"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_IPPAN, KubunName = "ポータブル"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_X_DR, KubunName = "上腹部"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_X_DR, KubunName = "注腸"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_X_DR, KubunName = "その他"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_CT, KubunName = "頭部"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_CT, KubunName = "胸腹部"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_CT, KubunName = "その他"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_KEKKAN, KubunName = "頭部"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_KEKKAN, KubunName = "胸腹部"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_KEKKAN, KubunName = "心臓"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_KEKKAN, KubunName = "その他"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_US, KubunName = "腹部"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_US, KubunName = "心臓"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_US, KubunName = "その他"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_MRI, KubunName = "頭部"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_MRI, KubunName = "胸腹部"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_MRI, KubunName = "その他"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_KESSEKI, KubunName = "尿路"},
			new GeppoWriterItem(){ BunruiID = GeppoDefine.BUNRUI_ID_KESSEKI, KubunName = "胆道"}
		};

		#endregion フィールド

		#region プロパティ

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GeppoWriterPartKubun()
		{

		}

		#endregion

		#region メソッド

		public override void Write(GeppoItem item, CrForm f)
		{
			for (int colIndex = 0; colIndex < _writerItems.Count(); colIndex++)
			{
				GeppoWriterItem writerItem = _writerItems[colIndex];

				GeppoItemBunrui bunruiItem = item.GeppoItemBunruiList.FirstOrDefault(
											b => b.BUNRUI_ID == writerItem.BunruiID);
				if (bunruiItem == null)
				{
					continue;
				}
				GeppoItemKubun kubunItem = bunruiItem.GeppoItemKubunList.FirstOrDefault(
											k => k.KBN_NAME == writerItem.KubunName);
				if (kubunItem == null)
				{
					continue;
				}
				f.CrObjects[COREPORTS_LIST].get_CrListCell(colIndex, 0).Text = kubunItem.CNT;
			}
		}

		public override void ClearDetail(CrForm f)
		{
			ClearCOReportsList(COREPORTS_LIST, f);
		}

		#region private


		#endregion

		#endregion

	}
}
