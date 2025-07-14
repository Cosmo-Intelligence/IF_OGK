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
	internal class GeppoPrintDataSetter : IGeppoPrintDataSetter
	{
		public void AddResultData(OracleDataReader dr, GeppoItem data)
		{
			string gyoSyosai = dr.GetStringByDB("GYO_SYOSAI");

			#region 帳票単位
			const string GYO_SYOSAI_TOTAL = "総計";
			if (GYO_SYOSAI_TOTAL == gyoSyosai)
			{
				GeppoItemBunrui bunruiItem = data.GeppoItemBunruiList.FirstOrDefault(b => b.BUNRUI_ID == GeppoDefine.BUNRUI_ID_GOKEI_KANJA_SU);
				if (bunruiItem == null)
				{
					bunruiItem = new GeppoItemBunrui();
					bunruiItem.BUNRUI_ID = GeppoDefine.BUNRUI_ID_GOKEI_KANJA_SU;
					bunruiItem.BUNRUI_NAME = "合計患者数";
					data.GeppoItemBunruiList.Add(bunruiItem);
				}
				bunruiItem.Total = dr.GetStringByDB("CNT");
				return;
			}

			#endregion

			AddBunruiList(dr, data.GeppoItemBunruiList);
		}

		/// <summary>
		/// 手入力の放射線治療を追加する
		/// </summary>
		/// <param name="monsu"></param>
		/// <param name="data"></param>
		public void AddHoshasen(string monsu, GeppoItem data)
		{
			GeppoItemBunrui bunruiItem = data.GeppoItemBunruiList.FirstOrDefault(b => b.BUNRUI_ID == GeppoDefine.BUNRUI_ID_HOSHASEN);
			if (bunruiItem == null)
			{
				bunruiItem = new GeppoItemBunrui();
				bunruiItem.BUNRUI_ID = GeppoDefine.BUNRUI_ID_HOSHASEN;
				bunruiItem.BUNRUI_NAME = "放射線治療";
				data.GeppoItemBunruiList.Add(bunruiItem);
			}

			string monsuDst = "0";
			if (StringUtils.StringToInt32(monsu) > 0) //NULLのものは"0"のままにしておく
			{
				monsuDst = monsu;
			}
			bunruiItem.Total = monsuDst;
		}

		/// <summary>
		/// 今年度で使用するリストを最初に作成する
		/// </summary>
		/// <param name="list"></param>
		public void CreateAllBunrui(List<GeppoItemBunrui> list)
		{
			foreach (var item in GeppoDefine.Instance.WriterItemsBunrui)
			{
				GeppoItemBunrui bunruiItem = list.FirstOrDefault(b => b.BUNRUI_ID == item.BunruiID);
				if (bunruiItem != null)
				{
					continue;
				}
				bunruiItem = new GeppoItemBunrui();
				bunruiItem.BUNRUI_ID = item.BunruiID;
				bunruiItem.Total = "0";
				list.Add(bunruiItem);
			}
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
				bunruiItem.BUNRUI_NAME = dr.GetStringByDB("BUNRUI_NAME");
				bunruiItem.BUNRUI_SHOWORDER = dr.GetStringByDB("BUNRUI_SHOWORDER");
				bunruiItem.Total = dr.GetStringByDB("CNT");
				list.Add(bunruiItem);
			}

			string gyoSyosai = dr.GetStringByDB("GYO_SYOSAI");
			const string GYO_SYOSAI_TOTAL_KUBUN = "小計(分類)";
			if (GYO_SYOSAI_TOTAL_KUBUN == gyoSyosai)
			{
				bunruiItem.Total = dr.GetStringByDB("CNT");
				return;
			}

			#endregion

			AddKubunList(dr, bunruiItem.GeppoItemKubunList);
		}

		private void AddKubunList(OracleDataReader dr, List<GeppoItemKubun> list)
		{
			#region 区分ごと

			string kbnName = dr.GetStringByDB("KBN_NAME");
			GeppoItemKubun kbnItem = list.FirstOrDefault(k => k.KBN_NAME == kbnName);
			if (kbnItem == null)
			{
				kbnItem = new GeppoItemKubun();
				kbnItem.KBN_NAME = dr.GetStringByDB("KBN_NAME");
				kbnItem.KBN_SHOWORDER = dr.GetStringByDB("KBN_SHOWORDER");
				kbnItem.CNT = dr.GetStringByDB("CNT");
				list.Add(kbnItem);
			}
			#endregion
		}

	}
}
