using System;
using System.Collections.Generic;
using System.Text;
using ClearCanvas.Enterprise.Common;
using System.ServiceModel;

namespace ClearCanvas.Ris.Application.Common.Admin
{
    [ServiceContract]
    public interface ILocationAdminService
    {
        /// <summary>
        /// Return all location options
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        GetAllLocationsResponse GetAllLocations(GetAllLocationsRequest request);

        /// <summary>
        /// Add the specified location
        /// </summary>
        /// <param name="location"></param>
        [OperationContract]
        AddLocationResponse AddLocation(AddLocationRequest request);

        /// <summary>
        /// Update the specified location
        /// </summary>
        /// <param name="location"></param>
        [OperationContract]
        UpdateLocationResponse UpdateLocation(UpdateLocationRequest request);

        [OperationContract]
        GetLocationEditViewResponse GetLocationEditView(GetLocationEditViewRequest request);

        [OperationContract]
        LoadLocationForEditResponse LoadLocationForEdit(LoadLocationForEditRequest request);
    }
}
