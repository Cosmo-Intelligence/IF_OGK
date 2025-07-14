using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoReports;
using System.Runtime.InteropServices;
using System.Diagnostics;
using StatisticsPrint.Model.Print;
using StatisticsPrint.Model.Print.Common;

namespace StatisticsPrint.Model.COReports
{
	/// <summary>
	/// COReportsHelper
	/// </summary>
	public class COReportsHelper: IDisposable
	{
		#region フィールド
		/// <summary>
		/// ログオブジェクト
		/// </summary>
		private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(
			System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// CoReportsドローコントロール
		/// </summary>
		private CrDraw _draw = null;

		//デリゲートの宣言
		//TimeEventArgs型のオブジェクトを返すようにする
		public delegate void DoOutputEventHandler(object sender, PrintEventArgs e);

		//イベントデリゲートの宣言
		//public event DoPrintEventHandler DoPrint;
 	
		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public COReportsHelper()
		{
			_draw = new CrDrawClass();
		}
		#endregion

		#region メソッド

		/// <summary>
		/// 印刷する
		/// </summary>
		/// <param name="documentName">印刷JOBの帳票名</param>
		/// <param name="formFilePath">フォームパス</param>
		/// <param name="printerName">プリンタ名</param>
		/// <param name="copies">部数</param>
		/// <param name="writer">書き込みクラス</param>
		public void Print(string documentName, string formFilePath, string printerName,
			int copies, IPrintWriter writer)
		{
			COReportsPrintout printOut = new COReportsPrintout();
			printOut.DocumentName = documentName;
			printOut.FormFilePath = formFilePath;
			printOut.PrinterName = printerName;
			printOut.Copies = copies;
			printOut.Output(_draw, writer);
		}

		/// <summary>
		/// プレビュー表示する
		/// </summary>
		/// <param name="documentName">ドキュメントファイルの帳票名</param>
		/// <param name="formFilePath">フォームパス</param>
		/// <param name="DocumentFilePath">出力するドキュメントフルパス</param>
		/// <param name="copies">部数</param>
		/// <param name="writer">書き込みクラス</param>
		public void Preview(string documentName, string formFilePath, string documentFilePath,
			int copies, IPrintWriter writer)
		{
			COReportsDocument printOut = new COReportsDocument();
			printOut.DocumentName = documentName;
			printOut.FormFilePath = formFilePath;
			printOut.DocumentFilePath = documentFilePath;
			printOut.Copies = copies;
			printOut.Output(_draw, writer);
			Process.Start(documentFilePath);
		}

		#region IDisposable メンバ

		/// <summary>
		/// 破棄
		/// </summary>
		public void Dispose()
		{
			if (_draw == null)
			{
				return;
			}
			try
			{
				Marshal.ReleaseComObject(_draw);
			}
			catch (Exception e)
			{
				_log.Error(e);
			}
			finally
			{
				_draw = null;
			}
		}

		#endregion

		#endregion
	}

	/// <summary>
	/// OnPrintイベントで返されるデータ
	/// </summary>
	public class PrintEventArgs : EventArgs
	{
		public CrForm crForm
		{
			get;
			set;
		}
	}

}
