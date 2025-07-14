using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace RISCommonLibrary.Lib.Debugger
{
	public class TextBoxWriterTraceListener : TraceListener
	{
		private TextBox _textBox;
		private Int32 _maxRow;
		private delegate void TextBoxInvoker(string args);

		public TextBoxWriterTraceListener(TextBox textBox, Int32 maxRow)
		{
			this._textBox = textBox;
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
				this._textBox.BeginInvoke(new TextBoxInvoker(WriteTextBox), 
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
			this._textBox.AppendText(logMessage);
		}

		public override void WriteLine(string message)
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
				object[] args = new object[] { message };
				this._textBox.BeginInvoke(new TextBoxInvoker(WriteLineTextBox), 
					new object[] {message});
				return;
			}

			WriteLineTextBox(message);
		}

		private void WriteLineTextBox(string logMessage)
		{
			if (logMessage == null)
			{
				return;
			}
			if (this._textBox.Lines.Count() > _maxRow)
			{
				this._textBox.ScrollBars = ScrollBars.None;
				this._textBox.Text = this._textBox.Text.Remove
				    (0, this._textBox.Lines[0].Length + Environment.NewLine.Length);
				this._textBox.ScrollBars = ScrollBars.Both;
				//string[] lines = this.textBox.Lines;
				//String[] dst = new []{Convert.ToString(args) + Environment.NewLine};
				//this.textBox.Lines = 
				//    (lines.Skip(1).Concat<String>(dst)).ToArray<String>();
				//return;
			}

			this._textBox.AppendText(logMessage + Environment.NewLine);
		}
	}
}
