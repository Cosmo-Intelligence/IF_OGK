using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoReports;
using CoReportsCore;
using StatisticsPrint.Model.COReports;
using StatisticsPrint.Data;
using RISCommonLibrary.Lib.Utils;
using StatisticsPrint.Model.Print.Common;
using StatisticsPrint.Model.Print.CT.Data;
using StatisticsPrint.Model.Print.Common.Data;

namespace StatisticsPrint.Model.Print.CT
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class CTWriter : PrintWriterBase
	{
		#region フィールド

		/// <summary>
		/// 一般部
		/// </summary>
		private CTWriterPart _ippanWriter;

		/// <summary>
		/// 特殊部
		/// </summary>
		private CTWriterPart _tokushuWriter;

		/// <summary>
		/// その他
		/// </summary>
		private CTWriterPart _sonotaWriter;
		#endregion

		#region プロパティ

		/// <summary>
		/// 部位データクラス
		/// </summary>
		public CTItem Data
		{
			get;
			set;
		}

		/// <summary>
		/// 最大行
		/// </summary>
		protected override int MaxRowCount
		{
			get
			{
				throw new NotImplementedException();
			}

		}
		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CTWriter()
		{
			_ippanWriter = new CTWriterPartIppan(this);
			_tokushuWriter = new CTWriterPartTokushu(this);
			_sonotaWriter = new CTWriterPartSonota(this);
		}
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
			WriteConst(f);

			foreach (CTItemPlace placeItem in Data.CTItemList)
			{
				WriteHeaderPlace(placeItem.PLACE_NAME, f);
				ClearDetailFields(f);

				_ippanWriter.Write(placeItem, f);
				_tokushuWriter.Write(placeItem, f);
				_sonotaWriter.Write(placeItem, f);

				PageCount = PrintoutCurrentPage(f, PageCount);
			}
		}

		#endregion

		#region protected
		
		/// <summary>
		/// 明細行クリア
		/// </summary>
		/// <param name="f"></param>
		protected override void ClearDetailFields(CrForm f)
		{
			_ippanWriter.ClearDetail(f);
			_tokushuWriter.ClearDetail(f);
			_sonotaWriter.ClearDetail(f);
		}

		#endregion

		#region private

		/// <summary>
		/// 定数書き込み
		/// </summary>
		/// <param name="f"></param>
		private void WriteConst(CrForm f)
		{
			_ippanWriter.WriteConst(f);
			_tokushuWriter.WriteConst(f);
			_sonotaWriter.WriteConst(f);
		}

		/// <summary>
		/// ヘッダ場所書き込み
		/// </summary>
		/// <param name="condtion"></param>
		/// <param name="crForm"></param>
		private void WriteHeaderPlace(string placeName, CrForm crForm)
		{
			crForm.CrObjects[COREPORTS_PLACE_NAME].Text = placeName;
		}

		#endregion

		#endregion
	}
}
