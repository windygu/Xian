﻿#region License

// Copyright (c) 2010, ClearCanvas Inc.
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
using System.Drawing;
using ClearCanvas.Dicom.Iod.Sequences;
using ClearCanvas.ImageViewer.Graphics;
using ClearCanvas.ImageViewer.InteractiveGraphics;
using ClearCanvas.ImageViewer.Mathematics;
using ClearCanvas.ImageViewer.PresentationStates.Dicom;
using GraphicObject = ClearCanvas.Dicom.Iod.Sequences.GraphicAnnotationSequenceItem.GraphicObjectSequenceItem;
using TextObject = ClearCanvas.Dicom.Iod.Sequences.GraphicAnnotationSequenceItem.TextObjectSequenceItem;

namespace ClearCanvas.ImageViewer.Tools.Measurement
{
	partial class ShowAnglesTool
	{
		[DicomSerializableGraphicAnnotation(typeof (CompositeGraphicSerializer))]
		private partial class ShowAnglesToolCompositeGraphic {}

		[DicomSerializableGraphicAnnotation(typeof (ShowAnglesToolGraphicSerializer))]
		private partial class ShowAnglesToolGraphic {}

		private class ShowAnglesToolGraphicSerializer : GraphicAnnotationSerializer<ShowAnglesToolGraphic>
		{
			protected override void Serialize(ShowAnglesToolGraphic showAnglesToolGraphic, GraphicAnnotationSequenceItem serializationState)
			{
				if (!showAnglesToolGraphic.Visible)
					return;

				foreach (IGraphic graphic in showAnglesToolGraphic.Graphics)
				{
					if (!graphic.Visible)
						continue;

					if (graphic is ILineSegmentGraphic)
						SerializeDashedLine((ILineSegmentGraphic) graphic, serializationState);
					else if (graphic is ICalloutGraphic)
						SerializeCallout((ICalloutGraphic) graphic, serializationState);
				}
			}

			private static void SerializeDashedLine(ILineSegmentGraphic lineSegmentGraphic, GraphicAnnotationSequenceItem serializationState)
			{
				lineSegmentGraphic.CoordinateSystem = CoordinateSystem.Source;
				try
				{
					SerializeDashedLine(lineSegmentGraphic.Point1, lineSegmentGraphic.Point2, lineSegmentGraphic.SpatialTransform, serializationState, true);
				}
				finally
				{
					lineSegmentGraphic.ResetCoordinateSystem();
				}
			}

			private static void SerializeDashedLine(PointF point1, PointF point2, SpatialTransform spatialTransform, GraphicAnnotationSequenceItem serializationState, bool showHashes)
			{
				// these control parameters are in screen pixels at the nominal presentation zoom level
				const float period = 8;
				const float amplitude = 0.5f;

				SizeF normalVector;
				SizeF dashVector;
				float periods;

				// compute the dash vector and cross hash vector to be sized relative to screen pixels at nominal presentation zoom level
				{
					PointF dstLineVector = spatialTransform.ConvertToDestination(new SizeF(point2) - new SizeF(point1)).ToPointF();
					float dstMagnitude = (float) Math.Sqrt(dstLineVector.X*dstLineVector.X + dstLineVector.Y*dstLineVector.Y);
					periods = dstMagnitude/period;
					dstLineVector.X /= dstMagnitude;
					dstLineVector.Y /= dstMagnitude;
					dashVector = spatialTransform.ConvertToSource(new SizeF(dstLineVector.X*period/2, dstLineVector.Y*period/2));
					normalVector = spatialTransform.ConvertToSource(new SizeF(-dstLineVector.Y*amplitude, dstLineVector.X*amplitude));
				}

				PointF start = point1;
				int limit = (int) periods;
				for (int n = 0; n < limit; n++)
				{
					PointF midPeriod = start + dashVector;
					start = midPeriod + dashVector;

					GraphicObject dash = new GraphicObject();
					dash.GraphicAnnotationUnits = GraphicAnnotationSequenceItem.GraphicAnnotationUnits.Pixel;
					dash.GraphicData = new PointF[] {midPeriod, start};
					dash.GraphicDimensions = 2;
					dash.GraphicFilled = GraphicAnnotationSequenceItem.GraphicFilled.N;
					dash.GraphicType = GraphicAnnotationSequenceItem.GraphicType.Polyline;
					dash.NumberOfGraphicPoints = 2;
					serializationState.AppendGraphicObjectSequence(dash);
				}

				// the first half of each period has no line, so this is only necessary if the residual is over half a period
				if (periods - limit > 0.5f)
				{
					GraphicObject dash = new GraphicObject();
					dash.GraphicAnnotationUnits = GraphicAnnotationSequenceItem.GraphicAnnotationUnits.Pixel;
					dash.GraphicData = new PointF[] {start + dashVector, point2};
					dash.GraphicDimensions = 2;
					dash.GraphicFilled = GraphicAnnotationSequenceItem.GraphicFilled.N;
					dash.GraphicType = GraphicAnnotationSequenceItem.GraphicType.Polyline;
					dash.NumberOfGraphicPoints = 2;
					serializationState.AppendGraphicObjectSequence(dash);
				}

				if (showHashes)
				{
					GraphicObject hash1 = new GraphicObject();
					hash1.GraphicAnnotationUnits = GraphicAnnotationSequenceItem.GraphicAnnotationUnits.Pixel;
					hash1.GraphicData = new PointF[] {point1 - normalVector, point1 + normalVector};
					hash1.GraphicDimensions = 2;
					hash1.GraphicFilled = GraphicAnnotationSequenceItem.GraphicFilled.N;
					hash1.GraphicType = GraphicAnnotationSequenceItem.GraphicType.Polyline;
					hash1.NumberOfGraphicPoints = 2;
					serializationState.AppendGraphicObjectSequence(hash1);

					GraphicObject hash2 = new GraphicObject();
					hash2.GraphicAnnotationUnits = GraphicAnnotationSequenceItem.GraphicAnnotationUnits.Pixel;
					hash2.GraphicData = new PointF[] {point2 - normalVector, point2 + normalVector};
					hash2.GraphicDimensions = 2;
					hash2.GraphicFilled = GraphicAnnotationSequenceItem.GraphicFilled.N;
					hash2.GraphicType = GraphicAnnotationSequenceItem.GraphicType.Polyline;
					hash2.NumberOfGraphicPoints = 2;
					serializationState.AppendGraphicObjectSequence(hash2);
				}
			}

			private static void SerializeCallout(ICalloutGraphic calloutGraphic, GraphicAnnotationSequenceItem serializationState)
			{
				calloutGraphic.CoordinateSystem = CoordinateSystem.Source;
				try
				{
					RectangleF textBoundingBox = RectangleUtilities.ConvertToPositiveRectangle(calloutGraphic.TextBoundingBox);

					TextObject text = new TextObject();
					text.BoundingBoxAnnotationUnits = GraphicAnnotationSequenceItem.BoundingBoxAnnotationUnits.Pixel;
					text.BoundingBoxBottomRightHandCorner = new PointF(textBoundingBox.Right, textBoundingBox.Bottom);
					text.BoundingBoxTextHorizontalJustification = GraphicAnnotationSequenceItem.BoundingBoxTextHorizontalJustification.Center;
					text.BoundingBoxTopLeftHandCorner = textBoundingBox.Location;
					text.UnformattedTextValue = calloutGraphic.Text;
					serializationState.AppendTextObjectSequence(text);

					// draw the callout line manually instead of anchoring the text,
					// since we do not want the text to be moveable as permitted if we have an anchor point
					SerializeDashedLine(calloutGraphic.TextLocation, calloutGraphic.AnchorPoint, calloutGraphic.SpatialTransform, serializationState, false);
				}
				finally
				{
					calloutGraphic.ResetCoordinateSystem();
				}
			}
		}

		private class CompositeGraphicSerializer : GraphicAnnotationSerializer<CompositeGraphic>
		{
			protected override void Serialize(CompositeGraphic graphic, GraphicAnnotationSequenceItem serializationState)
			{
				foreach (IGraphic subgraphic in graphic.Graphics)
					SerializeGraphic(subgraphic, serializationState);
			}
		}
	}
}