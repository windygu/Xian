#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System.ServiceModel;

namespace ClearCanvas.Ris.Application.Common.AttachedDocumentService
{
    [RisApplicationService]
    [ServiceContract]
    public interface IAttachedDocumentService
    {
        [OperationContract]
        GetAttachedDocumentFormDataResponse GetAttachedDocumentFormData(GetAttachedDocumentFormDataRequest request);
    }
}
