using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoReports;
using CoReportsCore;
using System.IO;
using StatisticsPrint.Model.Print;
using StatisticsPrint.Model.Print.Common;

namespace StatisticsPrint.Model.COReports
{
	/// <summary>
	/// ドキュメント作成(プレビュー)
	/// </summary>
	public class COReportsDocument : COReportsExecBase
	{
		#region プロパティ
		/// <summary>
		/// 出力するドキュメントファイルパス
		/// </summary>
		public string DocumentFilePath
		{
			get;
			set;
		}
		
		#endregion

		#region メソッド

		/// <summary>
		/// ドキュメントファイルを作成します。
		/// </summary>
		/// <param name="draw">シーオーリポーツ 描画オブジェクト</param>
		public override void Output(CrDraw crDraw, IPrintWriter writer)
		{
			CrForm f = null;

			//すでに同じ名前のファイルがあったら削除
			DeleteFile(DocumentFilePath);
			// 初期化
			crDraw.Initialize();

			// フラグの指定
			crDraw.Flags = (int)corFlags.corFlagsCompress; //ドキュメントファイル及びPDFファイルを圧縮した形で出力

			// ドキュメントファイル作成開始
			int ret = crDraw.StartDocument(DocumentFilePath, DocumentName);
			if (ret != (int)corError.corNoError)
			{
				throw new COReportsException(string.Format("StartDocumentで例外が発生しました={0}:{1}", ret, 
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
				int retEndDocument = crDraw.EndDocument();
				if (retEndDocument != (int)corError.corNoError)
				{
					throw new COReportsException(string.Format("EndDocumentで例外が発生しました={0}:{1}", retEndDocument,
					COReportsUtils.GetErrMessage(retEndDocument)));
				}
			}
			catch
			{
				crDraw.AbortDocument();
				throw;
			}
		}

		/// <summary>
		/// ファイルの存在・使用チェック
		/// </summary>
		/// <param name="path">ファイルパス</param>
		private void DeleteFile(string path)
		{
			if (!File.Exists(path))
			{
				return;
			}
			File.Delete(path);
		}

		#endregion

	}
}
