namespace AddToHisInfoFromSQDB
{
    partial class AddToHisInfoFromSQDBForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddToHisInfoFromSQDBForm));
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.logButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.headerPanel = new System.Windows.Forms.Panel();
			this.loopStopButton = new System.Windows.Forms.Button();
			this.executeButton = new System.Windows.Forms.Button();
			this.executeLoopButton = new System.Windows.Forms.Button();
			this.splBodyBottom = new System.Windows.Forms.Splitter();
			this.bottomPanel.SuspendLayout();
			this.headerPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// bottomPanel
			// 
			this.bottomPanel.Controls.Add(this.logButton);
			this.bottomPanel.Controls.Add(this.exitButton);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Location = new System.Drawing.Point(0, 51);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(402, 40);
			this.bottomPanel.TabIndex = 1;
			// 
			// logButton
			// 
			this.logButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.logButton.Location = new System.Drawing.Point(233, 5);
			this.logButton.Name = "logButton";
			this.logButton.Size = new System.Drawing.Size(75, 23);
			this.logButton.TabIndex = 0;
			this.logButton.Text = "Log(&L)";
			this.logButton.UseVisualStyleBackColor = true;
			this.logButton.Click += new System.EventHandler(this.logButton_Click);
			// 
			// exitButton
			// 
			this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.exitButton.Location = new System.Drawing.Point(314, 5);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(75, 23);
			this.exitButton.TabIndex = 1;
			this.exitButton.Text = "Exit(&X)";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// headerPanel
			// 
			this.headerPanel.Controls.Add(this.loopStopButton);
			this.headerPanel.Controls.Add(this.executeButton);
			this.headerPanel.Controls.Add(this.executeLoopButton);
			this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.headerPanel.Location = new System.Drawing.Point(0, 0);
			this.headerPanel.Name = "headerPanel";
			this.headerPanel.Size = new System.Drawing.Size(402, 51);
			this.headerPanel.TabIndex = 0;
			// 
			// loopStopButton
			// 
			this.loopStopButton.Location = new System.Drawing.Point(233, 12);
			this.loopStopButton.Name = "loopStopButton";
			this.loopStopButton.Size = new System.Drawing.Size(75, 23);
			this.loopStopButton.TabIndex = 5;
			this.loopStopButton.Text = "LoopStop";
			this.loopStopButton.UseVisualStyleBackColor = true;
			this.loopStopButton.Click += new System.EventHandler(this.loopStopButton_Click);
			// 
			// executeButton
			// 
			this.executeButton.Location = new System.Drawing.Point(14, 12);
			this.executeButton.Name = "executeButton";
			this.executeButton.Size = new System.Drawing.Size(94, 23);
			this.executeButton.TabIndex = 3;
			this.executeButton.Text = "ExecuteOne(&O)";
			this.executeButton.UseVisualStyleBackColor = true;
			this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
			// 
			// executeLoopButton
			// 
			this.executeLoopButton.Location = new System.Drawing.Point(124, 12);
			this.executeLoopButton.Name = "executeLoopButton";
			this.executeLoopButton.Size = new System.Drawing.Size(103, 23);
			this.executeLoopButton.TabIndex = 4;
			this.executeLoopButton.Text = "ExecuteLoop";
			this.executeLoopButton.UseVisualStyleBackColor = true;
			this.executeLoopButton.Click += new System.EventHandler(this.executeLoopButton_Click);
			// 
			// splBodyBottom
			// 
			this.splBodyBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splBodyBottom.Location = new System.Drawing.Point(0, 48);
			this.splBodyBottom.Name = "splBodyBottom";
			this.splBodyBottom.Size = new System.Drawing.Size(402, 3);
			this.splBodyBottom.TabIndex = 4;
			this.splBodyBottom.TabStop = false;
			// 
			// AddToHisInfoFromSQDBForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.exitButton;
			this.ClientSize = new System.Drawing.Size(402, 91);
			this.Controls.Add(this.splBodyBottom);
			this.Controls.Add(this.headerPanel);
			this.Controls.Add(this.bottomPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AddToHisInfoFromSQDBForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AddToHisInfoFromSQDBForm";
			this.Load += new System.EventHandler(this.ArisSendForm_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ArisSendForm_FormClosed);
			this.bottomPanel.ResumeLayout(false);
			this.headerPanel.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Panel headerPanel;
		private System.Windows.Forms.Splitter splBodyBottom;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.Button loopStopButton;
		private System.Windows.Forms.Button executeLoopButton;
		private System.Windows.Forms.Button logButton;
    }
}