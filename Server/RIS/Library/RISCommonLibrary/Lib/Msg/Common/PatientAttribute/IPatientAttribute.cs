using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RISCommonLibrary.Lib.Msg.Common.PatientAttribute
{
	/// <summary>
	/// PatientAttribute属性
	/// </summary>
	public interface IPatientAttribute
	{
		PatientAttributeAggregate PatientAttribute
		{
			get;
			set;
		}
	}
}
