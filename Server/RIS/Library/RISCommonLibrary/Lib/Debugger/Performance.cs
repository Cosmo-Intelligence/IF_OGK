using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace RISCommonLibrary.Lib.Debugger
{

    /// <summary>
    /// パフォーマンスの確認を行うクラスです。
    /// </summary>
    /// <remarks>
    /// http://sinproject.blog47.fc2.com/blog-entry-45.html
    /// からコピー
    /// </remarks>
    public class Performance
    {
        public delegate string CallbackString();

        private int m_iTestCount = 1;
        private int m_iCaptionLength = 50;
        private string m_strFromatCaption = null;
        private string m_strFormatResult = "{0:000.000000} / {1:000.000000} : {2}";
        private Stopwatch m_stopwatch = new Stopwatch();
        private long m_lFirstTick = 0L;
        private long m_lLastTick = 0L;
        private string m_strTitle = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Performance()
        {
            // do nothing
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="iTestCount">テストの回数</param>
        /// <param name="iCaptionLength">キャプション文字列の長さ</param>
        public Performance(int iTestCount, int iCaptionLength)
        {
            m_iTestCount = iTestCount;
            m_iCaptionLength = iCaptionLength;
        }

        /// <summary>
        /// パフォーマンス確認を開始します。
        /// </summary>
        public void Start(string strTitle)
        {
            m_strTitle = strTitle;
            m_strFromatCaption = "{0, -" + m_iCaptionLength.ToString() + "}: ";

            Console.WriteLine("\r\n--- performance check [{0}] start --- >>", m_strTitle);
            Console.WriteLine("{0, -" + m_iCaptionLength.ToString() + "}: Time-Span  / Time-Total : Result ", "Caption");
            m_stopwatch.Start();
            m_lFirstTick = m_stopwatch.ElapsedTicks;
            m_lLastTick = m_lFirstTick;
        }

        /// <summary>
        /// Split共通処理用メソッドです。
        /// </summary>
        /// <param name="bShowCaption">キャプション文字列を表示するかどうか</param>
        /// <param name="bShowResult">結果表示を行うかどうか</param>
        /// <param name="strCaption">キャプション文字列</param>
        /// <param name="objValue">表示したい値がある場合は指定します。</param>
        private void Split(bool bShowCaption, bool bShowResult, string strCaption, object objValue)
        {
            long lSplit = m_stopwatch.ElapsedTicks;

            if (bShowCaption)
            {
                Console.Write(m_strFromatCaption, strCaption);
            }

            if (bShowResult)
            {
                Console.WriteLine(
                    m_strFormatResult,
                    (double)(lSplit - m_lLastTick) / (double)Stopwatch.Frequency,
                    (double)(lSplit - m_lFirstTick) / (double)Stopwatch.Frequency,
                    objValue
                    );
            }

            m_lLastTick = lSplit;
        }

        /// <summary>
        /// 以前のチェックポイントからの時間を計測します。
        /// </summary>
        /// <param name="strCaption">キャプション文字列</param>
        /// <param name="objValue">表示したい値がある場合は指定します。</param>
        public void Split(string strCaption, object objValue)
        {
            Split(true, true, strCaption, objValue);
        }

        /// <summary>
        /// パフォーマンス確認のチェックポイントをセットします。
        /// </summary>
        /// <param name="strCaption">キャプション文字列</param>
        public void BeginSplit(string strCaption)
        {
            Split(true, false, strCaption, null);
        }

        /// <summary>
        /// 以前のチェックポイントからの結果を表示します。
        /// </summary>
        /// <param name="objValue">表示したい値がある場合は指定します。</param>
        public void EndSplit(object objValue)
        {
            Split(false, true, null, objValue);
        }

        /// <summary>
        /// Callback で指定された Method のパフォーマンス確認を行います。
        /// </summary>
        /// <param name="callbackString">Callback する Method</param>
        /// <param name="strCaption">キャプション文字列</param>
        public void CheckString(CallbackString callbackString, string strCaption)
        {
            StringBuilder sbBuf = new StringBuilder();

            BeginSplit(strCaption);
            for (int i = 0; i < m_iTestCount; i++)
            {
                sbBuf.Append(callbackString());
            }
            EndSplit(null);
        }

        /// <summary>
        /// パフォーマンス確認を停止します。
        /// </summary>
        public void Stop()
        {
            m_stopwatch.Stop();
            Console.WriteLine("--- performance check [{0}] end ----- <<", m_strTitle);
        }
    }

    #region 使用例

    /*
//Performanceテスト用のクラスを改良
//3つの計測方法をサポート（詳細は以下）

using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

namespace ConsoleApplication
{
	class Program
	{
		public const int LIMIT = 30000000;

		static void Main(string[] args)
		{
			Performance		performance =  new Performance(LIMIT, 40);
			StringBuilder	sbBuf		= null;

			// 計測方法1 Splitのみ
			// 前回のSplitからの差分を計測して表示します。
			// 使い方は一番単純で簡単。それぞれの分割時間=合計時間 となります。
			performance.Start("Split Only");
			{
				sbBuf = new StringBuilder();
				for (int i = 0; i < LIMIT; i++) {
					sbBuf.Append(Test1());
				}
				performance.Split("Test1", null);

				sbBuf = new StringBuilder();
				for (int i = 0; i < LIMIT; i++) {
					sbBuf.Append(Test2());
				}
				performance.Split("Test2", null);
			}
			performance.Stop();

			// 計測方法2 BeginSplit-EndSplit
			// 分割計測の開始位置と終了位置を指定して計測します。
			// 計測中のCaptionを先に表示したり、個所箇所で厳密に計測したい場合はこちら。
			performance.Start("BeginSplit-EndSplit");
			{
				sbBuf = new StringBuilder();
				performance.BeginSplit("Test1");
				for (int i = 0; i < LIMIT; i++) {
					sbBuf.Append(Test1());
				}
				performance.EndSplit(null);

				sbBuf = new StringBuilder();
				performance.BeginSplit("Test2");
				for (int i = 0; i < LIMIT; i++) {
					sbBuf.Append(Test2());
				}
				performance.EndSplit(null);
			}
			performance.Stop();
			
			// 計測方法3 Callbackタイプ
			// コールバック関数を実行します。
			// このやり方では、引数や戻り値が違う場合はメソッドの追加が必要となります。
			performance.Start("Callback");
			{
				performance.CheckString(Test1, "Test1");
				performance.CheckString(Test2, "Test2");
			}
			performance.Stop();

			WaitKeyPress();
		}

		private static string Test1()
		{
			return String.Empty + String.Empty + String.Empty;
		}
		
		private static string Test2()
		{
			return "" + "" + "";
		}
		
		private static void WaitKeyPress()
		{
			Console.Write("\r\npress any key.");
			Console.Read();
		}
	}
 
 */
    #endregion

}
