#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

// This file is auto-generated by the ClearCanvas.Model.SqlServer2005.CodeGenerator project.

namespace ClearCanvas.ImageServer.Model.EntityBrokers
{
    using System;
    using System.Xml;
    using ClearCanvas.Enterprise.Core;
    using ClearCanvas.ImageServer.Enterprise;

    public partial class ArchiveStudyStorageSelectCriteria : EntitySelectCriteria
    {
        public ArchiveStudyStorageSelectCriteria()
        : base("ArchiveStudyStorage")
        {}
        public ArchiveStudyStorageSelectCriteria(ArchiveStudyStorageSelectCriteria other)
        : base(other)
        {}
        public override object Clone()
        {
            return new ArchiveStudyStorageSelectCriteria(this);
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ArchiveStudyStorage", ColumnName="PartitionArchiveGUID")]
        public ISearchCondition<ServerEntityKey> PartitionArchiveKey
        {
            get
            {
              if (!SubCriteria.ContainsKey("PartitionArchiveKey"))
              {
                 SubCriteria["PartitionArchiveKey"] = new SearchCondition<ServerEntityKey>("PartitionArchiveKey");
              }
              return (ISearchCondition<ServerEntityKey>)SubCriteria["PartitionArchiveKey"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ArchiveStudyStorage", ColumnName="StudyStorageGUID")]
        public ISearchCondition<ServerEntityKey> StudyStorageKey
        {
            get
            {
              if (!SubCriteria.ContainsKey("StudyStorageKey"))
              {
                 SubCriteria["StudyStorageKey"] = new SearchCondition<ServerEntityKey>("StudyStorageKey");
              }
              return (ISearchCondition<ServerEntityKey>)SubCriteria["StudyStorageKey"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ArchiveStudyStorage", ColumnName="ServerTransferSyntaxGUID")]
        public ISearchCondition<ServerEntityKey> ServerTransferSyntaxKey
        {
            get
            {
              if (!SubCriteria.ContainsKey("ServerTransferSyntaxKey"))
              {
                 SubCriteria["ServerTransferSyntaxKey"] = new SearchCondition<ServerEntityKey>("ServerTransferSyntaxKey");
              }
              return (ISearchCondition<ServerEntityKey>)SubCriteria["ServerTransferSyntaxKey"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ArchiveStudyStorage", ColumnName="ArchiveTime")]
        public ISearchCondition<DateTime> ArchiveTime
        {
            get
            {
              if (!SubCriteria.ContainsKey("ArchiveTime"))
              {
                 SubCriteria["ArchiveTime"] = new SearchCondition<DateTime>("ArchiveTime");
              }
              return (ISearchCondition<DateTime>)SubCriteria["ArchiveTime"];
            } 
        }
        [EntityFieldDatabaseMappingAttribute(TableName="ArchiveStudyStorage", ColumnName="ArchiveXml")]
        public ISearchCondition<XmlDocument> ArchiveXml
        {
            get
            {
              if (!SubCriteria.ContainsKey("ArchiveXml"))
              {
                 SubCriteria["ArchiveXml"] = new SearchCondition<XmlDocument>("ArchiveXml");
              }
              return (ISearchCondition<XmlDocument>)SubCriteria["ArchiveXml"];
            } 
        }
    }
}
