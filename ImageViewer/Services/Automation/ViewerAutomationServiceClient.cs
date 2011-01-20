#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ClearCanvas.ImageViewer.Services.Automation
{
	public class ViewerAutomationServiceClient : ClientBase<IViewerAutomation>, IViewerAutomation
	{
		public ViewerAutomationServiceClient()
		{
		}

		public ViewerAutomationServiceClient(string endpointConfigurationName)
			: base(endpointConfigurationName)
		{
		}

		public ViewerAutomationServiceClient(Binding binding, EndpointAddress remoteAddress)
			: base(binding, remoteAddress)
		{
		}

		public ViewerAutomationServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress)
			: base(endpointConfigurationName, remoteAddress)
		{
		}

		#region IViewerAutomation Members

		public GetActiveViewersResult GetActiveViewers()
		{
			return base.Channel.GetActiveViewers();
		}

		public GetViewerInfoResult GetViewerInfo(GetViewerInfoRequest request)
		{
			return base.Channel.GetViewerInfo(request);
		}

		public OpenStudiesResult OpenStudies(OpenStudiesRequest request)
		{
			return base.Channel.OpenStudies(request);
		}

		public void ActivateViewer(ActivateViewerRequest request)
		{
			base.Channel.ActivateViewer(request);
		}

		public void CloseViewer(CloseViewerRequest request)
		{
			base.Channel.CloseViewer(request);
		}

		#endregion
	}
}
