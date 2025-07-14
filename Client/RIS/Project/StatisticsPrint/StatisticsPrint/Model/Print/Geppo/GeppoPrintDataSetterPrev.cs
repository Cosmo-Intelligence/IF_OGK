using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using StatisticsPrint.Model.Print.Geppo.Data;
using RISCommonLibrary.Lib.Utils;
using RISODPLibrary.Lib.Utils;

namespace StatisticsPrint.Model.Print.Geppo
{
	/// <summary>
	/// データリーダからGeppoItemへデータ設定する
	/// </summary>
	/// <remarks>前年度</remarks>
	internal class GeppoPrintDataSetterPrev : IGeppoPrintDataSetter
	{
		public void AddResultData(OracleDataReader dr, GeppoItem data)
		{
			AddBunruiList(dr, data.GeppoItemBunruiList);
		}

		private void AddBunruiList(OracleDataReader dr, List<GeppoItemBunrui> list)
		{
			#region 分類ごと

			string bunruiID = dr.GetStringByDB("BUNRUI_ID"); //これはstatistics_monthly_dat.class_keyが使えるのでIDを使う
			GeppoItemBunrui bunruiItem = list.FirstOrDefault(b => b.BUNRUI_ID == bunruiID);
			if (bunruiItem == null)
			{
				bunruiItem = new GeppoItemBunrui();
				bunruiItem.BUNRUI_ID = dr.GetStringByDB("BUNRUI_ID");
				bunruiItem.TotalPrevious = dr.GetStringByDB("CNT");

				list.Add(bunruiItem);
			}
			bunruiItem.TotalPrevious = dr.GetStringByDB("CNT");

			#endregion
		}
	}
}
