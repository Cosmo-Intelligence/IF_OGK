using System;
using System.Linq;
using CoReports;
using StatisticsPrint.Data;
using StatisticsPrint.Model.COReports;
using StatisticsPrint.Model.Print.Bui.Data;
using StatisticsPrint.Model.Print.Common.Data;
using StatisticsPrint.Model.Print.Bui.Data.ALL;

namespace StatisticsPrint.Model.Print.Bui
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class BuiWriterAll : BuiWriter
	{
		#region 定数

		#endregion

		#region フィールド
		#endregion

		#region プロパティ

		#endregion

		#region メソッド

		#region public
		
		/// <summary>
		/// 書き込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public override void Write(object sender, PrintEventArgs e)
		{
			CrForm f = e.crForm;
			//フィールドを全てクリアします
			f.ClearAllFields();

			WriteHeader(Condition, f);

			int rowIndex = 0;
			foreach (BuiItemPlaceAll placeRoomItem in Data.ChildItemList)
			{
				ConditionPrintPlace conditionPlace = (ConditionPrintPlace)Condition;
				f.CrObjects[COREPORTS_EXAM_ROOM_NAME].Text = conditionPlace.PlaceID;
				f.CrObjects[COREPORTS_PLACE_NAME].Text = placeRoomItem.PLACE_NAME;
				
				rowIndex = WriteBuiItemChild(f, rowIndex, placeRoomItem);
			}
		}

		#endregion

		#region protected

		#endregion

		#region private

		#endregion
	
		#endregion
	}
}
