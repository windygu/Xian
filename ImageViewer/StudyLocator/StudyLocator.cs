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
using ClearCanvas.Dicom.ServiceModel.Query;
using ClearCanvas.ImageViewer.Configuration;
using ClearCanvas.ImageViewer.Services.ServerTree;
using System.ServiceModel;
using ClearCanvas.Common;
using ClearCanvas.Common.Utilities;

namespace ClearCanvas.ImageViewer.StudyLocator
{
	//This stuff is temporary until we can fix the server tree.  Really need to do that soon.

	public sealed class LocalStudyRootQueryExtensionPoint : ExtensionPoint<IStudyRootQuery>
	{
	}

	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, UseSynchronizationContext = false, ConfigurationName = "StudyLocator", Namespace = QueryNamespace.Value)]
	public class StudyLocator : IStudyRootQuery
	{
		private delegate IList<T> QueryDelegate<T>(T criteria, IStudyRootQuery query) where T : Identifier;
		private delegate string GetUidDelegate<T>(T identifier) where T : Identifier;

		private class GenericQuery<T> where T : Identifier, new()
		{
			private readonly QueryDelegate<T> _query;
			private readonly GetUidDelegate<T> _getUid;

			public GenericQuery(QueryDelegate<T> query, GetUidDelegate<T> getUid)
			{
				_query = query;
				_getUid = getUid;
			}

			public IList<T> Query(T queryCriteria)
			{
				if (queryCriteria == null)
				{
					string message = "The argument cannot be null.";
					Platform.Log(LogLevel.Error, message);
					throw new FaultException(message);
				}

				Dictionary<string, T> combinedResults = new Dictionary<string, T>();

				try
				{
					foreach (IStudyRootQuery query in GetQueryInterfaces())
					{
						try
						{
							IList<T> results = _query(queryCriteria, query);
							foreach (T result in results)
							{
								string uid = _getUid(result);
								if (!combinedResults.ContainsKey(uid))
									combinedResults[uid] = result;
							}
						}
						catch (Exception e)
						{
							QueryFailedFault fault = new QueryFailedFault();
							fault.Description = String.Format("Failed to query server {0}.", query);
							Platform.Log(LogLevel.Error, e, fault.Description);
							throw new FaultException<QueryFailedFault>(fault, fault.Description);
						}
						finally
						{
							if (query is IDisposable)
								(query as IDisposable).Dispose();
						}
					}
				}
				catch (FaultException)
				{
					throw;
				}
				catch (Exception e)
				{
					QueryFailedFault fault = new QueryFailedFault();
					fault.Description = String.Format("An unexpected error has occurred.");
					Platform.Log(LogLevel.Error, e, fault.Description);
					throw new FaultException<QueryFailedFault>(fault, fault.Description);
				}

				return new List<T>(combinedResults.Values);
			}
		}

		public StudyLocator()
		{
		}

		#region IStudyRootQuery Members

		public IList<StudyRootStudyIdentifier> StudyQuery(StudyRootStudyIdentifier queryCriteria)
		{
			QueryDelegate<StudyRootStudyIdentifier> query =
				delegate(StudyRootStudyIdentifier criteria, IStudyRootQuery studyRootQuery)
					{
						return studyRootQuery.StudyQuery(criteria);
					};

			GetUidDelegate<StudyRootStudyIdentifier> getUid =
				delegate(StudyRootStudyIdentifier result)
					{
						return result.StudyInstanceUid;
					};

			return new GenericQuery<StudyRootStudyIdentifier>(query, getUid).Query(queryCriteria);
		}

		public IList<SeriesIdentifier> SeriesQuery(SeriesIdentifier queryCriteria)
		{
			QueryDelegate<SeriesIdentifier> query =
				delegate(SeriesIdentifier criteria, IStudyRootQuery studyRootQuery)
					{
						return studyRootQuery.SeriesQuery(criteria);
					};

			GetUidDelegate<SeriesIdentifier> getUid =
				delegate(SeriesIdentifier result)
					{
						return result.SeriesInstanceUid;
					};

			return new GenericQuery<SeriesIdentifier>(query, getUid).Query(queryCriteria);
		}

		public IList<ImageIdentifier> ImageQuery(ImageIdentifier queryCriteria)
		{
			QueryDelegate<ImageIdentifier> query =
				delegate(ImageIdentifier criteria, IStudyRootQuery studyRootQuery)
					{
						return studyRootQuery.ImageQuery(criteria);
					};

			GetUidDelegate<ImageIdentifier> getUid =
				delegate(ImageIdentifier result)
					{
						return result.SopInstanceUid;
					};

			return new GenericQuery<ImageIdentifier>(query, getUid).Query(queryCriteria);
		}

		#endregion

		private static IEnumerable<IStudyRootQuery> GetQueryInterfaces()
		{
			IStudyRootQuery localDataStoreQuery;
			try
			{
				localDataStoreQuery = (IStudyRootQuery)new LocalStudyRootQueryExtensionPoint().CreateExtension();
			}
			catch(NotSupportedException)
			{
				localDataStoreQuery = null;
			}

			if (localDataStoreQuery != null)
				yield return localDataStoreQuery;

			string localAE = ServerTree.GetClientAETitle();

			List<Server> defaultServers = DefaultServers.SelectFrom(new ServerTree());
			List<Server> streamingServers = CollectionUtils.Select(defaultServers, 
				delegate(Server server) { return server.IsStreaming; });

			List<Server> nonStreamingServers = CollectionUtils.Select(defaultServers,
				delegate(Server server) { return !server.IsStreaming; });

			foreach (Server server in streamingServers)
			{
				DicomStudyRootQuery remoteQuery = new DicomStudyRootQuery(localAE, server.AETitle, server.Host, server.Port);
				yield return remoteQuery;
			}

			foreach (Server server in nonStreamingServers)
			{
				DicomStudyRootQuery remoteQuery = new DicomStudyRootQuery(localAE, server.AETitle, server.Host, server.Port);
				yield return remoteQuery;
			}
		}
	}
}
