namespace ARISReceive
{
	partial class ARISReceiveFormHospital
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ARISReceiveFormHospital));
			this.topPannel = new System.Windows.Forms.Panel();
			this.listenerCloseTimeoutLabel = new System.Windows.Forms.Label();
			this.listenerCloseTimeoutTextBox = new System.Windows.Forms.TextBox();
			this.clinetCloseTimeoutLabel = new System.Windows.Forms.Label();
			this.clientCloseTimeoutTextBox = new System.Windows.Forms.TextBox();
			this.receiveTimeoutLabel = new System.Windows.Forms.Label();
			this.sendTimeoutLabel = new System.Windows.Forms.Label();
			this.receiveTimeoutTextBox = new System.Windows.Forms.TextBox();
			this.stopButton = new System.Windows.Forms.Button();
			this.sendTimeoutTextBox = new System.Windows.Forms.TextBox();
			this.startButton = new System.Windows.Forms.Button();
			this.portTextBox = new System.Windows.Forms.TextBox();
			this.portLabel = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.exitButton = new System.Windows.Forms.Button();
			this.bottomPannel = new System.Windows.Forms.Panel();
			this.logButton = new System.Windows.Forms.Button();
			this.topPannel.SuspendLayout();
			this.bottomPannel.SuspendLayout();
			this.SuspendLayout();
			// 
			// topPannel
			// 
			this.topPannel.Controls.Add(this.listenerCloseTimeoutLabel);
			this.topPannel.Controls.Add(this.listenerCloseTimeoutTextBox);
			this.topPannel.Controls.Add(this.clinetCloseTimeoutLabel);
			this.topPannel.Controls.Add(this.clientCloseTimeoutTextBox);
			this.topPannel.Controls.Add(this.receiveTimeoutLabel);
			this.topPannel.Controls.Add(this.sendTimeoutLabel);
			this.topPannel.Controls.Add(this.receiveTimeoutTextBox);
			this.topPannel.Controls.Add(this.stopButton);
			this.topPannel.Controls.Add(this.sendTimeoutTextBox);
			this.topPannel.Controls.Add(this.startButton);
			this.topPannel.Controls.Add(this.portTextBox);
			this.topPannel.Controls.Add(this.portLabel);
			this.topPannel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.topPannel.Location = new System.Drawing.Point(0, 0);
			this.topPannel.Name = "topPannel";
			this.topPannel.Size = new System.Drawing.Size(540, 137);
			this.topPannel.TabIndex = 0;
			// 
			// listenerCloseTimeoutLabel
			// 
			this.listenerCloseTimeoutLabel.AutoSize = true;
			this.listenerCloseTimeoutLabel.Location = new System.Drawing.Point(204, 6);
			this.listenerCloseTimeoutLabel.Name = "listenerCloseTimeoutLabel";
			this.listenerCloseTimeoutLabel.Size = new System.Drawing.Size(169, 12);
			this.listenerCloseTimeoutLabel.TabIndex = 6;
			this.listenerCloseTimeoutLabel.Text = "リスナスレッド終了待ち時間(msec)";
			// 
			// listenerCloseTimeoutTextBox
			// 
			this.listenerCloseTimeoutTextBox.Location = new System.Drawing.Point(375, 2);
			this.listenerCloseTimeoutTextBox.Name = "listenerCloseTimeoutTextBox";
			this.listenerCloseTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
			this.listenerCloseTimeoutTextBox.TabIndex = 7;
			this.listenerCloseTimeoutTextBox.Text = "60000";
			// 
			// clinetCloseTimeoutLabel
			// 
			this.clinetCloseTimeoutLabel.AutoSize = true;
			this.clinetCloseTimeoutLabel.Location = new System.Drawing.Point(179, 27);
			this.clinetCloseTimeoutLabel.Name = "clinetCloseTimeoutLabel";
			this.clinetCloseTimeoutLabel.Size = new System.Drawing.Size(194, 12);
			this.clinetCloseTimeoutLabel.TabIndex = 8;
			this.clinetCloseTimeoutLabel.Text = "クライアントスレッド終了待ち時間(msec)";
			// 
			// clientCloseTimeoutTextBox
			// 
			this.clientCloseTimeoutTextBox.Location = new System.Drawing.Point(375, 23);
			this.clientCloseTimeoutTextBox.Name = "clientCloseTimeoutTextBox";
			this.clientCloseTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
			this.clientCloseTimeoutTextBox.TabIndex = 9;
			this.clientCloseTimeoutTextBox.Text = "30000";
			// 
			// receiveTimeoutLabel
			// 
			this.receiveTimeoutLabel.AutoSize = true;
			this.receiveTimeoutLabel.Location = new System.Drawing.Point(3, 26);
			this.receiveTimeoutLabel.Name = "receiveTimeoutLabel";
			this.receiveTimeoutLabel.Size = new System.Drawing.Size(109, 12);
			this.receiveTimeoutLabel.TabIndex = 2;
			this.receiveTimeoutLabel.Text = "受信待ち時間(msec)";
			// 
			// sendTimeoutLabel
			// 
			this.sendTimeoutLabel.AutoSize = true;
			this.sendTimeoutLabel.Location = new System.Drawing.Point(6, 48);
			this.sendTimeoutLabel.Name = "sendTimeoutLabel";
			this.sendTimeoutLabel.Size = new System.Drawing.Size(109, 12);
			this.sendTimeoutLabel.TabIndex = 4;
			this.sendTimeoutLabel.Text = "送信待ち時間(msec)";
			// 
			// receiveTimeoutTextBox
			// 
			this.receiveTimeoutTextBox.Location = new System.Drawing.Point(114, 23);
			this.receiveTimeoutTextBox.Name = "receiveTimeoutTextBox";
			this.receiveTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
			this.receiveTimeoutTextBox.TabIndex = 3;
			this.receiveTimeoutTextBox.Text = "0";
			// 
			// stopButton
			// 
			this.stopButton.Enabled = false;
			this.stopButton.Location = new System.Drawing.Point(436, 41);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(75, 23);
			this.stopButton.TabIndex = 11;
			this.stopButton.Text = "Stop";
			this.stopButton.UseVisualStyleBackColor = true;
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// sendTimeoutTextBox
			// 
			this.sendTimeoutTextBox.Location = new System.Drawing.Point(114, 45);
			this.sendTimeoutTextBox.Name = "sendTimeoutTextBox";
			this.sendTimeoutTextBox.Size = new System.Drawing.Size(45, 19);
			this.sendTimeoutTextBox.TabIndex = 5;
			this.sendTimeoutTextBox.Text = "0";
			// 
			// startButton
			// 
			this.startButton.Location = new System.Drawing.Point(436, 12);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(75, 23);
			this.startButton.TabIndex = 10;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// portTextBox
			// 
			this.portTextBox.Location = new System.Drawing.Point(114, 3);
			this.portTextBox.Name = "portTextBox";
			this.portTextBox.Size = new System.Drawing.Size(68, 19);
			this.portTextBox.TabIndex = 1;
			this.portTextBox.Text = "9999";
			// 
			// portLabel
			// 
			this.portLabel.AutoSize = true;
			this.portLabel.Location = new System.Drawing.Point(83, 5);
			this.portLabel.Name = "portLabel";
			this.portLabel.Size = new System.Drawing.Size(25, 12);
			this.portLabel.TabIndex = 0;
			this.portLabel.Text = "port";
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 83);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(540, 6);
			this.splitter1.TabIndex = 4;
			this.splitter1.TabStop = false;
			// 
			// exitButton
			// 
			this.exitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.exitButton.Location = new System.Drawing.Point(453, 6);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(75, 23);
			this.exitButton.TabIndex = 1;
			this.exitButton.Text = "Exit(&X)";
			this.exitButton.UseVisualStyleBackColor = true;
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// bottomPannel
			// 
			this.bottomPannel.Controls.Add(this.logButton);
			this.bottomPannel.Controls.Add(this.exitButton);
			this.bottomPannel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPannel.Location = new System.Drawing.Point(0, 89);
			this.bottomPannel.Name = "bottomPannel";
			this.bottomPannel.Size = new System.Drawing.Size(540, 48);
			this.bottomPannel.TabIndex = 1;
			// 
			// logButton
			// 
			this.logButton.Location = new System.Drawing.Point(360, 6);
			this.logButton.Name = "logButton";
			this.logButton.Size = new System.Drawing.Size(75, 23);
			this.logButton.TabIndex = 0;
			this.logButton.Text = "Log(&L)";
			this.logButton.UseVisualStyleBackColor = true;
			this.logButton.Click += new System.EventHandler(this.logButton_Click);
			// 
			// ARISReceiveFormHospital
			// 
			this.AcceptButton = this.startButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.exitButton;
			this.ClientSize = new System.Drawing.Size(540, 137);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.bottomPannel);
			this.Controls.Add(this.topPannel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ARISReceiveFormHospital";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "入退院受信画面";
			this.Load += new System.EventHandler(this.ArisReceiveForm_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ArisReceiveForm_FormClosed);
			this.topPannel.ResumeLayout(false);
			this.topPannel.PerformLayout();
			this.bottomPannel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel topPannel;
		private System.Windows.Forms.Label listenerCloseTimeoutLabel;
		private System.Windows.Forms.TextBox listenerCloseTimeoutTextBox;
		private System.Windows.Forms.Label clinetCloseTimeoutLabel;
		private System.Windows.Forms.TextBox clientCloseTimeoutTextBox;
		private System.Windows.Forms.Label receiveTimeoutLabel;
		private System.Windows.Forms.Label sendTimeoutLabel;
		private System.Windows.Forms.TextBox receiveTimeoutTextBox;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.TextBox sendTimeoutTextBox;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.TextBox portTextBox;
		private System.Windows.Forms.Label portLabel;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Button exitButton;
		private System.Windows.Forms.Panel bottomPannel;
		private System.Windows.Forms.Button logButton;
	}
}