﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearCanvas.ImageViewer.Layout.Basic;
using ClearCanvas.ImageViewer.Layout;

namespace ClearCanvas.ImageViewer.Web
{
    internal class KeyImageDisplaySetCreationOptions : IModalityDisplaySetCreationOptions
    {
        private readonly IModalityDisplaySetCreationOptions _real;

        public KeyImageDisplaySetCreationOptions(IModalityDisplaySetCreationOptions real)
        {
            _real = real;
        }

        public bool ShowGrayscaleInverted
        {
            get { return _real.ShowGrayscaleInverted; }
        }

        public bool ShowOriginalMixedMultiframeSeries
        {
            get { return _real.ShowOriginalMixedMultiframeSeries; }
        }

        public bool SplitMixedMultiframes
        {
            get { return _real.SplitMixedMultiframes; }
        }

        public bool ShowOriginalMultiEchoSeries
        {
            get { return _real.ShowOriginalMultiEchoSeries; }
        }

        public bool SplitMultiEchoSeries
        {
            get { return _real.SplitMultiEchoSeries; }
        }

        public bool ShowOriginalSeries
        {
            get { return false; }
        }

        public bool CreateSingleImageDisplaySets
        {
            get { return false; }
        }

        public bool CreateAllImagesDisplaySet
        {
            get { return true; }
        }

        public string Modality
        {
            get { return _real.Modality; }
        }
    }

    internal class KeyImageLayoutHook : HpLayoutHook
    {
        #region IHpLayoutHook Members

        public override bool HandleLayout(IHpLayoutHookContext context)
        {
            var primaryImageSet = context.ImageViewer.LogicalWorkspace.ImageSets[0];
            var firstKeyImageDisplaySet = primaryImageSet.DisplaySets.FirstOrDefault(IsKeyImageDisplaySet);
            if (firstKeyImageDisplaySet == null)
                return false;

            context.ImageViewer.PhysicalWorkspace.SetImageBoxGrid(1, 1);
            var imageBox = context.ImageViewer.PhysicalWorkspace.ImageBoxes[0];
            imageBox.SetTileGrid(1, 1);
            imageBox.DisplaySet = firstKeyImageDisplaySet.CreateFreshCopy();

            return true;
        }

        #endregion

        private bool IsKeyImageDisplaySet(IDisplaySet displaySet)
        {
            if (displaySet == null || displaySet.PresentationImages.Count == 0)
                return false;

            var descriptor = displaySet.Descriptor as IDicomDisplaySetDescriptor;
            if (descriptor == null)
                return false;

            const string koModality = "KO";

            if (descriptor.SourceSeries != null && descriptor.SourceSeries.Modality == koModality)
                return true;

            var allImagesDescriptor = descriptor as ModalityDisplaySetDescriptor;
            return allImagesDescriptor != null && allImagesDescriptor.Modality == koModality;
        }
    }

    internal class CustomLayoutHook : HpLayoutHook
    {
        private int _rows;
        private int _cols;

        public CustomLayoutHook()
        {
        }

        public CustomLayoutHook(int rows, int cols)
        {
            _rows = rows;
            _cols = cols;
        }

        #region IHpLayoutHook Members

        public override bool HandleLayout(IHpLayoutHookContext context)
        {
            if (_rows > 0 && _cols > 0)
            {
                var primaryImageSet = context.ImageViewer.LogicalWorkspace.ImageSets[0];
                context.ImageViewer.PhysicalWorkspace.SetImageBoxGrid(_rows, _cols);
                foreach (var imageBox in context.ImageViewer.PhysicalWorkspace.ImageBoxes)
                    imageBox.SetTileGrid(1, 1);

                context.PerformDefaultFillPhysicalWorkspace();

                return true;
            }
            else
                return false;
            
        }

        #endregion

        private bool IsKeyImageDisplaySet(IDisplaySet displaySet)
        {
            if (displaySet == null || displaySet.PresentationImages.Count == 0)
                return false;

            var descriptor = displaySet.Descriptor as IDicomDisplaySetDescriptor;
            if (descriptor == null)
                return false;

            const string koModality = "KO";

            if (descriptor.SourceSeries != null && descriptor.SourceSeries.Modality == koModality)
                return true;

            var allImagesDescriptor = descriptor as ModalityDisplaySetDescriptor;
            return allImagesDescriptor != null && allImagesDescriptor.Modality == koModality;
        }
    }
}
