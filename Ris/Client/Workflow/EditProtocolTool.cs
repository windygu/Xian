#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System.Collections.Generic;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Ris.Application.Common.ReportingWorkflow;

namespace ClearCanvas.Ris.Client.Workflow
{
	[MenuAction("apply", "folderexplorer-items-contextmenu/Open Protocol", "Apply")]
	[ButtonAction("apply", "folderexplorer-items-toolbar/Open Protocol", "Apply")]
	[IconSet("apply", IconScheme.Colour, "Icons.EditReportToolSmall.png", "Icons.EditReportToolMedium.png", "Icons.EditReportToolLarge.png")]
	[EnabledStateObserver("apply", "Enabled", "EnabledChanged")]
	[ActionPermission("apply", Application.Common.AuthorityTokens.Workflow.Protocol.Create)]
	[ExtensionOf(typeof(ProtocolWorkflowItemToolExtensionPoint))]
	public class EditProtocolTool : ProtocolWorkflowItemTool
	{
		public EditProtocolTool()
			: base("EditProtocol")
		{
		}

		public override void Initialize()
		{
			base.Initialize();

			this.Context.RegisterDropHandler(typeof(Folders.Reporting.DraftProtocolFolder), this);
			this.Context.RegisterDoubleClickHandler(
				(IClickAction)CollectionUtils.SelectFirst(this.Actions, a => a is IClickAction && a.ActionID.EndsWith("apply")));
		}

		public override bool Enabled
		{
			get
			{
				if (this.Context.SelectedItems.Count != 1)
					return false;

				var item = CollectionUtils.FirstElement(this.Context.SelectedItems);
				if (item.OrderRef == null)
					return false;

				return item.ProcedureStepRef != null;
			}
		}

		public override bool CanAcceptDrop(ICollection<ReportingWorklistItemSummary> items)
		{
			// this tool is only registered as a drop handler for the Drafts folder
			// and the only operation that would make sense in this context is StartInterpretation
			return this.Context.GetOperationEnablement("StartOrderProtocol");
		}

		protected override bool Execute(ReportingWorklistItemSummary item)
		{
			// check if the document is already open
			if (ActivateIfAlreadyOpen(item))
				return true;

			OpenProtocolEditor(item);

			return true;
		}

		private void OpenProtocolEditor(ReportingWorklistItemSummary item)
		{
			if (ActivateIfAlreadyOpen(item))
				return;

			if (!ProtocollingSettings.Default.AllowMultipleProtocollingWorkspaces)
			{
				var documents = DocumentManager.GetAll<ProtocolDocument>();

				// Show warning message and ask if the existing document should be closed or not
				if (documents.Count > 0)
				{
					var firstDocument = CollectionUtils.FirstElement(documents);
					firstDocument.Open();

					var message = string.Format(SR.MessageProtocollingComponentAlreadyOpened, 
						ProtocolDocument.StripTitle(firstDocument.GetTitle()), 
						ProtocolDocument.StripTitle(ProtocolDocument.GetTitle(item)));
					if (DialogBoxAction.No == this.Context.DesktopWindow.ShowMessageBox(message, MessageBoxActions.YesNo))
						return;		// Leave the existing document open

					// close documents and continue
					CollectionUtils.ForEach(documents, document => document.SaveAndClose());
				}
			}

			// open the protocol editor
			var protocolDocument = new ProtocolDocument(item, GetMode(item), this.Context);
			protocolDocument.Open();

			var selectedFolderType = this.Context.SelectedFolder.GetType();
			protocolDocument.Closed += delegate { DocumentManager.InvalidateFolder(selectedFolderType); };
		}

		private static bool ActivateIfAlreadyOpen(ReportingWorklistItemSummary item)
		{
			var document = DocumentManager.Get<ProtocolDocument>(item.OrderRef);
			if (document != null)
			{
				document.Open();
				return true;
			}
			return false;
		}

		private IContinuousWorkflowComponentMode GetMode(ReportingWorklistItemSummary item)
		{
			if (item == null)
				return ProtocollingComponentModes.Review;

			if (CanCreateProtocol(item))
				return ProtocollingComponentModes.Assign;
			
			return CanEditProtocol(item) 
				? ProtocollingComponentModes.Edit 
				: ProtocollingComponentModes.Review;
		}

		private bool CanCreateProtocol(ReportingWorklistItemSummary item)
		{
			return this.Context.GetOperationEnablement("StartProtocol");
		}

		private bool CanEditProtocol(ReportingWorklistItemSummary item)
		{
			// there is no specific workflow operation for editing a previously created draft,
			// so we enable the tool if it looks like a draft and SaveReport is enabled
			return this.Context.GetOperationEnablement("SaveProtocol") && item.ActivityStatus.Code == StepState.InProgress;
		}
	}
}