using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace ARISReceive
{
	[RunInstaller(true)]
	public partial class ProjectInstallerHospital : Installer
	{
		public ProjectInstallerHospital()
		{
			InitializeComponent();
		}
	}
}
