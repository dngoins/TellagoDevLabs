using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tellago.BizTalk.REST.Resources;
using System.Configuration;

namespace Tellago.BizTalk.REST.Services
{
    public class BTSManagementServiceContext
    {
        private List<Application> applications;
        private List<Orchestration> orchestrations;
        private List<Assembly> assemblies;
        private List<Host> hosts;
        private List<Pipeline> pipelines;
        private List<ReceiveHandler> receiveHandlers;
        private List<Schema> schemas;
        private List<InProcessInstance> inProcessInstances;
        private List<Message> inProcessMessages;
        private List<Transform> transforms;
        private List<SendPort> sendPorts;
        private List<ReceivePort> receivePorts;
        private List<Party> parties;

        public BTSManagementServiceContext()
        { }

        public IQueryable<Application> Applications
        {
            get 
            {
                if(null == applications)
                {
                    BTSContext.Current.ManagementDBCS= ConfigurationManager.AppSettings["mgmtDB"];
                    applications= BTSContext.Current.Applications;
                }
                return applications.AsQueryable<Application>();
            }
        }

        public IQueryable<Orchestration> Orchestrations
        {
            get 
            {
                return null;
            }
        }

        public IQueryable<Assembly> Assemblies
        {
            get 
            {
                if (null == assemblies)
                {
                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    assemblies = BTSContext.Current.Assemblies;
                }

                return assemblies.AsQueryable<Assembly>();
            }
        }

        public IQueryable<Host> Hosts
        {
            get 
            {
                if (null == hosts)
                {
                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    hosts= BTSContext.Current.Hosts;
                }
                return hosts.AsQueryable<Host>();
            }
        }

        public IQueryable<Party> Parties
        {
            get
            {
                if (null == parties)
                {
                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    parties = BTSContext.Current.Parties;
                }
                return parties.AsQueryable<Party>();
            }
        }

        public IQueryable<Pipeline> Pipelines
        {
            get
            {
                if (null == pipelines)
                {

                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    pipelines= BTSContext.Current.Pipelines;
                }
                return pipelines.AsQueryable<Pipeline>();
            }
        }

        public IQueryable<ReceiveHandler> ReceiveHandlers
        {
            get
            {
                if (null == receiveHandlers)
                {
                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    receiveHandlers= BTSContext.Current.ReceiveHandlers;
                }
                return receiveHandlers.AsQueryable<ReceiveHandler>();
            }
        }

        public IQueryable<Schema> Schemas
        {
            get 
            {
                if (null == schemas)
                {
                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    schemas= BTSContext.Current.Schemas;
                }
                return schemas.AsQueryable<Schema>();
            }
        }

        public IQueryable<Transform> Transforms
        {
            get 
            {
                if (null == transforms)
                {
                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    transforms= BTSContext.Current.Transforms;
                }
                return transforms.AsQueryable<Transform>();
            }
        }

        public IQueryable<InProcessInstance> InProcessInstances
        {
            get 
            {
                if (null == inProcessInstances)
                {

                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    inProcessInstances= BTSContext.Current.InProcessInstances;
                }
                return inProcessInstances.AsQueryable<InProcessInstance>();
            }
        }

        public IQueryable<SendPort> SendPorts
        {
            get
            {
                if (null == sendPorts)
                {

                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    sendPorts= BTSContext.Current.SendPorts;
                }
                return sendPorts.AsQueryable<SendPort>();
            }
        }

        public IQueryable<ReceivePort> ReceivePorts
        {
            get 
            {
                if (null == receivePorts)
                {

                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    receivePorts= BTSContext.Current.ReceivePorts;
                }
                return receivePorts.AsQueryable<ReceivePort>();
            }

        }

        public IQueryable<Message> InProcessMessages
        {
            get 
            {
                if (null == inProcessMessages)
                {

                    BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    inProcessMessages= BTSContext.Current.InProcessMessages;
                }
                return inProcessMessages.AsQueryable<Message>();
            }
        }

        public IQueryable<ContextProperty> ContextProperties
        {
            get { return null;  }
        }

        public IQueryable<ReceiveLocation> ReceiveLocations
        {
            get { return null; }
        }
    }
}
