#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.ServiceModel;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Enterprise.Common;
using Castle.Core.Interceptor;
using System.Reflection;

namespace ClearCanvas.Enterprise.Core
{
	/// <summary>
	/// Promotes certain types of exceptions to WCF faults (<see cref="FaultException{TDetail}"/>).
	/// </summary>
	public class ExceptionPromotionAdvice : IInterceptor
	{
		#region IMethodInterceptor Members

		public void Intercept(IInvocation invocation)
		{
			try
			{
				invocation.Proceed();
			}
			catch (Exception e)
			{
				var ex = e;

				// perform any necessary exception translation
				var isTranslated = TranslateException(ref ex);

				// try promoting the exception to a fault - if successful, throw the fault exception
				Exception fault;
				if (PromoteExceptionToFault(ex, invocation.InvocationTarget, invocation.Method, out fault))
				{
					throw fault;
				}

				// could not promote to fault
				// if a translation occured, throw the translated exception
				if(isTranslated)
					throw ex;

				// rethrow the original exception
				throw;
			}
		}

		#endregion

		/// <summary>
		/// Translate certain "special" classes of exception to classes that can be passed through the service boundary.
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		private static bool TranslateException(ref Exception e)
		{
			// special handling of EntityVersionException
			// assume all such exceptions occured because of concurrent modifications
			// wrap in ConcurrentModificationException will be used in the fault contract
			if (e is EntityVersionException)
			{
				e = new ConcurrentModificationException(e.Message);
				return true;
			}

			// special handling of EntityValidationException
			// convert to RequestValidationException
			if (e is EntityValidationException)
			{
				e = new RequestValidationException(e.Message);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Promotes the exception to a fault, if the exception type has a corresponding fault contract.
		/// Otherwise returns null.
		/// </summary>
		/// <param name="e"></param>
		/// <param name="serviceInstance"></param>
		/// <param name="method"></param>
		/// <param name="fault"></param>
		/// <returns></returns>
		private static bool PromoteExceptionToFault(Exception e, object serviceInstance, MethodInfo method, out Exception fault)
		{
			fault = null;

			// get the service contract
			var serviceContractAttr = AttributeUtils.GetAttribute<ServiceImplementsContractAttribute>(serviceInstance.GetType(), false);

			// this should never happen, but if it does, there's nothing we can do
			if (serviceContractAttr == null)
				return false;

			// find a fault contract for this exception type
			var faultContract = AttributeUtils.GetAttribute<FaultContractAttribute>(method, true, a => a.DetailType.Equals(e.GetType()));
			if (faultContract != null)
			{
				// from Juval Lowey
				var faultBoundedType = typeof(FaultException<>).MakeGenericType(e.GetType());
				fault = (FaultException)Activator.CreateInstance(faultBoundedType, e, new FaultReason(e.Message));
				return true;
			}

			return false;    // no fault contract for this exception
		}
	}
}
