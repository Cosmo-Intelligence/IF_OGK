using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatisticsPrint.Model.Print.Geppo
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>シングルトン</remarks>
	public class GeppoDefine
	{

		#region 定数

		#region 分類ID

		/// <summary>
		/// 分類ID 一般撮影
		/// </summary>
		public const string BUNRUI_ID_IPPAN = "100";

		/// <summary>
		/// 分類ID X線透視
		/// </summary>
		public const string BUNRUI_ID_X_DR = "200";

		/// <summary>
		/// 分類ID CT
		/// </summary>
		public const string BUNRUI_ID_CT = "500";

		/// <summary>
		/// 分類ID 血管造影
		/// </summary>
		public const string BUNRUI_ID_KEKKAN = "800";

		/// <summary>
		/// 分類ID 超音波
		/// </summary>
		public const string BUNRUI_ID_US = "400";

		/// <summary>
		/// 分類ID MRI
		/// </summary>
		public const string BUNRUI_ID_MRI = "600";

		/// <summary>
		/// 分類ID 結石破砕治療
		/// </summary>
		public const string BUNRUI_ID_KESSEKI = "201";

		/// <summary>
		/// 分類ID RI体外測定
		/// </summary>
		public const string BUNRUI_ID_RI_TAIGAI = "700";

		/// <summary>
		/// 分類ID RI内用療法
		/// </summary>
		public const string BUNRUI_ID_RI_NAIYO = "702";

		/// <summary>
		/// 分類ID 放射線治療(門数)
		/// </summary>
		public const string BUNRUI_ID_HOSHASEN = "900";

		/// <summary>
		/// 分類ID PET-CT
		/// </summary>
		public const string BUNRUI_ID_PETCT = "901";

		/// <summary>
		/// 分類ID 骨塩定量
		/// </summary>
		public const string BUNRUI_ID_KOTSUEN = "120";

		/// <summary>
		/// 分類ID 合計患者数
		/// </summary>
		public const string BUNRUI_ID_GOKEI_KANJA_SU = "0";
		#endregion

		#endregion

		#region フィールド
		/// <summary>
		/// インスタンス
		/// </summary>
		private static readonly GeppoDefine _instance = new GeppoDefine();

		/// <summary>
		/// 書き込む項目指定
		/// </summary>
		/// <remarks>
		/// 配列のインデックスとCOReportsの明細列をあわせること
		/// GeppoItemBunruiListの生成にも使用
		/// </remarks>
		public readonly GeppoWriterItem[] WriterItemsBunrui = new[]
		{
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_IPPAN, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_X_DR, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_CT, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_KEKKAN, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_US, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_MRI, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_KESSEKI, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_HOSHASEN, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_RI_TAIGAI, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_RI_NAIYO, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_PETCT, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_KOTSUEN, KubunName = ""},
			new GeppoWriterItem(){ BunruiID= GeppoDefine.BUNRUI_ID_GOKEI_KANJA_SU, KubunName = ""}
		};

		#endregion

		#region プロパティ
		/// <summary>
		/// インスタンスにアクセスするプロパティ
		/// </summary>
		public static GeppoDefine Instance
		{
			get
			{
				return _instance;
			}
		}
		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <remarks>プライベート</remarks>
		private GeppoDefine()
		{

		}


	}
}
