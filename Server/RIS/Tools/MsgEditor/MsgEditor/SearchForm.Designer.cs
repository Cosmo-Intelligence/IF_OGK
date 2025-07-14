namespace MsgEditor
{
	partial class SearchForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
			this.mainPanel = new System.Windows.Forms.Panel();
			this.BodyPanel = new System.Windows.Forms.Panel();
			this.searchTextBox = new System.Windows.Forms.TextBox();
			this.searchLabel = new System.Windows.Forms.Label();
			this.rightPanel = new System.Windows.Forms.Panel();
			this.cancelButton = new System.Windows.Forms.Button();
			this.candidateDownsideButton = new System.Windows.Forms.Button();
			this.candidateUpsideButton = new System.Windows.Forms.Button();
			this.mainPanel.SuspendLayout();
			this.BodyPanel.SuspendLayout();
			this.rightPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.BodyPanel);
			this.mainPanel.Controls.Add(this.rightPanel);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(431, 175);
			this.mainPanel.TabIndex = 0;
			// 
			// BodyPanel
			// 
			this.BodyPanel.Controls.Add(this.searchTextBox);
			this.BodyPanel.Controls.Add(this.searchLabel);
			this.BodyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BodyPanel.Location = new System.Drawing.Point(0, 0);
			this.BodyPanel.Name = "BodyPanel";
			this.BodyPanel.Size = new System.Drawing.Size(287, 175);
			this.BodyPanel.TabIndex = 0;
			// 
			// searchTextBox
			// 
			this.searchTextBox.Location = new System.Drawing.Point(66, 12);
			this.searchTextBox.Name = "searchTextBox";
			this.searchTextBox.Size = new System.Drawing.Size(206, 19);
			this.searchTextBox.TabIndex = 1;
			// 
			// searchLabel
			// 
			this.searchLabel.AutoSize = true;
			this.searchLabel.Location = new System.Drawing.Point(16, 19);
			this.searchLabel.Name = "searchLabel";
			this.searchLabel.Size = new System.Drawing.Size(44, 12);
			this.searchLabel.TabIndex = 0;
			this.searchLabel.Text = "検索(&S)";
			// 
			// rightPanel
			// 
			this.rightPanel.Controls.Add(this.cancelButton);
			this.rightPanel.Controls.Add(this.candidateDownsideButton);
			this.rightPanel.Controls.Add(this.candidateUpsideButton);
			this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.rightPanel.Location = new System.Drawing.Point(287, 0);
			this.rightPanel.Name = "rightPanel";
			this.rightPanel.Size = new System.Drawing.Size(144, 175);
			this.rightPanel.TabIndex = 1;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(9, 70);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(126, 23);
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// candidateDownsideButton
			// 
			this.candidateDownsideButton.Location = new System.Drawing.Point(9, 41);
			this.candidateDownsideButton.Name = "candidateDownsideButton";
			this.candidateDownsideButton.Size = new System.Drawing.Size(126, 23);
			this.candidateDownsideButton.TabIndex = 1;
			this.candidateDownsideButton.Text = "下候補(&N) F3";
			this.candidateDownsideButton.UseVisualStyleBackColor = true;
			this.candidateDownsideButton.Click += new System.EventHandler(this.candidateDownsideButton_Click);
			// 
			// candidateUpsideButton
			// 
			this.candidateUpsideButton.Location = new System.Drawing.Point(9, 10);
			this.candidateUpsideButton.Name = "candidateUpsideButton";
			this.candidateUpsideButton.Size = new System.Drawing.Size(126, 23);
			this.candidateUpsideButton.TabIndex = 0;
			this.candidateUpsideButton.Text = "上候補(&P) Shift+F3";
			this.candidateUpsideButton.UseVisualStyleBackColor = true;
			this.candidateUpsideButton.Click += new System.EventHandler(this.candidateUpsideButton_Click);
			// 
			// searchForm
			// 
			this.AcceptButton = this.candidateDownsideButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(431, 175);
			this.Controls.Add(this.mainPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "searchForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "検索";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchForm_KeyDown);
			this.mainPanel.ResumeLayout(false);
			this.BodyPanel.ResumeLayout(false);
			this.BodyPanel.PerformLayout();
			this.rightPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Panel BodyPanel;
		private System.Windows.Forms.TextBox searchTextBox;
		private System.Windows.Forms.Label searchLabel;
		private System.Windows.Forms.Panel rightPanel;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button candidateDownsideButton;
		private System.Windows.Forms.Button candidateUpsideButton;
	}
}