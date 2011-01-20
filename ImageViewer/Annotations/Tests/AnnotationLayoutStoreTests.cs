#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

#if UNIT_TESTS

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;
using ClearCanvas.Common.Utilities;
using NUnit.Framework;
using ClearCanvas.Common;

namespace ClearCanvas.ImageViewer.Annotations.Tests
{
#pragma warning disable 1591,0419,1574,1587

	[TestFixture]
	public class AnnotationLayoutStoreTests
	{
		public AnnotationLayoutStoreTests()
		{
		}

		[TestFixtureSetUp]
		public void Setup()
		{
			Platform.SetExtensionFactory(new NullExtensionFactory());
		}

		[TestFixtureTearDown]
		public void Teardown()
		{
			// reset all the changes we made in this test
			AnnotationLayoutStore.Instance.Reset();
		}

		[Test]
		public void Test()
		{
			List<IAnnotationItem> annotationItems = new List<IAnnotationItem>();

			AnnotationLayoutStore.Instance.Clear();

			IList<StoredAnnotationLayout> layouts = AnnotationLayoutStore.Instance.GetLayouts(annotationItems);
			Assert.AreEqual(0, layouts.Count);

			AnnotationLayoutStore.Instance.Update(this.CreateLayout("testLayout1"));
			layouts = AnnotationLayoutStore.Instance.GetLayouts(annotationItems);
			Assert.AreEqual(1, layouts.Count);

			layouts = new List<StoredAnnotationLayout>();
			layouts.Clear();
			layouts.Add(CreateLayout("testLayout1"));
			layouts.Add(CreateLayout("testLayout2"));
			layouts.Add(CreateLayout("testLayout3"));
			layouts.Add(CreateLayout("testLayout4"));

			AnnotationLayoutStore.Instance.Update(layouts);

			layouts = AnnotationLayoutStore.Instance.GetLayouts(annotationItems);
			Assert.AreEqual(4, layouts.Count);

			ResourceResolver resolver = new ResourceResolver(this.GetType().Assembly);
			using (Stream stream = resolver.OpenResource("AnnotationLayoutStoreDefaults.xml"))
			{
				AnnotationLayoutStoreSettings.Default.LayoutSettingsXml = new XmlDocument();
				AnnotationLayoutStoreSettings.Default.LayoutSettingsXml.Load(stream);
			}

			layouts = AnnotationLayoutStore.Instance.GetLayouts(annotationItems);
			int xmlLayoutCount = layouts.Count;

			StoredAnnotationLayout layout = AnnotationLayoutStore.Instance.GetLayout("Dicom.OT", annotationItems);
			layout = CopyLayout(layout, "Dicom.OT.Copied");

			AnnotationLayoutStore.Instance.Update(layout);
			layouts = AnnotationLayoutStore.Instance.GetLayouts(annotationItems);
			Assert.AreEqual(xmlLayoutCount + 1, layouts.Count);

			layout = AnnotationLayoutStore.Instance.GetLayout("Dicom.OT.Copied", annotationItems);
			Assert.IsNotNull(layout);
			
			AnnotationLayoutStore.Instance.RemoveLayout("Dicom.OT.Copied");
			layouts = AnnotationLayoutStore.Instance.GetLayouts(annotationItems);
			Assert.AreEqual(xmlLayoutCount, layouts.Count);

			layout = AnnotationLayoutStore.Instance.GetLayout("Dicom.OT.Copied", annotationItems);
			Assert.IsNull(layout);

			layout = AnnotationLayoutStore.Instance.GetLayout("Dicom.OT", annotationItems);
			Assert.IsNotNull(layout);

			layouts = new List<StoredAnnotationLayout>(); 
			layouts.Clear();
			layouts.Add(CreateLayout("testLayout1"));
			layouts.Add(CreateLayout("testLayout2"));
			layouts.Add(CreateLayout("testLayout3"));
			layouts.Add(CreateLayout("testLayout4"));

			AnnotationLayoutStore.Instance.Update(layouts);

			layouts = AnnotationLayoutStore.Instance.GetLayouts(annotationItems);
			Assert.AreEqual(xmlLayoutCount + 4, layouts.Count);

			AnnotationLayoutStore.Instance.RemoveLayout("testLayout1");
			layouts = AnnotationLayoutStore.Instance.GetLayouts(annotationItems);
			Assert.AreEqual(xmlLayoutCount + 3, layouts.Count);

			AnnotationLayoutStore.Instance.RemoveLayout("testLayout2");
			layouts = AnnotationLayoutStore.Instance.GetLayouts(annotationItems);
			Assert.AreEqual(xmlLayoutCount + 2, layouts.Count);

			layout = AnnotationLayoutStore.Instance.GetLayout("Dicom.OT", annotationItems);
			Assert.IsNotNull(layout);

			layout = AnnotationLayoutStore.Instance.GetLayout("testLayout3", annotationItems); 
			Assert.AreEqual(1, layout.AnnotationBoxGroups.Count);

			layout = AnnotationLayoutStore.Instance.GetLayout("Dicom.OT", annotationItems);
			layout = CopyLayout(layout, "testLayout3");
			AnnotationLayoutStore.Instance.Update(layout);
			layout = AnnotationLayoutStore.Instance.GetLayout("testLayout3", annotationItems);
			Assert.AreEqual(4, layout.AnnotationBoxGroups.Count);
		}

		StoredAnnotationLayout CopyLayout(StoredAnnotationLayout layout, string newIdentifier)
		{
			StoredAnnotationLayout newLayout = new StoredAnnotationLayout(newIdentifier);
			foreach (StoredAnnotationBoxGroup group in layout.AnnotationBoxGroups)
				newLayout.AnnotationBoxGroups.Add(group);
			
			return newLayout;
		}

		StoredAnnotationLayout CreateLayout(string identifier)
		{
			StoredAnnotationLayout layout = new StoredAnnotationLayout(identifier);
			layout.AnnotationBoxGroups.Add(new StoredAnnotationBoxGroup("group1"));
			layout.AnnotationBoxGroups[0].AnnotationBoxes.Add(
				new AnnotationBox(new RectangleF(0.0F, 0.0F, 0.5F, 0.1F), AnnotationLayoutFactory.GetAnnotationItem("Dicom.GeneralStudy.StudyDescription")));
			layout.AnnotationBoxGroups[0].AnnotationBoxes.Add(
				new AnnotationBox(new RectangleF(0.0F, 0.1F, 0.5F, 0.2F), AnnotationLayoutFactory.GetAnnotationItem("Dicom.GeneralStudy.StudyDescription")));

			return layout;
		}
	}
}

#endif