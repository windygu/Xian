using System;
using System.Drawing;
using System.Diagnostics;
using ClearCanvas.Common;
using ClearCanvas.ImageViewer.Imaging;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Common.Utilities;
using ClearCanvas.ImageViewer.InputManagement;
using ClearCanvas.ImageViewer.Tools.Standard;
using ClearCanvas.ImageViewer.BaseTools;
using ClearCanvas.ImageViewer.Graphics;

namespace ClearCanvas.ImageViewer.Tools.Standard
{
	[MouseToolButton(XMouseButtons.Right, true)]

	[MenuAction("activate", "global-menus/MenuTools/MenuStandard/MenuWindowLevel", Flags = ClickActionFlags.CheckAction)]
	[KeyboardAction("activate", "imageviewer-keyboard/ToolsStandardWindowLevel/Activate", KeyStroke = XKeys.W)]
	[MenuAction("activate", "imageviewer-contextmenu/MenuWindowLevel", Flags = ClickActionFlags.CheckAction)]
    [ButtonAction("activate", "global-toolbars/ToolbarStandard/ToolbarWindowLevel", Flags = ClickActionFlags.CheckAction)]
	[CheckedStateObserver("activate", "Active", "ActivationChanged")]
	[ClickHandler("activate", "Select")]
    [Tooltip("activate", "TooltipWindowLevel")]
	[IconSet("activate", IconScheme.Colour, "Icons.WindowLevelToolSmall.png", "Icons.WindowLevelToolMedium.png", "Icons.WindowLevelToolLarge.png")]
	[GroupHint("activate", "Tools.Image.Manipulation.Lut.WindowLevel")]

	[KeyboardAction("incrementwindowwidth", "imageviewer-keyboard/ToolsStandardWindowLevel/IncrementWindowWidth", KeyStroke = XKeys.Right)]
	[ClickHandler("incrementwindowwidth", "IncrementWindowWidth")]

	[KeyboardAction("decrementwindowwidth", "imageviewer-keyboard/ToolsStandardWindowLevel/DecrementWindowWidth", KeyStroke = XKeys.Left)]
	[ClickHandler("decrementwindowwidth", "DecrementWindowWidth")]

	[KeyboardAction("incrementwindowcenter", "imageviewer-keyboard/ToolsStandardWindowLevel/IncrementWindowCenter", KeyStroke = XKeys.Up)]
	[ClickHandler("incrementwindowcenter", "IncrementWindowCenter")]

	[KeyboardAction("decrementwindowcenter", "imageviewer-keyboard/ToolsStandardWindowLevel/DecrementWindowCenter", KeyStroke = XKeys.Down)]
	[ClickHandler("decrementwindowcenter", "DecrementWindowCenter")]

	[ExtensionOf(typeof(ImageViewerToolExtensionPoint))]
	public class WindowLevelTool : MouseImageViewerTool
	{
		private UndoableCommand _command;
		private VoiLutOperationApplicator _applicator;

		public WindowLevelTool()
		{
			this.CursorToken = new CursorToken("Icons.WindowLevelToolSmall.png", this.GetType().Assembly);
        }

		public override void Initialize()
        {
            base.Initialize();
		}

		private void CaptureBeginState()
		{
			// this.SelectedVOILUTLinearProvider.VoiLutLinear is null 
			// when image is not grayscale
			if (this.SelectedPresentationImage == null ||
				this.SelectedVOILUTLinearProvider == null ||
				this.SelectedVOILUTLinearProvider.VoiLutLinear == null)
				return;

			_applicator = new VoiLutOperationApplicator(this.SelectedPresentationImage);
			_command = new UndoableCommand(_applicator);
			_command.Name = SR.CommandWindowLevel;
			_command.BeginState = _applicator.CreateMemento();
		}

		private void CaptureEndState()
		{
			// this.SelectedVOILUTLinearProvider.VoiLutLinear is null 
			// when image is not grayscale
			if (this.SelectedPresentationImage == null ||
				this.SelectedVOILUTLinearProvider == null ||
				this.SelectedVOILUTLinearProvider.VoiLutLinear == null)
				return;

			if (_command == null)
				return;

			_applicator.ApplyToLinkedImages();
			_command.EndState = _applicator.CreateMemento();

			// If the state hasn't changed since MouseDown just return
			if (_command.EndState.Equals(_command.BeginState))
			{
				_command = null;
				return;
			}

			this.Context.Viewer.CommandHistory.AddCommand(_command);
		}

		private void IncrementWindowWidth()
		{
			IncrementWindowWithUndo(10, 0);
		}

		private void DecrementWindowWidth()
		{
			IncrementWindowWithUndo(-10, 0);
		}

		private void IncrementWindowCenter()
		{
			IncrementWindowWithUndo(0, 10);
		}

		private void DecrementWindowCenter()
		{
			IncrementWindowWithUndo(0, -10);
		}

		public void IncrementWindowWithUndo(double windowIncrement, double levelIncrement)
		{
			this.CaptureBeginState();
			this.IncrementWindow(windowIncrement, levelIncrement);
			this.CaptureEndState();
		}

		private void IncrementWindow(double windowIncrement, double levelIncrement)
		{
			// this.SelectedVOILUTLinearProvider.VoiLutLinear is null 
			// when image is not grayscale
			if (this.SelectedPresentationImage == null ||
				this.SelectedVOILUTLinearProvider == null ||
				this.SelectedVOILUTLinearProvider.VoiLutLinear == null)
				return;

			CodeClock counter = new CodeClock();
			counter.Start();

			this.SelectedVOILUTLinearProvider.VoiLutLinear.WindowWidth += windowIncrement;
			this.SelectedVOILUTLinearProvider.VoiLutLinear.WindowCenter += levelIncrement;
			this.SelectedVOILUTLinearProvider.Draw();

			counter.Stop();

			string str = String.Format("WindowLevel: {0}\n", counter.ToString());
			Trace.Write(str);
		}

		public override bool Start(IMouseInformation mouseInformation)
		{
			base.Start(mouseInformation);

			CaptureBeginState();

			return true;
		}

		public override bool Track(IMouseInformation mouseInformation)
		{
			base.Track(mouseInformation);

			IncrementWindow(this.DeltaX * 10, this.DeltaY * 10);

			return true;
		}

		public override bool Stop(IMouseInformation mouseInformation)
		{
			base.Stop(mouseInformation);

			this.CaptureEndState();

			return false;
		}

		public override void Cancel()
		{
			this.CaptureEndState();
		}
	}
}
