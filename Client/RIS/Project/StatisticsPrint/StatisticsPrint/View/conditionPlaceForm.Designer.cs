namespace StatisticsPrint.View
{
	partial class conditionPlaceForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(conditionPlaceForm));
			this.placeLabel = new System.Windows.Forms.Label();
			this.placeComboBox = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.conditionErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// printerNameLabel
			// 
			this.printerNameLabel.Location = new System.Drawing.Point(4, 106);
			this.printerNameLabel.TabIndex = 4;
			// 
			// printerNameComboBox
			// 
			this.printerNameComboBox.Location = new System.Drawing.Point(109, 103);
			this.printerNameComboBox.TabIndex = 5;
			// 
			// copiesLabel
			// 
			this.copiesLabel.Location = new System.Drawing.Point(46, 150);
			this.copiesLabel.TabIndex = 6;
			// 
			// copiesTextBox
			// 
			this.copiesTextBox.Location = new System.Drawing.Point(109, 147);
			this.copiesTextBox.TabIndex = 7;
			// 
			// copiesNameLabel
			// 
			this.copiesNameLabel.Location = new System.Drawing.Point(215, 147);
			this.copiesNameLabel.TabIndex = 8;
			// 
			// printButton
			// 
			this.printButton.Location = new System.Drawing.Point(219, 191);
			this.printButton.TabIndex = 9;
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(360, 191);
			this.cancelButton.TabIndex = 10;
			// 
			// placeLabel
			// 
			this.placeLabel.AutoSize = true;
			this.placeLabel.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.placeLabel.Location = new System.Drawing.Point(0, 62);
			this.placeLabel.Name = "placeLabel";
			this.placeLabel.Size = new System.Drawing.Size(114, 22);
			this.placeLabel.TabIndex = 2;
			this.placeLabel.Text = "実施場所：";
			// 
			// placeComboBox
			// 
			this.placeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.placeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.placeComboBox.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.placeComboBox.FormattingEnabled = true;
			this.placeComboBox.Location = new System.Drawing.Point(109, 59);
			this.placeComboBox.Name = "placeComboBox";
			this.placeComboBox.Size = new System.Drawing.Size(371, 29);
			this.placeComboBox.TabIndex = 3;
			this.placeComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.placeComboBox_Validating);
			this.placeComboBox.Validated += new System.EventHandler(this.placeComboBox_Validated);
			// 
			// conditionPlaceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(492, 273);
			this.Controls.Add(this.placeComboBox);
			this.Controls.Add(this.placeLabel);
			this.Name = "conditionPlaceForm";
			this.Controls.SetChildIndex(this.placeLabel, 0);
			this.Controls.SetChildIndex(this.printerNameLabel, 0);
			this.Controls.SetChildIndex(this.printerNameComboBox, 0);
			this.Controls.SetChildIndex(this.copiesLabel, 0);
			this.Controls.SetChildIndex(this.copiesNameLabel, 0);
			this.Controls.SetChildIndex(this.copiesTextBox, 0);
			this.Controls.SetChildIndex(this.nengetsuLabel, 0);
			this.Controls.SetChildIndex(this.nengetsuDateTimePicker, 0);
			this.Controls.SetChildIndex(this.printButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.Controls.SetChildIndex(this.placeComboBox, 0);
			((System.ComponentModel.ISupportInitialize)(this.conditionErrorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label placeLabel;
		private System.Windows.Forms.ComboBox placeComboBox;
	}
}