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
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;

namespace ClearCanvas.ImageViewer.Externals.CoreTools
{
	[ExtensionOf(typeof (ImageViewerToolExtensionPoint))]
	public class PresentationImageExternalTool : ExternalToolBase
	{
		private readonly IResourceResolver _resourceResolver = new ResourceResolver(typeof (PresentationImageExternalTool).Assembly);

		private IActionSet _actions = null;
		private IPresentationImage _selectedPresentationImage = null;

		public override IActionSet Actions
		{
			get
			{
				if (_actions == null)
				{
					List<IAction> actions = new List<IAction>();
					foreach (IExternal external in ExternalCollection.SavedExternals)
					{
						if (!external.IsValid || string.IsNullOrEmpty(external.Label))
							continue;

						IPresentationImageExternal consumer = external as IPresentationImageExternal;
						if (consumer != null && consumer.CanLaunch(base.SelectedPresentationImage))
						{
							string id = Guid.NewGuid().ToString();
							ActionPath actionPath = new ActionPath(string.Format("imageviewer-contextmenu/MenuExternals/{0}", id), _resourceResolver);
							MenuAction action = new MenuAction(id, actionPath, ClickActionFlags.None, _resourceResolver);
							action.Label = string.Format(SR.FormatOpenImageWith, consumer.Label);
							action.SetPermissibility(AuthorityTokens.Externals);
							action.SetClickHandler(delegate
							                       	{
							                       		try
							                       		{
							                       			consumer.Launch(base.SelectedPresentationImage);
							                       		}
							                       		catch (Exception ex)
							                       		{
							                       			ExceptionHandler.Report(ex, base.Context.DesktopWindow);
							                       		}
							                       	});
							actions.Add(action);
						}
					}
					_actions = new ActionSet(actions);
				}
				return _actions;
			}
		}

		protected override void OnPresentationImageSelected(object sender, PresentationImageSelectedEventArgs e)
		{
			base.OnPresentationImageSelected(sender, e);
			if (_selectedPresentationImage != this.SelectedPresentationImage)
			{
				_selectedPresentationImage = this.SelectedPresentationImage;
				_actions = null;
			}
		}

		protected override void OnExternalsChanged(EventArgs e)
		{
			base.OnExternalsChanged(e);
			_actions = null;
		}
	}
}