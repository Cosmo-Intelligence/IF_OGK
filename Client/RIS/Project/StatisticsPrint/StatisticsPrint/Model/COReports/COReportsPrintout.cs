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
	/// 印刷
	/// </summary>
	public class COReportsPrintout : COReportsExecBase
	{
		#region プロパティ
		/// <summary>
		/// プリンタ名
		/// </summary>
		public string PrinterName
		{
			get;
			set;
		}

		#endregion

		#region メソッド

		/// <summary>
		/// ドキュメントファイルを直接印刷します。
		/// </summary>
		/// <param name="draw">書き込みクラス</param>
		public override void Output(CrDraw crDraw, IPrintWriter writer)
		{
			CrForm f = null;
			// 初期化
			crDraw.Initialize();

			// プリンタの指定
			int ret = crDraw.StartPrinter(PrinterName, DocumentName);
			if (ret != (int)corError.corNoError)
			{
				throw new COReportsException(string.Format("StartPrinterで例外が発生しました={0}:{1}", ret,
					COReportsUtils.GetErrMessage(ret)));
			}
			try 
			{
				// CoReports フォームファイルを開く
				f = crDraw.OpenForm(FormFilePath);
				try 
				{
					// プリンタの設定
					SetPrinterConfig(crDraw.Printer);

					// 用紙サイズをフォームオブジェクトの設定にする
					crDraw.Printer.SetFormSize(f);
					
					// データのセット
					PrintEventArgs eventArgs = new PrintEventArgs()
												{
													crForm = f
												};
					writer.Write(this, eventArgs);
				} 
				finally 
				{
					f.Close();
				}

				// 印刷終了
				int retEndPrinter = crDraw.EndPrinter();
				if (retEndPrinter != (int)corError.corNoError)
				{
					throw new COReportsException(string.Format("EndPrinterで例外が発生しました={0}:{1}", retEndPrinter,
					COReportsUtils.GetErrMessage(retEndPrinter)));
				}
			} 
			catch
			{
				crDraw.AbortPrinter();
				throw;
			}
		}

		#endregion

	}
}
