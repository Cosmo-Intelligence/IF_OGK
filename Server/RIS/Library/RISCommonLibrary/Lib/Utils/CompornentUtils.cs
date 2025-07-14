using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace RISCommonLibrary.Lib.Utils
{
	public static class CompornentUtils
	{
		/// <summary>
		/// NumericUpDownのMinimum、Maximum内の値を取得する
		/// </summary>
		/// <param name="nud"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static decimal GetContainsValue(this NumericUpDown nud, decimal value)
		{
			if (nud.Minimum > value)
			{
				return nud.Minimum;
			}
			if (nud.Maximum < value)
			{
				return nud.Maximum;
			}

			return value;
		}

		/// <summary>
		/// カラーダイアログでカラーを取得する
		/// </summary>
		/// <returns></returns>
		public static Color GetColorByDialog()
		{
			return GetColorByDialog(new Color());
		}

		/// <summary>
		/// カラーダイアログでカラーを取得する
		/// </summary>
		/// <returns></returns>
		public static Color GetColorByDialog(Color selected)
		{

			using (ColorDialog cd = new ColorDialog())
			{
				//はじめに選択されている色を設定
				cd.Color = selected;
				//ダイアログを表示する
				if (cd.ShowDialog() != DialogResult.OK)
				{
					return selected;
				}
				return cd.Color;
			};
		}

		/// <summary>
		/// Color型をへARGB形式の文字列変換する
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public static string ColorToArgbString(Color c)
		{
			return c.ToArgb().ToString();
		}

		/// <summary>
		/// ARGB形式の文字列をColor型へ変換する
		/// </summary>
		/// <param name="argbString"></param>
		/// <returns></returns>
		public static Color ArgbStringToColor(string argbString)
		{
			int intArgb;
			if (!int.TryParse(argbString, out intArgb))
			{
				return Color.FromArgb(-1);
			}

			return Color.FromArgb(intArgb);
		}

		/// <summary>
		/// DateTimePickerに値を設定する
		/// </summary>
		/// <param name="setDate"></param>
		/// <param name="dtp"></param>
		public static void SetDateTimeToDateTimePicker(Nullable<DateTime> setDate, DateTimePicker dtp)
		{
			if (!setDate.HasValue)
			{
				dtp.Checked = false;
				return;
			}
			dtp.Value = setDate.Value;
		}


		/// <summary>
		/// DateTimePickerから値を設定する
		/// </summary>
		/// <param name="setDate"></param>
		/// <param name="dtp"></param>
		public static Nullable<DateTime> GetDateTimeToDateTimePicker(DateTimePicker dtp)
		{
			if (!dtp.Checked)
			{
				return null;
			}
			return dtp.Value;
		}

	}
}
