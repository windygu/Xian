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
using System.Diagnostics;
using ClearCanvas.Common;
using ClearCanvas.Dicom.Iod;

namespace ClearCanvas.ImageViewer.StudyManagement
{
	internal interface ISopDataCacheItemReference : IDisposable
	{
		ISopDataSource RealDataSource { get; }
		IList<VoiDataLut> VoiDataLuts { get; }
		ISopDataCacheItemReference Clone();
	}

	internal static class SopDataCache
	{
		#region Cache Item

		private class Item
		{
			private readonly object _syncLock = new object();
			private readonly ISopDataSource _realDataSource;
			private volatile IList<VoiDataLut> _sopVoiDataLuts;
			private volatile int _referenceCount = 0;

			public Item(ISopDataSource realDataSource)
			{
				_realDataSource = realDataSource;
			}

			~Item()
			{
				//the only way this object will get finalized is when all the frames
				//referencing it are dead themselves.  So, we set a boolean to indicate
				//that we know there are dead WeakReference objects to clean up.
				if (_referenceCount > -1)
					_containsDeadItems = true;
			}

			public ISopDataSource RealDataSource
			{
				get { return _realDataSource; }	
			}

			public IList<VoiDataLut> VoiDataLuts
			{
				get
				{
					if (_sopVoiDataLuts == null)
					{
						lock (_syncLock)
						{
							if (_sopVoiDataLuts == null)
							{
								try
								{
									_sopVoiDataLuts = VoiDataLut.Create(_realDataSource).AsReadOnly();
								}
								catch (Exception ex)
								{
									Platform.Log(LogLevel.Warn, ex, SR.MessageFailedToGetVOIDataLUTs);
									_sopVoiDataLuts = new List<VoiDataLut>().AsReadOnly();
								}
							}
						}
					}

					return _sopVoiDataLuts;
				}
			}

			public void OnReferenceCreated()
			{
				lock (_syncLock)
				{
					if (_referenceCount < 0)
						throw new ObjectDisposedException("The underlying sop data source has been disposed.");

					++_referenceCount;
				}
			}

			public void OnReferenceDisposed()
			{
				string removeSopInstanceUid = null;

				lock (_syncLock)
				{
					if (_referenceCount > 0)
						--_referenceCount;

					if (_referenceCount == 0)
					{
						_referenceCount = -1;
						GC.SuppressFinalize(this);

						removeSopInstanceUid = _realDataSource.SopInstanceUid;

						try
						{
							_realDataSource.Dispose();
						}
						catch(Exception e)
						{
							Platform.Log(LogLevel.Debug, e);
						}
					}
				}

				if (removeSopInstanceUid != null)
					Remove(removeSopInstanceUid);
			}
		}

		#endregion
		
		#region Cached Data Source

		private class ItemReference : ISopDataCacheItemReference
		{
			private Item _item;

			public ItemReference(Item item)
			{
				_item = item;
				_item.OnReferenceCreated();
			}

			#region ICachedSopDataSource Members

			public ISopDataSource RealDataSource
			{
				get { return _item.RealDataSource; }
			}

			public IList<VoiDataLut> VoiDataLuts
			{
				get { return _item.VoiDataLuts; }
			}

			public ISopDataCacheItemReference Clone()
			{
				return new ItemReference(_item);
			}

			#endregion

			#region IDisposable Members

			public void Dispose()
			{
				if (_item != null)
				{
					_item.OnReferenceDisposed();
					_item = null;
				}
			}

			#endregion
		}
		
		#endregion

		private static readonly object _itemsLock = new object();
		private static readonly Dictionary<string, WeakReference> _items = new Dictionary<string, WeakReference>();
		private static volatile bool _containsDeadItems = false;
		
#if UNIT_TESTS

		internal static int ItemCount
		{
			get { return _items.Count; }
		}

#endif
		public static ISopDataCacheItemReference Add(ISopDataSource dataSource)
		{
			lock(_itemsLock)
			{
				CleanupDeadItems();

				WeakReference weakReference = null;
				Item item = null;

				if (_items.ContainsKey(dataSource.SopInstanceUid))
				{
					weakReference = _items[dataSource.SopInstanceUid];
					try
					{
						item = weakReference.Target as Item;
					}
					catch (InvalidOperationException)
					{
						weakReference = null;
						item = null;
					}
				}

				if (weakReference == null)
				{
					weakReference = new WeakReference(null);
					_items[dataSource.SopInstanceUid] = weakReference;
				}


				if (item != null && weakReference.IsAlive)
				{
					if (!ReferenceEquals(item.RealDataSource, dataSource))
						dataSource.Dispose(); //silently dispose the new one, we already have it.
				}
				else
				{
					item = new Item(dataSource);
					weakReference.Target = item;
				}

				return new ItemReference(item);
			}
		}

		private static void Remove(string sopInstanceUid)
		{
			lock (_itemsLock)
			{
				_items.Remove(sopInstanceUid);

				if (_items.Count == 0)
					Trace.WriteLine("The sop data cache is empty.");
			}
		}

		private static void CleanupDeadItems()
		{
			if (!_containsDeadItems)
				return;

			_containsDeadItems = false;

			// Note that the only way we should even get to here is if some Frames that were
			// allocated didn't get disposed, which should never happen.  This is a failsafe
			// to ensure that the dictionary doesn't get ridiculous.

			List<string> deadObjectKeys = new List<string>();

			foreach (KeyValuePair<string, WeakReference> pair in _items)
			{
				try
				{
					if (!pair.Value.IsAlive || pair.Value.Target == null)
						deadObjectKeys.Add(pair.Key);
				}
				catch (InvalidOperationException)
				{
					deadObjectKeys.Add(pair.Key);
				}
			}

			foreach (string deadObjectKey in deadObjectKeys)
				_items.Remove(deadObjectKey);

			if (_items.Count == 0)
				Trace.WriteLine("The sop data cache is empty.");
		}
	}
}
