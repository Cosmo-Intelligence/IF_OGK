/** MaxByteLengthTextBox コントロール クラス */
using System.Windows.Forms;
using System.ComponentModel;

namespace MsgEditor.CommonUI
{

	/// ---------------------------------------------------------------------------------------
	/// <summary>
	///     入力できる文字バイト数を制限できる TextBox コントロールを表します。
	/// </summary>
	/// <remarks>
	/// 以下のサイトを参考にした
	/// http://jeanne.wankuma.com/library/maxbytelengthtextbox/source.html
	/// </remarks>
	/// ---------------------------------------------------------------------------------------
	public class MaxByteLengthTextBox : System.Windows.Forms.TextBox
	{

		#region　コンストラクタ

		/// ---------------------------------------------------------------------------------------
		/// <summary>
		///     MaxByteLengthTextBox コントロールの新しいインスタンスを初期化します。
		/// </summary>
		/// ---------------------------------------------------------------------------------------

		public MaxByteLengthTextBox()
		{
			this.MaxByteLength = 65535;
		}

		#endregion

		#region　MaxByteLength プロパティ (virtual)

		private int _MaxByteLength;

		/// ---------------------------------------------------------------------------------------
		/// <summary>
		///     ユーザーがテキスト ボックス コントロールに、
		///     入力または貼り付けできる最大文字バイト数を取得または設定します。
		/// </summary>
		/// ---------------------------------------------------------------------------------------

		[Category("動作")]
		[DefaultValue(65535)]
		[Description("エディット コントロールに入力できる最大文字バイト数を指定します。")]
		public virtual int MaxByteLength
		{
			get
			{
				return this._MaxByteLength;
			}

			set
			{
				if (this._MaxByteLength != value)
				{
					this._MaxByteLength = value;
				}
			}
		}

		#endregion

		#region　WndProc メソッド (override)

		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			const int WM_CHAR  = 0x0102;
			const int WM_PASTE = 0x0302;

			switch (m.Msg)
			{
				case WM_CHAR:
					KeyPressEventArgs eKeyPress = new KeyPressEventArgs((char)(m.WParam.ToInt32()));
					this.OnChar(eKeyPress);

					if (eKeyPress.Handled)
					{
						return;
					}

					break;
				case WM_PASTE:
					if (this.MaxLength * 2 > this.MaxByteLength)
					{
						this.OnPaste(new System.EventArgs());
						return;
					}

					break;
			}

			base.WndProc(ref m);
		}

		#endregion

		#region　OnChar メソッド (virtual)

		protected virtual void OnChar(System.Windows.Forms.KeyPressEventArgs e)
		{
			if (char.IsControl(e.KeyChar))
			{
				return;
			}

			System.Text.Encoding sjisEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
			int textByteCount = sjisEncoding.GetByteCount(this.Text);
			int inputByteCount = sjisEncoding.GetByteCount(e.KeyChar.ToString());
			int selectedTextByteCount = sjisEncoding.GetByteCount(this.SelectedText);

			if ((textByteCount + inputByteCount - selectedTextByteCount) > this.MaxByteLength)
			{
				e.Handled = true;
			}
		}

		#endregion

		#region　OnPaste メソッド (virtual)

		protected virtual void OnPaste(System.EventArgs e)
		{
			object clipboardText = Clipboard.GetDataObject().GetData(System.Windows.Forms.DataFormats.Text);

			if (clipboardText == null)
			{
				return;
			}

			System.Text.Encoding sjisEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
			string inputText = clipboardText.ToString();
			int textByteCount = sjisEncoding.GetByteCount(this.Text);
			int inputByteCount = sjisEncoding.GetByteCount(inputText);
			int selectedTextByteCount = sjisEncoding.GetByteCount(this.SelectedText);
			int remainByteCount = this.MaxByteLength - (textByteCount - selectedTextByteCount);

			if (remainByteCount <= 0)
			{
				return;
			}

			if (remainByteCount >= inputByteCount)
			{
				this.SelectedText = inputText;
			}
			else
			{
				this.SelectedText = inputText.Substring(0, remainByteCount);
			}
		}

		#endregion

	}

}
