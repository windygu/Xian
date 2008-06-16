using System;
using System.Collections.Generic;
using System.Text;
using ClearCanvas.Enterprise.Common;
using System.Runtime.Serialization;

namespace ClearCanvas.Ris.Application.Common.Admin.DiagnosticServiceAdmin
{
	[DataContract]
	public class ListDiagnosticServicesResponse : DataContractBase
	{
		public ListDiagnosticServicesResponse(List<DiagnosticServiceSummary> DiagnosticServices)
		{
			this.DiagnosticServices = DiagnosticServices;
		}

		[DataMember]
		public List<DiagnosticServiceSummary> DiagnosticServices;
	}
}
