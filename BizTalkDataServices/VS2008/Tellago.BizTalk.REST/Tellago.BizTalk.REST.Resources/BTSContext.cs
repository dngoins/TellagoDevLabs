using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.BizTalk.Operations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Services;

using OM = Microsoft.BizTalk.ExplorerOM;
using DTO = Tellago.BizTalk.REST.Resources;

namespace Tellago.BizTalk.REST.Resources
{
    public class BTSContext 
    {               
        private BtsCatalogExplorer btsRepository;
        private BizTalkOperations btsOperations;
        private string managementDBCS;
        private static BTSContext current;
        private int ipRecordLimit;
        private int msgRecordLimit;

        public BTSContext()
        {
            btsRepository = new BtsCatalogExplorer();                       
        }

        public static BTSContext Current
        {
            get
            {
                if (current == null)
                    current = new BTSContext();
                return current;
            }
        }

        public string ManagementDBCS
        {
            get 
            {
                return managementDBCS; 
            }
            set 
            {
                managementDBCS = value;
                btsRepository.ConnectionString = managementDBCS;
                SqlConnection sqlconn = new SqlConnection(managementDBCS);
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

        #region Properties

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

        public List<Tellago.BizTalk.REST.Resources.HostInstance> HostInstances
        {
            get
            {
                List<HostInstance> hostinstances = GetHostInstances(null);                

                return hostinstances;
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
                        instances.Add(new InProcessInstance((MessageBoxServiceInstance)instancesEnumerator.Current, btsOperations));
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

        public List<Tellago.BizTalk.REST.Resources.Orchestration> Orchestrations
        {
            get
            {
                List<Orchestration> orchestrations = new List<Orchestration>();

                foreach (Application app in Applications)
                {
                    orchestrations.AddRange(GetApplicationOrchestrations(app));
                }

                return orchestrations;
            }
        }

        public List<Tellago.BizTalk.REST.Resources.ReceiveLocation> ReceiveLocations
        {
            get
            {
                List<ReceiveLocation> receiveLocations = new List<ReceiveLocation>();

                foreach (Microsoft.BizTalk.ExplorerOM.ReceivePort rp in btsRepository.ReceivePorts)
                    receiveLocations.AddRange(GetReceiveLocations(rp));
                return receiveLocations;
            }

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

        #endregion

        #region Methods

        internal List<HostInstance> GetHostInstances(OM.Host btsHost)
        {
            List<HostInstance> hostInstances = 
                (
                    from Microsoft.BizTalk.Management.HostInstance hi in Microsoft.BizTalk.Management.HostInstance.GetInstances()
                    where btsHost == null || hi.HostName.Equals(btsHost.Name)
                    select new HostInstance(hi)
                ).ToList();

            return hostInstances;

        }
        
        internal List<Orchestration> GetApplicationOrchestrations(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<Orchestration> orchestrations = 
                (
                    from OM.BtsOrchestration o in btsapplication.Orchestrations
                    select new Orchestration(o)
                ).ToList();

            return orchestrations;
        }

        internal List<Orchestration> GetApplicationOrchestrations(Tellago.BizTalk.REST.Resources.Application app)
        {
            List<Orchestration> orchestrations =
                (
                    from OM.Application a in btsRepository.Applications
                    from OM.BtsOrchestration o in a.Orchestrations
                    where a.Name == o.Application.Name && a.Name == app.Name
                    select new Orchestration(o)
                ).ToList();

            return orchestrations;
        }

        internal List<Orchestration> GetAssemblyOrchestrations(Microsoft.BizTalk.ExplorerOM.BtsAssembly btsassembly)
        {
            List<Orchestration> orchestrations = 
                (
                    from OM.BtsOrchestration o in btsassembly.Orchestrations
                    select new Orchestration(o)
                 ).ToList();    
                             
            return orchestrations;
        }

        internal List<Assembly> GetApplicationAssemblies(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<Assembly> assemblies =
                (
                    from OM.BtsAssembly a in btsapplication.Assemblies
                    select new Assembly(a)
                ).ToList();

            return assemblies;

        }

        internal List<Pipeline> GetAssemblyPipelines(Microsoft.BizTalk.ExplorerOM.BtsAssembly btsassembly)
        {
            List<Pipeline> pipelines =
                (
                    from OM.Pipeline p in btsassembly.Pipelines
                    select new Pipeline(p)
                ).ToList();

            return pipelines;
        }

        internal List<Pipeline> GetApplicationPipelines(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<Pipeline> pipelines =
                (
                    from OM.Pipeline p in btsapplication.Pipelines
                    select new Pipeline(p)
                ).ToList();

            return pipelines;
             
        }

        internal List<Message> GetMessageBoxServiceInstaceMessages(Microsoft.BizTalk.Operations.MessageBoxServiceInstance btsinstace)
        {
            List<Message> messages = new List<Message>();
            IEnumerator messageEnumerator = btsinstace.Messages.GetEnumerator();
            while (messageEnumerator.MoveNext())
                messages.Add(new Message(((Microsoft.BizTalk.Message.Interop.IBaseMessage)messageEnumerator.Current)));
            return messages;
        }

        internal List<Transform> GetAssemblyTransforms(Microsoft.BizTalk.ExplorerOM.BtsAssembly btsassembly)
        {
            List<Transform> transforms =
                (
                    from OM.Transform t in btsassembly.Transforms
                    select new Transform(t)
                ).ToList();

            return transforms;
        }

        internal List<Transform> GetApplicationTransforms(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<Transform> transforms =
                (
                    from OM.Transform t in btsapplication.Transforms
                    select new Transform(t)
                ).ToList();

            return transforms;
        }

        internal List<SendPort> GetApplicationSendPorts(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<SendPort> sendports =
                (
                    from OM.SendPort sp in btsapplication.SendPorts
                    select new SendPort(sp)
                ).ToList();

            return sendports;
        }

        internal List<SendPort> GetPartySendPorts(Microsoft.BizTalk.ExplorerOM.Party btsparty)
        {
            List<SendPort> sendports =
                (
                    from OM.SendPort sp in btsparty.SendPorts                    
                    select new SendPort(sp)
                ).ToList();

            return sendports;
        }

        internal List<ReceivePort> GetApplicationReceivePorts(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<ReceivePort> receivePorts =
                (
                    from OM.ReceivePort rp in btsapplication.ReceivePorts
                    select new ReceivePort(rp)
                ).ToList();

            return receivePorts;
        }

        internal List<ReceiveLocation> GetApplicationReceiveLocations(Microsoft.BizTalk.ExplorerOM.Application btsapplication)
        {
            List<ReceiveLocation> receiveLocations =
                (                    
                    from OM.ReceivePort rp in btsapplication.ReceivePorts
                    from OM.ReceiveLocation rl in rp.ReceiveLocations 
                    where rl.ReceivePort.Name == rp.Name && rl.ReceivePort.Name == rp.Name
                    select new ReceiveLocation(rl)
                ).ToList();

            return receiveLocations;
        }

        internal List<ReceiveLocation> GetReceiveLocations(Microsoft.BizTalk.ExplorerOM.ReceivePort btsReceivePort)
        {
            List<ReceiveLocation> receiveLocations =
                (
                    from OM.ReceiveLocation rl in btsReceivePort.ReceiveLocations
                    select new ReceiveLocation(rl)
                ).ToList();

            return receiveLocations;
        }                

        #endregion

        #region BaseObject Lookup

        public object GetSingleObject(object resource)
        {
            switch (resource.GetType().FullName)
            {
                case "Tellago.BizTalk.REST.Resources.ReceiveLocation":
                    return GetSingleReceiveLocation(resource as DTO.ReceiveLocation);
                case "Tellago.BizTalk.REST.Resources.SendPort":
                    return GetSingleSendPort(resource as DTO.SendPort);
                case "Tellago.BizTalk.REST.Resources.Application":
                    return GetSingleApplication(resource as DTO.Application);
                case "Tellago.BizTalk.REST.Resources.Orchestration":
                    return GetSingleOrchestration(resource as DTO.Orchestration);
                case "Tellago.BizTalk.REST.Resources.HostInstance":
                case "Tellago.BizTalk.REST.Resources.InProcessInstance":
                    // There is no need to refresh the source object in this case.
                    return resource;
                default:
                    return null;
            }
        }

        private BaseObject GetSingleOrchestration(Tellago.BizTalk.REST.Resources.Orchestration obj)
        {
            var orch =
                (
                    from OM.Application a in btsRepository.Applications
                    from OM.BtsOrchestration o in a.Orchestrations
                    where o.FullName.Equals(obj.Name)
                    select o
                ).FirstOrDefault();
            return orch;
        }

        private BaseObject GetSingleApplication(Tellago.BizTalk.REST.Resources.Application obj)
        {
            var app = 
                (
                    from OM.Application a in btsRepository.Applications
                    where string.Compare(a.Name, obj.Name, true) == 0
                    select a
                 ).FirstOrDefault();
            return app;
        }

        private BaseObject GetSingleReceiveLocation(Tellago.BizTalk.REST.Resources.ReceiveLocation obj)
        {

            var rl = 
                (
                    from OM.ReceivePort a in BTSRespository.ReceivePorts
                    from OM.ReceiveLocation r in a.ReceiveLocations
                    where string.Compare(r.Name, obj.Name, true) == 0
                    select r
                 ).FirstOrDefault();
            return rl;

        }

        private BaseObject GetSingleSendPort(Tellago.BizTalk.REST.Resources.SendPort obj)
        {
            var rp = 
                (
                    from OM.ReceivePort a in BTSRespository.ReceivePorts
                    where string.Compare(a.Name, obj.Name, true) == 0
                    select a
                ).FirstOrDefault();
            return rp;
        }

        #endregion       
    }   
}
