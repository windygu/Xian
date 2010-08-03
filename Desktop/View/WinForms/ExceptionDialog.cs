﻿using System;
using System.Drawing;
using ClearCanvas.Common;
using System.Windows.Forms;

namespace ClearCanvas.Desktop.View.WinForms
{
	[ExtensionOf(typeof(ExceptionDialogExtensionPoint))]
	public class ExceptionDialog : Desktop.ExceptionDialog
	{
		private DialogBoxForm _form;

		protected override ExceptionDialogAction Show()
		{
			var control = new ExceptionDialogControl(Exception, Message, Actions, CloseForm, CloseForm);
			_form = new DialogBoxForm(Title, control, Size.Empty, DialogSizeHint.Auto);

			var screen = ScreenFromActiveForm() ?? ScreenFromMousePosition();
			int xdiff = screen.Bounds.Width - _form.Bounds.Width;
			int ydiff = screen.Bounds.Height - _form.Bounds.Height;
			int locationX = screen.WorkingArea.Left + Math.Max(0, (xdiff)/2);
			int locationY = screen.WorkingArea.Top + Math.Max(0, (ydiff)/2);

			_form.StartPosition = FormStartPosition.Manual;
			_form.Location = new Point(locationX, locationY);
			//_form.TopMost = true;
			_form.ShowDialog();

			return control.Result;
		}

		private void CloseForm()
		{
			_form.Close();
		}

		private static System.Windows.Forms.Screen ScreenFromActiveForm()
		{
			try
			{
				var activeForm = Form.ActiveForm;
				if (activeForm != null)
					return System.Windows.Forms.Screen.FromControl(activeForm);
			}
			catch
			{
			}

			return null;
		}

		private static System.Windows.Forms.Screen ScreenFromMousePosition()
		{
			return System.Windows.Forms.Screen.FromPoint(Control.MousePosition);
		}
	}
}