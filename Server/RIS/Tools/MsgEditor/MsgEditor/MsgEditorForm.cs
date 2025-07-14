using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MsgEditor.CommonUI;
using RISCommonLibrary.Lib.Exceptions;
using RISCommonLibrary.Lib.Msg;
using RISCommonLibrary.Lib.Msg.Common;
using RISCommonLibrary.Lib.Utils;

namespace MsgEditor
{
	public partial class MsgEditorForm : Form
	{

		#region field
		
		/// <summary>
		/// 現在選択しているメッセージクラス
		/// </summary>
		private BaseMsg _msg;

		/// <summary>
		/// 現在編集対象のファイル名
		/// </summary>
		private string _editFile;

		/// <summary>
		/// ノード検索
		/// </summary>
		private SearchNodeHelper _searchNodeHelper;
	
		/// <summary>
		/// 検索フォーム
		/// </summary>
		private SearchForm _searchForm;

		#endregion

		#region property
		
		/// <summary>
		/// エディット対象ファイル名
		/// </summary>
		public string EditFile
		{
			get
			{
				return _editFile;
			}
			set
			{
				_editFile = value;


				const string FORMAT_FORM_TEXT = "メッセージエディタ [{0}]";
				if (string.IsNullOrEmpty(_editFile))
				{
					this.Text = string.Format(FORMAT_FORM_TEXT, "新規");
					return;
				}
				this.Text = string.Format("{0} {1}", Path.GetFileName(_editFile), _editFile);
			}
		}

		/// <summary>
		/// エディット中
		/// </summary>
		public bool IsEditing
		{
			get
			{
				return _msg != null;
			}
		}

		#endregion

		#region コンストラクタ
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MsgEditorForm()
		{
			InitializeComponent();

			process1.StartInfo.FileName = Application.ExecutablePath;

			#region TreeView上でTooltipを非表示にする
			treeView1.ShowNodeToolTips = true;
			toolTip1.SetToolTip(treeView1, "tekito");
			toolTip1.Active = false;
			#endregion

			_searchForm = new SearchForm();
			_searchForm.Owner = this;
			_searchForm.TargetTreeView = treeView1;
			_searchNodeHelper = new SearchNodeHelper();
			_searchNodeHelper.Tree = treeView1;
			_searchForm.SearchInstance = _searchNodeHelper;
		}

		#endregion

		#region event
		
		/// <summary>
		/// フォームロードイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MsgEditorForm_Load(object sender, EventArgs e)
		{
			RegistMsgToFactory.Regist();

			#region 新規作成メニュー
			List<Type>list = MsgFactory.Instance.MsgTypeList;
			foreach (Type item in list)
			{
				BaseMsg baseMsg = Activator.CreateInstance(item) as BaseMsg;
				if (baseMsg == null)
				{
					continue;
				}

				ToolStripMenuItemWithMsg newItem = 
					new ToolStripMenuItemWithMsg(baseMsg.MessageNameJ, null, newMsgMenuItem_Click, item);
				newFileMenuItem.DropDownItems.Add(newItem);
			}
			#endregion

			this.treeView1.ContextMenuStrip = contextMenuStrip1;

			String[] arguments = Environment.GetCommandLineArgs();
			if (arguments.Count() < 2)
			{
				return;
			}
			OpenFileToForm(arguments[1]);

			this.ActiveControl = treeView1;
		}

		private void MsgEditorForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_searchForm != null)
			{
				_searchForm.Close();
			}
		}

		/// <summary>
		/// 閉じるメニュー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// 開くメニュー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenFileMenuItemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName = GetFileNameByDailog(EditFile);
			OpenFileToForm(fileName);
		}

		/// <summary>
		/// ツリービューノード選択後
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNodeWithBaseNode nbn = e.Node as TreeNodeWithBaseNode;
			if (nbn == null)
			{
				return;
			}
			SetDataToInfos(nbn.Data);
		}

		/// <summary>
		/// 確定ボタン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void confirmButton_Click(object sender, EventArgs e)
		{
			Trace.WriteLine("confirmButton_Click");
			if (_msg == null)
			{
				return;
			}
			EditNode(_msg, nodePathTextBox.Text, dataTextBox.Text, treeView1.Nodes);
			msgTextBox.Text = _msg.TextMessageByTrimedEnclosure;
			TreeNodeWithBaseNode node = treeView1.SelectedNode as TreeNodeWithBaseNode;
			SelectText(node.Data);
			treeView1.Focus();
		}

		/// <summary>
		/// 上書き保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveMenuItem_Click(object sender, EventArgs e)
		{
			if (_msg == null)
			{
				return;
			}

			string saveFilePath = EditFile;
			if (string.IsNullOrEmpty(saveFilePath))
			{
				if (saveFileDialog1.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				saveFilePath = saveFileDialog1.FileName;
			}
			SaveText(saveFilePath, _msg.TextMessage);
			EditFile = saveFilePath;
		}

		/// <summary>
		/// 名前をつけて保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void saveAsMenuItem_Click(object sender, EventArgs e)
		{
			if (_msg == null)
			{
				return;
			}

			if (!string.IsNullOrEmpty(EditFile))
			{
				saveFileDialog1.InitialDirectory = Path.GetDirectoryName(EditFile);
				saveFileDialog1.FileName = Path.GetFileName(EditFile);
			}
			if (saveFileDialog1.ShowDialog() != DialogResult.OK)
			{
				return;
			}

			SaveText(saveFileDialog1.FileName, _msg.TextMessage);
			EditFile = saveFileDialog1.FileName;
		}

		/// <summary>
		/// ポップアップメニューの追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addMenuItem_Click(object sender, EventArgs e)
		{
			TreeNodeWithBaseNode n = this.treeView1.SelectedNode as TreeNodeWithBaseNode;
			if (n == null)
			{
				return;
			}
			BaseNode bn = n.Data;
			if (bn is DynamicArrayNode)
			{
				DynamicArrayNode da = (DynamicArrayNode)bn;
				AddElement(da);
				return;
			}
			AddElement(bn as ArrayNode);
		}

		/// <summary>
		/// ポップアップメニューの削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void deleteMenuItem_Click(object sender, EventArgs e)
		{
			TreeNodeWithBaseNode n = this.treeView1.SelectedNode as TreeNodeWithBaseNode;
			TreeNodeWithBaseNode parent = n.Parent as TreeNodeWithBaseNode;

			if (n == null)
			{
				return;
			}
			BaseNode bn = n.Data;
			DeleteElement(bn);
		}

		/// <summary>
		/// ポップアップメニュー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			addMenuItem.Enabled = false;
			deleteMenuItem.Enabled = false;
			TreeNodeWithBaseNode n = this.treeView1.SelectedNode as TreeNodeWithBaseNode;
			if (n == null)
			{
				return;
			}
			BaseNode bn = n.Data;
			addMenuItem.Enabled = bn.IsAddAble();
			deleteMenuItem.Enabled = bn.IsDeleteAble();
		}

		/// <summary>
		/// 新規作成の子メニュークリックイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void newMsgMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItemWithMsg clickedItem = sender as ToolStripMenuItemWithMsg;
			if (clickedItem == null)
			{
				return;
			}

			_msg = Activator.CreateInstance(clickedItem.MsgType) as BaseMsg;
			SetDataFromMsg(_msg);
			EditFile = "";
		}

		/// <summary>
		/// ドラッグエンター
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
				return;
			}
			e.Effect = DragDropEffects.None;
		}

		/// <summary>
		/// ドラッグドロップ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_DragDrop(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				return;
			}

			foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
			{
				if (!this.IsEditing)
				{
					OpenFileToForm(fileName);
					continue;
				}
				process1.StartInfo.Arguments = fileName;
				process1.Start();
			}
		}

		/// <summary>
		/// 再読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void refleshMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileToForm(EditFile);
		}

		/// <summary>
		/// ツリービューキーダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				dataTextBox.Focus();
				return;
			}

			if (e.Control && e.KeyCode == Keys.C)
			{
				TreeNode node = treeView1.SelectedNode;
				if (node == null)
				{
					return;
				}

				TreeNodeWithBaseNode nbn = node as TreeNodeWithBaseNode;
				if (nbn == null)
				{
					return;
				}
				BaseNode bn = nbn.Data as BaseNode;
				if (bn == null)
				{
					return;
				}
				Clipboard.SetText(bn.Snapshot);
			}
		}

		/// <summary>
		/// データテキストボックスキーダウン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (confirmButton.Enabled)
				{
					confirmButton_Click(sender, null);
					return;
				}
			}
		}

		/// <summary>
		/// 電文長再計算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void recalcLengthMenuItem_Click(object sender, EventArgs e)
		{
			if (_msg == null)
			{
				return;
			}
			_msg.Body.ReCalcDataLength();
			SetDataFromMsg(_msg);
		}

		/// <summary>
		/// ツリービューダブルクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void treeView1_DoubleClick(object sender, EventArgs e)
		{
			TreeNodeWithBaseNode node = treeView1.SelectedNode as TreeNodeWithBaseNode;
			if (node == null)
			{
				return;
			}
			dataTextBox.Focus();
		}

		/// <summary>
		/// 検索メニュークリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void searchMenuItem_Click(object sender, EventArgs e)
		{
			TreeNodeWithBaseNode node = treeView1.SelectedNode as TreeNodeWithBaseNode;
			if (node != null)
			{
				BaseNode bn = node.Data;
				if (bn != null)
				{
					_searchForm.DefaultSearchString = string.Format("{0}-{1}", bn.NameJ, bn.Name);
				}
			}
			_searchForm.Show();
		}

		/// <summary>
		/// 下候補
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void candidateDownsideMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode n = _searchNodeHelper.SearchDownside();
			if (n == null)
			{
				Console.WriteLine("見つかりませんでした");
				return;
			}
			treeView1.SelectedNode = n;
		}

		/// <summary>
		/// 上候補
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void candidateUpsideMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode n = _searchNodeHelper.SearchUpside();
			if (n == null)
			{
				Console.WriteLine("見つかりませんでした");
				return;
			}
			treeView1.SelectedNode = n;
		}
		#endregion

		#region method

		/// <summary>
		/// 開くダイアログでファイル名を取得する
		/// </summary>
		/// <returns></returns>
		private string GetFileNameByDailog(string initFileName)
		{
			if (!String.IsNullOrEmpty(initFileName))
			{
				openFileDialog1.InitialDirectory = Path.GetDirectoryName(Path.GetFullPath(initFileName));
				openFileDialog1.FileName = Path.GetFileName(initFileName);
			}
			if (openFileDialog1.ShowDialog() != DialogResult.OK)
			{
				return "";
			}

			return openFileDialog1.FileName;
		}

		/// <summary>
		/// 指定のMsgの内容をツリーに反映させる
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="nodes"></param>
		private void DrawTree(BaseNode msg, TreeNodeCollection nodes)
		{
			
			TreeNodeWithBaseNode newNode = new TreeNodeWithBaseNode(msg.Snapshot, msg);
			nodes.Add(newNode);
			DrawChildNodes(msg, newNode.Nodes);
		}

		/// <summary>
		/// 指定のMsgの子供クラスをツリーに反映させる
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="nodes"></param>
		private void DrawChildNodes(BaseNode msg, TreeNodeCollection nodes)
		{
			for (int idx = 0; idx < msg.Count; idx++)
			{
				BaseNode msgItem = msg[idx];
				DrawTree(msgItem, nodes);
			}
		}

		/// <summary>
		/// データの内容を上部テキストボックスに表示する
		/// </summary>
		/// <param name="b"></param>
		private void SelectText(BaseNode b)
		{
			msgTextBox.SelectionLength = 0;
			if (b == null)
			{
				return;
			}

			if (b.NodeInfo.FieldLength < 1)
			{
				return;
			}

			msgTextBox.SelectionStart = MBCSHelper.ShiftJisCountToCharCount(
				msgTextBox.Text, 0, b.GetStartPos() -1);
			msgTextBox.SelectionLength = MBCSHelper.ShiftJisCountToCharCount(msgTextBox.Text, 
				b.GetStartPos() -1, b.NodeInfo.FieldLength);
		}

		private void SetDataToInfos(BaseNode b)
		{
			nodePathTextBox.Clear();
			startPosTextBox.Clear();
			sizeTextBox.Clear();
			dataTextBox.Clear();
			msgTextBox.SelectionLength = 0;
			if (b == null)
			{
				return;
			}
			SelectText(b);

			nodePathTextBox.Text = b.Path;
			//MaxByteLengthTextBoxコントロールのMaxByteLength使用
			dataTextBox.MaxByteLength = b.Size;
			if (b is DataNode)
			{
				startPosTextBox.Text = Convert.ToString(b.GetStartPos());
				sizeTextBox.Text = Convert.ToString(b.Size);
				dataTextBox.Text = b.Data;
				confirmButton.Enabled =true;
				return;
			}
			if (b is ArrayNode)
			{
				ArrayNode ba = (ArrayNode)b;
				startPosTextBox.Text = Convert.ToString(ba.GetStartPos());
				sizeTextBox.Text = Convert.ToString(ba.Size);
				dataTextBox.Text = Convert.ToString(ba.ChildCount);
				confirmButton.Enabled =false;
				return;
			}
			confirmButton.Enabled =false;
		}

		private void EditNode(BaseMsg srcMsg, string nodePath, string value,
			TreeNodeCollection nodes)
		{

			string nodePathWithoutBodyName = nodePath.Remove(0, srcMsg.Body.Name.Length +1);

			BaseNode node = srcMsg.Body.GetNodeByPath(nodePathWithoutBodyName);
			if (node == null)
			{
				return;
			}
			node.Data = value;
			TreeNodeWithBaseNode tnbs = FindNode(node, nodes) as TreeNodeWithBaseNode;
			if (tnbs == null)
			{
				return;
			}
			SetDataToTreeNode(node, tnbs);
		}

		private void SetDataToTreeNode(BaseNode node, TreeNodeWithBaseNode dst)
		{
			dst.Text = node.Snapshot;
			dst.Data = node;
		}

		private TreeNode FindNode(BaseNode baseNode, TreeNodeCollection nodes)
		{
			if (nodes == null)
			{
				return null;
			}
			for (int idx = 0; idx < nodes.Count; idx++)
			{
				TreeNodeWithBaseNode node = nodes[idx] as TreeNodeWithBaseNode;
				if (node == null)
				{
					continue;
				}

				TreeNode childNode = FindNode(baseNode, node.Nodes);
				if (childNode != null)
				{
					return childNode;
				}

				if (node.Data != baseNode)
				{
					continue;
				}
				return node;
			}
			return null;
		}

		private void SaveText(string fileFullName, string text)
		{
			File.WriteAllText(fileFullName, text, Encoding.GetEncoding("Shift_JIS"));
		}

		private void AddElement(IArrayNode arrayNode)
		{
			if (arrayNode == null)
			{
				return;
			}

			arrayNode.Add();

			treeView1.BeginUpdate();
			try
			{
				TreeNodeWithBaseNode tnbs = FindNode((BaseNode)arrayNode, treeView1.Nodes) as TreeNodeWithBaseNode;
				if (tnbs == null)
				{
					return;
				}
				treeView1.Nodes.Clear();
				DrawTree(_msg.Body, treeView1.Nodes);
				treeView1.ExpandAll();

				tnbs = FindNode((BaseNode)arrayNode, treeView1.Nodes) as TreeNodeWithBaseNode;
				treeView1.SelectedNode = tnbs;
				treeView1.TopNode = tnbs;

				msgTextBox.Text = _msg.TextMessageByTrimedEnclosure;
				SelectText(tnbs.Data);

			}
			finally
			{
				treeView1.EndUpdate();
			}
		}

		private void DeleteElement(BaseNode childNode)
		{
			if (childNode == null)
			{
				return;
			}

			BaseNode parent = childNode.Parent;
			parent.Delete(childNode.Index);
			TreeNodeWithBaseNode tnbs;
			treeView1.BeginUpdate();
			try
			{
				tnbs = FindNode(parent, treeView1.Nodes) as TreeNodeWithBaseNode;
				if (tnbs == null)
				{
					return;
				}
				treeView1.Nodes.Clear();
				DrawTree(_msg.Body, treeView1.Nodes);
				treeView1.ExpandAll();

				tnbs = FindNode(parent, treeView1.Nodes) as TreeNodeWithBaseNode;
				treeView1.SelectedNode = tnbs;
				treeView1.TopNode = tnbs;

				msgTextBox.Text = _msg.TextMessageByTrimedEnclosure;
				SelectText(tnbs.Data);
			}
			finally
			{
				treeView1.EndUpdate();
			}
		}

		/// <summary>
		/// 画面に設定する
		/// </summary>
		/// <param name="srcMsg"></param>
		private void SetDataFromMsg(BaseMsg srcMsg)
		{
			msgTextBox.Clear();
			msgTextBox.Text = srcMsg.TextMessageByTrimedEnclosure;

			treeView1.BeginUpdate();
			try
			{
				treeView1.Nodes.Clear();
				DrawTree(srcMsg.Body, treeView1.Nodes);
				treeView1.ExpandAll();
				TreeNodeWithBaseNode firstNode = treeView1.Nodes[0] as TreeNodeWithBaseNode;
				treeView1.SelectedNode = firstNode;
				treeView1.TopNode = firstNode;
				msgTextBox.SelectionLength = 0;
			}
			finally
			{
				treeView1.EndUpdate();
			}
		}

		/// <summary>
		/// ファイルを開きます
		/// </summary>
		/// <param name="fileName"></param>
		private void OpenFileToForm(String fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				return;
			}
			if (!File.Exists(fileName))
			{
				return;
			}

			EditFile = fileName;
			string textMessage;
			using (StreamReader sr = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS")))
			{
				textMessage = sr.ReadToEnd();
			}
			_msg = MsgFactory.Instance.CreateMsgByTextMessage(textMessage);
			if (_msg == null)
			{
				MessageBox.Show(string.Format(
					"未登録の電文種別か固定長の電文長が違っています。電文長={0},電文種別={1},オーダNo={2},処理タイプ={3}",
					MsgUtils.GetDataLength(textMessage), MsgUtils.GetTelegraphKind(textMessage),
					MsgUtils.GetOrderNo(textMessage), MsgUtils.GetProcessingType(textMessage))
					, "エラー", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return;
			}
			try
			{
				_msg.TextMessage = textMessage;
			}
			catch (NodeOparationException e)
			{
				MessageBox.Show(string.Format("{0}\nStartPos={1}\nName={2}\nPath={3}\nSize={4}", e.Message,
					e.Node.GetStartPos(), e.Node.NameJ, e.Node.Path, e.Node.Size), "エラー", MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				MessageBox.Show("メッセージを編集可能な状態に調整します", "エラー", MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}

			SetDataFromMsg(_msg);
		}

		#endregion


	}
}
