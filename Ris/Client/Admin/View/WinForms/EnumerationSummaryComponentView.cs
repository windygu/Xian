using System;
using System.Collections.Generic;
using System.Text;

using ClearCanvas.Common;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.View.WinForms;

namespace ClearCanvas.Ris.Client.Admin.View.WinForms
{
    /// <summary>
    /// Provides a Windows Forms view onto <see cref="EnumerationSummaryComponent"/>
    /// </summary>
    [ExtensionOf(typeof(EnumerationSummaryComponentViewExtensionPoint))]
    public class EnumerationSummaryComponentView : WinFormsView, IApplicationComponentView
    {
        private EnumerationSummaryComponent _component;
        private EnumerationSummaryComponentControl _control;


        #region IApplicationComponentView Members

        public void SetComponent(IApplicationComponent component)
        {
            _component = (EnumerationSummaryComponent)component;
        }

        #endregion

        public override object GuiElement
        {
            get
            {
                if (_control == null)
                {
                    _control = new EnumerationSummaryComponentControl(_component);
                }
                return _control;
            }
        }
    }
}
