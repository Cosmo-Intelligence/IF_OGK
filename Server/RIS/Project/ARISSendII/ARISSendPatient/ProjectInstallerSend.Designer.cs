namespace ARISSend
{
	partial class ProjectInstallerSend
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

		#region コンポーネント デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.serviceProcessInstallerSend = new System.ServiceProcess.ServiceProcessInstaller();
			this.serviceInstallerSend = new System.ServiceProcess.ServiceInstaller();
			// 
			// serviceProcessInstallerSend
			// 
			this.serviceProcessInstallerSend.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessInstallerSend.Password = null;
			this.serviceProcessInstallerSend.Username = null;
			// 
			// serviceInstallerSend
			// 
			this.serviceInstallerSend.Description = "RISIF送信サービス(患者要求のみ)";
			this.serviceInstallerSend.DisplayName = "ARISSendPatientService";
			this.serviceInstallerSend.ServiceName = "ARISSendPatientService";
			this.serviceInstallerSend.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstallerSend
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstallerSend,
            this.serviceInstallerSend});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstallerSend;
		private System.ServiceProcess.ServiceInstaller serviceInstallerSend;
	}
}