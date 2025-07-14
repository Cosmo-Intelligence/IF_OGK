namespace AddToHisInfoFromSQDB
{
	partial class AddToHisInfoFromSQDBServiceProjectInstaller
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
			this.AddToHisInfoFromSQDBServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.AddToHisInfoFromSQDBServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// AddToHisInfoFromSQDBServiceProcessInstaller
			// 
			this.AddToHisInfoFromSQDBServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.AddToHisInfoFromSQDBServiceProcessInstaller.Password = null;
			this.AddToHisInfoFromSQDBServiceProcessInstaller.Username = null;
			// 
			// AddToHisInfoFromSQDBServiceInstaller
			// 
			this.AddToHisInfoFromSQDBServiceInstaller.Description = "画像通知用HIS送信キュー作成サービス";
			this.AddToHisInfoFromSQDBServiceInstaller.DisplayName = "AddToHisInfoFromSQDBService";
			this.AddToHisInfoFromSQDBServiceInstaller.ServiceName = "AddToHisInfoFromSQDBService";
			this.AddToHisInfoFromSQDBServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// AddToHisInfoFromSQDBServiceProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.AddToHisInfoFromSQDBServiceProcessInstaller,
            this.AddToHisInfoFromSQDBServiceInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller AddToHisInfoFromSQDBServiceProcessInstaller;
		private System.ServiceProcess.ServiceInstaller AddToHisInfoFromSQDBServiceInstaller;
	}
}