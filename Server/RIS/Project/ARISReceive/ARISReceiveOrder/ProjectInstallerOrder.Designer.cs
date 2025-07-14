namespace ARISReceive
{
	partial class ProjectInstallerOrder
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
			this.serviceProcessInstallerOrder = new System.ServiceProcess.ServiceProcessInstaller();
			this.serviceInstallerOrder = new System.ServiceProcess.ServiceInstaller();
			// 
			// serviceProcessInstallerOrder
			// 
			this.serviceProcessInstallerOrder.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessInstallerOrder.Password = null;
			this.serviceProcessInstallerOrder.Username = null;
			// 
			// serviceInstallerOrder
			// 
			this.serviceInstallerOrder.Description = "RISIFオーダー情報受信サービス";
			this.serviceInstallerOrder.DisplayName = "ARISReceiveOrderService";
			this.serviceInstallerOrder.ServiceName = "ARISReceiveOrderService";
			this.serviceInstallerOrder.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstallerOrder
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstallerOrder,
            this.serviceInstallerOrder});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstallerOrder;
		private System.ServiceProcess.ServiceInstaller serviceInstallerOrder;
	}
}