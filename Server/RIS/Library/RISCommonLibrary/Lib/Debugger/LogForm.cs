using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace RISCommonLibrary.Lib.Debugger
{
	public partial class LogForm : Form
	{
		public LogForm()
		{
			InitializeComponent();
		}

		private void LogForm_Load(object sender, EventArgs e)
		{
			Debug.Listeners.Add(new RichTextBoxWriterTraceListener(logRichTextBox, 3000));
		}

		private void hideButton_Click(object sender, EventArgs e)
		{
			Hide();
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			logRichTextBox.Clear();
		}

	}
}
