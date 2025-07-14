using System;
using System.Collections.Generic;
using System.Text;
using StatisticsPrint.View;
using System.Windows.Forms;
using StatisticsPrint.Model.Print;
using StatisticsPrint.Data;
using StatisticsPrint.Model.Print.BuiBunrui;
using StatisticsPrint.Model.Print.Bui;
using StatisticsPrint.Model.Print.Film;
using StatisticsPrint.Model.Print.Zoueizai;
using StatisticsPrint.Model.Print.Emergency;
using StatisticsPrint.Model.Print.Ippan;
using StatisticsPrint.Model.Print.RI;
using StatisticsPrint.Model.Print.DR;
using StatisticsPrint.Model.Print.CT;
using StatisticsPrint.Model.Print.US;
using StatisticsPrint.Model.Print.Geppo;
using StatisticsPrint.Model.Print.CDFilm;

namespace StatisticsPrint.Controller
{
	internal class StatisticsController
	{
		/// <summary>
		/// 撮影検査数一覧（部位分類）出力
		/// </summary>
		internal void PrintBuibunrui()
		{
			using (conditionPlaceForm f = new conditionPlaceForm())
			{
				f.PrintInstance = new BuiBunruiPrint();
				f.ShowDialog();
			};
		}

		/// <summary>
		/// 撮影検査数一覧（主行為）出力
		/// </summary>
		internal void PrintBui()
		{
			using (conditionPlaceForm f = new conditionPlaceForm())
			{
				f.IsAddAll = true;
				f.PrintInstance = new BuiPrint();
				f.ShowDialog();
			};
		}

		/// <summary>
		/// フィルム一覧出力
		/// </summary>
		internal void PrintFilm()
		{
			using (conditionPlaceForm f = new conditionPlaceForm())
			{
				f.PrintInstance = new FilmPrint();
				f.ShowDialog();
			};
		}

		/// <summary>
		/// 造影・薬剤一覧
		/// </summary>
		internal void PrintZoueizai()
		{
			using (conditionPlaceForm f = new conditionPlaceForm())
			{
				f.PrintInstance = new ZoueizaiPrint();
				f.ShowDialog();
			};
		}

		/// <summary>
		/// Film読込・書出/CD読込・書出
		/// </summary>
		internal void PrintCDFilm()
		{
			using (conditionForm f = new conditionForm())
			{
				f.PrintInstance = new CDFilmPrint();
				f.ShowDialog();
			};
		}

		/// <summary>
		/// 救命救急センター　集計表
		/// </summary>
		internal void PrintEmergency()
		{
			using (conditionForm f = new conditionForm())
			{
				f.PrintInstance = new EmergencyPrint();
				if (f.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			};
		}

		/// <summary>
		/// 一般撮影　分類別集計表
		/// </summary>
		internal void PrintIppan()
		{
			using (conditionForm f = new conditionForm())
			{
				f.PrintInstance = new IppanPrint();
				if (f.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			};
		}

		/// <summary>
		/// ＣＴ　分類別集計表
		/// </summary>
		internal void PrintCT()
		{
			using (conditionForm f = new conditionForm())
			{
				f.PrintInstance = new CTPrint();
				if (f.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			};
		}

		/// <summary>
		/// ＲＩ　分類別集計表
		/// </summary>
		internal void PrintRI()
		{
			using (conditionForm f = new conditionForm())
			{
				f.PrintInstance = new RIPrint();
				if (f.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			};
		}

		/// <summary>
		/// 透視造影　分類別集計表
		/// </summary>
		internal void PrintDR()
		{
			using (conditionForm f = new conditionForm())
			{
				f.PrintInstance = new DRPrint();
				if (f.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			};
		}

		/// <summary>
		/// ＵＳ　分類別集計表
		/// </summary>
		internal void PrintUS()
		{
			using (conditionForm f = new conditionForm())
			{
				f.PrintInstance = new USPrint();
				if (f.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			};
		}

		/// <summary>
		/// 医療技術部診療検査科　月報
		/// </summary>
		internal void PrintGeppo()
		{
			using (conditionGeppoForm f = new conditionGeppoForm())
			{
				f.PrintInstance = new GeppoPrint();
				if (f.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			};
		}

	}
}
