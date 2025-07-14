namespace ServiceWatcher
{
	partial class serviceWatcherForm
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(serviceWatcherForm));
			this.bodySplitContainer = new System.Windows.Forms.SplitContainer();
			this.serviceDataGridView = new System.Windows.Forms.DataGridView();
			this.displayNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.statusNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.startModeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.startNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.serviceContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.serviceBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.refleshLabel = new System.Windows.Forms.Label();
			this.refleshTextBox = new System.Windows.Forms.TextBox();
			this.refleshButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.refleshTimer = new System.Windows.Forms.Timer(this.components);
			this.bodySplitContainer.Panel1.SuspendLayout();
			this.bodySplitContainer.Panel2.SuspendLayout();
			this.bodySplitContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.serviceDataGridView)).BeginInit();
			this.serviceContextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// bodySplitContainer
			// 
			this.bodySplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bodySplitContainer.Location = new System.Drawing.Point(0, 0);
			this.bodySplitContainer.Name = "bodySplitContainer";
			this.bodySplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// bodySplitContainer.Panel1
			// 
			this.bodySplitContainer.Panel1.Controls.Add(this.serviceDataGridView);
			// 
			// bodySplitContainer.Panel2
			// 
			this.bodySplitContainer.Panel2.Controls.Add(this.refleshLabel);
			this.bodySplitContainer.Panel2.Controls.Add(this.refleshTextBox);
			this.bodySplitContainer.Panel2.Controls.Add(this.refleshButton);
			this.bodySplitContainer.Panel2.Controls.Add(this.exitButton);
			this.bodySplitContainer.Size = new System.Drawing.Size(549, 262);
			this.bodySplitContainer.SplitterDistance = 199;
			this.bodySplitContainer.TabIndex = 0;
			// 
			// serviceDataGridView
			// 
			this.serviceDataGridView.AllowUserToAddRows = false;
			this.serviceDataGridView.AllowUserToDeleteRows = false;
			this.serviceDataGridView.AllowUserToOrderColumns = true;
			this.serviceDataGridView.AutoGenerateColumns = false;
			this.serviceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.serviceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.displayNameColumn,
            this.descriptionColumn,
            this.statusNameColumn,
            this.startModeColumn,
            this.startNameColumn});
			this.serviceDataGridView.ContextMenuStrip = this.serviceContextMenuStrip;
			this.serviceDataGridView.DataSource = this.serviceBindingSource;
			this.serviceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.serviceDataGridView.Location = new System.Drawing.Point(0, 0);
			this.serviceDataGridView.MultiSelect = false;
			this.serviceDataGridView.Name = "serviceDataGridView";
			this.serviceDataGridView.RowHeadersVisible = false;
			this.serviceDataGridView.RowTemplate.Height = 21;
			this.serviceDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.serviceDataGridView.Size = new System.Drawing.Size(549, 199);
			this.serviceDataGridView.TabIndex = 0;
			this.serviceDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.serviceDataGridView_MouseDown);
			// 
			// displayNameColumn
			// 
			this.displayNameColumn.DataPropertyName = "ServiceName";
			this.displayNameColumn.HeaderText = "サービス名";
			this.displayNameColumn.Name = "displayNameColumn";
			this.displayNameColumn.ReadOnly = true;
			// 
			// descriptionColumn
			// 
			this.descriptionColumn.DataPropertyName = "Description";
			this.descriptionColumn.HeaderText = "説明";
			this.descriptionColumn.Name = "descriptionColumn";
			this.descriptionColumn.ReadOnly = true;
			// 
			// statusNameColumn
			// 
			this.statusNameColumn.DataPropertyName = "StatusName";
			this.statusNameColumn.HeaderText = "状態";
			this.statusNameColumn.Name = "statusNameColumn";
			this.statusNameColumn.ReadOnly = true;
			// 
			// startModeColumn
			// 
			this.startModeColumn.DataPropertyName = "StartMode";
			this.startModeColumn.HeaderText = "スタートアップの種類";
			this.startModeColumn.Name = "startModeColumn";
			this.startModeColumn.ReadOnly = true;
			this.startModeColumn.Width = 127;
			// 
			// startNameColumn
			// 
			this.startNameColumn.DataPropertyName = "StartName";
			this.startNameColumn.HeaderText = "ログオン";
			this.startNameColumn.Name = "startNameColumn";
			this.startNameColumn.ReadOnly = true;
			// 
			// serviceContextMenuStrip
			// 
			this.serviceContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
			this.serviceContextMenuStrip.Name = "contextMenuStrip1";
			this.serviceContextMenuStrip.Size = new System.Drawing.Size(120, 48);
			this.serviceContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.serviceContextMenuStrip_Opening);
			// 
			// startToolStripMenuItem
			// 
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.startToolStripMenuItem.Text = "開始(&S)";
			this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
			this.stopToolStripMenuItem.Text = "停止(&O)";
			this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
			// 
			// refleshLabel
			// 
			this.refleshLabel.AutoSize = true;
			this.refleshLabel.Location = new System.Drawing.Point(12, 11);
			this.refleshLabel.Name = "refleshLabel";
			this.refleshLabel.Size = new System.Drawing.Size(99, 12);
			this.refleshLabel.TabIndex = 2;
			this.refleshLabel.Text = "再表示間隔(ミリ秒)";
			// 
			// refleshTextBox
			// 
			this.refleshTextBox.Location = new System.Drawing.Point(117, 8);
			this.refleshTextBox.Name = "refleshTextBox";
			this.refleshTextBox.ReadOnly = true;
			this.refleshTextBox.Size = new System.Drawing.Size(49, 19);
			this.refleshTextBox.TabIndex = 0;
			this.refleshTextBox.Text = "3000";
			// 
			// refleshButton
			// 
			this.refleshButton.Location = new System.Drawing.Point(172, 8);
			this.refleshButton.Name = "refleshButton";
			this.refleshButton.Size = new System.Drawing.Size(75, 23);
			this.refleshButton.TabIndex = 0;
			this.refleshButton.Text = "再表示(&R)";
			this.refleshButton.UseVisualStyleBackColor = true;
			this.refleshButton.Click += new System.EventHandler(this.refleshButton_Click);
			// 
			// exitButton
			// 
			this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.exitButton.Location = new System.Drawing.Point(462, 15);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(75, 23);
			this.exitButton.TabIndex = 1;
			this.exitButton.Text = "閉じる(&X)";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// refleshTimer
			// 
			this.refleshTimer.Interval = 30000;
			this.refleshTimer.Tick += new System.EventHandler(this.refleshTimer_Tick);
			// 
			// serviceWatcherForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(549, 262);
			this.Controls.Add(this.bodySplitContainer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "serviceWatcherForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "サービス監視";
			this.Load += new System.EventHandler(this.serviceWatcherForm_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.serviceWatcherForm_FormClosed);
			this.bodySplitContainer.Panel1.ResumeLayout(false);
			this.bodySplitContainer.Panel2.ResumeLayout(false);
			this.bodySplitContainer.Panel2.PerformLayout();
			this.bodySplitContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.serviceDataGridView)).EndInit();
			this.serviceContextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.serviceBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer bodySplitContainer;
		private System.Windows.Forms.DataGridView serviceDataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn displayNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn statusNameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn startModeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn startNameColumn;
		private System.Windows.Forms.ContextMenuStrip serviceContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
		private System.Windows.Forms.Button refleshButton;
		private System.Windows.Forms.Button exitButton;
		private System.Windows.Forms.Timer refleshTimer;
		private System.Windows.Forms.BindingSource serviceBindingSource;
		private System.Windows.Forms.Label refleshLabel;
		private System.Windows.Forms.TextBox refleshTextBox;
	}
}

