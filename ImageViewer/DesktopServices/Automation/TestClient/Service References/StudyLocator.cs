﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.832
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator
{
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.clearcanvas.ca/dicom/query")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesIdentifier))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageIdentifier))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyIdentifier))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier))]
    public partial class Identifier : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InstanceAvailabilityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RetrieveAeTitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SpecificCharacterSetField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InstanceAvailability
        {
            get
            {
                return this.InstanceAvailabilityField;
            }
            set
            {
                this.InstanceAvailabilityField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RetrieveAeTitle
        {
            get
            {
                return this.RetrieveAeTitleField;
            }
            set
            {
                this.RetrieveAeTitleField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SpecificCharacterSet
        {
            get
            {
                return this.SpecificCharacterSetField;
            }
            set
            {
                this.SpecificCharacterSetField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.clearcanvas.ca/dicom/query")]
    [System.SerializableAttribute()]
    public partial class SeriesIdentifier : ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.Identifier
    {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ModalityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> NumberOfSeriesRelatedInstancesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SeriesDescriptionField;
        
        private string SeriesInstanceUidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SeriesNumberField;
        
        private string StudyInstanceUidField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Modality
        {
            get
            {
                return this.ModalityField;
            }
            set
            {
                this.ModalityField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> NumberOfSeriesRelatedInstances
        {
            get
            {
                return this.NumberOfSeriesRelatedInstancesField;
            }
            set
            {
                this.NumberOfSeriesRelatedInstancesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SeriesDescription
        {
            get
            {
                return this.SeriesDescriptionField;
            }
            set
            {
                this.SeriesDescriptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string SeriesInstanceUid
        {
            get
            {
                return this.SeriesInstanceUidField;
            }
            set
            {
                this.SeriesInstanceUidField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SeriesNumber
        {
            get
            {
                return this.SeriesNumberField;
            }
            set
            {
                this.SeriesNumberField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string StudyInstanceUid
        {
            get
            {
                return this.StudyInstanceUidField;
            }
            set
            {
                this.StudyInstanceUidField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.clearcanvas.ca/dicom/query")]
    [System.SerializableAttribute()]
    public partial class ImageIdentifier : ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.Identifier
    {
        
        private System.Nullable<int> InstanceNumberField;
        
        private string SeriesInstanceUidField;
        
        private string SopClassUidField;
        
        private string SopInstanceUidField;
        
        private string StudyInstanceUidField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Nullable<int> InstanceNumber
        {
            get
            {
                return this.InstanceNumberField;
            }
            set
            {
                this.InstanceNumberField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string SeriesInstanceUid
        {
            get
            {
                return this.SeriesInstanceUidField;
            }
            set
            {
                this.SeriesInstanceUidField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string SopClassUid
        {
            get
            {
                return this.SopClassUidField;
            }
            set
            {
                this.SopClassUidField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string SopInstanceUid
        {
            get
            {
                return this.SopInstanceUidField;
            }
            set
            {
                this.SopInstanceUidField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string StudyInstanceUid
        {
            get
            {
                return this.StudyInstanceUidField;
            }
            set
            {
                this.StudyInstanceUidField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.clearcanvas.ca/dicom/query")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier))]
    public partial class StudyIdentifier : ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.Identifier
    {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AccessionNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.ComponentModel.BindingList<string> ModalitiesInStudyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> NumberOfStudyRelatedInstancesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> NumberOfStudyRelatedSeriesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StudyDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StudyDescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StudyIdField;
        
        private string StudyInstanceUidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StudyTimeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AccessionNumber
        {
            get
            {
                return this.AccessionNumberField;
            }
            set
            {
                this.AccessionNumberField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.ComponentModel.BindingList<string> ModalitiesInStudy
        {
            get
            {
                return this.ModalitiesInStudyField;
            }
            set
            {
                this.ModalitiesInStudyField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> NumberOfStudyRelatedInstances
        {
            get
            {
                return this.NumberOfStudyRelatedInstancesField;
            }
            set
            {
                this.NumberOfStudyRelatedInstancesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> NumberOfStudyRelatedSeries
        {
            get
            {
                return this.NumberOfStudyRelatedSeriesField;
            }
            set
            {
                this.NumberOfStudyRelatedSeriesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StudyDate
        {
            get
            {
                return this.StudyDateField;
            }
            set
            {
                this.StudyDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StudyDescription
        {
            get
            {
                return this.StudyDescriptionField;
            }
            set
            {
                this.StudyDescriptionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StudyId
        {
            get
            {
                return this.StudyIdField;
            }
            set
            {
                this.StudyIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string StudyInstanceUid
        {
            get
            {
                return this.StudyInstanceUidField;
            }
            set
            {
                this.StudyInstanceUidField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StudyTime
        {
            get
            {
                return this.StudyTimeField;
            }
            set
            {
                this.StudyTimeField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.clearcanvas.ca/dicom/query")]
    [System.SerializableAttribute()]
    public partial class StudyRootStudyIdentifier : ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyIdentifier
    {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientsBirthDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientsBirthTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientsNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientsSexField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatientId
        {
            get
            {
                return this.PatientIdField;
            }
            set
            {
                this.PatientIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatientsBirthDate
        {
            get
            {
                return this.PatientsBirthDateField;
            }
            set
            {
                this.PatientsBirthDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatientsBirthTime
        {
            get
            {
                return this.PatientsBirthTimeField;
            }
            set
            {
                this.PatientsBirthTimeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatientsName
        {
            get
            {
                return this.PatientsNameField;
            }
            set
            {
                this.PatientsNameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatientsSex
        {
            get
            {
                return this.PatientsSexField;
            }
            set
            {
                this.PatientsSexField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.clearcanvas.ca/dicom/query")]
    [System.SerializableAttribute()]
    public partial class QueryFailedFault : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string DescriptionField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.clearcanvas.ca/dicom/query")]
    [System.SerializableAttribute()]
    public partial class DataValidationFault : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.clearcanvas.ca/imageViewer/studyLocator", ConfigurationName="ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudy" +
        "Locator")]
    public interface IStudyLocator
    {
        
        // CODEGEN: Generating message contract since the wrapper namespace (http://www.clearcanvas.ca/dicom/query) of message StudyQueryRequest does not match the default value (http://www.clearcanvas.ca/imageViewer/studyLocator)
        [System.ServiceModel.OperationContractAttribute(Action="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/StudyQuery", ReplyAction="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/StudyQueryResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.QueryFailedFault), Action="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/StudyQueryQueryFailedFaultF" +
            "ault", Name="QueryFailedFault", Namespace="http://www.clearcanvas.ca/dicom/query")]
        [System.ServiceModel.FaultContractAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.DataValidationFault), Action="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/StudyQueryDataValidationFau" +
            "ltFault", Name="DataValidationFault", Namespace="http://www.clearcanvas.ca/dicom/query")]
        ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyQueryResponse StudyQuery(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyQueryRequest request);
        
        // CODEGEN: Generating message contract since the wrapper namespace (http://www.clearcanvas.ca/dicom/query) of message SeriesQueryRequest does not match the default value (http://www.clearcanvas.ca/imageViewer/studyLocator)
        [System.ServiceModel.OperationContractAttribute(Action="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/SeriesQuery", ReplyAction="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/SeriesQueryResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.QueryFailedFault), Action="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/SeriesQueryQueryFailedFault" +
            "Fault", Name="QueryFailedFault", Namespace="http://www.clearcanvas.ca/dicom/query")]
        [System.ServiceModel.FaultContractAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.DataValidationFault), Action="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/SeriesQueryDataValidationFa" +
            "ultFault", Name="DataValidationFault", Namespace="http://www.clearcanvas.ca/dicom/query")]
        ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesQueryResponse SeriesQuery(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesQueryRequest request);
        
        // CODEGEN: Generating message contract since the wrapper namespace (http://www.clearcanvas.ca/dicom/query) of message ImageQueryRequest does not match the default value (http://www.clearcanvas.ca/imageViewer/studyLocator)
        [System.ServiceModel.OperationContractAttribute(Action="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/ImageQuery", ReplyAction="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/ImageQueryResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.DataValidationFault), Action="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/ImageQueryDataValidationFau" +
            "ltFault", Name="DataValidationFault", Namespace="http://www.clearcanvas.ca/dicom/query")]
        [System.ServiceModel.FaultContractAttribute(typeof(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.QueryFailedFault), Action="http://www.clearcanvas.ca/dicom/query/IStudyRootQuery/ImageQueryQueryFailedFaultF" +
            "ault", Name="QueryFailedFault", Namespace="http://www.clearcanvas.ca/dicom/query")]
        ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageQueryResponse ImageQuery(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageQueryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.clearcanvas.ca/imageViewer/studyLocator/IStudyLocator/FindByStudyInsta" +
            "nceUid", ReplyAction="http://www.clearcanvas.ca/imageViewer/studyLocator/IStudyLocator/FindByStudyInsta" +
            "nceUidResponse")]
        System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier> FindByStudyInstanceUid(System.ComponentModel.BindingList<string> studyInstanceUids);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.clearcanvas.ca/imageViewer/studyLocator/IStudyLocator/FindByAccessionN" +
            "umber", ReplyAction="http://www.clearcanvas.ca/imageViewer/studyLocator/IStudyLocator/FindByAccessionN" +
            "umberResponse")]
        System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier> FindByAccessionNumber(string accessionNumber);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="StudyQuery", WrapperNamespace="http://www.clearcanvas.ca/dicom/query", IsWrapped=true)]
    public partial class StudyQueryRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.clearcanvas.ca/dicom/query", Order=0)]
        public ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier queryCriteria;
        
        public StudyQueryRequest()
        {
        }
        
        public StudyQueryRequest(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier queryCriteria)
        {
            this.queryCriteria = queryCriteria;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="StudyQueryResponse", WrapperNamespace="http://www.clearcanvas.ca/dicom/query", IsWrapped=true)]
    public partial class StudyQueryResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.clearcanvas.ca/dicom/query", Order=0)]
        public System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier> StudyQueryResult;
        
        public StudyQueryResponse()
        {
        }
        
        public StudyQueryResponse(System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier> StudyQueryResult)
        {
            this.StudyQueryResult = StudyQueryResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SeriesQuery", WrapperNamespace="http://www.clearcanvas.ca/dicom/query", IsWrapped=true)]
    public partial class SeriesQueryRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.clearcanvas.ca/dicom/query", Order=0)]
        public ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesIdentifier queryCriteria;
        
        public SeriesQueryRequest()
        {
        }
        
        public SeriesQueryRequest(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesIdentifier queryCriteria)
        {
            this.queryCriteria = queryCriteria;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="SeriesQueryResponse", WrapperNamespace="http://www.clearcanvas.ca/dicom/query", IsWrapped=true)]
    public partial class SeriesQueryResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.clearcanvas.ca/dicom/query", Order=0)]
        public System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesIdentifier> SeriesQueryResult;
        
        public SeriesQueryResponse()
        {
        }
        
        public SeriesQueryResponse(System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesIdentifier> SeriesQueryResult)
        {
            this.SeriesQueryResult = SeriesQueryResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ImageQuery", WrapperNamespace="http://www.clearcanvas.ca/dicom/query", IsWrapped=true)]
    public partial class ImageQueryRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.clearcanvas.ca/dicom/query", Order=0)]
        public ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageIdentifier queryCriteria;
        
        public ImageQueryRequest()
        {
        }
        
        public ImageQueryRequest(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageIdentifier queryCriteria)
        {
            this.queryCriteria = queryCriteria;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ImageQueryResponse", WrapperNamespace="http://www.clearcanvas.ca/dicom/query", IsWrapped=true)]
    public partial class ImageQueryResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.clearcanvas.ca/dicom/query", Order=0)]
        public System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageIdentifier> ImageQueryResult;
        
        public ImageQueryResponse()
        {
        }
        
        public ImageQueryResponse(System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageIdentifier> ImageQueryResult)
        {
            this.ImageQueryResult = ImageQueryResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IStudyLocatorChannel : ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudyLocator, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class StudyLocatorClient : System.ServiceModel.ClientBase<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudyLocator>, ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudyLocator
    {
        
        public StudyLocatorClient()
        {
        }
        
        public StudyLocatorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
        
        public StudyLocatorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public StudyLocatorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public StudyLocatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyQueryResponse ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudyLocator.StudyQuery(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyQueryRequest request)
        {
            return base.Channel.StudyQuery(request);
        }
        
        public System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier> StudyQuery(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier queryCriteria)
        {
            ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyQueryRequest inValue = new ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyQueryRequest();
            inValue.queryCriteria = queryCriteria;
            ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyQueryResponse retVal = ((ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudyLocator)(this)).StudyQuery(inValue);
            return retVal.StudyQueryResult;
        }
        
        ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesQueryResponse ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudyLocator.SeriesQuery(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesQueryRequest request)
        {
            return base.Channel.SeriesQuery(request);
        }
        
        public System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesIdentifier> SeriesQuery(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesIdentifier queryCriteria)
        {
            ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesQueryRequest inValue = new ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesQueryRequest();
            inValue.queryCriteria = queryCriteria;
            ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.SeriesQueryResponse retVal = ((ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudyLocator)(this)).SeriesQuery(inValue);
            return retVal.SeriesQueryResult;
        }
        
        ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageQueryResponse ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudyLocator.ImageQuery(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageQueryRequest request)
        {
            return base.Channel.ImageQuery(request);
        }
        
        public System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageIdentifier> ImageQuery(ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageIdentifier queryCriteria)
        {
            ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageQueryRequest inValue = new ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageQueryRequest();
            inValue.queryCriteria = queryCriteria;
            ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.ImageQueryResponse retVal = ((ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.IStudyLocator)(this)).ImageQuery(inValue);
            return retVal.ImageQueryResult;
        }
        
        public System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier> FindByStudyInstanceUid(System.ComponentModel.BindingList<string> studyInstanceUids)
        {
            return base.Channel.FindByStudyInstanceUid(studyInstanceUids);
        }
        
        public System.ComponentModel.BindingList<ClearCanvas.ImageViewer.DesktopServices.Automation.TestClient.StudyLocator.StudyRootStudyIdentifier> FindByAccessionNumber(string accessionNumber)
        {
            return base.Channel.FindByAccessionNumber(accessionNumber);
        }
    }
}
