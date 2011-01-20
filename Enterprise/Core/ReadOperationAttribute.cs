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
using System.Text;

namespace ClearCanvas.Enterprise.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ReadOperationAttribute : ServiceOperationAttribute
    {
        public ReadOperationAttribute()
        {
        }

        public override PersistenceScope CreatePersistenceScope()
        {
            return new PersistenceScope(PersistenceContextType.Read, this.PersistenceScopeOption);
        }
    }
}
