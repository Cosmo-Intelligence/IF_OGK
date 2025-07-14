using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace AddToHisInfoFromSQDB
{
	[RunInstaller(true)]
	public partial class AddToHisInfoFromSQDBServiceProjectInstaller : Installer
	{
		public AddToHisInfoFromSQDBServiceProjectInstaller()
		{
			InitializeComponent();
		}
	}
}
