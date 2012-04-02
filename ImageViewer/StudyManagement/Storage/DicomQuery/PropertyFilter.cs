﻿using System.Collections.Generic;
using System.Linq;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Utilities;

namespace ClearCanvas.ImageViewer.StudyManagement.Storage.DicomQuery
{
    internal interface IPropertyFilter<T>
    {
        DicomTagPath Path { get; }
        DicomAttribute Criterion { get; }
        bool IsNoOp { get; }

        IQueryable<T> AddToQuery(IQueryable<T> query);
        IEnumerable<T> FilterResults(IEnumerable<T> results);

        void SetAttributeValue(T item, DicomAttributeCollection result);
    }

    internal class PropertyFilter<T> : IPropertyFilter<T>
    {
        protected PropertyFilter(DicomTagPath path, IDicomAttributeProvider criteria)
        {
            Path = path;
            Criterion = Path.GetAttribute(criteria);
            IsReturnValueRequired = false;
            AddToQueryEnabled = true;
            FilterResultsEnabled = false;
        }

        public DicomTagPath Path { get; private set; }
        public DicomAttribute Criterion { get; private set; }

        public virtual bool IsNoOp
        {
            get { return IsCriterionEmpty && !IsReturnValueRequired; }
        }

        /// <summary>
        /// The value is required to be returned in the results.
        /// </summary>
        protected internal bool IsReturnValueRequired { get; set; }
        protected internal bool AddToQueryEnabled { get; set; }
        protected internal bool FilterResultsEnabled { get; set; }

        protected internal virtual bool IsCriterionEmpty
        {
            get { return Criterion == null || Criterion.IsEmpty; }
        }

        protected internal virtual bool IsCriterionNull
        {
            get { return Criterion != null && Criterion.IsNull; }
        }

        protected internal virtual bool ShouldAddToQuery
        {
            get { return !IsCriterionEmpty && !IsCriterionNull; }
        }

        protected internal virtual bool ShouldAddToResult
        {
            get
            {
                if (IsReturnValueRequired)
                    return true;

                return !IsCriterionEmpty;
            }
        }

        protected virtual IQueryable<T> AddToQuery(IQueryable<T> query)
        {
            return query;
        }

        protected virtual IEnumerable<T> FilterResults(IEnumerable<T> results)
        {
            return results;
        }

        protected virtual void AddValueToResult(T item, DicomAttribute resultAttribute)
        {
            resultAttribute.SetNullValue();
        }

        #region IPropertyFilter<T> Members

        IQueryable<T> IPropertyFilter<T>.AddToQuery(IQueryable<T> query)
        {
            if (!AddToQueryEnabled || !ShouldAddToQuery)
                return query;

            return AddToQuery(query);
        }

        IEnumerable<T> IPropertyFilter<T>.FilterResults(IEnumerable<T> results)
        {
            if (!FilterResultsEnabled || !ShouldAddToQuery)
                return results;

            return FilterResults(results);
        }

        void IPropertyFilter<T>.SetAttributeValue(T item, DicomAttributeCollection result)
        {
            if (!ShouldAddToResult)
                return;

            var resultAttribute = Path.GetAttribute(result, true);
            AddValueToResult(item, resultAttribute);
        }

        #endregion
    }
}