using System;
using System.Collections.Generic;
using System.Text;

using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.Desktop.Actions;

namespace ClearCanvas.Ris.Client.Adt
{
    [ButtonAction("search", "folderexplorer-folders-toolbar/Search")]
    [ButtonAction("search", "folders-toolbar/Search")]
    [Tooltip("search", "Search for a patient")]
    [IconSet("search", IconScheme.Colour, "Icons.SearchToolSmall.png", "Icons.SearchToolMedium.png", "Icons.SearchToolLarge.png")]
    [ClickHandler("search", "Search")]

    [ExtensionOf(typeof(RegistrationWorkflowFolderToolExtensionPoint))]
    public class PatientSearchTool : ToolBase
    {
        private PatientSearchComponent _searchComponent;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PatientSearchTool()
        {
        }

        /// <summary>
        /// Called by the framework to initialize this tool.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // TODO: add any significant initialization code here rather than in the constructor
        }

        public void Search()
        {
            if (this.ContextBase is IRegistrationWorkflowFolderToolContext)
            {
                IRegistrationWorkflowFolderToolContext context = (IRegistrationWorkflowFolderToolContext)this.ContextBase;

                if (_searchComponent == null)
                {
                    _searchComponent = new PatientSearchComponent();
                    _searchComponent.SearchRequested += SearchRequestedEventHandler;

                    ApplicationComponent.LaunchAsShelf(
                        context.DesktopWindow,
                        _searchComponent,
                        SR.TitleSearch,
                        ShelfDisplayHint.DockFloat,
                        delegate(IApplicationComponent c)
                        {
                            _searchComponent.SearchRequested -= SearchRequestedEventHandler;
                            _searchComponent = null;
                        });
                }
            }

        }

        private void SearchRequestedEventHandler(object sender, PatientSearchRequestedEventArgs e)
        {
            if (this.ContextBase is IRegistrationWorkflowFolderToolContext)
            {
                IRegistrationWorkflowFolderToolContext context = (IRegistrationWorkflowFolderToolContext)this.ContextBase;
                context.SearchCriteria = _searchComponent.SearchCriteria;
            }
        }
    }
}
