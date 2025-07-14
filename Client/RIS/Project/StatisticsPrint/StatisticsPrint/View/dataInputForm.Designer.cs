namespace StatisticsPrint.View
{
	partial class dataInputForm
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
			this.monsuGroupBox = new System.Windows.Forms.GroupBox();
			this.monsuTextBox = new RISCommonLibrary.Lib.Compornent.NumericTextBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.monsuGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// monsuGroupBox
			// 
			this.monsuGroupBox.Controls.Add(this.monsuTextBox);
			this.monsuGroupBox.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.monsuGroupBox.Location = new System.Drawing.Point(25, 12);
			this.monsuGroupBox.Name = "monsuGroupBox";
			this.monsuGroupBox.Size = new System.Drawing.Size(223, 121);
			this.monsuGroupBox.TabIndex = 0;
			this.monsuGroupBox.TabStop = false;
			this.monsuGroupBox.Text = "放射線治療(門数)";
			// 
			// monsuTextBox
			// 
			this.monsuTextBox.Location = new System.Drawing.Point(26, 69);
			this.monsuTextBox.MaxLength = 6;
			this.monsuTextBox.Name = "monsuTextBox";
			this.monsuTextBox.Size = new System.Drawing.Size(171, 29);
			this.monsuTextBox.TabIndex = 0;
			this.monsuTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.cancelButton.Location = new System.Drawing.Point(138, 139);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(120, 49);
			this.cancelButton.TabIndex = 10;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = false;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.okButton.Location = new System.Drawing.Point(12, 139);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(120, 49);
			this.okButton.TabIndex = 9;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = false;
			// 
			// dataInputForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(273, 194);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.monsuGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dataInputForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "データ入力画面";
			this.monsuGroupBox.ResumeLayout(false);
			this.monsuGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox monsuGroupBox;
		protected internal System.Windows.Forms.Button cancelButton;
		protected internal System.Windows.Forms.Button okButton;
		private RISCommonLibrary.Lib.Compornent.NumericTextBox monsuTextBox;
	}
}