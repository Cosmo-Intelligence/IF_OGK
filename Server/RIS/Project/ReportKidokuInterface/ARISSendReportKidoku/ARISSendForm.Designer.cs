namespace ARISSend
{
    partial class ARISSendForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ARISSendForm));
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.logButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.headerPanel = new System.Windows.Forms.Panel();
			this.reportDokueiGroupBox = new System.Windows.Forms.GroupBox();
			this.reportDokueiRequestCheckBox = new System.Windows.Forms.CheckBox();
			this.reportDokueiConnectTcpCheckBox = new System.Windows.Forms.CheckBox();
			this.reportDokueiHostTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.receiveTimeoutLabel = new System.Windows.Forms.Label();
			this.sendTimeoutLabel = new System.Windows.Forms.Label();
			this.reportDokueiReceiveTimeoutTextBox = new System.Windows.Forms.TextBox();
			this.reportDokueiSendTimeoutTextBox = new System.Windows.Forms.TextBox();
			this.reportDokueiPortTextBox = new System.Windows.Forms.TextBox();
			this.portLabel = new System.Windows.Forms.Label();
			this.loopStopButton = new System.Windows.Forms.Button();
			this.executeButton = new System.Windows.Forms.Button();
			this.executeLoopButton = new System.Windows.Forms.Button();
			this.splBodyBottom = new System.Windows.Forms.Splitter();
			this.bottomPanel.SuspendLayout();
			this.headerPanel.SuspendLayout();
			this.reportDokueiGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// bottomPanel
			// 
			this.bottomPanel.Controls.Add(this.logButton);
			this.bottomPanel.Controls.Add(this.exitButton);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Location = new System.Drawing.Point(0, 332);
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
			this.headerPanel.Controls.Add(this.reportDokueiGroupBox);
			this.headerPanel.Controls.Add(this.loopStopButton);
			this.headerPanel.Controls.Add(this.executeButton);
			this.headerPanel.Controls.Add(this.executeLoopButton);
			this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.headerPanel.Location = new System.Drawing.Point(0, 0);
			this.headerPanel.Name = "headerPanel";
			this.headerPanel.Size = new System.Drawing.Size(402, 332);
			this.headerPanel.TabIndex = 0;
			// 
			// reportDokueiGroupBox
			// 
			this.reportDokueiGroupBox.Controls.Add(this.reportDokueiRequestCheckBox);
			this.reportDokueiGroupBox.Controls.Add(this.reportDokueiConnectTcpCheckBox);
			this.reportDokueiGroupBox.Controls.Add(this.reportDokueiHostTextBox);
			this.reportDokueiGroupBox.Controls.Add(this.label1);
			this.reportDokueiGroupBox.Controls.Add(this.receiveTimeoutLabel);
			this.reportDokueiGroupBox.Controls.Add(this.sendTimeoutLabel);
			this.reportDokueiGroupBox.Controls.Add(this.reportDokueiReceiveTimeoutTextBox);
			this.reportDokueiGroupBox.Controls.Add(this.reportDokueiSendTimeoutTextBox);
			this.reportDokueiGroupBox.Controls.Add(this.reportDokueiPortTextBox);
			this.reportDokueiGroupBox.Controls.Add(this.portLabel);
			this.reportDokueiGroupBox.Location = new System.Drawing.Point(12, 3);
			this.reportDokueiGroupBox.Name = "reportDokueiGroupBox";
			this.reportDokueiGroupBox.Size = new System.Drawing.Size(375, 90);
			this.reportDokueiGroupBox.TabIndex = 0;
			this.reportDokueiGroupBox.TabStop = false;
			this.reportDokueiGroupBox.Text = "既読";
			// 
			// reportDokueiRequestCheckBox
			// 
			this.reportDokueiRequestCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.reportDokueiRequestCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.reportDokueiRequestCheckBox.Location = new System.Drawing.Point(131, 52);
			this.reportDokueiRequestCheckBox.Name = "reportDokueiRequestCheckBox";
			this.reportDokueiRequestCheckBox.Size = new System.Drawing.Size(126, 24);
			this.reportDokueiRequestCheckBox.TabIndex = 9;
			this.reportDokueiRequestCheckBox.Text = "接続要求を送信";
			this.reportDokueiRequestCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.reportDokueiRequestCheckBox.UseVisualStyleBackColor = true;
			this.reportDokueiRequestCheckBox.CheckedChanged += new System.EventHandler(this.reportDokueiRequestCheckBox_CheckedChanged);
			// 
			// reportDokueiConnectTcpCheckBox
			// 
			this.reportDokueiConnectTcpCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.reportDokueiConnectTcpCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.reportDokueiConnectTcpCheckBox.Location = new System.Drawing.Point(14, 52);
			this.reportDokueiConnectTcpCheckBox.Name = "reportDokueiConnectTcpCheckBox";
			this.reportDokueiConnectTcpCheckBox.Size = new System.Drawing.Size(111, 24);
			this.reportDokueiConnectTcpCheckBox.TabIndex = 8;
			this.reportDokueiConnectTcpCheckBox.Text = "ソケット接続を開く";
			this.reportDokueiConnectTcpCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.reportDokueiConnectTcpCheckBox.UseVisualStyleBackColor = true;
			this.reportDokueiConnectTcpCheckBox.CheckedChanged += new System.EventHandler(this.reportDokueiConnectTcpCheckBox_CheckedChanged);
			// 
			// reportDokueiHostTextBox
			// 
			this.reportDokueiHostTextBox.Location = new System.Drawing.Point(14, 27);
			this.reportDokueiHostTextBox.Name = "reportDokueiHostTextBox";
			this.reportDokueiHostTextBox.ReadOnly = true;
			this.reportDokueiHostTextBox.Size = new System.Drawing.Size(68, 19);
			this.reportDokueiHostTextBox.TabIndex = 1;
			this.reportDokueiHostTextBox.Text = "127.0.0.1";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "host";
			// 
			// receiveTimeoutLabel
			// 
			this.receiveTimeoutLabel.AutoSize = true;
			this.receiveTimeoutLabel.Location = new System.Drawing.Point(162, 12);
			this.receiveTimeoutLabel.Name = "receiveTimeoutLabel";
			this.receiveTimeoutLabel.Size = new System.Drawing.Size(41, 12);
			this.receiveTimeoutLabel.TabIndex = 4;
			this.receiveTimeoutLabel.Text = "受信待";
			// 
			// sendTimeoutLabel
			// 
			this.sendTimeoutLabel.AutoSize = true;
			this.sendTimeoutLabel.Location = new System.Drawing.Point(213, 12);
			this.sendTimeoutLabel.Name = "sendTimeoutLabel";
			this.sendTimeoutLabel.Size = new System.Drawing.Size(41, 12);
			this.sendTimeoutLabel.TabIndex = 6;
			this.sendTimeoutLabel.Text = "送信待";
			// 
			// reportDokueiReceiveTimeoutTextBox
			// 
			this.reportDokueiReceiveTimeoutTextBox.Location = new System.Drawing.Point(164, 27);
			this.reportDokueiReceiveTimeoutTextBox.Name = "reportDokueiReceiveTimeoutTextBox";
			this.reportDokueiReceiveTimeoutTextBox.ReadOnly = true;
			this.reportDokueiReceiveTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
			this.reportDokueiReceiveTimeoutTextBox.TabIndex = 5;
			this.reportDokueiReceiveTimeoutTextBox.Text = "0";
			// 
			// reportDokueiSendTimeoutTextBox
			// 
			this.reportDokueiSendTimeoutTextBox.Location = new System.Drawing.Point(215, 27);
			this.reportDokueiSendTimeoutTextBox.Name = "reportDokueiSendTimeoutTextBox";
			this.reportDokueiSendTimeoutTextBox.ReadOnly = true;
			this.reportDokueiSendTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
			this.reportDokueiSendTimeoutTextBox.TabIndex = 7;
			this.reportDokueiSendTimeoutTextBox.Text = "0";
			// 
			// reportDokueiPortTextBox
			// 
			this.reportDokueiPortTextBox.Location = new System.Drawing.Point(90, 27);
			this.reportDokueiPortTextBox.Name = "reportDokueiPortTextBox";
			this.reportDokueiPortTextBox.ReadOnly = true;
			this.reportDokueiPortTextBox.Size = new System.Drawing.Size(68, 19);
			this.reportDokueiPortTextBox.TabIndex = 3;
			this.reportDokueiPortTextBox.Text = "9999";
			// 
			// portLabel
			// 
			this.portLabel.AutoSize = true;
			this.portLabel.Location = new System.Drawing.Point(88, 12);
			this.portLabel.Name = "portLabel";
			this.portLabel.Size = new System.Drawing.Size(25, 12);
			this.portLabel.TabIndex = 2;
			this.portLabel.Text = "port";
			// 
			// loopStopButton
			// 
			this.loopStopButton.Location = new System.Drawing.Point(237, 303);
			this.loopStopButton.Name = "loopStopButton";
			this.loopStopButton.Size = new System.Drawing.Size(75, 23);
			this.loopStopButton.TabIndex = 5;
			this.loopStopButton.Text = "LoopStop";
			this.loopStopButton.UseVisualStyleBackColor = true;
			this.loopStopButton.Click += new System.EventHandler(this.loopStopButton_Click);
			// 
			// executeButton
			// 
			this.executeButton.Location = new System.Drawing.Point(18, 303);
			this.executeButton.Name = "executeButton";
			this.executeButton.Size = new System.Drawing.Size(94, 23);
			this.executeButton.TabIndex = 3;
			this.executeButton.Text = "ExecuteOne(&O)";
			this.executeButton.UseVisualStyleBackColor = true;
			this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
			// 
			// executeLoopButton
			// 
			this.executeLoopButton.Location = new System.Drawing.Point(128, 303);
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
			this.splBodyBottom.Location = new System.Drawing.Point(0, 329);
			this.splBodyBottom.Name = "splBodyBottom";
			this.splBodyBottom.Size = new System.Drawing.Size(402, 3);
			this.splBodyBottom.TabIndex = 4;
			this.splBodyBottom.TabStop = false;
			// 
			// ARISSendForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.exitButton;
			this.ClientSize = new System.Drawing.Size(402, 372);
			this.Controls.Add(this.splBodyBottom);
			this.Controls.Add(this.headerPanel);
			this.Controls.Add(this.bottomPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ARISSendForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ARISSendForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ArisSendForm_FormClosed);
			this.Load += new System.EventHandler(this.ArisSendForm_Load);
			this.bottomPanel.ResumeLayout(false);
			this.headerPanel.ResumeLayout(false);
			this.reportDokueiGroupBox.ResumeLayout(false);
			this.reportDokueiGroupBox.PerformLayout();
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
		private System.Windows.Forms.GroupBox reportDokueiGroupBox;
		private System.Windows.Forms.Label receiveTimeoutLabel;
		private System.Windows.Forms.Label sendTimeoutLabel;
		private System.Windows.Forms.TextBox reportDokueiReceiveTimeoutTextBox;
		private System.Windows.Forms.TextBox reportDokueiSendTimeoutTextBox;
		private System.Windows.Forms.TextBox reportDokueiPortTextBox;
		private System.Windows.Forms.Label portLabel;
		private System.Windows.Forms.TextBox reportDokueiHostTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox reportDokueiConnectTcpCheckBox;
		private System.Windows.Forms.CheckBox reportDokueiRequestCheckBox;
    }
}