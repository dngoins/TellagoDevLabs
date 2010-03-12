using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tellago.BizTalk.REST.Resources
{
    public class Application
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
            status = btsapplication.Status.ToString();
        }

        //properties
        public string ID{get { return id; }}
        public string Name{get { return name; }}
        public bool IsConfigured{get { return isConfigured; }}
        public string Description{get { return description; }}
        public bool IsDefaultApplication{ get { return isDefaultApplication; } }
        public bool IsSystem { get { return isSystem; } }
        public string Status { get { return status; } }
        //relationships
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
    }
}
