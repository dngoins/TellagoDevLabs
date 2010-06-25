using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;

// Extensions
using Tellago.BizTalk.REST.Resources.Util;


namespace Tellago.BizTalk.REST.Resources
{
    public class HostInstance : IUpdatableResource
    {
        private string id;
        private string name;
        private string description;
        private string state;
        private string hostName;
        private string hostType;

        private Microsoft.BizTalk.Management.HostInstance instance;

        public HostInstance()
        { }

        public HostInstance(Microsoft.BizTalk.Management.HostInstance instance)
        {
            this.instance = instance;
            id = instance.UniqueID;
            name = instance.Name;
            description = instance.Description;
            state = instance.ServiceState.ToString();
            hostName = instance.HostName;
            hostType = instance.HostType.ToString();
        }

        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public string State { get { return state; } set { state = value; } }
        public string HostName { get { return hostName; } }
        public string HostType { get { return hostType; } }


        #region IUpdatableResource Members

        void IUpdatableResource.UpdateResource(string propertyName, object value)
        {
            ((IUpdatableResource)this).Update(propertyName, value);
        }

        object IUpdatableResource.UpdateUnderlyingObject(object baseObject)
        {

            if (state.Equals("Stopped"))
                this.instance.Stop();
            else if (state.Equals("Started"))
                this.instance.Start();

            return baseObject;
        }

        #endregion
    }
}
