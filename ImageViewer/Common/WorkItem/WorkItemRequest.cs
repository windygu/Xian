﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using ClearCanvas.Common.Serialization;
using ClearCanvas.Dicom.ServiceModel.Query;

namespace ClearCanvas.ImageViewer.Common.WorkItem
{


    public static class WorkItemRequestTypeProvider
    {
        private static readonly List<Type> List = new List<Type>
                                                      {
                                                          typeof (WorkItemRequest),
                                                          typeof (DicomSendRequest),
                                                          typeof (DicomImportRequest),
                                                          typeof (DicomRetreiveRequest),
                                                      };

        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider ignored)
        {
            foreach (var type in List)
                yield return type;
        }
    }

    /// <summary>
    /// Base Request object for the creation of <see cref="WorkItem"/>s.
    /// </summary>
    [XmlInclude(typeof(DicomSendRequest))]
    [XmlInclude(typeof(DicomImportRequest))]
    [DataContract(Namespace = ImageViewerNamespace.Value)]
    public abstract class WorkItemRequest : DataContractBase
    {
        [DataMember]
        public WorkItemPriorityEnum Priority { get; set; }

        [DataMember]
        public WorkItemTypeEnum Type { get; set; }

        //TODO:  We may not be able to localize this, if its included here, however, for things input by the service.
        [DataMember]
        public string ActivityType { get; set; }

        [DataMember]
        public string ActivityDescription { get; set; }

    }

    /// <summary>
    /// <see cref="WorkItemRequest"/> for sending a study to a DICOM AE.
    /// </summary>
    [DataContract(Namespace = ImageViewerNamespace.Value)]
    public class DicomSendRequest : WorkItemRequest
    {
        DicomSendRequest()
        {
            Type = WorkItemTypeEnum.DicomSend;
            Priority = WorkItemPriorityEnum.Normal;
        }

        [DataMember]
        public string AeTitle { get; set; }

        [DataMember]
        public string Host { get; set; }

        [DataMember]
        public int Port { get; set; }

        [DataMember]
        public string StudyInstanceUid { get; set; }

        [DataMember]
        public string TransferSyntaxUid { get; set; }
    }

    /// <summary>
    /// <see cref="WorkItemRequest"/> for importing files/studies.
    /// </summary>
    [DataContract(Namespace = ImageViewerNamespace.Value)]
    public class DicomImportRequest : WorkItemRequest
    {
        DicomImportRequest()
        {
            Type = WorkItemTypeEnum.Import;
            Priority = WorkItemPriorityEnum.Stat;
        }

        [DataMember(IsRequired = true)]
        public bool Recursive { get; set; }

        [DataMember(IsRequired = true)]
        public IEnumerable<string> FileExtensions { get; set; }

        [DataMember(IsRequired = true)]
        public IEnumerable<string> FilePaths { get; set; }
    }


    /// <summary>
    /// DICOM Retrieve Request
    /// </summary>
    public class DicomRetreiveRequest : WorkItemRequest
    {
        [DataMember(IsRequired = true)]
        public string FromAETitle { get; set; }

        [DataMember(IsRequired = true)]
        public IStudyIdentifier StudyInformation { get; set; }
    }
}
