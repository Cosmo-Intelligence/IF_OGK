using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoReports;
using CoReportsCore;
using StatisticsPrint.Model.Print;
using StatisticsPrint.Model.Print.Common;

namespace StatisticsPrint.Model.COReports
{
	/// <summary>
	/// COReports実行基底クラス
	/// </summary>
	public abstract class COReportsExecBase
	{
		#region プロパティ
		/// <summary>
		/// 印刷JOB の帳票
		/// </summary>
		public string DocumentName
		{
			get;
			set;
		}

		/// <summary>
		/// フォームファイル名
		/// </summary>
		public string FormFilePath
		{
			get;
			set;
		}

		/// <summary>
		/// 部数
		/// </summary>
		public int Copies
		{
			get;
			set;
		}
		#endregion

		#region コンストラクタ

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public COReportsExecBase()
		{
			Copies = 1;
		}
		#endregion

		#region メソッド
		/// <summary>
		/// 出力する
		/// </summary>
		/// <param name="crDraw"></param>
		/// <param name="writer">書き込みクラス</param>
		public abstract void Output(CrDraw crDraw, IPrintWriter writer);

		/// <summary>
		/// プリンターの情報を設定します。
		/// </summary>
		/// <param name="printer">シーオーリポーツ プリンターオブジェクト</param>
		protected virtual void SetPrinterConfig(CrPrinter printer)
		{
			// 拡大縮小率
			printer.PrintScale = 100;
			//部数
			printer.Copies = (short)Copies;
		}
		
		#endregion
	}
}
