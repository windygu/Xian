#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using ClearCanvas.Common;
using ClearCanvas.ImageViewer.StudyManagement;

namespace ClearCanvas.ImageViewer.Annotations.Dicom
{
	/// <summary>
	/// A specialization of <see cref="AnnotationItem"/> for showing dicom tag data on the overlay.
	/// </summary>
	/// <seealso cref="AnnotationItem"/>
	public class DicomAnnotationItem<T>: AnnotationItem
	{
		private readonly FrameDataRetrieverDelegate<T> _sopDataRetrieverDelegate;
		private readonly ResultFormatterDelegate<T> _resultFormatterDelegate;

		/// <summary>
		/// A constructor that uses the <see cref="DicomAnnotationItem{T}"/>'s unique identifier to determine
		/// the display name and label using an <see cref="IAnnotationResourceResolver"/>.
		/// </summary>
		/// <param name="identifier">The unique identifier of the <see cref="DicomAnnotationItem{T}"/>.</param>
		/// <param name="resolver">The object that will resolve the display name and label 
		/// from the <see cref="DicomAnnotationItem{T}"/>'s unique identifier.</param>
		/// <param name="sopDataRetrieverDelegate">A delegate used to retrieve the Dicom tag data.</param>
		/// <param name="resultFormatterDelegate">A delegate that will format the Dicom tag data as a string.</param>
		public DicomAnnotationItem
			(
				string identifier,
				IAnnotationResourceResolver resolver,
				FrameDataRetrieverDelegate<T> sopDataRetrieverDelegate,
				ResultFormatterDelegate<T> resultFormatterDelegate
			)
			: this(identifier, resolver.ResolveDisplayName(identifier), resolver.ResolveLabel(identifier), sopDataRetrieverDelegate, resultFormatterDelegate)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="identifier">The unique identifier of the <see cref="DicomAnnotationItem{T}"/>.</param>
		/// <param name="displayName">The <see cref="DicomAnnotationItem{T}"/>'s display name.</param>
		/// <param name="label">The <see cref="DicomAnnotationItem{T}"/>'s label.</param>
		/// <param name="sopDataRetrieverDelegate">A delegate used to retrieve the Dicom tag data.</param>
		/// <param name="resultFormatterDelegate">A delegate that will format the Dicom tag data as a string.</param>
		public DicomAnnotationItem
			(
				string identifier,
				string displayName,
				string label,
				FrameDataRetrieverDelegate<T> sopDataRetrieverDelegate,
				ResultFormatterDelegate<T> resultFormatterDelegate
			)
			: base(identifier, displayName, label)
		{
			Platform.CheckForNullReference(sopDataRetrieverDelegate, "sopDataRetrieverDelegate");
			Platform.CheckForNullReference(resultFormatterDelegate, "resultFormatterDelegate");

			_sopDataRetrieverDelegate = sopDataRetrieverDelegate;
			_resultFormatterDelegate = resultFormatterDelegate;
		}

		/// <summary>
		/// Gets the annotation text for display on the overlay.
		/// </summary>
		/// <remarks>
		/// The input <see cref="IPresentationImage"/> must implement <see cref="IImageSopProvider"/> in 
		/// order for a non-empty string to be returned.
		/// </remarks>
		public override string GetAnnotationText(IPresentationImage presentationImage)
		{
			IImageSopProvider associatedDicom = presentationImage as IImageSopProvider;
			if (associatedDicom == null)
				return "";

			return _resultFormatterDelegate(_sopDataRetrieverDelegate(associatedDicom.Frame));
		}
	}
}
