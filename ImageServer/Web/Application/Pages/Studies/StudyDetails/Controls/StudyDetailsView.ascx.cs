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
using System.Web.UI.WebControls;
using ClearCanvas.ImageServer.Model;
using ClearCanvas.ImageServer.Web.Common.Data;
using ClearCanvas.ImageServer.Web.Common.Data.DataSource;

namespace ClearCanvas.ImageServer.Web.Application.Pages.Studies.StudyDetails.Controls
{
    /// <summary>
    /// Study level detailed information panel within the <see cref="StudyDetailsPanel"/>
    /// </summary>
    public partial class StudyDetailsView : System.Web.UI.UserControl
    {
        #region Private members

        private Unit _width;

        private IList<StudySummary> _studies = new List<StudySummary>();

        #endregion Private members

        #region Public Properties

        /// <summary>
        /// Sets or gets the list of studies whose information are displayed
        /// </summary>
        public IList<StudySummary> Studies
        {
            get { return _studies; }
            set { _studies = value; }
        }

        public Unit Width
        {
            get { return _width; }
            set { _width = value;

                StudyDetailView.Width = value;
            }
        }


        #endregion Public Properties

        #region Protected Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            StudyDetailView.DataSource = Studies;
            StudyDetailView.DataBind();
        }

        #endregion Protected Methods
    }
}