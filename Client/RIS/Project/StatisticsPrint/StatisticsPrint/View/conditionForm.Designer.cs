namespace StatisticsPrint.View
{
	partial class conditionForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(conditionForm));
			this.nengetsuLabel = new System.Windows.Forms.Label();
			this.nengetsuDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.printerNameLabel = new System.Windows.Forms.Label();
			this.printerNameComboBox = new System.Windows.Forms.ComboBox();
			this.copiesLabel = new System.Windows.Forms.Label();
			this.copiesTextBox = new System.Windows.Forms.TextBox();
			this.copiesNameLabel = new System.Windows.Forms.Label();
			this.printButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.conditionErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.conditionErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// nengetsuLabel
			// 
			this.nengetsuLabel.AutoSize = true;
			this.nengetsuLabel.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.nengetsuLabel.Location = new System.Drawing.Point(46, 21);
			this.nengetsuLabel.Name = "nengetsuLabel";
			this.nengetsuLabel.Size = new System.Drawing.Size(68, 22);
			this.nengetsuLabel.TabIndex = 0;
			this.nengetsuLabel.Text = "年月：";
			// 
			// nengetsuDateTimePicker
			// 
			this.nengetsuDateTimePicker.CustomFormat = "yyyy/MM";
			this.nengetsuDateTimePicker.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.nengetsuDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.nengetsuDateTimePicker.Location = new System.Drawing.Point(109, 15);
			this.nengetsuDateTimePicker.Name = "nengetsuDateTimePicker";
			this.nengetsuDateTimePicker.ShowUpDown = true;
			this.nengetsuDateTimePicker.Size = new System.Drawing.Size(108, 29);
			this.nengetsuDateTimePicker.TabIndex = 1;
			this.nengetsuDateTimePicker.Value = new System.DateTime(2008, 12, 29, 0, 0, 0, 0);
			this.nengetsuDateTimePicker.Validated += new System.EventHandler(this.nengetsuDateTimePicker_Validated);
			// 
			// printerNameLabel
			// 
			this.printerNameLabel.AutoSize = true;
			this.printerNameLabel.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.printerNameLabel.Location = new System.Drawing.Point(4, 77);
			this.printerNameLabel.Name = "printerNameLabel";
			this.printerNameLabel.Size = new System.Drawing.Size(110, 22);
			this.printerNameLabel.TabIndex = 2;
			this.printerNameLabel.Text = "プリンタ名：";
			// 
			// printerNameComboBox
			// 
			this.printerNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.printerNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.printerNameComboBox.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.printerNameComboBox.FormattingEnabled = true;
			this.printerNameComboBox.Location = new System.Drawing.Point(109, 74);
			this.printerNameComboBox.Name = "printerNameComboBox";
			this.printerNameComboBox.Size = new System.Drawing.Size(371, 29);
			this.printerNameComboBox.TabIndex = 3;
			this.printerNameComboBox.Validating += new System.ComponentModel.CancelEventHandler(this.printerNameComboBox_Validating);
			this.printerNameComboBox.Validated += new System.EventHandler(this.printerNameComboBox_Validated);
			// 
			// copiesLabel
			// 
			this.copiesLabel.AutoSize = true;
			this.copiesLabel.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.copiesLabel.Location = new System.Drawing.Point(46, 136);
			this.copiesLabel.Name = "copiesLabel";
			this.copiesLabel.Size = new System.Drawing.Size(68, 22);
			this.copiesLabel.TabIndex = 4;
			this.copiesLabel.Text = "部数：";
			// 
			// copiesTextBox
			// 
			this.copiesTextBox.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.copiesTextBox.Location = new System.Drawing.Point(109, 133);
			this.copiesTextBox.Name = "copiesTextBox";
			this.copiesTextBox.Size = new System.Drawing.Size(100, 29);
			this.copiesTextBox.TabIndex = 5;
			this.copiesTextBox.Text = "1";
			this.copiesTextBox.Validated += new System.EventHandler(this.copiesTextBox_Validated);
			this.copiesTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.copiesTextBox_Validating);
			// 
			// copiesNameLabel
			// 
			this.copiesNameLabel.AutoSize = true;
			this.copiesNameLabel.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.copiesNameLabel.Location = new System.Drawing.Point(215, 136);
			this.copiesNameLabel.Name = "copiesNameLabel";
			this.copiesNameLabel.Size = new System.Drawing.Size(33, 22);
			this.copiesNameLabel.TabIndex = 6;
			this.copiesNameLabel.Text = "部";
			// 
			// printButton
			// 
			this.printButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.printButton.Location = new System.Drawing.Point(219, 193);
			this.printButton.Name = "printButton";
			this.printButton.Size = new System.Drawing.Size(120, 49);
			this.printButton.TabIndex = 7;
			this.printButton.Text = "印刷";
			this.printButton.UseVisualStyleBackColor = false;
			this.printButton.Click += new System.EventHandler(this.printButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.CausesValidation = false;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cancelButton.Location = new System.Drawing.Point(360, 193);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(120, 49);
			this.cancelButton.TabIndex = 8;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = false;
			// 
			// conditionErrorProvider
			// 
			this.conditionErrorProvider.ContainerControl = this;
			// 
			// conditionForm
			// 
			this.AcceptButton = this.printButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(492, 273);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.printButton);
			this.Controls.Add(this.copiesNameLabel);
			this.Controls.Add(this.copiesTextBox);
			this.Controls.Add(this.copiesLabel);
			this.Controls.Add(this.printerNameComboBox);
			this.Controls.Add(this.printerNameLabel);
			this.Controls.Add(this.nengetsuDateTimePicker);
			this.Controls.Add(this.nengetsuLabel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "conditionForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "統計抽出条件";
			this.Load += new System.EventHandler(this.conditionForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.conditionErrorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected internal System.Windows.Forms.Label nengetsuLabel;
		protected internal System.Windows.Forms.DateTimePicker nengetsuDateTimePicker;
		protected internal System.Windows.Forms.Label printerNameLabel;
		protected internal System.Windows.Forms.ComboBox printerNameComboBox;
		protected internal System.Windows.Forms.Label copiesLabel;
		protected internal System.Windows.Forms.TextBox copiesTextBox;
		protected internal System.Windows.Forms.Label copiesNameLabel;
		protected internal System.Windows.Forms.Button printButton;
		protected internal System.Windows.Forms.Button cancelButton;
		protected internal System.Windows.Forms.ErrorProvider conditionErrorProvider;

	}
}