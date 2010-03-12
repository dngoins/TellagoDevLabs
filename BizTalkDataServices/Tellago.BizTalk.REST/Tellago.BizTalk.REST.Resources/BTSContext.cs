using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.BizTalk.Operations;
using System.Configuration;

namespace Tellago.BizTalk.REST.Resources
{
    public class BTSContext
    {

        public static BTSContext Current = new BTSContext();

        private BtsCatalogExplorer btsRepository;
        private BizTalkOperations btsOperations;
        private string managementDBCS;
        private int ipRecordLimit;
        private int msgRecordLimit;

        public BTSContext()
        {
            btsRepository = new BtsCatalogExplorer();
                       
        }

        public string ManagementDBCS
        {
            get { return managementDBCS; }
            set 
            {
                managementDBCS = value;
                btsRepository.ConnectionString = managementDBCS;
                System.Data.SqlClient.SqlConnection sqlconn = new System.Data.SqlClient.SqlConnection(managementDBCS);
                btsOperations = new BizTalkOperations(sqlconn.DataSource, sqlconn.Database);
            }
        }

        public BtsCatalogExplorer BTSRespository
        {
            get 
            {
                btsRepository.ConnectionString = managementDBCS;
                return btsRepository;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.Application> Applications
        {
            get 
            {
                List<Tellago.BizTalk.REST.Resources.Application> applications = new List<Application>();
                foreach (Microsoft.BizTalk.ExplorerOM.Application app in btsRepository.Applications)
                    applications.Add(new Application(app));
                return applications;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.Assembly> Assemblies
        {
            get 
            {
                List<Assembly> assemblies = new List<Assembly>();

                foreach (Microsoft.BizTalk.ExplorerOM.BtsAssembly assm in btsRepository.Assemblies)
                    assemblies.Add(new Assembly(assm));
                return assemblies;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.Host> Hosts
        {
            get 
            {
                List<Host> hosts = new List<Host>();
                foreach (Microsoft.BizTalk.ExplorerOM.Host host in btsRepository.Hosts)
                    hosts.Add(new Host(host));
                return hosts;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.Party> Parties
        {
            get
            {
                List<Party> parties = new List<Party>();
                foreach (Microsoft.BizTalk.ExplorerOM.Party party in btsRepository.Parties)
                    parties.Add(new Party(party));
                return parties;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.Pipeline> Pipelines
        {
            get
            {
                List<Pipeline> pipelines = new List<Pipeline>();
                foreach (Microsoft.BizTalk.ExplorerOM.Pipeline pp in btsRepository.Pipelines)
                    pipelines.Add(new Pipeline(pp));
                return pipelines;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.ReceiveHandler> ReceiveHandlers
        {
            get 
            {
                List<ReceiveHandler> receiveHandlers = new List<ReceiveHandler>();
                foreach (Microsoft.BizTalk.ExplorerOM.ReceiveHandler rh in btsRepository.ReceiveHandlers)
                    receiveHandlers.Add(new ReceiveHandler(rh));
                return receiveHandlers;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.Schema> Schemas
        {
            get
            {
                List<Schema> schemas = new List<Schema>();
                foreach (Microsoft.BizTalk.ExplorerOM.Schema sch in btsRepository.Schemas)
                    schemas.Add(new Schema(sch));
                return schemas;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.InProcessInstance> InProcessInstances
        {
            get 
            {
                IEnumerable instancesEnumerable= btsOperations.GetServiceInstances();
                IEnumerator instancesEnumerator= instancesEnumerable.GetEnumerator();
                List<Tellago.BizTalk.REST.Resources.InProcessInstance> instances= new List<InProcessInstance>();
                ipRecordLimit = Convert.ToInt32(ConfigurationManager.AppSettings["ipRecordLimit"]);
                int index = 0;
                while(instancesEnumerator.MoveNext() && index<= ipRecordLimit)
                {
                    if (instancesEnumerator.Current is MessageBoxServiceInstance)
                    {
                        instances.Add(new InProcessInstance((MessageBoxServiceInstance)instancesEnumerator.Current));
                        index++;
                    }
                }
                return instances;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.Transform> Transforms
        {
            get 
            {
                List<Transform> transforms = new List<Transform>();
                foreach (Microsoft.BizTalk.ExplorerOM.Transform transform in btsRepository.Transforms)
                    transforms.Add(new Transform(transform));
                return transforms;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.SendPort> SendPorts
        {
            get 
            {
                List<SendPort> sendPorts = new List<SendPort>();
                foreach (Microsoft.BizTalk.ExplorerOM.SendPort sp in btsRepository.SendPorts)
                    sendPorts.Add(new SendPort(sp));
                return sendPorts;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.ReceivePort> ReceivePorts
        {
            get
            {
                List <ReceivePort> receivePorts = new List<ReceivePort>();
                foreach (Microsoft.BizTalk.ExplorerOM.ReceivePort rp in btsRepository.ReceivePorts)
                    receivePorts.Add(new ReceivePort(rp));
                return receivePorts;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.ReceiveLocation> GetReceiveLocations(Microsoft.BizTalk.ExplorerOM.ReceivePort btsReceivePort)
        {
                List<ReceiveLocation> receiveLocations = new List<ReceiveLocation>();
                foreach(Microsoft.BizTalk.ExplorerOM.ReceiveLocation rl in btsReceivePort.ReceiveLocations)
                    receiveLocations.Add(new ReceiveLocation(rl));
                return receiveLocations;
        }

        public List<Tellago.BizTalk.REST.Resources.Message> InProcessMessages
        {
            get
            {
                IEnumerable messageEnumerable= btsOperations.GetMessages();
                IEnumerator messageEnumerator = messageEnumerable.GetEnumerator();
                int index = 0;
                msgRecordLimit= Convert.ToInt32(ConfigurationManager.AppSettings["msgRecordLimit"]);
                List<Tellago.BizTalk.REST.Resources.Message> messages = new List<Message>();
                while(messageEnumerator.MoveNext() && index <= msgRecordLimit)
                {
                    messages.Add(new Message((Microsoft.BizTalk.Message.Interop.IBaseMessage)messageEnumerator.Current));
                    index++;
                }
                return messages;
            }
        }

        public List<Orchestration> GetApplicationOrchestrations(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<Orchestration> orchestrations = new List<Orchestration>();
            foreach (Microsoft.BizTalk.ExplorerOM.BtsOrchestration orch in btsapplication.Orchestrations)
                orchestrations.Add(new Orchestration(orch));
            return orchestrations;
        }

        public List<Orchestration> GetAssemblyOrchestrations(Microsoft.BizTalk.ExplorerOM.BtsAssembly btsassembly)
        {
            List<Orchestration> orchestrations = new List<Orchestration>();
            foreach (Microsoft.BizTalk.ExplorerOM.BtsOrchestration orch in btsassembly.Orchestrations)
                orchestrations.Add(new Orchestration(orch));
            return orchestrations;
        }

        public List<Assembly> GetApplicationAssemblies(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<Assembly> assemblies= new List<Assembly>();
            foreach (Microsoft.BizTalk.ExplorerOM.BtsAssembly assm in btsapplication.Assemblies)
                assemblies.Add(new Assembly(assm));
            return assemblies;
        }

        public List<Pipeline> GetAssemblyPipelines(Microsoft.BizTalk.ExplorerOM.BtsAssembly btsassembly)
        {
            List<Pipeline> pipelines = new List<Pipeline>();
            foreach (Microsoft.BizTalk.ExplorerOM.Pipeline pp in btsassembly.Pipelines)
                pipelines.Add(new Pipeline(pp));
            return pipelines;
        }

        public List<Message> GetMessageBoxServiceInstaceMessages(Microsoft.BizTalk.Operations.MessageBoxServiceInstance btsinstace)
        {
            List<Message> messages = new List<Message>();
            IEnumerator messageEnumerator = btsinstace.Messages.GetEnumerator();
            while (messageEnumerator.MoveNext())
                messages.Add(new Message(((Microsoft.BizTalk.Message.Interop.IBaseMessage)messageEnumerator.Current)));
            return messages;
        }

        public List<Transform> GetAssemblyTransforms(Microsoft.BizTalk.ExplorerOM.BtsAssembly btsassembly)
        {
            List<Transform> transforms = new List<Transform>();
            foreach(Microsoft.BizTalk.ExplorerOM.Transform t in btsassembly.Transforms)
                transforms.Add(new Transform(t));
            return transforms; 
        }

        public List<Transform> GetApplicationTransforms(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<Transform> transforms = new List<Transform>();
            foreach (Microsoft.BizTalk.ExplorerOM.Transform t in btsapplication.Transforms)
                transforms.Add(new Transform(t));
            return transforms;
        }

        public List<SendPort> GetApplicationSendPorts(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<SendPort> sendPorts = new List<SendPort>();
            foreach (Microsoft.BizTalk.ExplorerOM.SendPort sp in btsapplication.SendPorts)
                sendPorts.Add(new SendPort(sp));
            return sendPorts;
        }

        public List<Pipeline> GetApplicationPipelines(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<Pipeline> pipelines= new List<Pipeline>();
            foreach (Microsoft.BizTalk.ExplorerOM.Pipeline p in btsapplication.Pipelines)
                pipelines.Add(new Pipeline(p));
            return pipelines;
        }

        public List<SendPort> GetPartySendPorts(Microsoft.BizTalk.ExplorerOM.Party btsparty)
        {
            List<SendPort> ports = new List<SendPort>();
            foreach (Microsoft.BizTalk.ExplorerOM.SendPort p in btsparty.SendPorts)
                ports.Add(new SendPort(p));
            return ports;
        }


    }
}
