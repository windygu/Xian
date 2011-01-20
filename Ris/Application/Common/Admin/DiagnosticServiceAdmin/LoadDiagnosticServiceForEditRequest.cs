#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using ClearCanvas.Enterprise.Common;
using System.Runtime.Serialization;

namespace ClearCanvas.Ris.Application.Common.Admin.DiagnosticServiceAdmin
{
	[DataContract]
	public class LoadDiagnosticServiceForEditRequest : DataContractBase
	{
		public LoadDiagnosticServiceForEditRequest(EntityRef entityRef)
		{
			this.DiagnosticServiceRef = entityRef;
		}

		[DataMember]
		public EntityRef DiagnosticServiceRef;
	}
}
