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
using ClearCanvas.Dicom.ServiceModel.Query;
using ClearCanvas.ImageViewer.Comparers;
using ClearCanvas.ImageViewer.StudyManagement;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Dicom.Iod;

namespace ClearCanvas.ImageViewer
{
	/// <summary>
	/// Defines an extension point for image layout management.
	/// </summary>
	[ExtensionPoint]
	public sealed class LayoutManagerExtensionPoint : ExtensionPoint<ILayoutManager>
	{
	}

	/// <summary>
	/// Specifies how the <see cref="ImageViewerComponent"/>'s <see cref="ILayoutManager"/>
	/// should be created.
	/// </summary>
	public enum LayoutManagerCreationParameters
	{
		/// <summary>
		/// Use a simple layout manager that initializes the layout strictly
		/// based on the number of <see cref="IDisplaySet"/>s available.
		/// </summary>
		Simple,

		/// <summary>
		/// Use the <see cref="LayoutManagerExtensionPoint"/> to create
		/// the <see cref="ImageViewerComponent"/>'s <see cref="ILayoutManager"/>.
		/// </summary>
		Extended
	}

	/// <summary>
	/// A base implementation of <see cref="ILayoutManager"/>.
	/// </summary>
	public class LayoutManager : ILayoutManager
	{
		private IImageViewer _imageViewer;
		private bool _layoutCompleted;

		/// <summary>
		/// Constructor.
		/// </summary>
		public LayoutManager()
		{
			ReconcilePatientInfo = true;
		}

		#region Protected Properties

		/// <summary>
		/// Convenience property for retrieving the owner <see cref="IImageViewer"/>.
		/// </summary>
		protected IImageViewer ImageViewer
		{
			get { return _imageViewer; }
		}

		/// <summary>
		/// Convenience property for retrieving the <see cref="IImageViewer.StudyTree"/>.
		/// </summary>
		protected StudyTree StudyTree
		{
			get { return _imageViewer != null ? _imageViewer.StudyTree : null; }
		}

		/// <summary>
		/// Convenience property for retrieving the <see cref="IImageViewer.PhysicalWorkspace"/> property.
		/// </summary>
		protected IPhysicalWorkspace PhysicalWorkspace
		{
			get { return _imageViewer != null ? _imageViewer.PhysicalWorkspace : null; }
		}

		/// <summary>
		/// Convenience property for retrieving the <see cref="IImageViewer.LogicalWorkspace"/> property.
		/// </summary>
		protected ILogicalWorkspace LogicalWorkspace
		{
			get { return _imageViewer != null ? _imageViewer.LogicalWorkspace : null; }
		}

		/// <summary>
		/// Gets or sets whether or not to allow an empty <see cref="IImageViewer"/> (e.g. no studies loaded).
		/// </summary>
		public bool AllowEmptyViewer { get; set; }

		/// <summary>
		/// Gets or sets whether or not to reconcile patient information.
		/// </summary>
		public bool ReconcilePatientInfo { get; set; }

		#endregion

		#region ILayoutManager Members

		/// <summary>
		/// Sets the owning <see cref="IImageViewer"/>.
		/// </summary>
		public virtual void SetImageViewer(IImageViewer imageViewer)
		{
			_imageViewer = imageViewer;
		}

		/// <summary>
		/// Builds the <see cref="ILogicalWorkspace"/>, lays out and fills the <see cref="IPhysicalWorkspace"/>.
		/// </summary>
		/// <remarks>
		/// Internally, this method calls <see cref="BuildLogicalWorkspace"/>, <see cref="ValidateLogicalWorkspace"/>,
		/// <see cref="LayoutPhysicalWorkspace"/>, <see cref="FillPhysicalWorkspace"/> and <see cref="SortStudies"/> in that order,
		/// followed by a call to <see cref="IDrawable.Draw">IPhysicalWorkspace.Draw</see>, and finally <see cref="OnLayoutCompleted"/>.
		/// You can override this method entirely, or you can override any of the 5 methods called by this method.
		/// Note that you must draw the <see cref="IPhysicalWorkspace"/> and call <see cref="OnLayoutCompleted"/> if you choose
		/// to override this method completely.
		/// </remarks>
		public virtual void Layout()
		{
			if (_layoutCompleted)
				throw new InvalidOperationException("Layout has already been called.");

			BuildLogicalWorkspace();
			ValidateLogicalWorkspace();
			LayoutPhysicalWorkspace();
			FillPhysicalWorkspace();
			
			// Now, only after showing the "primary study", sort the image sets according to study order. (yes, this calls SortStudies)
			SortImageSets();

			ImageViewer.PhysicalWorkspace.Draw();
			OnLayoutCompleted();
		}

		/// <summary>
		/// Called from <see cref="Layout"/> to signal that the layout is complete.
		/// </summary>
		protected virtual void OnLayoutCompleted()
		{
			_layoutCompleted = true;
			ImageViewer.EventBroker.StudyLoaded += OnPriorStudyLoaded;
		}

		#endregion

		#region Protected Methods

		#region Virtual
		/// <summary>
		/// Builds the <see cref="LogicalWorkspace"/>, creating and populating <see cref="ILogicalWorkspace.ImageSets">Image Sets</see>
		/// from the contents of <see cref="StudyTree"/>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// By default, this method simply creates <see cref="IImageSet">Image Sets</see> and <see cref="IDisplaySet">Display Sets</see>
		/// to match the <see cref="Study"/> and <see cref="Series"/> hierarchy in <see cref="StudyTree"/>.
		/// </para>
		/// <para>
		/// Override this method to change how <see cref="IImageSet">Image Sets</see> and <see cref="IDisplaySet">Display Sets</see> are constructed.
		/// </para>
		/// </remarks>
		protected virtual void BuildLogicalWorkspace()
		{
			foreach (Patient patient in ImageViewer.StudyTree.Patients)
			{
				foreach (Study study in patient.Studies)
				{
					BuildFromStudy(study);
				}
			}
		}

		/// <summary>
		/// Validates the <see cref="ILogicalWorkspace"/>.
		/// </summary>
		/// <remarks>
		/// A <see cref="NoVisibleDisplaySetsException"/> is thrown if no
		/// <see cref="IDisplaySet"/>s exist in the <see cref="ILogicalWorkspace"/>.
		/// </remarks>
		protected virtual void ValidateLogicalWorkspace()
		{
			foreach (IImageSet imageSet in _imageViewer.LogicalWorkspace.ImageSets)
			{
				foreach (IDisplaySet displaySet in imageSet.DisplaySets)
				{
					foreach (IPresentationImage image in displaySet.PresentationImages)
						return;
				}
			}

			if (!AllowEmptyViewer)
				throw new NoVisibleDisplaySetsException("The Layout operation has resulted in no images to be displayed.");
		}

		/// <summary>
		/// Lays out the physical workspace, adding and setting up the <see cref="IPhysicalWorkspace.ImageBoxes"/>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// By default, this method determines a simple layout based on the number of <see cref="IDisplaySet"/>s
		/// available (e.g. it tries to set the layout so all <see cref="IDisplaySet"/>s are visible).
		/// </para>
		/// <para>
		/// Typically, subclasses would override this method, at the very least.
		/// </para>
		/// </remarks>
		protected virtual void LayoutPhysicalWorkspace()
		{
			int numDisplaySets = GetNumberOfDisplaySets();

			if (numDisplaySets == 1)
				ImageViewer.PhysicalWorkspace.SetImageBoxGrid(1, 1);
			else if (numDisplaySets == 2)
				ImageViewer.PhysicalWorkspace.SetImageBoxGrid(1, 2);
			else if (numDisplaySets <= 4)
				ImageViewer.PhysicalWorkspace.SetImageBoxGrid(2, 2);
			else if (numDisplaySets <= 8)
				ImageViewer.PhysicalWorkspace.SetImageBoxGrid(2, 4);
			else if (numDisplaySets <= 12)
				ImageViewer.PhysicalWorkspace.SetImageBoxGrid(3, 4);
			else
				ImageViewer.PhysicalWorkspace.SetImageBoxGrid(4, 4);

			foreach (IImageBox imageBox in ImageViewer.PhysicalWorkspace.ImageBoxes)
				imageBox.SetTileGrid(1, 1);
		}

		/// <summary>
		/// Fills <see cref="IPhysicalWorkspace.ImageBoxes"/> with <see cref="IDisplaySet"/>s.
		/// </summary>
		/// <remarks>
		/// <para>
		/// By default, <see cref="IPhysicalWorkspace.ImageBoxes"/> is filled starting with the first 
		/// <see cref="IImageSet"/>'s <see cref="IDisplaySet"/>, and continuing until there
		/// are no empty <see cref="IImageBox"/>es or all <see cref="IDisplaySet"/>s have been assigned to an <see cref="IImageBox"/>.
		/// </para>
		/// <para>
		/// Override this method to change how <see cref="IDisplaySet"/>s are assigned to <see cref="IPhysicalWorkspace.ImageBoxes"/>.
		/// </para>
		/// </remarks>
		protected virtual void FillPhysicalWorkspace()
		{
			IPhysicalWorkspace physicalWorkspace = ImageViewer.PhysicalWorkspace;
			ILogicalWorkspace logicalWorkspace = ImageViewer.LogicalWorkspace;

			if (logicalWorkspace.ImageSets.Count == 0)
				return;

			int imageSetIndex = 0;
			int displaySetIndex = 0;

			foreach (IImageBox imageBox in physicalWorkspace.ImageBoxes)
			{
				if (displaySetIndex == logicalWorkspace.ImageSets[imageSetIndex].DisplaySets.Count)
				{
					imageSetIndex++;
					displaySetIndex = 0;

					if (imageSetIndex == logicalWorkspace.ImageSets.Count)
						break;
				}

				imageBox.DisplaySet = logicalWorkspace.ImageSets[imageSetIndex].DisplaySets[displaySetIndex].CreateFreshCopy();
				displaySetIndex++;
			}
		}

		protected virtual IPatientData ReconcilePatient(Study study)
		{
			//The study tree naturally does the grouping that we need.
			return new StudyRootStudyIdentifier(study.ParentPatient, study.GetIdentifier());
		}

		/// <summary>
		/// Creates a <see cref="DicomImageSetDescriptor"/> for the given <see cref="IStudyRootStudyIdentifier">study</see>.
		/// </summary>
		protected virtual DicomImageSetDescriptor CreateImageSetDescriptor(IStudyRootStudyIdentifier studyData)
		{
			return new DicomImageSetDescriptor(studyData);
		}

		/// <summary>
		/// Creates, but does not populate, an <see cref="IImageSet"/> for the given <see cref="IStudyRootStudyIdentifier">study</see>.
		/// </summary>
		protected virtual IImageSet CreateImageSet(IStudyRootStudyIdentifier studyData)
		{
			return new ImageSet(CreateImageSetDescriptor(studyData));
		}
		
		/// <summary>
		/// Updates the contents of the given <see cref="IImageSet"/> by populating it with <see cref="IDisplaySet"/>s created
		/// from the given <see cref="Series"/>.
		/// </summary>
		protected virtual void UpdateImageSet(IImageSet imageSet, Series series)
		{
			foreach (IDisplaySet displaySet in BasicDisplaySetFactory.CreateSeriesDisplaySets(series, StudyTree))
				imageSet.DisplaySets.Add(displaySet);
		}

		/// <summary>
		/// Sorts the given <see cref="SopCollection"/>.
		/// </summary>
		/// <param name="sops"></param>
		protected virtual void SortSops(SopCollection sops)
		{
			sops.Sort(GetSopComparer());
		}

		/// <summary>
		/// Creates a comparer for <see cref="Sop"/>s.
		/// </summary>
		/// <returns></returns>
		protected virtual IComparer<Sop> GetSopComparer()
		{
			return new InstanceNumberComparer();
		}

		/// <summary>
		/// Sorts the given <see cref="SeriesCollection"/>.
		/// </summary>
		/// <param name="series"></param>
		protected virtual void SortSeries(SeriesCollection series)
		{
			series.Sort(GetSeriesComparer());
		}

		/// <summary>
		/// Creates a comparer for <see cref="Series"/>.
		/// </summary>
		protected virtual IComparer<Series> GetSeriesComparer()
		{
			return new SeriesNumberComparer();
		}

		/// <summary>
		/// Sorts the given <see cref="StudyCollection"/>.
		/// </summary>
		protected virtual void SortStudies(StudyCollection studies)
		{
			studies.Sort(GetStudyComparer());
		}

		/// <summary>
		/// Creates a comparer for <see cref="Study">studies</see>.
		/// </summary>
		/// <returns></returns>
		protected virtual IComparer<Study> GetStudyComparer()
		{
			return new StudyDateComparer();
		}

		#endregion

		#endregion

		#region Logical Workspace Building Methods

		private IStudyRootStudyIdentifier GetStudyIdentifier(Study study)
		{
			if (ReconcilePatientInfo)
				return new StudyRootStudyIdentifier(ReconcilePatient(study), study.GetIdentifier());

			return study.GetIdentifier();
		}

		private void BuildFromStudy(Study study)
		{
			IImageSet imageSet = GetImageSet(study.StudyInstanceUid);
			// Abort if image set has already been added
			if (imageSet != null)
				return;

			imageSet = CreateImageSet(GetStudyIdentifier(study));
			if (imageSet.Uid != study.StudyInstanceUid)
				throw new InvalidOperationException("ImageSet Uid must be the same as Study Instance Uid.");

			SortSeries(study.Series);

			foreach (Series series in study.Series)
			{
				SortSops(series.Sops);
				UpdateImageSet(imageSet, series);
			}

			if (imageSet.DisplaySets.Count == 0)
				imageSet.Dispose();
			else
				AddImageSet(imageSet);
		}

		private void AddImageSet(IImageSet imageSet)
		{
			int insertIndex = LogicalWorkspace.ImageSets.Count;
			if (_layoutCompleted)
			{
				//A bit cheap, but once the initial layout is done, we need to keep everything sorted.
				var sortedImageSets = new ObservableList<IImageSet>();
				foreach(IImageSet set in LogicalWorkspace.ImageSets)
					sortedImageSets.Add(set);

				sortedImageSets.Add(imageSet);
				SortImageSets(sortedImageSets);
				insertIndex = sortedImageSets.IndexOf(imageSet);
			}

			LogicalWorkspace.ImageSets.Insert(insertIndex, imageSet);
		}

		private void SortImageSets()
		{
			SortImageSets(LogicalWorkspace.ImageSets);
		}

		private void SortImageSets(ObservableList<IImageSet> imageSets)
		{
			SortImageSets(imageSets, GetAllStudiesSorted());
		}

		internal static void SortImageSets(ObservableList<IImageSet> imageSets, IList<Study> studies)
		{
			imageSets.Sort(new ImageSetComparer(studies));
		}

		#endregion

		#region Helper Methods

		private IImageSet GetImageSet(string studyInstanceUid)
		{
			foreach (ImageSet imageSet in LogicalWorkspace.ImageSets)
			{
				if (imageSet.Uid == studyInstanceUid)
					return imageSet;
			}

			return null;
		}

		private StudyCollection GetAllStudiesSorted()
		{
			var studies = new StudyCollection();

			foreach (Patient patient in StudyTree.Patients)
			{
				foreach (Study study in patient.Studies)
					studies.Add(study);
			}

			SortStudies(studies);
			return studies;
		}

		private int GetNumberOfDisplaySets()
		{
			int count = 0;

			foreach (IImageSet imageSet in ImageViewer.LogicalWorkspace.ImageSets)
				count += imageSet.DisplaySets.Count;

			return count;
		}

		#endregion

		private void OnPriorStudyLoaded(object sender, StudyLoadedEventArgs args)
		{
			OnPriorStudyLoaded(args.Study);
		}

		private void OnPriorStudyLoaded(Study study)
		{
			BuildFromStudy(study);
		}

		#region Disposal

		/// <summary>
		/// Implementation of <see cref="IDisposable"/>.
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && _imageViewer != null)
			{
				_imageViewer.EventBroker.StudyLoaded -= OnPriorStudyLoaded;
				_imageViewer = null;
			}
		}

		#region IDisposable Members

		/// <summary>
		/// Implementation of <see cref="IDisposable"/>.
		/// </summary>
		public void Dispose()
		{
			try
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			catch(Exception e)
			{
				Platform.Log(LogLevel.Warn, e);
			}
		}

		#endregion
		#endregion

		private class ImageSetComparer : IComparer<IImageSet>
		{
			private readonly IList<Study> _studies;

			public ImageSetComparer(IList<Study> studies)
			{
				_studies = studies;
			}

			#region IComparer<IImageSet> Members

			public int Compare(IImageSet x, IImageSet y)
			{
				int index1 = IndexOfStudy(x.Uid);
				int index2 = IndexOfStudy(y.Uid);

				if (index1 < index2)
					return -1;
				
				if (index1 > index2)
					return 1;

				return 0;
			}

			private int IndexOfStudy(string studyInstanceUid)
			{
				int i = 0;
				foreach (Study study in _studies)
				{
					if (study.StudyInstanceUid == studyInstanceUid)
						return i;

					++i;
				}

				return -1;
			}

			#endregion
		}

		#region Public Static Factory

		public static ILayoutManager Create()
		{
			return Create(LayoutManagerCreationParameters.Extended);
		}

		internal static ILayoutManager Create(LayoutManagerCreationParameters creationParameters)
		{
			ILayoutManager layoutManager = null;

			if (creationParameters == LayoutManagerCreationParameters.Extended)
			{
				try
				{
					layoutManager = (ILayoutManager)new LayoutManagerExtensionPoint().CreateExtension();
				}
				catch (NotSupportedException e)
				{
					Platform.Log(LogLevel.Debug, e);
				}
			}

			return layoutManager ?? new LayoutManager();
		}

		#endregion
	}
}
