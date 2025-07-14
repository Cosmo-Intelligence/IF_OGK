namespace StatisticsPrint.View
{
	partial class conditionGeppoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(conditionGeppoForm));
			this.monsuLabel = new System.Windows.Forms.Label();
			this.monsuTextBox = new System.Windows.Forms.TextBox();
			this.dataInputButton = new System.Windows.Forms.Button();
			this.dataSelectButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.conditionErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// nengetsuLabel
			// 
			this.nengetsuLabel.Location = new System.Drawing.Point(46, 18);
			// 
			// nengetsuDateTimePicker
			// 
			this.nengetsuDateTimePicker.Location = new System.Drawing.Point(109, 12);
			this.nengetsuDateTimePicker.ValueChanged += new System.EventHandler(this.nengetsuDateTimePicker_ValueChanged);
			// 
			// printerNameLabel
			// 
			this.printerNameLabel.Location = new System.Drawing.Point(4, 70);
			// 
			// printerNameComboBox
			// 
			this.printerNameComboBox.Location = new System.Drawing.Point(109, 67);
			// 
			// copiesLabel
			// 
			this.copiesLabel.Location = new System.Drawing.Point(46, 125);
			// 
			// copiesTextBox
			// 
			this.copiesTextBox.Location = new System.Drawing.Point(109, 122);
			// 
			// copiesNameLabel
			// 
			this.copiesNameLabel.Location = new System.Drawing.Point(215, 125);
			// 
			// printButton
			// 
			this.printButton.Location = new System.Drawing.Point(217, 232);
			this.printButton.TabIndex = 11;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(360, 232);
			this.cancelButton.TabIndex = 12;
			// 
			// monsuLabel
			// 
			this.monsuLabel.AutoSize = true;
			this.monsuLabel.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.monsuLabel.Location = new System.Drawing.Point(34, 180);
			this.monsuLabel.Name = "monsuLabel";
			this.monsuLabel.Size = new System.Drawing.Size(207, 22);
			this.monsuLabel.TabIndex = 7;
			this.monsuLabel.Text = "放射線治療（門数）：";
			// 
			// monsuTextBox
			// 
			this.monsuTextBox.BackColor = System.Drawing.SystemColors.Control;
			this.monsuTextBox.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.monsuTextBox.Location = new System.Drawing.Point(237, 177);
			this.monsuTextBox.Name = "monsuTextBox";
			this.monsuTextBox.ReadOnly = true;
			this.monsuTextBox.Size = new System.Drawing.Size(100, 29);
			this.monsuTextBox.TabIndex = 8;
			this.monsuTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// dataInputButton
			// 
			this.dataInputButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.dataInputButton.Location = new System.Drawing.Point(360, 177);
			this.dataInputButton.Name = "dataInputButton";
			this.dataInputButton.Size = new System.Drawing.Size(120, 49);
			this.dataInputButton.TabIndex = 10;
			this.dataInputButton.Text = "データ入力";
			this.dataInputButton.UseVisualStyleBackColor = false;
			this.dataInputButton.Click += new System.EventHandler(this.dataInputButton_Click);
			// 
			// dataSelectButton
			// 
			this.dataSelectButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.dataSelectButton.Location = new System.Drawing.Point(360, 122);
			this.dataSelectButton.Name = "dataSelectButton";
			this.dataSelectButton.Size = new System.Drawing.Size(120, 49);
			this.dataSelectButton.TabIndex = 9;
			this.dataSelectButton.Text = "データ参照";
			this.dataSelectButton.UseVisualStyleBackColor = false;
			this.dataSelectButton.Click += new System.EventHandler(this.dataSelectButton_Click);
			// 
			// conditionGeppoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 295);
			this.Controls.Add(this.monsuTextBox);
			this.Controls.Add(this.dataSelectButton);
			this.Controls.Add(this.monsuLabel);
			this.Controls.Add(this.dataInputButton);
			this.Name = "conditionGeppoForm";
			this.Controls.SetChildIndex(this.dataInputButton, 0);
			this.Controls.SetChildIndex(this.monsuLabel, 0);
			this.Controls.SetChildIndex(this.printButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.Controls.SetChildIndex(this.nengetsuLabel, 0);
			this.Controls.SetChildIndex(this.printerNameLabel, 0);
			this.Controls.SetChildIndex(this.printerNameComboBox, 0);
			this.Controls.SetChildIndex(this.copiesLabel, 0);
			this.Controls.SetChildIndex(this.copiesTextBox, 0);
			this.Controls.SetChildIndex(this.copiesNameLabel, 0);
			this.Controls.SetChildIndex(this.nengetsuDateTimePicker, 0);
			this.Controls.SetChildIndex(this.dataSelectButton, 0);
			this.Controls.SetChildIndex(this.monsuTextBox, 0);
			((System.ComponentModel.ISupportInitialize)(this.conditionErrorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label monsuLabel;
		private System.Windows.Forms.TextBox monsuTextBox;
		private System.Windows.Forms.Button dataInputButton;
		private System.Windows.Forms.Button dataSelectButton;
	}
}