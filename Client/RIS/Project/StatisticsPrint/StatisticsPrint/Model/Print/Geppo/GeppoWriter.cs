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
using StatisticsPrint.Model.Print.Geppo.Data;
using StatisticsPrint.Model.Print.Common.Data;
using System.Globalization;

namespace StatisticsPrint.Model.Print.Geppo
{
	/// <summary>
	/// フォームファイル書き込みクラス
	/// </summary>
	public class GeppoWriter : PrintWriterBase
	{
		#region フィールド

		/// <summary>
		/// 区分部
		/// </summary>
		private GeppoWriterPart _kubunWriter;

		/// <summary>
		/// 分類部
		/// </summary>
		private GeppoWriterPart _bunruiWriter;

		#endregion

		#region プロパティ

		/// <summary>
		/// 部位データクラス
		/// </summary>
		public GeppoItem Data
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
		public GeppoWriter()
		{
			_kubunWriter = new GeppoWriterPartKubun();
			_bunruiWriter = new GeppoWriterPartBunrui();
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

			ClearDetailFields(f);
			
			_kubunWriter.Write(Data, f);
			_bunruiWriter.Write(Data, f);
			
			PageCount = PrintoutCurrentPage(f, PageCount);
		}

		#endregion

		#region protected

		/// <summary>
		/// 月ヘッダ書き込み
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="crForm"></param>
		protected override void WriteHeaderMonth(ConditionPrint condition, CrForm crForm)
		{
			CultureInfo culture = new CultureInfo("ja-JP", true);
			culture.DateTimeFormat.Calendar = new JapaneseCalendar();
			crForm.CrObjects[COREPORTS_MONTH].Text = condition.Nengetsu.ToString("（ggyy年MM月分）", culture);
		}

		/// <summary>
		/// 明細行クリア
		/// </summary>
		/// <param name="f"></param>
		protected override void ClearDetailFields(CrForm f)
		{
			_kubunWriter.ClearDetail(f);
			_bunruiWriter.ClearDetail(f);
		}

		/// <summary>
		/// ページ数書き込み
		/// </summary>
		/// <param name="pageCount"></param>
		protected override void WritePageCount(int pageCount, CrForm f)
		{
			//ページ数なし
		}
		#endregion

		#region private

		#endregion

		#endregion

	}

}
