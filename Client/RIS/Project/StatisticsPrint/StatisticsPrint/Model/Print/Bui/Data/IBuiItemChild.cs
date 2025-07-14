using System;
using System.Collections.Generic;
namespace StatisticsPrint.Model.Print.Bui.Data
{
	public interface IBuiItemChild
	{
		string TARGET_ID
		{
			get;
			set;
		}

		string TARGET_NAME
		{
			get;
			set;
		}

		string TARGET_SHOWORDER
		{
			get;
			set;
		}

		List<BuiItemKensaType> KensaTypeItemList
		{
			get;
			set;
		}
	}
}
