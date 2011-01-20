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
using ClearCanvas.ImageViewer.Utilities.StudyFilters.Utilities;

namespace ClearCanvas.ImageViewer.Utilities.StudyFilters.AutoFilters.Actions
{
	[ExtensionPoint]
	public class CompareFilterMenuActionViewExtensionPoint : ExtensionPoint<IActionView> {}

	[AssociateView(typeof (CompareFilterMenuActionViewExtensionPoint))]
	public class CompareFilterMenuAction : Action
	{
		private readonly IFilterMenuActionOwner _owner;
		private readonly IList<CompareFilterMode> _allowedModes;

		public CompareFilterMenuAction(string actionID, ActionPath actionPath, IFilterMenuActionOwner owner, IList<CompareFilterMode> allowedModes, IResourceResolver resourceResolver)
			: base(actionID, actionPath, resourceResolver)
		{
			Platform.CheckTrue(allowedModes.Count > 0, "allowedModes should be non-empty");
			_allowedModes = allowedModes;
			_owner = owner;
		}

		private Predicate Filter
		{
			get
			{
				Predicate filter = Predicate.Find(_owner.ParentFilterPredicate.Predicates, base.ActionID);
				if (filter == null)
				{
					filter = new Predicate(_owner.Column, base.ActionID);
					filter.CurrentMode = _allowedModes[0];
					_owner.ParentFilterPredicate.Predicates.Add(filter);	
				}
				return filter;
			}
		}

		public IList<CompareFilterMode> AllowedModes
		{
			get { return _allowedModes; }
		}

		public CompareFilterMode CurrentMode
		{
			get { return this.Filter.CurrentMode; }
			set { this.Filter.CurrentMode = value; }
		}

		public string Value
		{
			get { return this.Filter.Value; }
			set { this.Filter.Value = value; }
		}

		public static CompareFilterMenuAction CreateAction(Type callingType, string actionID, string actionPath, IFilterMenuActionOwner owner, IList<CompareFilterMode> allowedModes, IResourceResolver resourceResolver)
		{
			CompareFilterMenuAction action = new CompareFilterMenuAction(
				string.Format("{0}:{1}", callingType.FullName, actionID),
				new ActionPath(actionPath, resourceResolver),
				owner, allowedModes, resourceResolver);
			action.Label = action.Path.LastSegment.LocalizedText;
			action.Persistent = true;
			return action;
		}

		public class Predicate : FilterPredicate
		{
			public event EventHandler CurrentModeChanged;
			public event EventHandler ValueChanged;

			private readonly StudyFilterColumn _column;
			private readonly string _id;
			private CompareFilterMode _mode;
			private string _stringValue = null;
			private object _value = null;

			internal Predicate(StudyFilterColumn column, string id)
			{
				_column = column;
				_id = id;
			}

			internal static Predicate Find(IEnumerable<FilterPredicate> haystack, string id)
			{
				foreach (FilterPredicate predicate in haystack)
				{
					if (predicate is Predicate && ((Predicate) predicate)._id == id)
						return (Predicate) predicate;
				}
				return null;
			}

			public CompareFilterMode CurrentMode
			{
				get { return _mode; }
				set
				{
					if (_mode != value)
					{
						_mode = value;
						EventsHelper.Fire(this.CurrentModeChanged, this, EventArgs.Empty);

						base.OnChanged();
					}
				}
			}

			public string Value
			{
				get { return _stringValue; }
				set
				{
					if (_stringValue != value)
					{
						object newValue = null;
						if (!string.IsNullOrEmpty(value))
						{
							if (!_column.Parse(value, out newValue))
								throw new FormatException();
						}

						_stringValue = value;
						_value = newValue;
						EventsHelper.Fire(this.ValueChanged, this, EventArgs.Empty);

						base.OnChanged();
					}
				}
			}

			public bool IsActive
			{
				get { return !string.IsNullOrEmpty(_stringValue); }
			}

			public override bool Evaluate(IStudyItem item)
			{
				if (!IsActive)
					return true;

				int result = Compare(_column.GetValue(item), _value);
				switch (_mode)
				{
					case CompareFilterMode.LessThan:
						return result < 0;
					case CompareFilterMode.LessThenOrEquals:
						return result <= 0;
					case CompareFilterMode.GreaterThan:
						return result > 0;
					case CompareFilterMode.GreaterThanOrEquals:
						return result >= 0;
					case CompareFilterMode.NotEquals:
						return result != 0;
					case CompareFilterMode.Equals:
					default:
						return result == 0;
				}
			}

			private static int Compare(object x, object y)
			{
				if (x is IComparable)
					return ((IComparable) x).CompareTo(y);
				else if (y is IComparable)
					return -((IComparable) y).CompareTo(x);
				return 0;
			}
		}
	}

	public enum CompareFilterMode : int
	{
		Equals = 0x1,
		NotEquals = 0x2,
		LessThan = 0x4,
		LessThenOrEquals = 0x8,
		GreaterThan = 0x10,
		GreaterThanOrEquals = 0x20
	}
}