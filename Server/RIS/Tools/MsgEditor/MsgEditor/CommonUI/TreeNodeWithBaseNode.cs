using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RISCommonLibrary.Lib.Msg;

namespace MsgEditor.CommonUI
{
	internal class TreeNodeWithBaseNode : TreeNode
	{
		public TreeNodeWithBaseNode(string text) :
			base(text)
		{
			InitializeComponent();
		}

		public TreeNodeWithBaseNode(string text, BaseNode data) :
			base(text)
		{
			InitializeComponent();
			Data = data;
		}

		private void InitializeComponent()
		{
		}

		public BaseNode Data
		{
			get;
			set;
		}
	}
}
