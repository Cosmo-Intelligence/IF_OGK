using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Collections.Specialized;
using System.Collections;
using StatisticsPrint.Data;
using System.Data;
using RISODPLibrary.Lib.Utils;
using System.Configuration;
using RISCommonLibrary.Lib.Utils;

namespace StatisticsPrint.Model.FormHelper
{
	public class ConditionFormHelper
	{
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		internal List<string> GetLocalPriterList()
		{
			PrinterSettings.StringCollection sc = PrinterSettings.InstalledPrinters;

			List<string> list = new List<string>();
			foreach (string item in sc)
			{
				list.Add(item);
			}
			return list;
		}

		internal string GetDefaultPrinterName()
		{
			//PrintDocumentの作成
			PrintDocument pd = new PrintDocument();
			return pd.PrinterSettings.PrinterName;
		}

	}
}
