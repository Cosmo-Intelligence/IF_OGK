using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatisticsPrint.Data;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.Bui.Data
{
	/// <summary>
	/// 検索結果保持クラス
	/// </summary>
	/// <remarks>
	/// 検査室の情報を保持
	/// </remarks>
	public class BuiItemExamRoom : IBuiItemChild
	{
		#region フィールド

		#endregion

		#region プロパティ

		public string KENSASITU_ID
		{
			get;
			set;
		}

		public string KENSASITU_NAME
		{
			get;
			set;
		}

		public string KENSASITU_SHOWORDER
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BuiItemExamRoom()
		{
			KensaTypeItemList = new List<BuiItemKensaType>();
		}
		#endregion

		#region メソッド

		#endregion

		#region IBuiItemExamRoom メンバ

		public string TARGET_ID
		{
			get
			{
				return KENSASITU_ID;
			}
			set
			{
				KENSASITU_ID = value;
			}
		}

		public string TARGET_NAME
		{
			get
			{
				return KENSASITU_NAME;
			}
			set
			{
				KENSASITU_NAME = value;
			}
		}

		public string TARGET_SHOWORDER
		{
			get
			{
				return KENSASITU_SHOWORDER;
			}
			set
			{
				KENSASITU_SHOWORDER = value;
			}
		}

		public List<BuiItemKensaType> KensaTypeItemList
		{
			get;
			set;
		}
		#endregion
	}
}
