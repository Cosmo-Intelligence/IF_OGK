using System.Collections.Generic;

namespace StatisticsPrint.Model.Print.Bui.Data.ALL
{
	/// <summary>
	/// All選択時検索結果保持クラス
	/// </summary>
	/// <remarks>
	/// 実施場所の情報を保持
	/// </remarks>
	public class BuiItemPlaceAll : IBuiItemChild
	{
		#region フィールド

		#endregion

		#region プロパティ

		public string PLACE_ID
		{
			get;
			set;
		}

		public string PLACE_NAME
		{
			get;
			set;
		}

		public string PLACE_SHOWORDER
		{
			get;
			set;
		}

		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BuiItemPlaceAll()
		{
			KensaTypeItemList = new List<BuiItemKensaType>();
		}
		#endregion

		#region メソッド

		#endregion

		#region IBuiItemChild メンバ

		public string TARGET_ID
		{
			get
			{
				return PLACE_ID;
			}
			set
			{
				PLACE_ID = value;
			}
		}

		public string TARGET_NAME
		{
			get
			{
				return PLACE_NAME;
			}
			set
			{
				PLACE_NAME = value;
			}
		}

		public string TARGET_SHOWORDER
		{
			get
			{
				return PLACE_SHOWORDER;
			}
			set
			{
				PLACE_SHOWORDER = value;
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
