using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StatisticsPrint.View
{
	public partial class dataInputForm : Form
	{

		/// <summary>
		/// 放射線治療(門数)
		/// </summary>
		public string Monsu
		{
			get
			{
				return monsuTextBox.Text;
			}
			set
			{
				monsuTextBox.Text = value;
			}
		}

		public dataInputForm()
		{
			InitializeComponent();
		}
	}
}
