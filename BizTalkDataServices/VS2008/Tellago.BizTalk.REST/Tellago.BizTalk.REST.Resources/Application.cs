using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Extensions
using Tellago.BizTalk.REST.Resources.Util;
using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class Application : IUpdatableResource
    {
        private string id;
        private string name;
        private string description;
        private bool isConfigured;
        private bool isDefaultApplication;
        private bool isSystem;
        private string status;

        private List<Orchestration> orchestrations;
        private List<Assembly> assemblies;
        private List<Transform> transforms;
        private List<SendPort> sendPorts;
        private List<Pipeline> pipelines;
        private List<ReceiveLocation> receiveLocations;
        private List<ReceivePort> receivePorts;

        private Microsoft.BizTalk.ExplorerOM.Application btsApplication;

        public Application()
        { }

        public Application(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            btsApplication = btsapplication;
            id = btsapplication.Name;
            name = btsapplication.Name;
            description = btsapplication.Description;
            isConfigured = btsapplication.IsConfigured;
            isDefaultApplication = btsapplication.IsDefaultApplication;
            isSystem = btsapplication.IsSystem;
            status = btsApplication.Status.ToString();
        }

        #region Properties

        public string ID{get { return id; }}
        public string Name{get { return name; }}
        public bool IsConfigured{get { return isConfigured; }}
        public string Description{get { return description; }}
        public bool IsDefaultApplication{ get { return isDefaultApplication; } }
        public bool IsSystem { get { return isSystem; } }
        public string Status { get { return status.ToString(); } set { status = value;  } }

        #endregion

        #region Relationships

        public IQueryable<Orchestration> Orchestrations
        {
            get
            {
                if (null == orchestrations)
                    orchestrations = BTSContext.Current.GetApplicationOrchestrations(btsApplication);
                return orchestrations.AsQueryable<Orchestration>();
            }
        }

        public IQueryable<Assembly> Assemblies
        {
            get 
            {
                if (null == assemblies)
                    assemblies = BTSContext.Current.GetApplicationAssemblies(btsApplication);
                return assemblies.AsQueryable<Assembly>();
            }
        }

        public IQueryable<Transform> Transforms
        {
            get
            {
                if (null == transforms)
                    transforms = BTSContext.Current.GetApplicationTransforms(btsApplication);
                return transforms.AsQueryable<Transform>();
            }
        }

        public IQueryable<Pipeline> Pipelines
        {
            get 
            {
                if (null == pipelines)
                    pipelines = BTSContext.Current.GetApplicationPipelines(btsApplication);
                return pipelines.AsQueryable<Pipeline>();
            }
        }

        public IQueryable<SendPort> SendPorts
        {
            get
            {
                if (null == sendPorts)
                    sendPorts = BTSContext.Current.GetApplicationSendPorts(btsApplication);
                return sendPorts.AsQueryable<SendPort>();
            }
        }

        public IQueryable<ReceivePort> ReceivePorts
        {
            get
            {
                if (null == receivePorts)
                    receivePorts = BTSContext.Current.GetApplicationReceivePorts(btsApplication);
                return receivePorts.AsQueryable<ReceivePort>();
            }
        }

        public IQueryable<ReceiveLocation> ReceiveLocations
        {
            get
            {
                if (null == receiveLocations)
                    receiveLocations = BTSContext.Current.GetApplicationReceiveLocations(btsApplication);

                return receiveLocations.AsQueryable<ReceiveLocation>();
            }
        }

        #endregion

        #region IUpdatableResource Members

        void IUpdatableResource.UpdateResource(string propertyName, object value)
        {
            ((IUpdatableResource)this).Update(propertyName, value);
        }

        object IUpdatableResource.UpdateUnderlyingObject(object baseObject)
        {
            this.btsApplication = baseObject as Microsoft.BizTalk.ExplorerOM.Application;

            if (Status.Equals("Start", StringComparison.InvariantCultureIgnoreCase))
                btsApplication.Start(ApplicationStartOption.StartAll);
            else if (Status.Equals("Stop", StringComparison.InvariantCultureIgnoreCase))
                btsApplication.Stop(ApplicationStopOption.StopAll);

            btsApplication.Description = Description;

            return this.btsApplication;
        }

        #endregion
    }
}
