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
            this.patientGroupBox = new System.Windows.Forms.GroupBox();
            this.patientRequestCheckBox = new System.Windows.Forms.CheckBox();
            this.patientConnectTcpCheckBox = new System.Windows.Forms.CheckBox();
            this.patientHostTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.patientReceiveTimeoutTextBox = new System.Windows.Forms.TextBox();
            this.patientSendTimeoutTextBox = new System.Windows.Forms.TextBox();
            this.patientPortTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.examGroupBox = new System.Windows.Forms.GroupBox();
            this.examRequestCheckBox = new System.Windows.Forms.CheckBox();
            this.examConnectTcpCheckBox = new System.Windows.Forms.CheckBox();
            this.examHostTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.examReceiveTimeoutTextBox = new System.Windows.Forms.TextBox();
            this.examSendTimeoutTextBox = new System.Windows.Forms.TextBox();
            this.examPortTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.receiptGroupBox = new System.Windows.Forms.GroupBox();
            this.receiptRequestCheckBox = new System.Windows.Forms.CheckBox();
            this.receiptConnectTcpCheckBox = new System.Windows.Forms.CheckBox();
            this.receiptHostTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.receiveTimeoutLabel = new System.Windows.Forms.Label();
            this.sendTimeoutLabel = new System.Windows.Forms.Label();
            this.receiptReceiveTimeoutTextBox = new System.Windows.Forms.TextBox();
            this.receiptSendTimeoutTextBox = new System.Windows.Forms.TextBox();
            this.receiptPortTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.loopStopButton = new System.Windows.Forms.Button();
            this.executeButton = new System.Windows.Forms.Button();
            this.executeLoopButton = new System.Windows.Forms.Button();
            this.splBodyBottom = new System.Windows.Forms.Splitter();
            this.bottomPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.patientGroupBox.SuspendLayout();
            this.examGroupBox.SuspendLayout();
            this.receiptGroupBox.SuspendLayout();
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
            this.headerPanel.Controls.Add(this.patientGroupBox);
            this.headerPanel.Controls.Add(this.examGroupBox);
            this.headerPanel.Controls.Add(this.receiptGroupBox);
            this.headerPanel.Controls.Add(this.loopStopButton);
            this.headerPanel.Controls.Add(this.executeButton);
            this.headerPanel.Controls.Add(this.executeLoopButton);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(402, 332);
            this.headerPanel.TabIndex = 0;
            // 
            // patientGroupBox
            // 
            this.patientGroupBox.Controls.Add(this.patientRequestCheckBox);
            this.patientGroupBox.Controls.Add(this.patientConnectTcpCheckBox);
            this.patientGroupBox.Controls.Add(this.patientHostTextBox);
            this.patientGroupBox.Controls.Add(this.label6);
            this.patientGroupBox.Controls.Add(this.label7);
            this.patientGroupBox.Controls.Add(this.label8);
            this.patientGroupBox.Controls.Add(this.patientReceiveTimeoutTextBox);
            this.patientGroupBox.Controls.Add(this.patientSendTimeoutTextBox);
            this.patientGroupBox.Controls.Add(this.patientPortTextBox);
            this.patientGroupBox.Controls.Add(this.label9);
            this.patientGroupBox.Location = new System.Drawing.Point(12, 195);
            this.patientGroupBox.Name = "patientGroupBox";
            this.patientGroupBox.Size = new System.Drawing.Size(375, 102);
            this.patientGroupBox.TabIndex = 2;
            this.patientGroupBox.TabStop = false;
            this.patientGroupBox.Text = "患者";
            // 
            // patientRequestCheckBox
            // 
            this.patientRequestCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.patientRequestCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.patientRequestCheckBox.Location = new System.Drawing.Point(131, 52);
            this.patientRequestCheckBox.Name = "patientRequestCheckBox";
            this.patientRequestCheckBox.Size = new System.Drawing.Size(126, 24);
            this.patientRequestCheckBox.TabIndex = 9;
            this.patientRequestCheckBox.Text = "接続要求を送信";
            this.patientRequestCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.patientRequestCheckBox.UseVisualStyleBackColor = true;
            this.patientRequestCheckBox.CheckedChanged += new System.EventHandler(this.patientRequestCheckBox_CheckedChanged);
            // 
            // patientConnectTcpCheckBox
            // 
            this.patientConnectTcpCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.patientConnectTcpCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.patientConnectTcpCheckBox.Location = new System.Drawing.Point(14, 52);
            this.patientConnectTcpCheckBox.Name = "patientConnectTcpCheckBox";
            this.patientConnectTcpCheckBox.Size = new System.Drawing.Size(111, 24);
            this.patientConnectTcpCheckBox.TabIndex = 8;
            this.patientConnectTcpCheckBox.Text = "ソケット接続を開く";
            this.patientConnectTcpCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.patientConnectTcpCheckBox.UseVisualStyleBackColor = true;
            this.patientConnectTcpCheckBox.CheckedChanged += new System.EventHandler(this.patientConnectTcpCheckBox_CheckedChanged);
            // 
            // patientHostTextBox
            // 
            this.patientHostTextBox.Location = new System.Drawing.Point(8, 30);
            this.patientHostTextBox.Name = "patientHostTextBox";
            this.patientHostTextBox.ReadOnly = true;
            this.patientHostTextBox.Size = new System.Drawing.Size(68, 19);
            this.patientHostTextBox.TabIndex = 1;
            this.patientHostTextBox.Text = "127.0.0.1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "host";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(156, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "受信待";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(207, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "送信待";
            // 
            // patientReceiveTimeoutTextBox
            // 
            this.patientReceiveTimeoutTextBox.Location = new System.Drawing.Point(158, 30);
            this.patientReceiveTimeoutTextBox.Name = "patientReceiveTimeoutTextBox";
            this.patientReceiveTimeoutTextBox.ReadOnly = true;
            this.patientReceiveTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
            this.patientReceiveTimeoutTextBox.TabIndex = 5;
            this.patientReceiveTimeoutTextBox.Text = "0";
            // 
            // patientSendTimeoutTextBox
            // 
            this.patientSendTimeoutTextBox.Location = new System.Drawing.Point(209, 30);
            this.patientSendTimeoutTextBox.Name = "patientSendTimeoutTextBox";
            this.patientSendTimeoutTextBox.ReadOnly = true;
            this.patientSendTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
            this.patientSendTimeoutTextBox.TabIndex = 7;
            this.patientSendTimeoutTextBox.Text = "0";
            // 
            // patientPortTextBox
            // 
            this.patientPortTextBox.Location = new System.Drawing.Point(82, 30);
            this.patientPortTextBox.Name = "patientPortTextBox";
            this.patientPortTextBox.ReadOnly = true;
            this.patientPortTextBox.Size = new System.Drawing.Size(68, 19);
            this.patientPortTextBox.TabIndex = 3;
            this.patientPortTextBox.Text = "9999";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(82, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "port";
            // 
            // examGroupBox
            // 
            this.examGroupBox.Controls.Add(this.examRequestCheckBox);
            this.examGroupBox.Controls.Add(this.examConnectTcpCheckBox);
            this.examGroupBox.Controls.Add(this.examHostTextBox);
            this.examGroupBox.Controls.Add(this.label2);
            this.examGroupBox.Controls.Add(this.label3);
            this.examGroupBox.Controls.Add(this.label4);
            this.examGroupBox.Controls.Add(this.examReceiveTimeoutTextBox);
            this.examGroupBox.Controls.Add(this.examSendTimeoutTextBox);
            this.examGroupBox.Controls.Add(this.examPortTextBox);
            this.examGroupBox.Controls.Add(this.label5);
            this.examGroupBox.Location = new System.Drawing.Point(12, 99);
            this.examGroupBox.Name = "examGroupBox";
            this.examGroupBox.Size = new System.Drawing.Size(375, 90);
            this.examGroupBox.TabIndex = 1;
            this.examGroupBox.TabStop = false;
            this.examGroupBox.Text = "実績";
            // 
            // examRequestCheckBox
            // 
            this.examRequestCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.examRequestCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.examRequestCheckBox.Location = new System.Drawing.Point(131, 55);
            this.examRequestCheckBox.Name = "examRequestCheckBox";
            this.examRequestCheckBox.Size = new System.Drawing.Size(126, 24);
            this.examRequestCheckBox.TabIndex = 9;
            this.examRequestCheckBox.Text = "接続要求を送信";
            this.examRequestCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.examRequestCheckBox.UseVisualStyleBackColor = true;
            this.examRequestCheckBox.CheckedChanged += new System.EventHandler(this.examRequestCheckBox_CheckedChanged);
            // 
            // examConnectTcpCheckBox
            // 
            this.examConnectTcpCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.examConnectTcpCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.examConnectTcpCheckBox.Location = new System.Drawing.Point(14, 55);
            this.examConnectTcpCheckBox.Name = "examConnectTcpCheckBox";
            this.examConnectTcpCheckBox.Size = new System.Drawing.Size(111, 24);
            this.examConnectTcpCheckBox.TabIndex = 8;
            this.examConnectTcpCheckBox.Text = "ソケット接続を開く";
            this.examConnectTcpCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.examConnectTcpCheckBox.UseVisualStyleBackColor = true;
            this.examConnectTcpCheckBox.CheckedChanged += new System.EventHandler(this.examConnectTcpCheckBox_CheckedChanged);
            // 
            // examHostTextBox
            // 
            this.examHostTextBox.Location = new System.Drawing.Point(8, 30);
            this.examHostTextBox.Name = "examHostTextBox";
            this.examHostTextBox.ReadOnly = true;
            this.examHostTextBox.Size = new System.Drawing.Size(68, 19);
            this.examHostTextBox.TabIndex = 1;
            this.examHostTextBox.Text = "127.0.0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "host";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "受信待";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(207, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "送信待";
            // 
            // examReceiveTimeoutTextBox
            // 
            this.examReceiveTimeoutTextBox.Location = new System.Drawing.Point(158, 30);
            this.examReceiveTimeoutTextBox.Name = "examReceiveTimeoutTextBox";
            this.examReceiveTimeoutTextBox.ReadOnly = true;
            this.examReceiveTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
            this.examReceiveTimeoutTextBox.TabIndex = 5;
            this.examReceiveTimeoutTextBox.Text = "0";
            // 
            // examSendTimeoutTextBox
            // 
            this.examSendTimeoutTextBox.Location = new System.Drawing.Point(209, 30);
            this.examSendTimeoutTextBox.Name = "examSendTimeoutTextBox";
            this.examSendTimeoutTextBox.ReadOnly = true;
            this.examSendTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
            this.examSendTimeoutTextBox.TabIndex = 7;
            this.examSendTimeoutTextBox.Text = "0";
            // 
            // examPortTextBox
            // 
            this.examPortTextBox.Location = new System.Drawing.Point(84, 30);
            this.examPortTextBox.Name = "examPortTextBox";
            this.examPortTextBox.ReadOnly = true;
            this.examPortTextBox.Size = new System.Drawing.Size(68, 19);
            this.examPortTextBox.TabIndex = 3;
            this.examPortTextBox.Text = "9999";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(82, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "port";
            // 
            // receiptGroupBox
            // 
            this.receiptGroupBox.Controls.Add(this.receiptRequestCheckBox);
            this.receiptGroupBox.Controls.Add(this.receiptConnectTcpCheckBox);
            this.receiptGroupBox.Controls.Add(this.receiptHostTextBox);
            this.receiptGroupBox.Controls.Add(this.label1);
            this.receiptGroupBox.Controls.Add(this.receiveTimeoutLabel);
            this.receiptGroupBox.Controls.Add(this.sendTimeoutLabel);
            this.receiptGroupBox.Controls.Add(this.receiptReceiveTimeoutTextBox);
            this.receiptGroupBox.Controls.Add(this.receiptSendTimeoutTextBox);
            this.receiptGroupBox.Controls.Add(this.receiptPortTextBox);
            this.receiptGroupBox.Controls.Add(this.portLabel);
            this.receiptGroupBox.Location = new System.Drawing.Point(12, 3);
            this.receiptGroupBox.Name = "receiptGroupBox";
            this.receiptGroupBox.Size = new System.Drawing.Size(375, 90);
            this.receiptGroupBox.TabIndex = 0;
            this.receiptGroupBox.TabStop = false;
            this.receiptGroupBox.Text = "受付";
            // 
            // receiptRequestCheckBox
            // 
            this.receiptRequestCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.receiptRequestCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.receiptRequestCheckBox.Location = new System.Drawing.Point(131, 52);
            this.receiptRequestCheckBox.Name = "receiptRequestCheckBox";
            this.receiptRequestCheckBox.Size = new System.Drawing.Size(126, 24);
            this.receiptRequestCheckBox.TabIndex = 9;
            this.receiptRequestCheckBox.Text = "接続要求を送信";
            this.receiptRequestCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.receiptRequestCheckBox.UseVisualStyleBackColor = true;
            this.receiptRequestCheckBox.CheckedChanged += new System.EventHandler(this.receiptRequestCheckBox_CheckedChanged);
            // 
            // receiptConnectTcpCheckBox
            // 
            this.receiptConnectTcpCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.receiptConnectTcpCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.receiptConnectTcpCheckBox.Location = new System.Drawing.Point(14, 52);
            this.receiptConnectTcpCheckBox.Name = "receiptConnectTcpCheckBox";
            this.receiptConnectTcpCheckBox.Size = new System.Drawing.Size(111, 24);
            this.receiptConnectTcpCheckBox.TabIndex = 8;
            this.receiptConnectTcpCheckBox.Text = "ソケット接続を開く";
            this.receiptConnectTcpCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.receiptConnectTcpCheckBox.UseVisualStyleBackColor = true;
            this.receiptConnectTcpCheckBox.CheckedChanged += new System.EventHandler(this.receiptConnectTcpCheckBox_CheckedChanged);
            // 
            // receiptHostTextBox
            // 
            this.receiptHostTextBox.Location = new System.Drawing.Point(14, 27);
            this.receiptHostTextBox.Name = "receiptHostTextBox";
            this.receiptHostTextBox.ReadOnly = true;
            this.receiptHostTextBox.Size = new System.Drawing.Size(68, 19);
            this.receiptHostTextBox.TabIndex = 1;
            this.receiptHostTextBox.Text = "127.0.0.1";
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
            // receiptReceiveTimeoutTextBox
            // 
            this.receiptReceiveTimeoutTextBox.Location = new System.Drawing.Point(164, 27);
            this.receiptReceiveTimeoutTextBox.Name = "receiptReceiveTimeoutTextBox";
            this.receiptReceiveTimeoutTextBox.ReadOnly = true;
            this.receiptReceiveTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
            this.receiptReceiveTimeoutTextBox.TabIndex = 5;
            this.receiptReceiveTimeoutTextBox.Text = "0";
            // 
            // receiptSendTimeoutTextBox
            // 
            this.receiptSendTimeoutTextBox.Location = new System.Drawing.Point(215, 27);
            this.receiptSendTimeoutTextBox.Name = "receiptSendTimeoutTextBox";
            this.receiptSendTimeoutTextBox.ReadOnly = true;
            this.receiptSendTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
            this.receiptSendTimeoutTextBox.TabIndex = 7;
            this.receiptSendTimeoutTextBox.Text = "0";
            // 
            // receiptPortTextBox
            // 
            this.receiptPortTextBox.Location = new System.Drawing.Point(90, 27);
            this.receiptPortTextBox.Name = "receiptPortTextBox";
            this.receiptPortTextBox.ReadOnly = true;
            this.receiptPortTextBox.Size = new System.Drawing.Size(68, 19);
            this.receiptPortTextBox.TabIndex = 3;
            this.receiptPortTextBox.Text = "9999";
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
            this.Text = "ARISSendForm(Cost)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ArisSendForm_FormClosed);
            this.Load += new System.EventHandler(this.ArisSendForm_Load);
            this.bottomPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.patientGroupBox.ResumeLayout(false);
            this.patientGroupBox.PerformLayout();
            this.examGroupBox.ResumeLayout(false);
            this.examGroupBox.PerformLayout();
            this.receiptGroupBox.ResumeLayout(false);
            this.receiptGroupBox.PerformLayout();
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
		private System.Windows.Forms.GroupBox receiptGroupBox;
		private System.Windows.Forms.GroupBox patientGroupBox;
		private System.Windows.Forms.GroupBox examGroupBox;
		private System.Windows.Forms.Label receiveTimeoutLabel;
		private System.Windows.Forms.Label sendTimeoutLabel;
		private System.Windows.Forms.TextBox receiptReceiveTimeoutTextBox;
		private System.Windows.Forms.TextBox receiptSendTimeoutTextBox;
		private System.Windows.Forms.TextBox receiptPortTextBox;
		private System.Windows.Forms.Label portLabel;
		private System.Windows.Forms.TextBox receiptHostTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox patientHostTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox patientReceiveTimeoutTextBox;
		private System.Windows.Forms.TextBox patientSendTimeoutTextBox;
		private System.Windows.Forms.TextBox patientPortTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox examHostTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox examReceiveTimeoutTextBox;
		private System.Windows.Forms.TextBox examSendTimeoutTextBox;
		private System.Windows.Forms.TextBox examPortTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox receiptConnectTcpCheckBox;
		private System.Windows.Forms.CheckBox receiptRequestCheckBox;
		private System.Windows.Forms.CheckBox examRequestCheckBox;
		private System.Windows.Forms.CheckBox examConnectTcpCheckBox;
		private System.Windows.Forms.CheckBox patientRequestCheckBox;
		private System.Windows.Forms.CheckBox patientConnectTcpCheckBox;
    }
}