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

namespace ClearCanvas.ImageViewer.Utilities.StudyFilters.CoreTools
{
	[ButtonAction("toggle", DefaultToolbarActionSite + "/ToolbarFiltersAreOn", "Toggle")]
	[IconSet("toggle", "Icons.ToggleFiltersToolSmall.png", "Icons.ToggleFiltersToolMedium.png", "Icons.ToggleFiltersToolLarge.png")]
	[EnabledStateObserver("toggle", "Enabled", "EnabledChanged")]
	[CheckedStateObserver("toggle", "Checked", "CheckedChanged")]
	[LabelValueObserver("toggle", "Label", "CheckedChanged")]
	[TooltipValueObserver("toggle", "Tooltip", "CheckedChanged")]
	[ExtensionOf(typeof (StudyFilterToolExtensionPoint))]
	public class ToggleFiltersTool : StudyFilterTool
	{
		public event EventHandler EnabledChanged;
		public event EventHandler CheckedChanged;

		private bool _enabled;

		public bool Enabled
		{
			get { return _enabled; }
			private set
			{
				if (_enabled != value)
				{
					_enabled = value;
					EventsHelper.Fire(this.EnabledChanged, this, EventArgs.Empty);

					if (_enabled) // if enabled changes from off to on, also toggle filters on
					{
						base.StudyFilter.FilterPredicatesEnabled = true;
					}
				}
			}
		}

		public bool Checked
		{
			get { return base.StudyFilter.FilterPredicatesEnabled; }
		}

		public string Label
		{
			get { return base.StudyFilter.FilterPredicatesEnabled ? SR.ToolbarFiltersAreOn : SR.ToolbarFiltersAreOff; }
		}

		public string Tooltip
		{
			get { return base.StudyFilter.FilterPredicatesEnabled ? SR.TooltipFiltersAreOn : SR.TooltipFiltersAreOff; }
		}

		public void Toggle()
		{
			base.StudyFilter.FilterPredicatesEnabled = !base.StudyFilter.FilterPredicatesEnabled;
			base.RefreshTable();
		}

		private bool AtLeastOneFilter
		{
			get
			{
				foreach (StudyFilterColumn column in (IEnumerable<StudyFilterColumn>) base.Columns)
				{
					if (column.IsColumnFiltered)
						return true;
				}
				return false;
			}
		}

		private void StudyFilter_FilterPredicatesEnabledChanged(object sender, EventArgs e)
		{
			EventsHelper.Fire(this.CheckedChanged, this, EventArgs.Empty);
		}

		private void StudyFilter_FilterPredicatesChanged(object sender, EventArgs e)
		{
			this.Enabled = this.AtLeastOneFilter;
		}

		public override void Initialize()
		{
			base.Initialize();
			base.StudyFilter.FilterPredicatesChanged += StudyFilter_FilterPredicatesChanged;
			base.StudyFilter.FilterPredicatesEnabledChanged += StudyFilter_FilterPredicatesEnabledChanged;
		}

		protected override void Dispose(bool disposing)
		{
			base.StudyFilter.FilterPredicatesEnabledChanged -= StudyFilter_FilterPredicatesEnabledChanged;
			base.StudyFilter.FilterPredicatesChanged -= StudyFilter_FilterPredicatesChanged;
			base.Dispose(disposing);
		}
	}
}