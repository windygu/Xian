#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System.Collections.Generic;
using ClearCanvas.Common.Utilities;
using ClearCanvas.ImageServer.Common;

namespace ClearCanvas.ImageServer.Services.ServiceLock
{
		/// <summary>
	/// Delegate used with <see cref="ServiceLockThreadPool"/> class.
	/// </summary>
	/// <param name="processor">The ServiceLock processor.</param>
	/// <param name="queueItem">The actual ServiceLock item.</param>
	public delegate void ServiceLockThreadDelegate(IServiceLockItemProcessor processor, Model.ServiceLock queueItem);

	/// <summary>
	/// Class used to pass parameters to threads in the <see cref="ServiceLockThreadPool"/>.
	/// </summary>
	public class ServiceLockThreadParameter
	{
		private readonly IServiceLockItemProcessor _processor;
		private readonly Model.ServiceLock _item;
		private readonly ServiceLockThreadDelegate _del;

		public ServiceLockThreadParameter(IServiceLockItemProcessor processor, Model.ServiceLock item, ServiceLockThreadDelegate del)
		{
			_item = item;
			_processor = processor;
			_del = del;
		}

		public IServiceLockItemProcessor Processor
		{
			get { return _processor; }
		}

		public Model.ServiceLock Item
		{
			get { return _item; }
		}

		public ServiceLockThreadDelegate Delegate
		{
			get { return _del; }
		}
	}

	/// <summary>
	/// Thread pool for handling ServiceLock entries, which cancels in progress entries.
	/// </summary>
	public class ServiceLockThreadPool : ItemProcessingThreadPool<ServiceLockThreadParameter>
	{
		#region Private Members
		private readonly object _syncLock = new object();
		private readonly List<ServiceLockThreadParameter> _queuedItems;
		#endregion

		#region Properties
		/// <summary>
		/// Are there threads available for queueing?
		/// </summary>
		public bool CanQueueItem
		{
			get
			{
				return (QueueCount + ActiveCount) < Concurrency;
			}
		}
		#endregion

		#region Contructors
		/// <summary>
		/// Constructors.
		/// </summary>
		/// <param name="totalThreadCount">Total threads to be put in the thread pool.</param>
		public ServiceLockThreadPool(int totalThreadCount)
			: base(totalThreadCount)
		{
			_queuedItems = new List<ServiceLockThreadParameter>(totalThreadCount + 1);
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Override of OnStop method.
		/// </summary>
		/// <param name="completeBeforeStop"></param>
		/// <returns></returns>
		protected override bool OnStop(bool completeBeforeStop)
		{
			if (!base.OnStop(completeBeforeStop))
				return false;
			lock (_syncLock)
			{
				foreach (ServiceLockThreadParameter queuedItem in _queuedItems)
				{
					ICancelable cancel = queuedItem.Processor as ICancelable;
					if (cancel != null)
						cancel.Cancel();
				}
			}
			return true;
		}
		#endregion


		/// <summary>
		/// Method called when a <see cref="ServiceLock"/> item completes.
		/// </summary>
		/// <param name="queueItem">The queue item completing.</param>
		private void QueueItemComplete(Model.ServiceLock queueItem)
		{
			lock (_syncLock)
			{
				foreach (ServiceLockThreadParameter queuedItem in _queuedItems)
				{
					if (queuedItem.Item.Key.Equals(queueItem.Key))
					{
						_queuedItems.Remove(queuedItem);
						break;
					}
				}
			}
		}

		public void Enqueue(IServiceLockItemProcessor processor, Model.ServiceLock item, ServiceLockThreadDelegate del)
		{
			ServiceLockThreadParameter parameter = new ServiceLockThreadParameter(processor, item, del);

			lock (_syncLock)
			{
				_queuedItems.Add(parameter);
			}

			Enqueue(parameter, delegate(ServiceLockThreadParameter threadParameter)
									{
										threadParameter.Delegate(threadParameter.Processor, threadParameter.Item);

										QueueItemComplete(threadParameter.Item);
									});
		}
	}
}
