using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class ReceivePort
    {
        private string id;
        private string name;
        private string description;
        private bool isTwoWay;
        private bool routeFailedMessage;
        private string tracking;
        private List<ReceiveLocation> receiveLocations;


        private Microsoft.BizTalk.ExplorerOM.ReceivePort btsPort;

        public ReceivePort()
        { }

        public ReceivePort(Microsoft.BizTalk.ExplorerOM.ReceivePort btsport)
        {
            btsPort = btsport;

            id = btsport.Name;
            name = btsport.Name;
            description = btsport.Description;
            isTwoWay = btsport.IsTwoWay;
            routeFailedMessage = btsport.RouteFailedMessage;
            tracking = btsport.Tracking.ToString();

        }


        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public bool IsTwoWay { get { return isTwoWay; } }
        public bool RouteFailedMessage { get { return routeFailedMessage; } }
        public string Tracking { get { return tracking; } }

        public IQueryable<ReceiveLocation> ReceiveLocations
        {
            get 
            {
                if (null == receiveLocations)
                    receiveLocations = BTSContext.Current.GetReceiveLocations(btsPort);
                return receiveLocations.AsQueryable<ReceiveLocation>();
            }
        }
    }
}
