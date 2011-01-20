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
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tables;
using ClearCanvas.Ris.Client;
using ClearCanvas.Ris.Application.Common;
using ClearCanvas.Ris.Application.Common.Admin.VisitAdmin;
using ClearCanvas.Ris.Application.Common.Admin.StaffAdmin;
using ClearCanvas.Ris.Application.Common.Admin;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Ris.Application.Common.Admin.ExternalPractitionerAdmin;

namespace ClearCanvas.Ris.Client
{
    /// <summary>
    /// Extension point for views onto <see cref="VisitPractitionersSummaryComponent"/>
    /// </summary>
    [ExtensionPoint]
    public class VisitPractitionerSummaryComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    /// <summary>
    /// VisitPractitionerSummaryComponent class
    /// </summary>
    [AssociateView(typeof(VisitPractitionerSummaryComponentViewExtensionPoint))]
    public class VisitPractitionersSummaryComponent : ApplicationComponent
    {
        private VisitDetail _visit;
        private VisitPractitionerDetail _currentVisitPractitionerSelection;
        private readonly VisitPractitionerTable _practitionersTable;
        private readonly List<EnumValueInfo> _visitPractitionerRoleChoices;
        private readonly CrudActionModel _visitPractitionerActionHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        public VisitPractitionersSummaryComponent(List<EnumValueInfo> visitPractitionerRoleChoices)
        {
            _practitionersTable = new VisitPractitionerTable();
            _visitPractitionerRoleChoices = visitPractitionerRoleChoices;

            _visitPractitionerActionHandler = new CrudActionModel();
            _visitPractitionerActionHandler.Add.SetClickHandler(AddVisitPractitioner);
            _visitPractitionerActionHandler.Edit.SetClickHandler(UpdateSelectedVisitPractitioner);
            _visitPractitionerActionHandler.Delete.SetClickHandler(DeleteSelectedVisitPractitioner);

            _visitPractitionerActionHandler.Add.Enabled = true;
        }

        public override void Start()
        {
            LoadVisitPractioners();

            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public VisitDetail Visit
        {
            get { return _visit; }
            set { _visit = value; }
        }

        public ITable VisitPractitioners
        {
            get { return _practitionersTable; }
        }

        public ActionModelNode VisitPractionersActionModel
        {
            get { return _visitPractitionerActionHandler; }
        }

        public VisitPractitionerDetail CurrentVisitPractitionerSelection
        {
            get { return _currentVisitPractitionerSelection; }
            set
            {
                _currentVisitPractitionerSelection = value;
                VisitPractitionerSelectionChanged();
            }
        }

        public void SetSelectedVisitPractitioner(ISelection selection)
        {
            this.CurrentVisitPractitionerSelection = (VisitPractitionerDetail)selection.Item;
        }

        private void VisitPractitionerSelectionChanged()
        {
            if (_currentVisitPractitionerSelection != null)
            {
                _visitPractitionerActionHandler.Edit.Enabled = true;
                _visitPractitionerActionHandler.Delete.Enabled = true;
            }
            else
            {
                _visitPractitionerActionHandler.Edit.Enabled = false;
                _visitPractitionerActionHandler.Delete.Enabled = false;
            }
        }

        public void AddVisitPractitioner()
        {
            DummyAddVisitPractitioner();

            LoadVisitPractioners();
            this.Modified = true;
        }

        public void UpdateSelectedVisitPractitioner()
        {
            DummyUpdateVisitPractitioner();

            LoadVisitPractioners();
            this.Modified = true;
        }

        public void DeleteSelectedVisitPractitioner()
        {
            _visit.Practitioners.Remove(_currentVisitPractitionerSelection);

            LoadVisitPractioners();
            this.Modified = true;
        }

        public void LoadVisitPractioners()
        {
            _practitionersTable.Items.Clear();
            _practitionersTable.Items.AddRange(_visit.Practitioners);
        }

        #region Dummy Code

        private void DummyAddVisitPractitioner()
        {
            try
            {
                Platform.GetService<IExternalPractitionerAdminService>(
                    delegate(IExternalPractitionerAdminService service)
                    {
                        ListExternalPractitionersResponse findResponse = service.ListExternalPractitioners(new ListExternalPractitionersRequest("Who", ""));
                        ExternalPractitionerSummary practitioner = findResponse.Practitioners[0];

                        VisitPractitionerDetail vp = new VisitPractitionerDetail();

                        vp.Role = CollectionUtils.SelectFirst(_visitPractitionerRoleChoices,
                                delegate(EnumValueInfo e) { return e.Code == "RF"; });
                        vp.Practitioner = practitioner;
                        vp.StartTime = Platform.Time;

                        _visit.Practitioners.Add(vp);
                    });
            }
            catch (Exception e)
            {
                ExceptionHandler.Report(e, SR.ExceptionCannotAddVisitPractitioner, this.Host.DesktopWindow,
                    delegate
                    {
                        this.ExitCode = ApplicationComponentExitCode.Error;
                        this.Host.Exit();
                    });
            }
        }

        private void DummyUpdateVisitPractitioner()
        {
            try
            {
                VisitPractitionerDetail vp = _currentVisitPractitionerSelection;

                vp.Role = CollectionUtils.SelectFirst<EnumValueInfo>(_visitPractitionerRoleChoices,
                        delegate(EnumValueInfo e) { return e.Code == "CN"; });
                vp.EndTime = Platform.Time;

            }
            catch (Exception e)
            {
                ExceptionHandler.Report(e, SR.ExceptionCannotAddVisitPractitioner, this.Host.DesktopWindow,
                    delegate
                    {
                        this.ExitCode = ApplicationComponentExitCode.Error;
                        this.Host.Exit();
                    });
            }
        }
    	#endregion    
    }
}
