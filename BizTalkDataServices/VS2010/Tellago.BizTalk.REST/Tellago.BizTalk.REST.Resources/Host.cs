using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class Host
    {
        private string id;
        private string name;
        private bool isDefault;
        private string ntGroupName;
        private string type;

        private List<HostInstance> hostInstances;

        private Microsoft.BizTalk.ExplorerOM.Host btsHost;

        public Host()
        { }

        public Host(Microsoft.BizTalk.ExplorerOM.Host btshost)
        {
            btsHost = btshost;
            id = btshost.Name;
            name = btshost.Name;
            isDefault = btshost.IsDefault;
            ntGroupName = btshost.NTGroupName;
            type = btshost.Type.ToString();
        }

        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public bool IsDefault { get { return isDefault; } }
        public string NTGroupName { get { return ntGroupName; } }
        public string Type { get { return type; } }

        public IQueryable<HostInstance> HostInstances
        {
            get
            {
                if (null == hostInstances)
                    hostInstances = BTSContext.Current.GetHostInstances(btsHost);
                return hostInstances.AsQueryable<HostInstance>();
            }
        }

    }
}
