namespace ARISReceive
{
	partial class ProjectInstallerHospital
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
			this.serviceProcessInstallerHospital = new System.ServiceProcess.ServiceProcessInstaller();
			this.serviceInstallerHospital = new System.ServiceProcess.ServiceInstaller();
			// 
			// serviceProcessInstallerHospital
			// 
			this.serviceProcessInstallerHospital.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessInstallerHospital.Password = null;
			this.serviceProcessInstallerHospital.Username = null;
			// 
			// serviceInstallerHospital
			// 
			this.serviceInstallerHospital.Description = "RISIF入退院情報受信サービス";
			this.serviceInstallerHospital.DisplayName = "ARISReceiveHospitalService";
			this.serviceInstallerHospital.ServiceName = "ARISReceiveHospitalService";
			this.serviceInstallerHospital.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstallerHospital
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstallerHospital,
            this.serviceInstallerHospital});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstallerHospital;
		private System.ServiceProcess.ServiceInstaller serviceInstallerHospital;
	}
}