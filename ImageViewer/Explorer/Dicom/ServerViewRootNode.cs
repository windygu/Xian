using System;
using System.Collections.Generic;
using System.Text;

using ClearCanvas.Common;


namespace ClearCanvas.ImageViewer.Explorer.Dicom
{
    public class ServerViewRootNode : BrowserNode
    {
        #region Fields
        private AEServerPool _serverPool;

        public AEServerPool ServerPool
        {
            get { return _serverPool; }
            set { _serverPool = value; }
        }

        #endregion

        public ServerViewRootNode(AEServerPool svrPool)
        {
            _serverPool = svrPool;
        }

        protected override void CreateChildNodes()
        {
            if (_serverPool == null)
                return;
            foreach (AEServer ae in _serverPool.Serverlist)
            {
                if (!ae.Servername.Equals(AENavigatorComponent.MyDatastoreTitle))
                    continue;
                AddChild(new ServerViewServerNode(ae, _serverPool));
            }
            foreach (AEServer ae in _serverPool.Serverlist)
            {
                if (!ae.Servername.Equals(AENavigatorComponent.EmptyNodeName) || !ae.Serverpath.Replace("/", "").Equals(AENavigatorComponent.MyServersTitle))
                    continue;
                AddChild(new ServerViewServerNode(ae, _serverPool));
            }
        }

        public override string ServerName
        {
            get { return AENavigatorComponent.MyServersTitle; }
        }

        public override string ServerPath
        {
            get { return ""; }
        }

        public override string Details
        {
            get { return ""; }
        }
    }
}
