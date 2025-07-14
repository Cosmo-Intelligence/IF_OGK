using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MsgEditor.CommonUI
{
	internal class SearchNodeHelper
	{

		public string SearchString
		{
			get;
			set;
		}

		public TreeView Tree
		{
			get;
			set;
		}

		public TreeNode SearchDownside()
		{
			if (string.IsNullOrEmpty(SearchString))
			{
				return null;
			}
			if (Tree.Nodes.Count < 1)
			{
				return null;
			}

			TreeNode targetNode = Tree.SelectedNode;
			if (targetNode == null)
			{
				targetNode = Tree.Nodes[0]; //指定されていなかったらルートから
			}
			return SearchDownsideInner(SearchString, targetNode);
		}

		public TreeNode SearchUpside()
		{
			if (string.IsNullOrEmpty(SearchString))
			{
				return null;
			}
			if (Tree.Nodes.Count < 1)
			{
				return null;
			}

			TreeNode targetNode = Tree.SelectedNode;
			if (targetNode == null)
			{
				targetNode = Tree.Nodes[0]; //指定されていなかったらルートから
			}
			return SearchUpsideInner(SearchString, targetNode);
		}

		private TreeNode SearchDownsideInner(string searchString, TreeNode firstNode)
		{
			if (firstNode == null)
			{
				return null;
			}

			TreeNode n = firstNode;
			//下候補は自分はみないで自分の子供から
			TreeNode childNode = SearchChildDownside(searchString, n.Nodes);
			if (childNode != null)
			{
				return childNode;
			}
			while ((n = n.NextNode) != null)
			{
				if (Regex.IsMatch(n.Text, Regex.Escape(searchString),
					RegexOptions.IgnoreCase | RegexOptions.Singleline))
				{
					return n;
				}
				childNode = SearchChildDownside(searchString, n.Nodes);
				if (childNode != null)
				{
					return childNode;
				}
			};

			if (firstNode.Parent == null)
			{
				return null;
			}

			return SearchDownsideInner(searchString, firstNode.Parent.NextNode);
		}

		private TreeNode SearchUpsideInner(string searchString, TreeNode firstNode)
		{
			if (firstNode == null)
			{
				return null;
			}

			TreeNode n = firstNode;
			//上候補は、上の兄弟から
			while ((n = n.PrevNode) != null)
			{
				TreeNode childNode = SearchChildUpside(searchString, n.Nodes);
				if (childNode != null)
				{
					return childNode;
				}
				if (Regex.IsMatch(n.Text, Regex.Escape(searchString),
					RegexOptions.IgnoreCase | RegexOptions.Singleline))
				{
					return n;
				}
			};

			if (firstNode.Parent == null)
			{
				return null;
			}
			return SearchUpsideInner(searchString, firstNode.Parent);
		}

		private TreeNode SearchChildDownside(string searchString, TreeNodeCollection nc)
		{
			if (nc == null)
			{
				return null;
			}

			for (int idx = 0; idx < nc.Count; idx++)
			{
				TreeNode item = nc[idx];
				if (Regex.IsMatch(item.Text, Regex.Escape(searchString),
					RegexOptions.IgnoreCase | RegexOptions.Singleline))
				{
					return item;
				}
				TreeNode childNode = SearchChildDownside(searchString, item.Nodes);
				if (childNode != null)
				{
					return childNode;
				}
			}
			return null;

		}

		private TreeNode SearchChildUpside(string searchString, TreeNodeCollection nc)
		{
			if (nc == null)
			{
				return null;
			}

			for (int idx = nc.Count -1; idx > -1; idx--)
			{
				TreeNode item = nc[idx];
				//上候補は子供たちから見る
				TreeNode childNode = SearchChildUpside(searchString, item.Nodes);
				if (childNode != null)
				{
					return childNode;
				}
				if (Regex.IsMatch(item.Text, Regex.Escape(searchString),
					RegexOptions.IgnoreCase | RegexOptions.Singleline))
				{
					return item;
				}
			}
			return null;
		}

	}
}
