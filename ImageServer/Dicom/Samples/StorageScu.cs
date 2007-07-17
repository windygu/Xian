using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

using ClearCanvas.ImageServer.Dicom;
using ClearCanvas.ImageServer.Dicom.Network;
using ClearCanvas.ImageServer.Dicom.Exceptions;

namespace ClearCanvas.ImageServer.Dicom.Samples
{
    public class StorageScu : IDicomClientHandler
    {
        #region Private Classes and Structures
        struct FileToSend
        {
            public String filename;
            public SopClass sopClass;
            public DicomUid transferSyntaxUid;
        }
        #endregion

        #region Private Members
        private List<FileToSend> _fileList = new List<FileToSend>();
        private int _fileListIndex = 0;
        private ClientAssociationParameters _assocParams = null;
        private DicomClient _dicomClient = null;
        #endregion

        #region Constructors
        public StorageScu()
        {
        }
        #endregion

        #region Private Methods
        private bool LoadDirectory(DirectoryInfo dir)
        {

            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                AddFileToSend(file.FullName);
            }

            String[] subdirectories = Directory.GetDirectories(dir.FullName);
            foreach (String subPath in subdirectories)
            {
                DirectoryInfo subDir = new DirectoryInfo(subPath);
                LoadDirectory(subDir);
                continue;
            }

            return true;
        }
        #endregion

        #region Public Methods
        public bool AddFileToSend(String file)
        {

            try
            {
                DicomFile dicomFile = new DicomFile(file);

                // Only load to specific character set to reduce amount of data read from file
                dicomFile.Load(DicomTags.SpecificCharacterSet, DicomReadOptions.Default);

                FileToSend fileStruct = new FileToSend();

                fileStruct.filename = file;
                fileStruct.sopClass = dicomFile.SopClass;
                fileStruct.transferSyntaxUid = dicomFile.TransferSyntax.DicomUid;
                if (dicomFile.TransferSyntax.Encapsulated)
                {
                    DicomLogger.LogError("Unsupported encapsulated transfer syntax in file: {0}.  Not sending file.", dicomFile.TransferSyntax.Name);
                    return false;
                }

                _fileList.Add(fileStruct);
            }
            catch (DicomException e)
            {
                DicomLogger.LogErrorException(e, "Unexpected exception when loading file for sending {0}", file);
                return false;
            }
            return true;
        }

        public bool AddDirectoryToSend(String directory)
        {
            DirectoryInfo dir = new DirectoryInfo(directory);

			return LoadDirectory(dir);
		}

        public void SetPresentationContexts()
        {
            foreach (FileToSend sendStruct in _fileList)
            {
                byte pcid = _assocParams.FindAbstractSyntax(sendStruct.sopClass);

                if (pcid == 0)
                {
                    pcid = _assocParams.AddPresentationContext(sendStruct.sopClass);

                    _assocParams.AddTransferSyntax(pcid, TransferSyntax.ExplicitVRLittleEndian);
                    _assocParams.AddTransferSyntax(pcid, TransferSyntax.ImplicitVRLittleEndian);
                }
            }
        }

        public void Send(String remoteAE, String host, int port)
        {
            if (_dicomClient == null)
            {
                if (_fileList.Count == 0)
                {
                    DicomLogger.LogInfo("Not sending, no files to send.");
                    return;
                }
                try
                {
                    IPAddress addr = null;
                    foreach (IPAddress dnsAddr in Dns.GetHostAddresses(host))
                        if (dnsAddr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            addr = dnsAddr;
                            break;
                        }
                    if (addr == null)
                    {
                        DicomLogger.LogError("No Valid IP addresses for host {0}", host);
                        return;
                    }
                    _assocParams = new ClientAssociationParameters("StorageSCU", remoteAE, new IPEndPoint(addr, port));

                    SetPresentationContexts();

                    _dicomClient = DicomClient.Connect(_assocParams, this);
                }
                catch (Exception e)
                {
                    DicomLogger.LogErrorException(e, "Unexpected exception trying to connect to Remote AE {0} on host {1} on port {2}", remoteAE, host, port);
                }
            }
        }

        public void SendCStore(DicomClient client, ClientAssociationParameters association)
        {
            FileToSend fileToSend = _fileList[_fileListIndex];

            DicomFile dicomFile = new DicomFile(fileToSend.filename);

            try
            {
                dicomFile.Load(DicomReadOptions.Default);
            }
            catch (DicomException e)
            {
                DicomLogger.LogErrorException(e, "Unexpected exception when loading DICOM file");
            }

            DicomMessage msg = new DicomMessage(dicomFile);

            byte pcid = association.FindAbstractSyntax(fileToSend.sopClass);

            client.SendCStoreRequest(pcid, client.NextMessageID(), DicomPriority.Medium, msg);
        }
        #endregion


        #region IDicomClientHandler Members

        public void OnReceiveAssociateAccept(DicomClient client, ClientAssociationParameters association)
        {
            _fileListIndex = 0;

            SendCStore(client, association);
        }

        public void OnReceiveAssociateReject(DicomClient client, ClientAssociationParameters association, DicomRejectResult result, DicomRejectSource source, DicomRejectReason reason)
        {
            DicomLogger.LogInfo("Association Rejection when {0} connected to remote AE {1}", association.CallingAE, association.CalledAE);
        }

        public void OnReceiveRequestMessage(DicomClient client, ClientAssociationParameters association, byte presentationID, DicomMessage message)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnReceiveResponseMessage(DicomClient client, ClientAssociationParameters association, byte presentationID, DicomMessage message)
        {
            _fileListIndex++;
            if (_fileListIndex >= _fileList.Count)
            {
                client.SendReleaseRequest();
                return;
            }

            SendCStore(client, association);
        }

        public void OnReceiveReleaseResponse(DicomClient client, ClientAssociationParameters association)
        {
            DicomLogger.LogInfo("Association released from ");
        }

        public void OnReceiveAbort(DicomClient client, ClientAssociationParameters association, DicomAbortSource source, DicomAbortReason reason)
        {
            DicomLogger.LogError("Unexpected association abort received from {0}", association.CalledAE);
        }

        public void OnNetworkError(DicomClient client, ClientAssociationParameters association, Exception e)
        {
            DicomLogger.LogErrorException(e, "Unexpected network error");
        }

        public void OnDimseTimeout(DicomClient client, ClientAssociationParameters association)
        {
            DicomLogger.LogInfo("Timeout waiting for response message, continuing.");
        }

        #endregion
    }
}
