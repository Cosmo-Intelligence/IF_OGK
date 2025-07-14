using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace ReportInterface
{
	[RunInstaller(true)]
	public partial class reportInterfaceProjectInstaller : Installer
	{
		public reportInterfaceProjectInstaller()
		{
			InitializeComponent();
		}
	}
}
