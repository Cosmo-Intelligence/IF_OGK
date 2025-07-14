namespace ReportInterface
{
	partial class reportInterfaceProjectInstaller
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
			this.reportInterfaceServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.reportInterfaceServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// reportInterfaceServiceProcessInstaller
			// 
			this.reportInterfaceServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.reportInterfaceServiceProcessInstaller.Password = null;
			this.reportInterfaceServiceProcessInstaller.Username = null;
			// 
			// reportInterfaceServiceInstaller
			// 
			this.reportInterfaceServiceInstaller.Description = "RISと所見機能の連携サービス";
			this.reportInterfaceServiceInstaller.DisplayName = "ReportInterfaceService";
			this.reportInterfaceServiceInstaller.ServiceName = "ReportInterfaceService";
			this.reportInterfaceServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// reportInterfaceProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.reportInterfaceServiceProcessInstaller,
            this.reportInterfaceServiceInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller reportInterfaceServiceProcessInstaller;
		private System.ServiceProcess.ServiceInstaller reportInterfaceServiceInstaller;
	}
}