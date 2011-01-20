#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System.Runtime.Serialization;
using ClearCanvas.Enterprise.Common;

namespace ClearCanvas.Enterprise.Common.Admin.AuthorityGroupAdmin
{
    [DataContract]
    public class AuthorityGroupSummary : DataContractBase
    {
        public AuthorityGroupSummary(EntityRef authorityGroupRef, string name)
        {
            this.AuthorityGroupRef = authorityGroupRef;
            this.Name = name;
        }

        [DataMember]
        public EntityRef AuthorityGroupRef;

        [DataMember]
        public string Name;
    }
}
