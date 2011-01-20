#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

// This file is auto-generated by the ClearCanvas.Model.SqlServer2005.CodeGenerator project.

namespace ClearCanvas.ImageServer.Model
{
    using System;
    using System.Xml;
    using ClearCanvas.Enterprise.Core;
    using ClearCanvas.ImageServer.Enterprise;
    using ClearCanvas.ImageServer.Model.EntityBrokers;

    [Serializable]
    public partial class DevicePreferredTransferSyntax: ServerEntity
    {
        #region Constructors
        public DevicePreferredTransferSyntax():base("DevicePreferredTransferSyntax")
        {}
        public DevicePreferredTransferSyntax(
             ServerEntityKey _deviceKey_
            ,ServerEntityKey _serverSopClassKey_
            ,ServerEntityKey _serverTransferSyntaxKey_
            ):base("DevicePreferredTransferSyntax")
        {
            DeviceKey = _deviceKey_;
            ServerSopClassKey = _serverSopClassKey_;
            ServerTransferSyntaxKey = _serverTransferSyntaxKey_;
        }
        #endregion

        #region Public Properties
        [EntityFieldDatabaseMappingAttribute(TableName="DevicePreferredTransferSyntax", ColumnName="DeviceGUID")]
        public ServerEntityKey DeviceKey
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="DevicePreferredTransferSyntax", ColumnName="ServerSopClassGUID")]
        public ServerEntityKey ServerSopClassKey
        { get; set; }
        [EntityFieldDatabaseMappingAttribute(TableName="DevicePreferredTransferSyntax", ColumnName="ServerTransferSyntaxGUID")]
        public ServerEntityKey ServerTransferSyntaxKey
        { get; set; }
        #endregion

        #region Static Methods
        static public DevicePreferredTransferSyntax Load(ServerEntityKey key)
        {
            using (IReadContext read = PersistentStoreRegistry.GetDefaultStore().OpenReadContext())
            {
                return Load(read, key);
            }
        }
        static public DevicePreferredTransferSyntax Load(IPersistenceContext read, ServerEntityKey key)
        {
            IDevicePreferredTransferSyntaxEntityBroker broker = read.GetBroker<IDevicePreferredTransferSyntaxEntityBroker>();
            DevicePreferredTransferSyntax theObject = broker.Load(key);
            return theObject;
        }
        static public DevicePreferredTransferSyntax Insert(DevicePreferredTransferSyntax entity)
        {
            using (IUpdateContext update = PersistentStoreRegistry.GetDefaultStore().OpenUpdateContext(UpdateContextSyncMode.Flush))
            {
                DevicePreferredTransferSyntax newEntity = Insert(update, entity);
                update.Commit();
                return newEntity;
            }
        }
        static public DevicePreferredTransferSyntax Insert(IUpdateContext update, DevicePreferredTransferSyntax entity)
        {
            IDevicePreferredTransferSyntaxEntityBroker broker = update.GetBroker<IDevicePreferredTransferSyntaxEntityBroker>();
            DevicePreferredTransferSyntaxUpdateColumns updateColumns = new DevicePreferredTransferSyntaxUpdateColumns();
            updateColumns.DeviceKey = entity.DeviceKey;
            updateColumns.ServerSopClassKey = entity.ServerSopClassKey;
            updateColumns.ServerTransferSyntaxKey = entity.ServerTransferSyntaxKey;
            DevicePreferredTransferSyntax newEntity = broker.Insert(updateColumns);
            return newEntity;
        }
        #endregion
    }
}
