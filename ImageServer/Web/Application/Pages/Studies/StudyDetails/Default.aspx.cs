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
using System.Security.Permissions;
using ClearCanvas.Common.Utilities;
using ClearCanvas.ImageServer.Enterprise.Authentication;
using ClearCanvas.ImageServer.Model;
using ClearCanvas.ImageServer.Model.EntityBrokers;
using ClearCanvas.ImageServer.Web.Application.Controls;
using ClearCanvas.ImageServer.Web.Application.Pages.Common;
using ClearCanvas.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls;
using ClearCanvas.ImageServer.Web.Common;
using ClearCanvas.ImageServer.Web.Common.Data;
using ClearCanvas.ImageServer.Web.Common.Data.DataSource;
using ClearCanvas.ImageServer.Web.Common.Exceptions;
using ClearCanvas.ImageServer.Web.Common.Utilities;

namespace ClearCanvas.ImageServer.Web.Application.Pages.Studies.StudyDetails
{
    /// <summary>
    /// Study details page.
    /// </summary>
    [PrincipalPermission(SecurityAction.Demand, Role = AuthorityTokens.Study.View)]
    public partial class Default : BasePage
    {
        #region Private members
        private string _studyInstanceUid;
        private string _serverae;
        private ServerPartition _partition;
        private StudySummary _study;
        
        #endregion Private members

        #region Public properties

        public ServerPartition Partition
        {
            get { return _partition; }
            set { _partition = value; }
        }


        #endregion Public properties

        #region Protected Methods

        protected void SetupEventHandlers()
        {
            StudyDetailsPanel.EditStudyClicked += StudyDetailsPanel_EditStudyClicked;
            StudyDetailsPanel.DeleteStudyClicked += StudyDetailsPanel_DeleteStudyClicked;
            StudyDetailsPanel.ReprocessStudyClicked += StudyDetailsPanel_ReprocessStudyClicked;
            EditStudyDialog.StudyEdited += EditStudyDialog_StudyEdited;
            DeleteStudyConfirmDialog.StudyDeleted += DeleteStudyConfirmDialog_StudyDeleted;
            ReprocessConfirmationDialog.Confirmed += ReprocessConfirmationDialog_Confirmed;

            StudyDetailsPanel.StudyDetailsTabsControl.DeleteSeriesClicked += StudyDetailsTabs_DeleteSeriesClicked;
            DeleteSeriesConfirmDialog.SeriesDeleted += DeleteSeriesConfirmDialog_SeriesDeleted;
        }

        void ReprocessConfirmationDialog_Confirmed(object data)
        {
            ReprocessStudy();
        }

        void StudyDetailsPanel_ReprocessStudyClicked(object sender, StudyDetailsPanelReprocessStudyClickEventArgs e)
        {
            ReprocessConfirmationDialog.Message = String.Format("Are you sure you want to reprocess this study?");
            ReprocessConfirmationDialog.MessageType = MessageBox.MessageTypeEnum.YESNO;
            ReprocessConfirmationDialog.Show();
        }

        void StudyDetailsPanel_DeleteStudyClicked(object sender, StudyDetailsPanelDeleteStudyClickEventArgs e)
        {
            DeleteStudy();
        }

        void StudyDetailsTabs_DeleteSeriesClicked(object sender, StudyDetailsTabs.StudyDetailsTabsDeleteSeriesClickEventArgs e)
        {
            DeleteSeries();
        }

        void StudyDetailsPanel_EditStudyClicked(object sender, StudyDetailsPanelEditStudyClickEventArgs e)
        {
            EditStudy();
        }

        void DeleteStudyConfirmDialog_StudyDeleted(object sender, DeleteStudyConfirmDialogStudyDeletedEventArgs e)
        {
            Refresh();
        }

        void DeleteSeriesConfirmDialog_SeriesDeleted(object sender, DeleteSeriesConfirmDialogSeriesDeletedEventArgs e)
        {
            Refresh();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            _studyInstanceUid = Request.QueryString[ImageServerConstants.QueryStrings.StudyInstanceUID];
            _serverae = Request.QueryString[ImageServerConstants.QueryStrings.ServerAE];

            if (!String.IsNullOrEmpty(_studyInstanceUid) && !String.IsNullOrEmpty(_serverae)) 
            {
                
                ServerPartitionDataAdapter adaptor = new ServerPartitionDataAdapter();
                ServerPartitionSelectCriteria criteria = new ServerPartitionSelectCriteria();
                criteria.AeTitle.EqualTo(_serverae);
                IList<ServerPartition> partitions = adaptor.Get(criteria);
                if (partitions != null && partitions.Count>0)
                {
                    if (partitions.Count==1)
                    {
                        Partition = partitions[0];

                        //TODO: Do something if parition is inactive. Perhapse show an alert on the screen?

                        LoadStudy();   
                    }
                    else
                    {
                        Response.Write("Unexpected Error: Multiple Partitions exist with AE title : " + _serverae);
                    }
                }
            }

            //Hide the UserPanel information
            IMasterProperties master = Master as IMasterProperties;
            if(master != null) master.DisplayUserInformationPanel = false;
        }

        protected override void OnInit(EventArgs e)
        {
            SetupEventHandlers();

            base.OnInit(e);
        }

        protected void LoadStudy()
        {
            if (String.IsNullOrEmpty(_studyInstanceUid))
                return;

            if (_partition == null)
                return;

		
			StudyAdaptor studyAdaptor = new StudyAdaptor();
			StudySelectCriteria criteria = new StudySelectCriteria();
			criteria.StudyInstanceUid.EqualTo(_studyInstanceUid);
			criteria.ServerPartitionKey.EqualTo(Partition.GetKey());
			Study study = studyAdaptor.GetFirst(HttpContextData.Current.ReadContext, criteria);

            if (study != null)
            {
                _study = StudySummaryAssembler.CreateStudySummary(HttpContextData.Current.ReadContext, study);
            }
            else
            {
                StudyNotFoundException exception =
                    new StudyNotFoundException(_studyInstanceUid, "The Study is null in Default.aspx -> LoadStudy()");
                ExceptionHandler.ThrowException(exception);
            }
			
        	StudyDetailsPanel.Study = _study;
            StudyDetailsPanel.DataBind();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e); 
            
            if (_partition!=null && _study == null)
            {
                StudyNotFoundException exception = new StudyNotFoundException(_studyInstanceUid, "The Study is null in Default.aspx -> OnPreRender()");
                ExceptionHandler.ThrowException(exception);
            } 
            if (_partition == null)
            {
                PartitionNotFoundException exception = new PartitionNotFoundException(_serverae, "The Server Partition is null in Default.aspx -> OnPreRender()");
                ExceptionHandler.ThrowException(exception);
            }

            if (_study==null)
            {
                StudyNotFoundException exception = new StudyNotFoundException(_studyInstanceUid, "The Study is null in Default.aspx -> OnPreRender()");
                ExceptionHandler.ThrowException(exception);
            }
            else
            {
                SetPageTitle(String.Format("{0}:{1}", NameFormatter.Format(_study.PatientsName) , _study.PatientId));
            }

        }

        #endregion Protected Methods

        #region Public Methods

        
        public void EditStudy()
        {
            if (_study!=null)
            {
                string reason;
                if (!_study.CanScheduleEdit(out reason))
                {
                    MessageDialog.MessageType = MessageBox.MessageTypeEnum.ERROR;
                    MessageDialog.Message = reason;
                    MessageDialog.Show();
                }
                else
                {
                    EditStudyDialog.Study = _study.TheStudy;
                    EditStudyDialog.Show(true);
                }
            }
            
        }

        public void DeleteStudy()
        {
            string reason;
            if (!_study.CanScheduleDelete(out reason))
            {
                MessageDialog.MessageType = MessageBox.MessageTypeEnum.ERROR;
                MessageDialog.Message = reason;
                MessageDialog.Show();
            }
            else
            {
                //DeleteConfirmDialog.MessageType = MessageBox.MessageTypeEnum.YESNO;
                //DeleteConfirmDialog.Message = App_GlobalResources.SR.SingleStudyDelete;
                //DeleteConfirmDialog.Data = _study.TheStudy;

                //DeleteConfirmDialog.Show();
                List<StudySummary> studyList = new List<StudySummary> {_study };

                DeleteStudyConfirmDialog.DeletingStudies = CollectionUtils.Map(
                    studyList,
                    delegate(StudySummary study)
                    {
                        DeleteStudyInfo info = new DeleteStudyInfo
                                                   {
                                                       StudyKey = study.TheStudy.Key,
                                                       AccessionNumber = study.AccessionNumber,
                                                       Modalities = study.ModalitiesInStudy,
                                                       PatientId = study.PatientId,
                                                       PatientsName = study.PatientsName,
                                                       StudyDate = study.StudyDate,
                                                       StudyDescription = study.StudyDescription,
                                                       StudyInstanceUid = study.StudyInstanceUid,
                                                       ServerPartitionAE = study.ThePartition.AeTitle
                                                   };
                        return info;
                    }
                );

                DeleteStudyConfirmDialog.Show();
                updatepanel.Update();
            }
        }

        public void DeleteSeries()
        {
            string reason;
            if (!_study.CanScheduleSeriesDelete(out reason))
            {
                MessageDialog.MessageType = MessageBox.MessageTypeEnum.ERROR;
                MessageDialog.Message = reason;
                MessageDialog.Show();
            }
            else
            {
                IList<Series> selectedSeries = StudyDetailsPanel.StudyDetailsTabsControl.SelectedSeries;

                DeleteSeriesConfirmDialog.DeleteEntireStudy = _study.TheStudy.Series.Count == selectedSeries.Count;

                DeleteSeriesConfirmDialog.DeletingSeries = CollectionUtils.Map(
                    selectedSeries,
                    delegate(Series series)
                    {
                        DeleteSeriesInfo info = new DeleteSeriesInfo
                                                    {
                                                        StudyKey = _study.TheStudy.Key,
                                                        Study = _study.TheStudy,
                                                        Series = series,
                                                        ServerPartitionAE = _study.ThePartition.AeTitle,
                                                        Description = series.SeriesDescription,
                                                        Modality = series.Modality,
                                                        SeriesNumber = series.SeriesNumber,
                                                        NumberOfSeriesRelatedInstances = series.NumberOfSeriesRelatedInstances,
                                                        PerformedProcedureStepStartDate = series.PerformedProcedureStepStartDate,
                                                        PerformedProcedureStepStartTime = series.PerformedProcedureStepStartTime,
                                                        SeriesInstanceUid = series.SeriesInstanceUid
                                                    };

                        return info;
                    }
                );

                DeleteSeriesConfirmDialog.Show();
                updatepanel.Update();
            }
        }

        private void ReprocessStudy()
        {
            StudyController controller = new StudyController();
            controller.ReprocessStudy("Reprocess Study via GUI", _study.TheStudyStorage.GetKey());
            Refresh();
        }

        void EditStudyDialog_StudyEdited()
        {
            Refresh();
        }

        #endregion



        public void Refresh()
        {
            LoadStudy();
        }
    }
}
