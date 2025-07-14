namespace RISCommonLibrary.Lib.Debugger
{
	partial class LogForm
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.logRichTextBox = new System.Windows.Forms.RichTextBox();
			this.clearButton = new System.Windows.Forms.Button();
			this.hideButton = new System.Windows.Forms.Button();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.logRichTextBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.clearButton);
			this.splitContainer1.Panel2.Controls.Add(this.hideButton);
			this.splitContainer1.Size = new System.Drawing.Size(456, 311);
			this.splitContainer1.SplitterDistance = 251;
			this.splitContainer1.TabIndex = 0;
			// 
			// logRichTextBox
			// 
			this.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logRichTextBox.Location = new System.Drawing.Point(0, 0);
			this.logRichTextBox.Name = "logRichTextBox";
			this.logRichTextBox.Size = new System.Drawing.Size(456, 251);
			this.logRichTextBox.TabIndex = 0;
			this.logRichTextBox.Text = "";
			this.logRichTextBox.WordWrap = false;
			// 
			// clearButton
			// 
			this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.clearButton.Location = new System.Drawing.Point(287, 21);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(75, 23);
			this.clearButton.TabIndex = 0;
			this.clearButton.Text = "Clear(&C)";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// hideButton
			// 
			this.hideButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.hideButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.hideButton.Location = new System.Drawing.Point(369, 21);
			this.hideButton.Name = "hideButton";
			this.hideButton.Size = new System.Drawing.Size(75, 23);
			this.hideButton.TabIndex = 1;
			this.hideButton.Text = "Hide(&H)";
			this.hideButton.UseVisualStyleBackColor = true;
			this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
			// 
			// LogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.hideButton;
			this.ClientSize = new System.Drawing.Size(456, 311);
			this.Controls.Add(this.splitContainer1);
			this.Name = "LogForm";
			this.Text = "LogForm";
			this.Load += new System.EventHandler(this.LogForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.RichTextBox logRichTextBox;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.Button hideButton;
	}
}