using System.Windows.Forms;

namespace RISCommonLibrary.Lib.Compornent
{

	/// <summary>
	/// マウスやキーボードによる数字以外のペーストを無効にしたTextBox
	/// </summary>
	public partial class NumericTextBox : TextBox
	{
		const int WM_PASTE = 0x302;
		const int ES_NUMBER = 0x2000;

		[System.Security.Permissions.SecurityPermission(
			System.Security.Permissions.SecurityAction.LinkDemand,
			Flags=System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_PASTE)
			{
				IDataObject iData = Clipboard.GetDataObject();
				//文字列がクリップボードにあるか
				if (iData != null && iData.GetDataPresent(DataFormats.Text))
				{
					string clipStr = (string)iData.GetData(DataFormats.Text);
					//クリップボードの文字列が数字か調べる
					if (!System.Text.RegularExpressions.Regex.IsMatch(
						clipStr,
						@"^[0-9]+$"))
						return;
				}
			}

			base.WndProc(ref m);
		}

		protected override CreateParams CreateParams
		{
			[System.Security.Permissions.SecurityPermission(
				System.Security.Permissions.SecurityAction.LinkDemand,
				Flags=System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams parms = base.CreateParams;
				parms.Style |= ES_NUMBER;
				return parms;
			}
		}
		
	}

}
