using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportInterface.Data
{
	internal class DataNewOrder : BaseData
	{

		public DataNewOrderOrder Order
		{
			get;
			set;
		}

		public DataNewOrderPatient Patient
		{
			get;
			set;
		}

		public DataNewOrder()
		{
			Order = new DataNewOrderOrder();
			Patient = new DataNewOrderPatient();
		}
	}
}
