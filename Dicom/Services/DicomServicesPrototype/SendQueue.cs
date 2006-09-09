using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Network;
using NHibernate;

namespace ClearCanvas.Dicom.Services
{
    public class SendQueue : ISendQueueService
    {
        #region Handcoded Members
        public SendQueue(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        #region Private Members
        private ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }

        private readonly ISessionFactory _sessionFactory;
        #endregion
        #endregion        

        #region ISendQueueService Members
        public void Add(IParcel aParcel)
        {
            ISession session = null;
            ITransaction tx = null;
            try
            {
                session = this.SessionFactory.OpenSession();

                tx = session.BeginTransaction();
                session.SaveOrUpdate(aParcel);
                tx.Commit();
                session.Close();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }
            finally
            {
                session.Close();
            }
        }

        public void Remove(IParcel aParcel)
        {
            ISession session = null;
            ITransaction tx = null;
            try
            {
                session = this.SessionFactory.OpenSession();

                tx = session.BeginTransaction();
                session.Delete(aParcel);
                tx.Commit();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }
            finally
            {
                session.Close();
            }
        }

        public IParcel CreateNewParcel(ApplicationEntity sourceAE, ApplicationEntity destinationAE)
        {
            return new Parcel(sourceAE, destinationAE);
        }

        public IEnumerable<IParcel> GetParcels()
        {
            IList listOfParcels;
            List<IParcel> returningParcels = new List<IParcel>();
            ISession session = null;

            try
            {
                session = this.SessionFactory.OpenSession();
                listOfParcels = session.Find("from Parcel");
                if (listOfParcels.Count <= 0)
                    return null;
            }
            catch { throw; }
            finally { session.Close(); }

            foreach (IParcel parcel in listOfParcels)
            {
                returningParcels.Add(parcel);
            }
            
            return returningParcels;
        }

        public IEnumerable<IParcel> GetSendIncompleteParcels()
        {
            IList listOfParcels;
            List<IParcel> returningParcels = new List<IParcel>();
            ISession session = null;

            try
            {
                session = this.SessionFactory.OpenSession();
                listOfParcels = session.Find("from Parcel as parcel where parcel.ParcelTransferState != ?",
                    ParcelTransferState.Completed,
                    NHibernateUtil.Int16);
                if (listOfParcels.Count <= 0)
                    return null;
            }
            catch { throw; }
            finally { session.Close(); }

            foreach (IParcel parcel in listOfParcels)
            {
                returningParcels.Add(parcel);
            }

            return returningParcels;
        }

        public void UpdateParcel(IParcel aParcel)
        {
            ISession session = null;
            ITransaction tx = null;
            try
            {
                session = this.SessionFactory.OpenSession();
                tx = session.BeginTransaction();
                session.Lock(aParcel, LockMode.Read);
                session.Update(aParcel);
                tx.Commit();
            }
            catch 
            {
                tx.Rollback();
                throw; 
            }
            finally { session.Close(); }
        }

        public void LoadAllReferences(IParcel aParcel)
        {
            ISession session = null;
            ITransaction tx = null;
            try
            {
                session = this.SessionFactory.OpenSession();
                tx = session.BeginTransaction();
                session.Lock(aParcel, LockMode.Read);
                NHibernateUtil.Initialize(aParcel);
                aParcel.GetReferencedSopInstanceFileNames();
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
            finally { session.Close(); }
        }

        #endregion
    }
}
