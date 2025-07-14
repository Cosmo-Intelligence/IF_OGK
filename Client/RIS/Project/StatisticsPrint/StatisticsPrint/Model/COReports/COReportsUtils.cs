using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoReports;

namespace StatisticsPrint.Model.COReports
{
	/// <summary>
	/// COReportsユーティリティ
	/// </summary>
	public static class COReportsUtils
	{
		/// <summary>
		/// エラーメッセージ取得
		/// シーオーリポーツのエラーメッセージ取得関数
		/// </summary>
		/// <param name="retVal">メソッドの戻り値</param>
		/// <returns>
		/// retVal	エラーメッセージ
		/// </returns>
		/// <remarks>
		/// COReportsのサンプルプログラムから流用
		/// </remarks>
		public static string GetErrMessage(int retVal)
		{
			string errMessage = "";
			//メソッドの戻り値によってエラーメッセージをかえます
			switch (retVal)
			{
				case (int)corError.corNoError:                  // 0
					errMessage = "正常終了";
					break;
				case (int)corError.corErrNotOpenDocument:       // -1
					errMessage = "シーオーリポーツドキュメントファイル未オープン";
					break;
				case (int)corError.corErrAlreadyOpenDocument:   // -2
					errMessage = "シーオーリポーツドキュメントファイルオープン済み";
					break;
				case (int)corError.corErrOpenDocument:          // -3
					errMessage = "シーオーリポーツドキュメントファイルオープンエラー";
					break;
				case (int)corError.corErrWriteDocument:         // -4
					errMessage = "シーオーリポーツドキュメントファイルライトエラー";
					break;
				case (int)corError.corErrReadDocument:          // -5
					errMessage = "シーオーリポーツドキュメントファイルリードエラー";
					break;
				case (int)corError.corErrNotOpenPrinter:		  // -6
					errMessage = "プリンタ開始指定がない";
					break;
				case (int)corError.corErrAlreadyOpenPrinter:    // -7
					errMessage = "プリンタ出力指定済み";
					break;
				case (int)corError.corErrInvalidHdc:            // -8
					errMessage = "デバイスコンテキストが有効でない";
					break;
				case (int)corError.corErrNoPrinter:             // -9
					errMessage = "プリンタが1つもインストールされていない";
					break;
				case (int)corError.corErrPrinterNotExists:      //-10
					errMessage = "指定されたプリンタがない";
					break;
				case (int)corError.corErrCreateDc:              //-11
					errMessage = "プリンタデバイスコンテキスト作成失敗";
					break;
				case (int)corError.corErrStartDoc:              //-12
					errMessage = "印刷JOB開始エラー";
					break;
				case (int)corError.corErrStartPage:             //-13
					errMessage = "ページ開始処理エラー";
					break;
				case (int)corError.corErrEndPage:               //-14
					errMessage = "ページ終了処理エラー";
					break;
				case (int)corError.corErrEndDoc:                //-15
					errMessage = "印刷JOB終了処理エラー";
					break;
				case (int)corError.corErrCreateDocument:        //-16
					errMessage = "シーオーリポーツドキュメントファイル作成エラー";
					break;
				case (int)corError.corErrOleDraw:               //-17
					errMessage = "OLE オブジェクトの描画に失敗";
					break;
				case (int)corError.corErrPrinterControl:        //-18
					errMessage = "プリンタ制御が正常に行われなかった";
					break;
				case (int)corError.corErrOutputNone:            //-19
					errMessage = "プリンタ制御エラー";
					break;
				case (int)corError.corErrObjectDraw:            //-20
					errMessage = "描画オブジェクトの描画に失敗";
					break;
				case (int)corError.corErrPaperSize:             //-21
					errMessage = "フォームのサイズが印刷可能な範囲内でない";
					break;
				case (int)corError.corErrAddFormDenied:         //-22
					errMessage = "用紙登録の権限がない";
					break;
				case (int)corError.corErrNotSupported:          //-23
					errMessage = "出力先がサポートされていない";
					break;
				case (int)corError.corErrCSVInvalidSeparator:   //-24
					errMessage = "CSVファイルのセパレータが不正な値";
					break;
				case (int)corError.corErrCSVEmptyFileName:      //-25
					errMessage = "CSVファイルのファイル名が指定されていない";
					break;
				case (int)corError.corErrCSVEmptyData:          //-26
					errMessage = "CSVファイルのデータが空";
					break;
				case (int)corError.corErrCSVNoAvailableField:   //-27
					errMessage = "フォームファイルにCSVファイルのデータをセットするフィールドがない";
					break;
				case (int)corError.corErrCSVNoMatch:            //-28
					errMessage = "フィールド数とCSVファイルのデータ数が一致しない";
					break;
				case (int)corError.corErrCSVDataField:          //-29
					errMessage = "CSVファイルからのデータセット時にデータフィールドでエラーが発生";
					break;
				case (int)corError.corErrCSVBarcodeField:       //-30
					errMessage = "CSVファイルからのデータセット時にバーコードフィールドでエラーが発生";
					break;
				case (int)corError.corErrCSVImageField:         //-31
					errMessage = "CSVファイルからのデータセット時にイメージフィールドでエラーが発生";
					break;
				case (int)corError.corErrCSVTextField:          //-32
					errMessage = "CSVファイルからのデータセット時にテキストフィールドでエラーが発生";
					break;
				case (int)corError.corErrCSVListField:          //-33
					errMessage = "CSVファイルからのデータセット時にリストフィールドでエラーが発生";
					break;
				case (int)corError.corErrCSVPDF417Field:        //-34
					errMessage = "CSVファイルからのデータセット時にPDF417バーコードフィールドでエラーが発生";
					break;
				case (int)corError.corErrCSVQRField:            //-35
					errMessage = "CSVファイルからのデータセット時にQRコードフィールドでエラーが発生";
					break;
				case (int)corError.corErrCSV:                   //-36
					errMessage = "CSVファイルからのデータセット時に「その他」のエラーが発生";
					break;
				case (int)corError.corErrNotPassword:           //-37
					errMessage = "指定したパスワードが正しくない";
					break;
				case (int)corError.corErrPrintNotPermitted:     //-38
					errMessage = "印刷許可のないドキュメントファイルの印刷を行ないました";
					break;
				case (int)corError.corErrNotOpenOption:        //-39
					errMessage = "オプションファイルをオープンせずにファイルのクローズを行ないました";
					break;
				case (int)corError.corErrAlreadyOpenOption:    //-40
					errMessage = "オプションファイルをオープンしてクローズするまでの間に再度ファイルのオープンを行ないました";
					break;
				case (int)corError.corErrOptionNotRegistered:  //-41
					errMessage = "オプションのライブラリが正常にレジストリ登録されていない";
					break;
				case (int)corError.corErrOpenExtCharFile:      //-42
					errMessage = "外字ファイルが開けない";
					break;
				case (int)corError.corErrUrl:					//-43
					errMessage = "URLが間違っている";
					break;
				case (int)corError.corErrInvalid:				//-44
					errMessage = "URLの参照データが不正";
					break;
				case (int)corError.corErrNoPage:					//-45
					errMessage = "印刷するページが存在しない";
					break;
				case -1000:
					errMessage = "何らかの原因でメモリの確保に失敗";
					break;
				case -1001:
					errMessage = "何らかの原因でPDFライブラリがロードされない";
					break;
				case -1003:
					errMessage = "何らかの原因でPDFライブラリの開始処理が既に行なわれている";
					break;
				case -1004:
					errMessage = "何らかの原因でPDFライブラリの開始処理がまだ行なわれていない";
					break;
				case -1008:
					errMessage = "何らかの原因でPDFファイルの作成に失敗";
					break;
				case -1009:
					errMessage = "何らかのエラーが発生";
					break;
				case -1010:
					errMessage = "PDFライブラリで出力するものがない";
					break;
				case -1011:
					errMessage = "何らかの原因でPDFファイルの書き込みができない";
					break;
				case -1100:
					errMessage = "何らかの原因で画像ファイルがオープンできない";
					break;
				case -1101:
					errMessage = "画像ファイルがPDFライブラリで処理できない";
					break;
				default:
					errMessage = "想定外のエラー";
					break;
			}
			return errMessage;
		}
		

	}
}
