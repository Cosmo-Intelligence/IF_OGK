using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace RISCommonLibrary.Lib.Debugger
{
	public class RichTextBoxWriterTraceListener : TraceListener
	{
		private RichTextBox _textBox;
		private Int32 _maxRow;

		private delegate void TextBoxInvoker(string args);

		public RichTextBoxWriterTraceListener(RichTextBox richTextBox, Int32 maxRow)
		{
			this._textBox = richTextBox;
			//リッチテキストのフォントが勝手に変更されるのを抑制する
			this._textBox.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
			this._maxRow = maxRow;
		}

		public override void Write(string message)
		{
			if (this._textBox == null)
			{
				return;
			}
			if (!(this._textBox.IsHandleCreated))
			{
				return;
			}
			if (this._textBox.InvokeRequired)
			{
				// 別スレッドから呼び出された場合
				this._textBox.BeginInvoke(new TextBoxInvoker(this.WriteTextBox), 
					new object[] {message});
				return;
			}
			WriteTextBox(message);
		}

		private void WriteTextBox(string logMessage)
		{
			if (logMessage == null)
			{
				return;
			}

			int startIndex = this._textBox.Text.Length;
			this._textBox.SelectionStart = startIndex;
			this._textBox.AppendText(logMessage);
			this._textBox.SelectionLength = this._textBox.Text.Length - 
				this._textBox.SelectionStart;
		}

		public override void WriteLine(string logMessage)
		{
			if (this._textBox == null)
			{
				return;
			}
			if (!(this._textBox.IsHandleCreated))
			{
				return;
			}

			if (this._textBox.InvokeRequired)
			{
				// 別スレッドから呼び出された場合
				object[] args = new object[] { logMessage };
				this._textBox.BeginInvoke(new TextBoxInvoker(WriteLineTextBox), 
					new object[] {logMessage});
				return;
			}

			WriteLineTextBox(logMessage);
		}

		private void WriteLineTextBox(string logMessage)
		{
			if (logMessage == null)
			{
				return;
			}

			int startIndex = this._textBox.Text.Length;
			this._textBox.SelectionStart = startIndex;
			this._textBox.AppendText(logMessage + "\n");
			this._textBox.SelectionLength = this._textBox.Text.Length - 
			this._textBox.SelectionStart;

			if (this._textBox.Lines.Count() > _maxRow +1)
			{
				int pos = this._textBox.GetFirstCharIndexFromLine(1);
				this._textBox.Select(0, pos);
				this._textBox.SelectedText = string.Empty;
			}

			this._textBox.Select(this._textBox.TextLength, 0);
			this._textBox.ScrollToCaret();

		}
	
	}
}
