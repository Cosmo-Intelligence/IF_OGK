namespace StatisticsPrint.View
{
	partial class menuForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(menuForm));
			this.mainPanel = new System.Windows.Forms.Panel();
			this.menuPanel = new System.Windows.Forms.Panel();
			this.cdFilmButton = new System.Windows.Forms.Button();
			this.emergencyButton = new System.Windows.Forms.Button();
			this.usButton = new System.Windows.Forms.Button();
			this.drButton = new System.Windows.Forms.Button();
			this.riButton = new System.Windows.Forms.Button();
			this.ctButton = new System.Windows.Forms.Button();
			this.ippansatsueiButton = new System.Windows.Forms.Button();
			this.geppoButton = new System.Windows.Forms.Button();
			this.zoueizaiButton = new System.Windows.Forms.Button();
			this.filmButton = new System.Windows.Forms.Button();
			this.buiButton = new System.Windows.Forms.Button();
			this.buibunruiButton = new System.Windows.Forms.Button();
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.exitButton = new System.Windows.Forms.Button();
			this.titlePanel = new System.Windows.Forms.Panel();
			this.titleLabel = new System.Windows.Forms.Label();
			this.mainPanel.SuspendLayout();
			this.menuPanel.SuspendLayout();
			this.bottomPanel.SuspendLayout();
			this.titlePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainPanel
			// 
			this.mainPanel.Controls.Add(this.menuPanel);
			this.mainPanel.Controls.Add(this.bottomPanel);
			this.mainPanel.Controls.Add(this.titlePanel);
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(724, 694);
			this.mainPanel.TabIndex = 0;
			// 
			// menuPanel
			// 
			this.menuPanel.BackColor = System.Drawing.Color.DarkSeaGreen;
			this.menuPanel.Controls.Add(this.cdFilmButton);
			this.menuPanel.Controls.Add(this.emergencyButton);
			this.menuPanel.Controls.Add(this.usButton);
			this.menuPanel.Controls.Add(this.drButton);
			this.menuPanel.Controls.Add(this.riButton);
			this.menuPanel.Controls.Add(this.ctButton);
			this.menuPanel.Controls.Add(this.ippansatsueiButton);
			this.menuPanel.Controls.Add(this.geppoButton);
			this.menuPanel.Controls.Add(this.zoueizaiButton);
			this.menuPanel.Controls.Add(this.filmButton);
			this.menuPanel.Controls.Add(this.buiButton);
			this.menuPanel.Controls.Add(this.buibunruiButton);
			this.menuPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.menuPanel.Location = new System.Drawing.Point(0, 92);
			this.menuPanel.Name = "menuPanel";
			this.menuPanel.Size = new System.Drawing.Size(724, 510);
			this.menuPanel.TabIndex = 1;
			// 
			// cdFilmButton
			// 
			this.cdFilmButton.BackColor = System.Drawing.SystemColors.Control;
			this.cdFilmButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cdFilmButton.Location = new System.Drawing.Point(30, 350);
			this.cdFilmButton.Name = "cdFilmButton";
			this.cdFilmButton.Size = new System.Drawing.Size(320, 50);
			this.cdFilmButton.TabIndex = 4;
			this.cdFilmButton.Text = "Film読込・書出/CD読込・書出";
			this.cdFilmButton.UseVisualStyleBackColor = false;
			this.cdFilmButton.Click += new System.EventHandler(this.cdFilmButton_Click);
			// 
			// emergencyButton
			// 
			this.emergencyButton.BackColor = System.Drawing.SystemColors.Control;
			this.emergencyButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.emergencyButton.Location = new System.Drawing.Point(380, 430);
			this.emergencyButton.Name = "emergencyButton";
			this.emergencyButton.Size = new System.Drawing.Size(320, 50);
			this.emergencyButton.TabIndex = 11;
			this.emergencyButton.Text = "救命救急センター　集計表";
			this.emergencyButton.UseVisualStyleBackColor = false;
			this.emergencyButton.Click += new System.EventHandler(this.emergencyButton_Click);
			// 
			// usButton
			// 
			this.usButton.BackColor = System.Drawing.SystemColors.Control;
			this.usButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.usButton.Location = new System.Drawing.Point(380, 350);
			this.usButton.Name = "usButton";
			this.usButton.Size = new System.Drawing.Size(320, 50);
			this.usButton.TabIndex = 10;
			this.usButton.Text = "ＵＳ　分類別集計表";
			this.usButton.UseVisualStyleBackColor = false;
			this.usButton.Click += new System.EventHandler(this.usButton_Click);
			// 
			// drButton
			// 
			this.drButton.BackColor = System.Drawing.SystemColors.Control;
			this.drButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.drButton.Location = new System.Drawing.Point(380, 270);
			this.drButton.Name = "drButton";
			this.drButton.Size = new System.Drawing.Size(320, 50);
			this.drButton.TabIndex = 9;
			this.drButton.Text = "透視造影　分類別集計表";
			this.drButton.UseVisualStyleBackColor = false;
			this.drButton.Click += new System.EventHandler(this.drButton_Click);
			// 
			// riButton
			// 
			this.riButton.BackColor = System.Drawing.SystemColors.Control;
			this.riButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.riButton.Location = new System.Drawing.Point(380, 190);
			this.riButton.Name = "riButton";
			this.riButton.Size = new System.Drawing.Size(320, 50);
			this.riButton.TabIndex = 8;
			this.riButton.Text = "ＲＩ　分類別集計表";
			this.riButton.UseVisualStyleBackColor = false;
			this.riButton.Click += new System.EventHandler(this.riButton_Click);
			// 
			// ctButton
			// 
			this.ctButton.BackColor = System.Drawing.SystemColors.Control;
			this.ctButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ctButton.Location = new System.Drawing.Point(380, 110);
			this.ctButton.Name = "ctButton";
			this.ctButton.Size = new System.Drawing.Size(320, 50);
			this.ctButton.TabIndex = 7;
			this.ctButton.Text = "ＣＴ　分類別集計表";
			this.ctButton.UseVisualStyleBackColor = false;
			this.ctButton.Click += new System.EventHandler(this.ctButton_Click);
			// 
			// ippansatsueiButton
			// 
			this.ippansatsueiButton.BackColor = System.Drawing.SystemColors.Control;
			this.ippansatsueiButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.ippansatsueiButton.Location = new System.Drawing.Point(380, 30);
			this.ippansatsueiButton.Name = "ippansatsueiButton";
			this.ippansatsueiButton.Size = new System.Drawing.Size(320, 50);
			this.ippansatsueiButton.TabIndex = 6;
			this.ippansatsueiButton.Text = "一般撮影　分類別集計表";
			this.ippansatsueiButton.UseVisualStyleBackColor = false;
			this.ippansatsueiButton.Click += new System.EventHandler(this.ippansatsueiButton_Click);
			// 
			// geppoButton
			// 
			this.geppoButton.BackColor = System.Drawing.SystemColors.Control;
			this.geppoButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.geppoButton.Location = new System.Drawing.Point(30, 430);
			this.geppoButton.Name = "geppoButton";
			this.geppoButton.Size = new System.Drawing.Size(320, 50);
			this.geppoButton.TabIndex = 5;
			this.geppoButton.Text = "医療技術部診療検査科　月報";
			this.geppoButton.UseVisualStyleBackColor = false;
			this.geppoButton.Click += new System.EventHandler(this.geppoButton_Click);
			// 
			// zoueizaiButton
			// 
			this.zoueizaiButton.BackColor = System.Drawing.SystemColors.Control;
			this.zoueizaiButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.zoueizaiButton.Location = new System.Drawing.Point(30, 270);
			this.zoueizaiButton.Name = "zoueizaiButton";
			this.zoueizaiButton.Size = new System.Drawing.Size(320, 50);
			this.zoueizaiButton.TabIndex = 3;
			this.zoueizaiButton.Text = "造影・薬剤一覧";
			this.zoueizaiButton.UseVisualStyleBackColor = false;
			this.zoueizaiButton.Click += new System.EventHandler(this.zoueizaiButton_Click);
			// 
			// filmButton
			// 
			this.filmButton.BackColor = System.Drawing.SystemColors.Control;
			this.filmButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.filmButton.Location = new System.Drawing.Point(30, 190);
			this.filmButton.Name = "filmButton";
			this.filmButton.Size = new System.Drawing.Size(320, 50);
			this.filmButton.TabIndex = 2;
			this.filmButton.Text = "フィルム一覧";
			this.filmButton.UseVisualStyleBackColor = false;
			this.filmButton.Click += new System.EventHandler(this.filmButton_Click);
			// 
			// buiButton
			// 
			this.buiButton.BackColor = System.Drawing.SystemColors.Control;
			this.buiButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buiButton.Location = new System.Drawing.Point(30, 110);
			this.buiButton.Name = "buiButton";
			this.buiButton.Size = new System.Drawing.Size(320, 50);
			this.buiButton.TabIndex = 1;
			this.buiButton.Text = "撮影検査数一覧（主行為）";
			this.buiButton.UseVisualStyleBackColor = false;
			this.buiButton.Click += new System.EventHandler(this.buiButton_Click);
			// 
			// buibunruiButton
			// 
			this.buibunruiButton.BackColor = System.Drawing.SystemColors.Control;
			this.buibunruiButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.buibunruiButton.Location = new System.Drawing.Point(30, 30);
			this.buibunruiButton.Name = "buibunruiButton";
			this.buibunruiButton.Size = new System.Drawing.Size(320, 50);
			this.buibunruiButton.TabIndex = 0;
			this.buibunruiButton.Text = "撮影検査数一覧（部位分類）";
			this.buibunruiButton.UseVisualStyleBackColor = false;
			this.buibunruiButton.Click += new System.EventHandler(this.buibunruiButton_Click);
			// 
			// bottomPanel
			// 
			this.bottomPanel.BackColor = System.Drawing.Color.Green;
			this.bottomPanel.Controls.Add(this.exitButton);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Location = new System.Drawing.Point(0, 602);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Size = new System.Drawing.Size(724, 92);
			this.bottomPanel.TabIndex = 2;
			// 
			// exitButton
			// 
			this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.exitButton.BackColor = System.Drawing.Color.PaleGreen;
			this.exitButton.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.exitButton.Location = new System.Drawing.Point(607, 21);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(93, 50);
			this.exitButton.TabIndex = 1;
			this.exitButton.Text = "終　了";
			this.exitButton.UseVisualStyleBackColor = false;
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// titlePanel
			// 
			this.titlePanel.BackColor = System.Drawing.Color.Green;
			this.titlePanel.Controls.Add(this.titleLabel);
			this.titlePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.titlePanel.Location = new System.Drawing.Point(0, 0);
			this.titlePanel.Name = "titlePanel";
			this.titlePanel.Size = new System.Drawing.Size(724, 92);
			this.titlePanel.TabIndex = 0;
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.titleLabel.ForeColor = System.Drawing.Color.White;
			this.titleLabel.Location = new System.Drawing.Point(12, 30);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(213, 33);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.Text = "統計帳票出力";
			// 
			// menuForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(724, 694);
			this.Controls.Add(this.mainPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "menuForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "統計帳票出力機能";
			this.Shown += new System.EventHandler(this.menuForm_Shown);
			this.mainPanel.ResumeLayout(false);
			this.menuPanel.ResumeLayout(false);
			this.bottomPanel.ResumeLayout(false);
			this.titlePanel.ResumeLayout(false);
			this.titlePanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Button exitButton;
		private System.Windows.Forms.Panel titlePanel;
		private System.Windows.Forms.Label titleLabel;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.Panel menuPanel;
		private System.Windows.Forms.Button buibunruiButton;
		private System.Windows.Forms.Button emergencyButton;
		private System.Windows.Forms.Button usButton;
		private System.Windows.Forms.Button drButton;
		private System.Windows.Forms.Button riButton;
		private System.Windows.Forms.Button ctButton;
		private System.Windows.Forms.Button ippansatsueiButton;
		private System.Windows.Forms.Button geppoButton;
		private System.Windows.Forms.Button zoueizaiButton;
		private System.Windows.Forms.Button filmButton;
		private System.Windows.Forms.Button buiButton;
		private System.Windows.Forms.Button cdFilmButton;
	}
}

