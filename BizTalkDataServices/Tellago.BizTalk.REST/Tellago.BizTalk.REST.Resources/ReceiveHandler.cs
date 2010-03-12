using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class ReceiveHandler
    {
        private string id;
        private string name;
        private string transportType;
        private Host host;

        private Microsoft.BizTalk.ExplorerOM.ReceiveHandler btsHandler; 

        public ReceiveHandler()
        { }

        public ReceiveHandler(Microsoft.BizTalk.ExplorerOM.ReceiveHandler btshandler)
        {
            btsHandler = btshandler;

            id = btshandler.TransportType.Name;
            name = btshandler.Name;
            transportType = btshandler.TransportType.Name;

            host = new Host(btshandler.Host);
        }
        //poroperties
        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string TransportType { get { return transportType; } }
        //relationships
        public Host Host { get { return host; } }

    }
}
