#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Threading;
using ClearCanvas.Common.Utilities;

namespace ClearCanvas.ImageViewer.Services.LocalDataStore
{
	public interface ILocalDataStoreEventBroker : IDisposable
	{
		event EventHandler<ItemEventArgs<SendProgressItem>> SendProgressUpdate;
		event EventHandler<ItemEventArgs<ReceiveProgressItem>> ReceiveProgressUpdate;
		event EventHandler<ItemEventArgs<ImportProgressItem>> ImportProgressUpdate;
		event EventHandler<ItemEventArgs<ReindexProgressItem>> ReindexProgressUpdate;
		event EventHandler<ItemEventArgs<ImportedSopInstanceInformation>> SopInstanceImported;
		event EventHandler<ItemEventArgs<DeletedInstanceInformation>> InstanceDeleted;
		event EventHandler LocalDataStoreCleared;
		event EventHandler LostConnection;
		event EventHandler Connected;

	}

	internal class LocalDataStoreEventBroker : ILocalDataStoreEventBroker
	{
		private readonly SynchronizationContext _synchronizationContext;
		private bool _disposed = false;
		private event EventHandler<ItemEventArgs<SendProgressItem>> _sendProgressUpdate;
		private event EventHandler<ItemEventArgs<ReceiveProgressItem>> _receiveProgressUpdate;
		private event EventHandler<ItemEventArgs<ImportProgressItem>> _importProgressUpdate;
		private event EventHandler<ItemEventArgs<ReindexProgressItem>> _reindexProgressUpdate;
		private event EventHandler<ItemEventArgs<ImportedSopInstanceInformation>> _sopInstanceImported;
		private event EventHandler<ItemEventArgs<DeletedInstanceInformation>> _instanceDeleted;
		private event EventHandler _localDataStoreCleared;
		private event EventHandler _lostConnection;
		private event EventHandler _connected;

		public LocalDataStoreEventBroker(SynchronizationContext synchronizationContext)
		{
			_synchronizationContext = synchronizationContext;
		}

		#region ILocalDataStoreActivityMonitorProxy Members

		public event EventHandler<ItemEventArgs<SendProgressItem>> SendProgressUpdate
		{
			add
			{
				CheckIsDisposed();
				if (_sendProgressUpdate == null)
					LocalDataStoreActivityMonitor.Instance.SendProgressUpdate += OnSendProgressUpdate;

				_sendProgressUpdate += value;
			}
			remove
			{
				CheckIsDisposed();
				_sendProgressUpdate -= value;

				if (_sendProgressUpdate == null)
					LocalDataStoreActivityMonitor.Instance.SendProgressUpdate -= OnSendProgressUpdate;
			}
		}

		public event EventHandler<ItemEventArgs<ReceiveProgressItem>> ReceiveProgressUpdate
		{
			add
			{
				CheckIsDisposed();
				if (_receiveProgressUpdate == null)
					LocalDataStoreActivityMonitor.Instance.ReceiveProgressUpdate += OnReceiveProgressUpdate;

				_receiveProgressUpdate += value;
			}
			remove
			{
				CheckIsDisposed();
				_receiveProgressUpdate -= value;

				if (_receiveProgressUpdate == null)
					LocalDataStoreActivityMonitor.Instance.ReceiveProgressUpdate -= OnReceiveProgressUpdate;
			}
		}

		public event EventHandler<ItemEventArgs<ImportProgressItem>> ImportProgressUpdate
		{
			add
			{
				CheckIsDisposed();
				if (_receiveProgressUpdate == null)
					LocalDataStoreActivityMonitor.Instance.ImportProgressUpdate += OnImportProgressUpdate;

				_importProgressUpdate += value;
			}
			remove
			{
				CheckIsDisposed();
				_importProgressUpdate -= value;

				if (_importProgressUpdate == null)
					LocalDataStoreActivityMonitor.Instance.ImportProgressUpdate -= OnImportProgressUpdate;
			}
		}

		public event EventHandler<ItemEventArgs<ReindexProgressItem>> ReindexProgressUpdate
		{
			add
			{
				CheckIsDisposed();
				if (_reindexProgressUpdate == null)
					LocalDataStoreActivityMonitor.Instance.ReindexProgressUpdate += OnReindexProgressUpdate;

				_reindexProgressUpdate += value;
			}
			remove
			{
				CheckIsDisposed();
				_reindexProgressUpdate -= value;

				if (_reindexProgressUpdate == null)
					LocalDataStoreActivityMonitor.Instance.ReindexProgressUpdate -= OnReindexProgressUpdate;
			}
		}

		public event EventHandler<ItemEventArgs<ImportedSopInstanceInformation>> SopInstanceImported
		{
			add
			{
				CheckIsDisposed();
				if (_sopInstanceImported == null)
					LocalDataStoreActivityMonitor.Instance.SopInstanceImported += OnSopInstanceImported;

				_sopInstanceImported += value;
			}
			remove
			{
				CheckIsDisposed();
				_sopInstanceImported -= value;

				if (_sopInstanceImported == null)
					LocalDataStoreActivityMonitor.Instance.SopInstanceImported -= OnSopInstanceImported;
			}
		}

		public event EventHandler<ItemEventArgs<DeletedInstanceInformation>> InstanceDeleted
		{
			add
			{
				CheckIsDisposed();
				if (_instanceDeleted == null)
					LocalDataStoreActivityMonitor.Instance.InstanceDeleted += OnInstanceDeleted;

				_instanceDeleted += value;
			}
			remove
			{
				CheckIsDisposed();
				_instanceDeleted -= value;

				if (_instanceDeleted == null)
					LocalDataStoreActivityMonitor.Instance.InstanceDeleted -= OnInstanceDeleted;
			}
		}

		public event EventHandler LocalDataStoreCleared
		{
			add
			{
				CheckIsDisposed();
				if (_localDataStoreCleared == null)
					LocalDataStoreActivityMonitor.Instance.LocalDataStoreCleared += OnLocalDataStoreCleared;

				_localDataStoreCleared += value;
			}
			remove
			{
				CheckIsDisposed();
				_localDataStoreCleared -= value;

				if (_localDataStoreCleared == null)
					LocalDataStoreActivityMonitor.Instance.LocalDataStoreCleared -= OnLocalDataStoreCleared;
			}
		}

		public event EventHandler LostConnection
		{
			add
			{
				CheckIsDisposed();
				if (_lostConnection == null)
					LocalDataStoreActivityMonitor.Instance.LostConnection += OnLostConnection;

				_lostConnection += value;
			}
			remove
			{
				CheckIsDisposed();
				_lostConnection -= value;

				if (_lostConnection == null)
					LocalDataStoreActivityMonitor.Instance.LostConnection -= OnLostConnection;
			}
		}

		public event EventHandler Connected
		{
			add
			{
				CheckIsDisposed();
				if (_connected == null)
					LocalDataStoreActivityMonitor.Instance.Connected += OnConnected;

				_connected += value;
			}
			remove
			{
				CheckIsDisposed();
				_connected -= value;

				if (_connected == null)
					LocalDataStoreActivityMonitor.Instance.Connected -= OnConnected;
			}
		}

		#endregion

		#region Private Methods

		private void OnSendProgressUpdate(object sender, ItemEventArgs<SendProgressItem> e)
		{
			FireEvent(_sendProgressUpdate, e);
		}

		private void OnReceiveProgressUpdate(object sender, ItemEventArgs<ReceiveProgressItem> e)
		{
			FireEvent(_receiveProgressUpdate, e);
		}

		private void OnImportProgressUpdate(object sender, ItemEventArgs<ImportProgressItem> e)
		{
			FireEvent(_importProgressUpdate, e);
		}

		private void OnReindexProgressUpdate(object sender, ItemEventArgs<ReindexProgressItem> e)
		{
			FireEvent(_reindexProgressUpdate, e);
		}

		private void OnSopInstanceImported(object sender, ItemEventArgs<ImportedSopInstanceInformation> e)
		{
			FireEvent(_sopInstanceImported, e);
		}

		private void OnInstanceDeleted(object sender, ItemEventArgs<DeletedInstanceInformation> e)
		{
			FireEvent(_instanceDeleted, e);
		}

		private void OnLocalDataStoreCleared(object sender, EventArgs e)
		{
			FireEvent(_localDataStoreCleared, e);
		}

		private void OnLostConnection(object sender, EventArgs e)
		{
			FireEvent(_lostConnection, e);
		}

		private void OnConnected(object sender, EventArgs e)
		{
			FireEvent(_connected, e);
		}

		private void FireEvent(Delegate del, EventArgs e)
		{
			if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
			{
				_synchronizationContext.Post(delegate { EventsHelper.Fire(del, this, e); }, null);
			}
			else
			{
				EventsHelper.Fire(del, this, e);
			}
		}

		private void UnsubscribeAll()
		{
			LocalDataStoreActivityMonitor.Instance.SendProgressUpdate -= OnSendProgressUpdate;
			LocalDataStoreActivityMonitor.Instance.ReceiveProgressUpdate -= OnReceiveProgressUpdate;
			LocalDataStoreActivityMonitor.Instance.ImportProgressUpdate -= OnImportProgressUpdate;
			LocalDataStoreActivityMonitor.Instance.ReindexProgressUpdate -= OnReindexProgressUpdate;
			LocalDataStoreActivityMonitor.Instance.SopInstanceImported -= OnSopInstanceImported;
			LocalDataStoreActivityMonitor.Instance.InstanceDeleted -= OnInstanceDeleted;
			LocalDataStoreActivityMonitor.Instance.LocalDataStoreCleared -= OnLocalDataStoreCleared;
			LocalDataStoreActivityMonitor.Instance.LostConnection -= OnLostConnection;
			LocalDataStoreActivityMonitor.Instance.Connected -= OnConnected;
		}

		private void CheckIsDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException("The object has already been disposed.");
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (!_disposed)
			{
				_disposed = true;
				UnsubscribeAll();
			}
		}

		#endregion
	}
}
