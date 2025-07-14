using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARISSend.Model.MessageCreator
{
	interface ISocketParameter
	{
		string Host
		{
			get;
		}

		int Port
		{
			get;
		}

		int ConnectionTimeout
		{
			get;
		}

		int SendTimeout
		{
			get;
		}

		int ReceiveTimeout
		{
			get;
		}

	}
}
