using System;
using System.Collections.Generic;
using System.Text;

using ClearCanvas.Common;
using ClearCanvas.Enterprise.Core;
using ClearCanvas.HL7;
using ClearCanvas.HL7.Brokers;
using ClearCanvas.HL7.PreProcessing;
using ClearCanvas.HL7.Processing;
using ClearCanvas.Healthcare;
using ClearCanvas.Healthcare.Brokers;
using ClearCanvas.Enterprise.Common;
using ClearCanvas.Ris.Application.Common.Admin.HL7Admin;
using ClearCanvas.Common.Utilities;
using ClearCanvas.Ris.Application.Common.Admin;

namespace ClearCanvas.Ris.Application.Services.Admin.HL7Admin
{
    [ExtensionOf(typeof(ApplicationServiceExtensionPoint))]
    public class HL7QueueService : ApplicationServiceBase, IHL7QueueService
    {
        //TODO:  The following three service methods need a new home

        #region To Be Refactored

        //[UpdateOperation]
        //public void EnqueueHL7QueueItem(ClearCanvas.HL7.HL7QueueItem item)
        //{
        //    this.CurrentContext.Lock(item, DirtyState.New);
        //}

        ////[UpdateOperation(PersistenceScopeOption = PersistenceScopeOption.Required)]
        //public string GetAccessionNumber()
        //{
        //    IAccessionNumberBroker broker = this.CurrentContext.GetBroker<IAccessionNumberBroker>();
        //    return broker.GetNextAccessionNumber();
        //}

        ////[ReadOperation(PersistenceScopeOption = PersistenceScopeOption.Required)]
        //public PractitionerAdmin FindPractitioner(string id)
        //{
        //    IPractitionerBroker broker = this.CurrentContext.GetBroker<IPractitionerBroker>();

        //    PractitionerSearchCriteria criteria = new PractitionerSearchCriteria();
        //    criteria.LicenseNumber.EqualTo(id);

        //    IList<PractitionerAdmin> results = broker.Find(criteria);
        //    if (results.Count == 0)
        //    {
        //        return null;
        //    }
        //    else if (results.Count == 1)
        //    {
        //        return results[0];
        //    }
        //    else
        //    {
        //        throw new Exception("Multiple practioners");
        //    }
        //}

        #endregion
        
        #region IHL7QueueService Members

        [ReadOperation]
        public GetHL7QueueFormDataResponse GetHL7QueueFormData(GetHL7QueueFormDataRequest request)
        {
            GetHL7QueueFormDataResponse response = new GetHL7QueueFormDataResponse();

            response.DirectionChoices = PersistenceContext.GetBroker<IHL7MessageDirectionEnumBroker>().Load().Values;
            response.StatusCodeChoices = PersistenceContext.GetBroker<IHL7MessageStatusCodeEnumBroker>().Load().Values;
            response.MessageTypeChoices = new string[]
                { "ADT_A01", "ADT_A02", "ADT_A03", "ADT_A04", "ADT_A05", "ADT_A06", "ADT_A07", "ADT_A08", "ADT_A09", "ADT_A10",
                /*"ADT_A11", "ADT_A12", "ADT_A13", "ADT_A14", "ADT_A15", "ADT_A16", "ADT_A17", "ADT_A18", "ADT_A19", "ADT_A20",
                  "ADT_A21", "ADT_A22", "ADT_A23", "ADT_A24", "ADT_A25", "ADT_A26", "ADT_A27", "ADT_A28", "ADT_A29", "ADT_A30",*/
                  "ORM_O01"};
            response.MessageFormatChoices = PersistenceContext.GetBroker<IHL7MessageFormatEnumBroker>().Load().Values;
            response.MessageVersionChoices = PersistenceContext.GetBroker<IHL7MessageVersionEnumBroker>().Load().Values;
            response.PeerChoices = PersistenceContext.GetBroker<IHL7MessagePeerEnumBroker>().Load().Values;

            return response;
        }

        [ReadOperation]
        public ListHL7QueueItemsResponse ListHL7QueueItems(ListHL7QueueItemsRequest request)
        {
            HL7QueueItemAssembler assembler = new HL7QueueItemAssembler();
            HL7QueueItemSearchCriteria criteria = assembler.CreateHL7QueueItemSearchCriteria(request, PersistenceContext);

            return new ListHL7QueueItemsResponse(
                CollectionUtils.Map<HL7QueueItem, HL7QueueItemSummary, List<HL7QueueItemSummary>>(
                    PersistanceContext.GetBroker<IHL7QueueItemBroker>().Find(criteria),
                    delegate(HL7QueueItem queueItem)
                    {
                        return assembler.CreateHL7QueueItemSummary(queueItem, PersistenceContext);
                    }));
        }

        [ReadOperation]
        public LoadHL7QueueItemResponse LoadHL7QueueItem(LoadHL7QueueItemRequest request)
        {
            HL7QueueItem queueItem = (HL7QueueItem)PersistenceContext.Load(request.QueueItemRef);
            HL7QueueItemAssembler assembler = new HL7QueueItemAssembler();
            
            return new LoadHL7QueueItemResponse(
                queueItem.GetRef(),
                assembler.CreateHL7QueueItemDetail(queueItem, PersistenceContext));
        }

        [ReadOperation]
        public GetReferencedPatientResponse GetReferencedPatient(GetReferencedPatientRequest request)
        {
            HL7QueueItem queueItem = (HL7QueueItem)PersistanceContext.Load(request.QueueItemRef);

            //TODO:  Refactor following region
            #region To Be Refactored
            
            IHL7PreProcessor preProcessor = new HL7PreProcessor();
            HL7QueueItem preProcessedQueueItem = preProcessor.ApplyAll(queueItem);

            IHL7Processor processor = HL7ProcessorFactory.GetProcessor(preProcessedQueueItem.Message);

            IList<string> identifiers = processor.ListReferencedPatientIdentifiers();
            if (identifiers.Count == 0)
            {
                return null;
            }
            string assigningAuthority = processor.ReferencedPatientIdentifiersAssigningAuthority();

            #endregion

            PatientProfileSearchCriteria criteria = new PatientProfileSearchCriteria();
            criteria.Mrn.Id.EqualTo(identifiers[0]);
            criteria.Mrn.AssigningAuthority.EqualTo(assigningAuthority);

            IPatientProfileBroker profileBroker = PersistenceContext.GetBroker<IPatientProfileBroker>();
            IList<PatientProfile> profiles = profileBroker.Find(criteria);

            if (profiles.Count == 0)
            {
                return new GetReferencedPatientResponse();
            }
            else
            {
                return new GetReferencedPatientResponse(profiles[0].GetRef());
            }
        }

        [UpdateOperation]
        public ProcessHL7QueueItemResponse ProcessHL7QueueItem(ProcessHL7QueueItemRequest request)
        {
            HL7QueueItem queueItem = (HL7QueueItem)PersistenceContext.Load(request.QueueItemRef);
            
            //TODO:  Refactor following region
            #region To Be Refactored

            IList<Patient> referencedPatients = null;

            IHL7PreProcessor preProcessor = new HL7PreProcessor();
            HL7QueueItem preProcessedQueueItem = preProcessor.ApplyAll(hl7QueueItem);

            IHL7Processor processor = HL7ProcessorFactory.GetProcessor(preProcessedQueueItem.Message);

            processor.Patients = LoadOrCreatePatientsFromMrn(
                processor.ListReferencedPatientIdentifiers(),
                processor.ReferencedPatientIdentifiersAssigningAuthority());
            processor.Visits = LoadOrCreateVisitFromVisitNumber(
                processor.ListReferencedVisitIdentifiers(),
                processor.ReferencedVisitIdentifierAssigningAuthority());
            processor.Orders = processor.HasOrders == false ? new List<EntityAccess<Order>>() :
                LoadOrCreateOrdersFromPlacerNumber(
                    processor.ListReferencedPlacerOrderNumbers(),
                    processor.ReferencedPlacerOrderNumberAssigningAuthority());

            processor.Process();

            foreach (EntityAccess<Patient> patientAccess in processor.Patients)
            {
                this.CurrentContext.Lock(patientAccess.Entity, patientAccess.State);
            }
            foreach (EntityAccess<Visit> visitAccess in processor.Visits)
            {
                this.CurrentContext.Lock(visitAccess.Entity, visitAccess.State);
            }
            foreach (EntityAccess<Order> orderAccess in processor.Orders)
            {
                this.CurrentContext.Lock(orderAccess.Entity, orderAccess.State);
            }

            #endregion

            return new ProcessHL7QueueItemResponse();
        }

        [UpdateOperation]
        public SetHL7QueueItemCompleteResponse SetHL7QueueItemComplete(SetHL7QueueItemCompleteRequest request)
        {
            HL7QueueItem queueItem = (HL7QueueItem)PersistenceContext.Load(request.QueueItemRef);

            PersistenceContext.Lock(queueItem);
            queueItem.SetComplete();

            return new SetHL7QueueItemCompleteResponse();
        }

        [UpdateOperation]
        public SetHL7QueueItemErrorResponse SetHL7QueueItemError(SetHL7QueueItemErrorRequest request)
        {
            HL7QueueItem queueItem = (HL7QueueItem)PersistenceContext.Load(request.QueueItemRef);

            PersistenceContext.Lock(queueItem);
            queueItem.SetError(request.ErrorMessage);

            return new SetHL7QueueItemErrorResponse();
        }

        #endregion

        #region Private Methods

        private IList<EntityAccess<Patient>> LoadOrCreatePatientsFromMrn(IList<string> mrns, string assigningAuthority)
        {
            IList<EntityAccess<Patient>> loadedPatients = new List<EntityAccess<Patient>>();

            foreach (string mrn in mrns)
            {
                PatientProfileSearchCriteria criteria = new PatientProfileSearchCriteria();
                criteria.Mrn.Id.EqualTo(mrn);
                criteria.Mrn.AssigningAuthority.EqualTo(assigningAuthority);

                IList<PatientProfile> profiles = PersistenceContext.GetBroker<IPatientProfileBroker>().Find(criteria);
                if (profiles.Count > 0)
                {
                    loadedPatients.Add(new EntityAccess<Patient>(profiles[0].Patient, DirtyState.Dirty));
                }
                else
                {
                    Patient patient = new Patient();
                    PatientProfile profile = new PatientProfile();

                    profile.Mrn.Id = mrn;
                    profile.Mrn.AssigningAuthority = assigningAuthority;

                    patient.AddProfile(profile);

                    loadedPatients.Add(new EntityAccess<Patient>(patient, DirtyState.New));
                }
            }

            return loadedPatients;
        }

        private IList<EntityAccess<Visit>> LoadOrCreateVisitFromVisitNumber(IList<string> visitNumbers, string assigningAuthority)
        {
            IList<EntityAccess<Visit>> loadedVisits = new List<EntityAccess<Visit>>();

            foreach (string visitNumber in visitNumbers)
            {
                VisitSearchCriteria criteria = new VisitSearchCriteria();
                criteria.VisitNumber.Id.EqualTo(visitNumber);
                criteria.VisitNumber.AssigningAuthority.EqualTo(assigningAuthority);

                IList<Visit> visits = PersistenceContext.GetBroker<IVisitBroker>().Find(criteria);
                if (visits.Count > 0)
                {
                    loadedVisits.Add(new EntityAccess<Visit>(visits[0], DirtyState.Dirty));
                }
                else
                {
                    Visit visit = new Visit();

                    visit.VisitNumber.Id = visitNumber;
                    visit.VisitNumber.AssigningAuthority = assigningAuthority;

                    IList<Facility> facilities = PersistenceContext.GetBroker<IFacilityBroker>().FindAll();
                    visit.Facility = facilities[0];

                    loadedVisits.Add(new EntityAccess<Visit>(visit, DirtyState.New));
                }
            }

            return loadedVisits;
        }


        private IList<EntityAccess<Order>> LoadOrCreateOrdersFromPlacerNumber(IList<string> placerNumbers, string assigningAuthority)
        {
            IList<EntityAccess<Order>> loadedOrders = new List<EntityAccess<Order>>();

            foreach (string placerNumber in placerNumbers)
            {
                OrderSearchCriteria criteria = new OrderSearchCriteria();
                criteria.PlacerNumber.EqualTo(placerNumber);

                IList<Order> orders = PersistenceContext.GetBroker<IOrderBroker>().Find(criteria);
                if (orders.Count > 0)
                {
                    loadedOrders.Add(new EntityAccess<Order>(orders[0], DirtyState.Dirty));
                }
                else
                {
                    loadedOrders.Add(new EntityAccess<Order>(null, DirtyState.New));
                }
            }

            return loadedOrders;
        }

        #endregion

    }
}


