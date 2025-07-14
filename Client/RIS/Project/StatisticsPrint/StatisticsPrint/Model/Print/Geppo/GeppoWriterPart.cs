using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Model.Print.Common;
using CoReports;
using StatisticsPrint.Model.Print.Geppo.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Geppo
{
	/// <summary>
	/// 帳票書き込み部分クラス基底
	/// </summary>
	public abstract class GeppoWriterPart
	{
		#region フィールド

		#endregion

		#region プロパティ

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GeppoWriterPart()
		{
		}

		#endregion

		#region メソッド

		#region public

		public abstract void Write(GeppoItem item, CrForm f);

		public virtual void ClearDetail(CrForm f)
		{
		}
		#endregion

		#region protected

		/// <summary>
		/// COReportsリストオブジェクトクリア
		/// </summary>
		/// <param name="listName"></param>
		/// <param name="f"></param>
		protected void ClearCOReportsList(string listName, CrForm f)
		{
			CrObject crList =  f.CrObjects[listName];

			for (int colIndex = 0; colIndex < crList.ListColumns; colIndex++)
			{
				crList.get_CrListCell(colIndex, 0).Text = "";
			}
		}

		#endregion

		#endregion
	}
}
