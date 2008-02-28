#region License

// Copyright (c) 2006-2008, ClearCanvas Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of ClearCanvas Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System;
using System.Diagnostics;
using System.Drawing;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Desktop;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop.Tools;
using ClearCanvas.ImageViewer.Graphics;
using ClearCanvas.ImageViewer.InputManagement;
using ClearCanvas.ImageViewer.Mathematics;

namespace ClearCanvas.ImageViewer.InteractiveGraphics
{
	/// <summary>
	/// An interactive graphic that consists of region of interest
	/// of some kind and a callout.
	/// </summary>
	/// <remarks>
	/// <see cref="RoiGraphic"/> essentially acts as a template for any kind
	/// of interactive region of interest.  The type of region of interest
	/// can be any <see cref="InteractiveGraphic"/>, such as a line, a rectangle, 
	/// an ellipse, etc.; it is definable by the tool writer via the constructor.  
	/// By default, the callout line will snap to the
	/// nearest point on the <see cref="InteractiveGraphic"/>.
	/// </remarks>
	public class RoiGraphic
		: StandardStatefulCompositeGraphic, 
		  ISelectableGraphic, 
		  IFocussableGraphic, 
		  IContextMenuProvider,
		  IMemorable
	{
		private class RoiGraphicMemento : IEquatable<RoiGraphicMemento>
		{
			public readonly object RoiMemento;
			public readonly object CalloutMemento;

			public RoiGraphicMemento(object roiMemento, object calloutMemento)
			{
				RoiMemento = roiMemento;
				CalloutMemento = calloutMemento;
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override bool Equals(object obj)
			{
				if (obj == this)
					return true;

				return this.Equals(obj as RoiGraphicMemento);
			}

			#region IEquatable<RoiGraphicMemento> Members

			public bool Equals(RoiGraphicMemento other)
			{
				if (other == null)
					return false;

				return RoiMemento.Equals(other.RoiMemento) && CalloutMemento.Equals(other.CalloutMemento);
			}

			#endregion
		}

		#region Private fields

		private InteractiveGraphic _roiGraphic;
        private CalloutGraphic _calloutGraphic;
		private ToolSet _toolSet;
		private event EventHandler _roiChangedEvent;
		private bool _selected;
		private bool _focussed;
		private bool _raiseRoiChangedEvent = true;

		#endregion

		/// <summary>
		/// Initializes a new instance of <see cref="RoiGraphic"/>.
		/// </summary>
		/// <param name="graphic"></param>
		/// <param name="userCreated"></param>
		public RoiGraphic(InteractiveGraphic graphic, bool userCreated)
		{
			_roiGraphic = graphic;

			BuildGraphic();

			if (userCreated)
				base.State = CreateCreateState();
			else
				base.State = CreateInactiveState();
		}

		/// <summary>
		/// Gets the <see cref="CalloutGraphic"/>.
		/// </summary>
		public CalloutGraphic Callout
		{
			get { return _calloutGraphic; }
		}

		/// <summary>
		/// Gets the <see cref="InteractiveGraphic"/> that defines
		/// the ROI.
		/// </summary>
        public InteractiveGraphic Roi
        {
            get { return _roiGraphic; }
        }

		/// <summary>
		/// Gets the colour of the <see cref="RoiGraphic"/>.
		/// </summary>
		public Color Color
		{
			get { return this.Roi.Color; }
			private set 
			{
				this.Roi.Color = value;
				this.Callout.Color = value;
			}
		}

		/// <summary>
		/// Occurs when the size or position of 
		/// <see cref="RoiGraphic.Roi"/> has changed
		/// </summary>
		public event EventHandler RoiChanged
		{
			add { _roiChangedEvent += value; }
			remove { _roiChangedEvent -= value; }
		}

		/// <summary>
		/// Gets the cursor token to be shown at the current mouse position.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public override CursorToken GetCursorToken(Point point)
		{
			CursorToken token = _roiGraphic.GetCursorToken(point);
			if (token == null)
				token = _calloutGraphic.GetCursorToken(point);

			return token;
		}

		/// <summary>
		/// Performs a hit test on both the ROI and callout.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public override bool HitTest(Point point)
		{
			return _roiGraphic.HitTest(point) || _calloutGraphic.HitTest(point);
		}

		/// <summary>
		/// Suspends the raising of the <see cref="RoiChanged"/> event.
		/// </summary>
		/// <remarks>
		/// There are times when it is desirable to suspend the raising of the
		/// <see cref="RoiChanged"/> event, such as when initializing 
		/// control points.  To resume the raising of the event, call
		/// <see cref="ResumeRoiChangedEvent"/>.
		/// </remarks>
		public void SuspendRoiChangedEvent()
		{
			_raiseRoiChangedEvent = false;
		}

		/// <summary>
		/// Resumes the raising of the <see cref="RoiChanged"/> event.
		/// </summary>
		/// <param name="raiseEventNow">If <b>true</b>, the <see cref="RoiChanged"/>
		/// event is raised immediately.
		/// </param>
		public void ResumeRoiChangedEvent(bool raiseEventNow)
		{
			_raiseRoiChangedEvent = true;
			
			if (raiseEventNow)
				EventsHelper.Fire(_roiChangedEvent, this, EventArgs.Empty);
		}

		#region IContextMenuProvider Members

		/// <summary>
		/// Gets the context menu <see cref="ActionModelNode"/> based on the current state of the mouse.
		/// </summary>
		public virtual ActionModelNode GetContextMenuModel(IMouseInformation mouseInformation)
		{
			if (!this.HitTest(mouseInformation.Location))
				return null;

			if (_toolSet == null)
				_toolSet = new ToolSet(new GraphicToolExtensionPoint(), new GraphicToolContext(this, this.ImageViewer.DesktopWindow));

			return ActionModelRoot.CreateModel(typeof(RoiGraphic).FullName, "basicgraphic-menu", _toolSet.Actions);
		}

		#endregion

		#region ISelectable Members

		/// <summary>
		/// Gets or set a value indicating whether the <see cref="RoiGraphic"/> is selected.
		/// </summary>
		public bool Selected
		{
			get
			{
				return _selected;
			}
			set
			{
				if (_selected != value)
				{
					_selected = value;

					if (_selected)
						this.ParentPresentationImage.SelectedGraphic = this;

					if (_focussed)
					{
						if (_selected)
							this.State = CreateFocussedSelectedState();
						else
							this.State = CreateFocussedState();
					}
					else
					{
						if (_selected)
							this.State = CreateSelectedState();
						else
							this.State = CreateInactiveState();
					}
				}
			}
		}

		
		#endregion

		#region IFocussable Members

		/// <summary>
		/// Gets or set a value indicating whether the <see cref="RoiGraphic"/> is in focus.
		/// </summary>
		public bool Focussed
		{
			get
			{
				return _focussed;
			}
			set
			{
				if (_focussed != value)
				{
					_focussed = value;

					if (_focussed)
					{
						this.ParentPresentationImage.FocussedGraphic = this;

						if (this.Selected)
							this.State = CreateFocussedSelectedState();
						else
							this.State = CreateFocussedState();
					}
					else
					{
						if (this.Selected)
							this.State = CreateSelectedState();
						else
							this.State = CreateInactiveState();
					}
				}
			}
		}

		#endregion

		#region IMemorable Members

		/// <summary>
		/// Creates a memento that can be used to restore the current state.
		/// </summary>
		public virtual object CreateMemento()
		{
			return new RoiGraphicMemento(_roiGraphic.CreateMemento(), _calloutGraphic.CreateMemento());
		}

		/// <summary>
		/// Restores the state of an object.
		/// </summary>
		/// <param name="memento">The object that was
		/// originally created with <see cref="IMemorable.CreateMemento"/>.</param>
		/// <remarks>
		/// The implementation of <see cref="IMemorable.SetMemento"/> should return the 
		/// object to the original state captured by <see cref="IMemorable.CreateMemento"/>.
		/// </remarks>
		public virtual void SetMemento(object memento)
		{
			RoiGraphicMemento roiMemento = memento as RoiGraphicMemento;
			Platform.CheckForInvalidCast(roiMemento, "memento", "RoiGraphicMemento");
			
			_calloutGraphic.SetMemento(roiMemento.CalloutMemento);
			_roiGraphic.SetMemento(roiMemento.RoiMemento);
		}

		#endregion

		#region Overrides 

		/// <summary>
		/// Creates a creation <see cref="GraphicState"/>.
		/// </summary>
		/// <returns></returns>
		public GraphicState CreateCreateState()
		{
			return new CreateRoiGraphicState(this);
		}

		/// <summary>
		/// Creates a focussed and selected <see cref="GraphicState"/>.
		/// </summary>
		/// <returns></returns>
		public override GraphicState CreateFocussedSelectedState()
		{
			return new FocussedSelectedRoiGraphicState(this);
		}

		#endregion

		/// <summary>
		/// Occurs when one of the <see cref="Roi"/>'s control points has changed.
		/// </summary>
		/// <remarks>
		/// This method does nothing unless overridden.  Derived classes have an
		/// opportunity to do things such as set the <see cref="Callout"/>'s location based on
		/// some property of the <see cref="Roi"/>, for example.
		/// </remarks>
		protected virtual void OnControlPointChanged()
		{
		}

		/// <summary>
		/// Occurs when the <see cref="Callout"/>'s location has changed.
		/// </summary>
		/// <remarks>
		/// This method does nothing unless overridden.
		/// </remarks>
		protected virtual void OnCalloutLocationChanged()
		{
		}

		/// <summary>
		/// Called when the <see cref="Callout"/>'s end point needs to be recalculated.
		/// </summary>
		/// <remarks>
		/// The callout endpoint is automatically calculated based on the properties
		/// of the <see cref="Roi"/> itself and the location of the <see cref="Callout"/>.
		/// By default, the value is the closest point to the <see cref="Callout"/>'s 
		/// location on the <see cref="Roi"/> (determined from <see cref="InteractiveGraphic.GetClosestPoint"/>).
		/// </remarks>
		protected virtual PointF CalculateCalloutEndPoint()
		{
			return _roiGraphic.GetClosestPoint(_calloutGraphic.StartPoint);
		}

		private void BuildGraphic()
		{
			base.Graphics.Add(_roiGraphic);

			_calloutGraphic = new CalloutGraphic();
			base.Graphics.Add(_calloutGraphic);

			_roiGraphic.ControlPoints.ControlPointChangedEvent += new EventHandler<ListEventArgs<PointF>>(OnControlPointChanged);
			_calloutGraphic.LocationChanged += new EventHandler<PointChangedEventArgs>(OnCalloutLocationChanged);

			this.StateChanged += new EventHandler<GraphicStateChangedEventArgs>(OnROIGraphicStateChanged);
		}

		private void OnROIGraphicStateChanged(object sender, GraphicStateChangedEventArgs e)
		{
			if (typeof(InactiveGraphicState).IsAssignableFrom(e.NewState.GetType()))
				EnterInactiveState(e.MouseInformation);
			else if (typeof(FocussedGraphicState).IsAssignableFrom(e.NewState.GetType()))
				EnterFocusState(e.MouseInformation);
			else if (typeof(SelectedGraphicState).IsAssignableFrom(e.NewState.GetType()))
				EnterSelectedState(e.MouseInformation);
			else if (typeof(FocussedSelectedGraphicState).IsAssignableFrom(e.NewState.GetType()))
				EnterFocusSelectedState(e.MouseInformation);
		}

		private void EnterInactiveState(IMouseInformation mouseInformation)
		{
			// If the currently selected graphic is this one,
			// and we're about to go inactive, set the selected graphic
			// to null, indicating that no graphic is currently selected
			if (this.ParentPresentationImage.SelectedGraphic == this)
				this.ParentPresentationImage.SelectedGraphic = null;

			if (this.ParentPresentationImage.FocussedGraphic == this)
				this.ParentPresentationImage.FocussedGraphic = null;

			this.Roi.State = this.Roi.CreateInactiveState();
			this.Callout.State = this.Callout.CreateInactiveState();

			this.Roi.ControlPoints.Visible = false;
			this.Color = Color.Yellow;
			Draw();

			Trace.Write("EnterInactiveState\n");
		}

		private void EnterFocusState(IMouseInformation mouseInformation)
		{
			this.Focussed = true;

			if (this.Roi.HitTest(mouseInformation.Location))
				this.Roi.ControlPoints.Visible = true;
			else
				this.Roi.ControlPoints.Visible = false;

			this.Roi.State = this.Roi.CreateFocussedState();
			this.Callout.State = this.Callout.CreateFocussedState();

			this.Color = Color.Orange;
			Draw();

			Trace.Write("EnterFocusState\n");
		}

		private void EnterSelectedState(IMouseInformation mouseInformation)
		{
			this.Selected = true;

			if (this.ParentPresentationImage.FocussedGraphic == this)
				this.ParentPresentationImage.FocussedGraphic = null;

			//synchronize the states of the child graphics on entering this state so that everything works correctly.
			this.Roi.State = this.Roi.CreateSelectedState();
			this.Callout.State = this.Callout.CreateSelectedState();

			this.Roi.ControlPoints.Visible = false;
			this.Color = Color.Tomato;
			Draw();

			Trace.Write("EnterSelectedState\n");
		}

		private void EnterFocusSelectedState(IMouseInformation mouseInformation)
		{
			this.Selected = true;
			this.Focussed = true;

			//synchronize the states of the child graphics on entering this state so that everything works correctly.
			this.Roi.State = this.Roi.CreateFocussedSelectedState();
			this.Callout.State = this.Callout.CreateFocussedSelectedState();

			this.Roi.ControlPoints.Visible = true;
			this.Color = Color.Tomato;
			Draw();

			Trace.Write("EnterFocusSelectedState\n");
		}

		private void SetCalloutEndPoint()
		{
			// We're attaching the callout to the ROI, so make sure the two
			// graphics are in the same coordinate system before we do that.
			_calloutGraphic.CoordinateSystem = _roiGraphic.CoordinateSystem;
			_calloutGraphic.EndPoint = CalculateCalloutEndPoint();
			_calloutGraphic.ResetCoordinateSystem();
		}

		private void OnControlPointChanged(object sender, ListEventArgs<PointF> e)
		{
			OnControlPointChanged();

			SetCalloutEndPoint();

			if (_raiseRoiChangedEvent)
				EventsHelper.Fire(_roiChangedEvent, this, EventArgs.Empty);
		}

		private void OnCalloutLocationChanged(object sender, PointChangedEventArgs e)
		{
			OnCalloutLocationChanged();

			SetCalloutEndPoint();
		}
	}
}