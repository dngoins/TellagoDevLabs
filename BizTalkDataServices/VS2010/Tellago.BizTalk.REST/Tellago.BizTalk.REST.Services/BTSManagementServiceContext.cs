using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Data.Services;
using System.Data.Services.Providers;

using Resources = Tellago.BizTalk.REST.Resources;
using OM = Microsoft.BizTalk.ExplorerOM;

using Tellago.BizTalk.REST.Resources;
using System.Collections;

namespace Tellago.BizTalk.REST.Services
{
    public partial class BTSManagementServiceContext : IUpdatable
    {
        #region Private Members

        private List<Resources.Application> applications;
        private List<Resources.Orchestration> orchestrations;
        private List<Resources.Assembly> assemblies;
        private List<Resources.Host> hosts;
        private List<Resources.HostInstance> hostInstances;
        private List<Resources.Pipeline> pipelines;
        private List<Resources.ReceiveHandler> receiveHandlers;
        private List<Resources.Schema> schemas;
        private List<Resources.InProcessInstance> inProcessInstances;
        private List<Resources.Message> inProcessMessages;
        private List<Resources.Transform> transforms;
        private List<Resources.SendPort> sendPorts;
        private List<Resources.ReceivePort> receivePorts;
        private List<Resources.ReceiveLocation> receiveLocations;
        private List<Resources.Party> parties;

        #endregion

        /// <summary>
        /// Encapsulates the access to the underlying biztalk objects
        /// </summary>
        Resources.BTSContext ctx = Resources.BTSContext.Current;

        /// <summary>List of pending changes to apply once the <see cref="SaveChanges"/> is called.</summary>
        /// <remarks>This is a list of actions which will be called to apply the changes. Discarding the changes is done
        /// simply by clearing this list.</remarks>
        private Dictionary<int, object> dirtyObjects;


        public BTSManagementServiceContext()
        {
            this.dirtyObjects = new Dictionary<int, object>();
        }

        #region IQueryable Resources

        public IQueryable<Resources.Application> Applications
        {
            get
            {
                if (null == applications)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    applications = Resources.BTSContext.Current.Applications;
                }
                return applications.AsQueryable<Resources.Application>();
            }
        }

        public IQueryable<Resources.Orchestration> Orchestrations
        {
            get
            {
                if (null == orchestrations)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    orchestrations = Resources.BTSContext.Current.Orchestrations;
                }
                return orchestrations.AsQueryable<Resources.Orchestration>();
            }
        }

        public IQueryable<Resources.Assembly> Assemblies
        {
            get
            {
                if (null == assemblies)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    assemblies = Resources.BTSContext.Current.Assemblies;
                }

                return assemblies.AsQueryable<Resources.Assembly>();
            }
        }

        public IQueryable<Resources.Host> Hosts
        {
            get
            {
                if (null == hosts)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    hosts = Resources.BTSContext.Current.Hosts;
                }
                return hosts.AsQueryable<Resources.Host>();
            }
        }

        public IQueryable<Resources.HostInstance> HostInstances
        {
            get
            {
                if (null == hosts)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    hostInstances = Resources.BTSContext.Current.HostInstances;
                }
                return hostInstances.AsQueryable<Resources.HostInstance>();
            }
        }

        public IQueryable<Resources.Party> Parties
        {
            get
            {
                if (null == parties)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    parties = Resources.BTSContext.Current.Parties;
                }
                return parties.AsQueryable<Resources.Party>();
            }
        }

        public IQueryable<Resources.Pipeline> Pipelines
        {
            get
            {
                if (null == pipelines)
                {

                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    pipelines = Resources.BTSContext.Current.Pipelines;
                }
                return pipelines.AsQueryable<Resources.Pipeline>();
            }
        }

        public IQueryable<Resources.ReceiveHandler> ReceiveHandlers
        {
            get
            {
                if (null == receiveHandlers)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    receiveHandlers = Resources.BTSContext.Current.ReceiveHandlers;
                }
                return receiveHandlers.AsQueryable<Resources.ReceiveHandler>();
            }
        }

        public IQueryable<Resources.Schema> Schemas
        {
            get
            {
                if (null == schemas)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    schemas = Resources.BTSContext.Current.Schemas;
                }
                return schemas.AsQueryable<Resources.Schema>();
            }
        }

        public IQueryable<Resources.Transform> Transforms
        {
            get
            {
                if (null == transforms)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    transforms = Resources.BTSContext.Current.Transforms;
                }
                return transforms.AsQueryable<Resources.Transform>();
            }
        }

        public IQueryable<Resources.InProcessInstance> InProcessInstances
        {
            get
            {
                if (null == inProcessInstances)
                {

                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    inProcessInstances = Resources.BTSContext.Current.InProcessInstances;
                }
                return inProcessInstances.AsQueryable<Resources.InProcessInstance>();
            }
        }

        public IQueryable<Resources.SendPort> SendPorts
        {
            get
            {
                if (null == sendPorts)
                {

                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    sendPorts = Resources.BTSContext.Current.SendPorts;
                }
                return sendPorts.AsQueryable<Resources.SendPort>();
            }
        }

        public IQueryable<Resources.ReceivePort> ReceivePorts
        {
            get
            {
                if (null == receivePorts)
                {

                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    receivePorts = Resources.BTSContext.Current.ReceivePorts;
                }
                return receivePorts.AsQueryable<Resources.ReceivePort>();
            }

        }

        public IQueryable<Resources.Message> InProcessMessages
        {
            get
            {
                if (null == inProcessMessages)
                {

                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    inProcessMessages = Resources.BTSContext.Current.InProcessMessages;
                }
                return inProcessMessages.AsQueryable<Resources.Message>();
            }
        }

        public IQueryable<Resources.ContextProperty> ContextProperties
        {
            get { return null; }
        }

        public IQueryable<Resources.ReceiveLocation> ReceiveLocations
        {
            get
            {
                if (null == receiveLocations)
                {
                    Resources.BTSContext.Current.ManagementDBCS = ConfigurationManager.AppSettings["mgmtDB"];
                    receiveLocations = Resources.BTSContext.Current.ReceiveLocations;

                }
                return receiveLocations.AsQueryable<Resources.ReceiveLocation>();
            }
        }

        #endregion

        #region IUpdatable Members

        object IUpdatable.GetResource(IQueryable query, string fullTypeName)
        {
            object resource = null;

            foreach (object r in query)
            {
                if (resource != null)
                    throw new ArgumentException(String.Format("Invalid Uri specified. The query '{0}' must refer to a single resource", query.ToString()));

                resource = r;
            }

            if (resource != null)
                return resource;
            else
                return null;
        }

        void IUpdatable.SetValue(object targetResource, string propertyName, object propertyValue)
        {
            var iupdatable = targetResource as IUpdatableResource;

            if (iupdatable != null)
            {
                iupdatable.UpdateResource(propertyName, propertyValue);

                if (!dirtyObjects.ContainsKey(targetResource.GetHashCode()))
                {
                    dirtyObjects.Add(targetResource.GetHashCode(), targetResource);
                }
            }
        }

        void IUpdatable.SaveChanges()
        {
            using (var context = Resources.BTSContext.Current.BTSRespository)
            {
                List<object> modifiedObjects = new List<object>();

                // Just run all the pending changes we gathered so far
                foreach (var dirtyObject in dirtyObjects)
                {
                    modifiedObjects.Add (((IUpdatableResource)dirtyObject.Value).UpdateUnderlyingObject(BTSContext.Current.GetSingleObject(dirtyObject.Value)));
                }
                
                context.SaveChanges();                
            }

            this.dirtyObjects.Clear();
        }

        object IUpdatable.ResolveResource(object resource)
        {
            return resource;
        }

        void IUpdatable.ClearChanges()
        {
            this.dirtyObjects.Clear();
        }

        object IUpdatable.CreateResource(string containerName, string fullTypeName)
        {
            throw new NotImplementedException();
        }

        void IUpdatable.AddReferenceToCollection(object targetResource, string propertyName, object resourceToBeAdded)
        {
            throw new NotImplementedException();
        }

        void IUpdatable.DeleteResource(object targetResource)
        {
            throw new NotImplementedException();
        }

        object IUpdatable.GetValue(object targetResource, string propertyName)
        {
            throw new NotImplementedException();
        }

        void IUpdatable.RemoveReferenceFromCollection(object targetResource, string propertyName, object resourceToBeRemoved)
        {
            throw new NotImplementedException();
        }

        object IUpdatable.ResetResource(object resource)
        {
            throw new NotImplementedException();
        }

        void IUpdatable.SetReference(object targetResource, string propertyName, object propertyValue)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
