
namespace StatisticsPrint.Model.Print.Bui.Data.ALL
{
	/// <summary>
	/// 合計フィールド名取得
	/// </summary>
	public class TotalFieldNameAll : TotalFieldName
	{
		public override string TotalName
		{
			get
			{
				return "小計(実施場所+検査種別)";
			}
		}

		public override string TotalNameDay
		{
			get
			{
			return "小計(実施場所+検査種別+日付)";
			}
		}

		public override string TotalNameBuiBunrui
		{
			get
			{
			return "小計(実施場所+検査種別+部位分類)";
			}
		}
	}
}
