using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MsgEditor.CommonUI;

namespace MsgEditor
{
	public partial class SearchForm : Form
	{
		private TreeView _targetTreeView;
		private SearchNodeHelper _searchNodeHelper;

		internal SearchNodeHelper SearchInstance
		{
			set
			{
				_searchNodeHelper = value;
			}
		}

		public string DefaultSearchString
		{
			set
			{
				searchTextBox.Text = value;
			}
		}

		public TreeView TargetTreeView
		{
			set
			{
				_targetTreeView = value;
			}
		}

		public SearchForm()
		{
			InitializeComponent();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void candidateDownsideButton_Click(object sender, EventArgs e)
		{
			SearchDown();
		}

		private void candidateUpsideButton_Click(object sender, EventArgs e)
		{
			SearchUp();
		}

		private void searchForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Shift && e.KeyCode == Keys.F3)
			{
				SearchUp();
				return;
			}
			SearchDown();
		}

		private void SearchDown()
		{
			_searchNodeHelper.SearchString = searchTextBox.Text;
			TreeNode n = _searchNodeHelper.SearchDownside();
			if (n == null)
			{
				Console.WriteLine("見つかりませんでした");
				return;
			}
			_targetTreeView.SelectedNode = n;
		}

		private void SearchUp()
		{
			_searchNodeHelper.SearchString = searchTextBox.Text;
			TreeNode n = _searchNodeHelper.SearchUpside();
			if (n == null)
			{
				Console.WriteLine("見つかりませんでした");
				return;
			}
			_targetTreeView.SelectedNode = n;
		}
	}
}
