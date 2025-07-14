using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsPrint.Model.Print.Bui.Data
{
	/// <summary>
	/// 部位アイテムが持つリストクラス
	/// </summary>
	public abstract class BuiItemList<T>
	{
		public abstract List<T> GetList();
	}
}
