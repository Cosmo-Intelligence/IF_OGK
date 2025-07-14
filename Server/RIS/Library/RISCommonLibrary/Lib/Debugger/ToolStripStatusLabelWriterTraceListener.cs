using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace RISCommonLibrary.Lib.Debugger
{
	public class ToolStripStatusLabelWriterTraceListener : TraceListener
	{
		private ToolStripStatusLabel _label;

		private delegate void LabelInvoker(string args);

		public ToolStripStatusLabelWriterTraceListener(ToolStripStatusLabel label)
		{
			this._label = label;
		}

		public override void Write(string message)
		{
			WriteLabel(message);
		}

		public override void WriteLine(string message)
		{
			WriteLabel(message);
		}

		private void WriteLabel(string logMessage)
		{
			if (logMessage == null)
			{
				return;
			}
			if (this._label == null)
			{
				return;
			}
			this._label.Text = logMessage;
		}
	}
}
