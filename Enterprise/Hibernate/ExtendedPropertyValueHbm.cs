﻿#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice, 
//      this list of conditions and the following disclaimer.
//    * Redistributions in binary form must reproduce the above copyright notice, 
//      this list of conditions and the following disclaimer in the documentation 
//      and/or other materials provided with the distribution.
//    * Neither the name of ClearCanvas Inc. nor the names of its contributors 
//      may be used to endorse or promote products derived from this software without 
//      specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR 
// PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
// OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE 
// GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) 
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
// OF SUCH DAMAGE.

#endregion

using System;
using NHibernate;
using NHibernate.Engine;
using NHibernate.UserTypes;
using ClearCanvas.Enterprise.Core;

namespace ClearCanvas.Enterprise.Hibernate
{
	/// <summary>
	/// Provides an NHibernate mapping of the <see cref="ExtendedPropertyValue"/> class.
	/// </summary>
	public class ExtendedPropertyValueHbm : ICompositeUserType
	{
		#region ICompositeUserType Members

		public object Assemble(object cached, ISessionImplementor session, object owner)
		{
			return DeepCopy(cached);
		}

		public object Replace(object original, object target, ISessionImplementor session, object owner)
		{
			return DeepCopy(original);
		}

		public object Disassemble(object value, ISessionImplementor session)
		{
			return DeepCopy(value);
		}

		public object DeepCopy(object value)
		{
			if (value == null)
				return null;

			var original = (ExtendedPropertyValue)value;  // throws InvalidCast... if wrong type of object
			return original.Clone();
		}

		public new bool Equals(object x, object y)
		{
			return object.Equals(x, y);
		}

		public int GetHashCode(object x)
		{
			return x.GetHashCode();
		}

		public string[] PropertyNames
		{
			get
			{
				return new[] { "Value", "SmallValue" };
			}
		}

		public NHibernate.Type.IType[] PropertyTypes
		{
			get
			{
				return new NHibernate.Type.IType[] { NHibernateUtil.StringClob, NHibernateUtil.String };
			}
		}

		public Type ReturnedClass
		{
			get { return typeof(ExtendedPropertyValue); }
		}

		public object GetPropertyValue(object component, int property)
		{
			switch (property)
			{
				case 0:
					return ((ExtendedPropertyValue)component).Value;
				case 1:
					return ((ExtendedPropertyValue)component).SmallValue;
			}
			return null;
		}

		public void SetPropertyValue(object component, int property, object value)
		{
			//switch (property)
			//{
			//    case 0:
			//        ((ExtendedPropertyValue)component).Value = (string)value;
			//        break;
			//    case 1:
			//        ((ExtendedPropertyValue)component).SmallValue = (string)value;
			//        break;
			//}
			throw new NotSupportedException();
		}

		public bool IsMutable
		{
			get { return false; }
		}

		public object NullSafeGet(System.Data.IDataReader dr, string[] names, ISessionImplementor session, object owner)
		{
			var value = (string)NHibernateUtil.StringClob.NullSafeGet(dr, names[0], session, owner);
			//var smallValue = (string)NHibernateUtil.String.NullSafeGet(dr, names[1], session, owner);

			return new ExtendedPropertyValue(value);
		}

		public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index, ISessionImplementor session)
		{
			var xpv = (ExtendedPropertyValue)value;

			NHibernateUtil.StringClob.NullSafeSet(cmd, xpv == null ? null : xpv.Value, index, session);
			NHibernateUtil.String.NullSafeSet(cmd, xpv == null ? null : xpv.SmallValue, index + 1, session);
		}


		#endregion
	}
}
