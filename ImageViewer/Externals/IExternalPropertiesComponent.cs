#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Validation;

namespace ClearCanvas.ImageViewer.Externals
{
	public interface IExternalPropertiesComponent : IApplicationComponent
	{
		void Load(IExternal external);
		void Update(IExternal external);
	}

	public abstract class ExternalPropertiesComponent<T> : ApplicationComponent, IExternalPropertiesComponent where T : IExternal
	{
		private string _name;
		private string _label;
		private bool _enabled;
		private WindowStyle _windowStyle;

		[ValidateLength(1, Message = "MessageValueCannotBeEmpty")]
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
				{
					_name = value;
					this.NotifyPropertyChanged("Name");
				}
			}
		}

		[ValidateLength(1, Message = "MessageValueCannotBeEmpty")]
		public string Label
		{
			get { return _label; }
			set
			{
				if (_label != value)
				{
					_label = value;
					this.NotifyPropertyChanged("Label");
				}
			}
		}

		public bool Enabled
		{
			get { return _enabled; }
			set
			{
				if (_enabled != value)
				{
					_enabled = value;
					this.NotifyPropertyChanged("Enabled");
				}
			}
		}

		public WindowStyle WindowStyle
		{
			get { return _windowStyle; }
			set
			{
				if (_windowStyle != value)
				{
					_windowStyle = value;
					this.NotifyPropertyChanged("WindowStyle");
				}
			}
		}

		public virtual void Load(T external)
		{
			Platform.CheckForNullReference(external, "external");

			this.Name = external.Name;
			this.Label = external.Label;
			this.Enabled = external.Enabled;
			this.WindowStyle = external.WindowStyle;
			this.Modified = false;
		}

		public virtual void Update(T external)
		{
			Platform.CheckForNullReference(external, "external");

			external.Name = this.Name;
			external.Label = this.Label;
			external.Enabled = this.Enabled;
			external.WindowStyle = this.WindowStyle;
		}

		void IExternalPropertiesComponent.Load(IExternal external)
		{
			this.Load((T) external);
		}

		void IExternalPropertiesComponent.Update(IExternal external)
		{
			this.Update((T) external);
		}
	}
}