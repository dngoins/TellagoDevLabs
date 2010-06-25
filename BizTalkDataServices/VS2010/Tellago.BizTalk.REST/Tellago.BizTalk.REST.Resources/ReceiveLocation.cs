using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;
using System.Data.Services;

// Extensions
using Tellago.BizTalk.REST.Resources.Util;

namespace Tellago.BizTalk.REST.Resources
{
    public class ReceiveLocation : IUpdatableResource
    {
        private string id;
        private string name;
        private string description;
        private string address;
        private bool enable;
        private DateTime endDate;
        private DateTime fromTime;
        private bool endDateEnabled;
        private bool isPrimary;
        private string publicAddress;
        private DateTime startDate;
        private bool startDateEnabled;
        private string transportType;
        private Pipeline receivePipeline;
        private Pipeline sendPipeline;
        private ReceiveHandler receiveHandler;


        private Microsoft.BizTalk.ExplorerOM.ReceiveLocation btsReceiveLocation;

        public ReceiveLocation()
        { }

        public ReceiveLocation(Microsoft.BizTalk.ExplorerOM.ReceiveLocation btsreceivelocation)
        {
            btsReceiveLocation = btsreceivelocation;
            id = btsreceivelocation.Name;
            name = btsreceivelocation.Name;
            description = btsreceivelocation.Description;
            address = btsreceivelocation.Address;
            enable = btsreceivelocation.Enable;
            endDate = btsreceivelocation.EndDate;
            fromTime = btsreceivelocation.FromTime;
            endDateEnabled = btsreceivelocation.EndDateEnabled;
            isPrimary = btsreceivelocation.IsPrimary;
            publicAddress = btsreceivelocation.PublicAddress;
            startDate = btsreceivelocation.StartDate;
            startDateEnabled = btsreceivelocation.StartDateEnabled;
            transportType = btsreceivelocation.TransportType.ToString();
            if (null != btsreceivelocation.ReceivePipeline)
              receivePipeline = new Pipeline(btsreceivelocation.ReceivePipeline);
            if(null != btsreceivelocation.SendPipeline)
              sendPipeline = new Pipeline(btsreceivelocation.SendPipeline);
            if(null != btsreceivelocation.ReceiveHandler)
              receiveHandler = new ReceiveHandler(btsreceivelocation.ReceiveHandler);
        }

        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } set { description = value; } }
        public string Address { get { return address; } set { address = value; } }
        public bool Enable { get { return enable; } set { enable = value; } }
        public DateTime EndDate { get { return endDate; }  }
        public DateTime FromTime { get { return fromTime; }  }
        public bool EndDateEnabled { get { return endDateEnabled; }  }
        public bool IsPrimary { get { return isPrimary; } }
        public string PublicAddress { get { return publicAddress; } set { publicAddress = value; } }
        public DateTime StartDate { get { return startDate; } }
        public bool StartDateEnabled { get { return startDateEnabled; } }
        public string TransportType { get { return transportType; }  }
        public Pipeline ReceivePipeline { get { return receivePipeline; }  }
        public Pipeline SendPipeline { get { return sendPipeline; }  }
        public ReceiveHandler ReceiveHandler { get { return receiveHandler; } }

        #region IUpdatableResource Members

        void IUpdatableResource.UpdateResource(string propertyName, object value)
        {
            ((IUpdatableResource)this).Update(propertyName, value);
        }

        object IUpdatableResource.UpdateUnderlyingObject(object baseObject)
        {
            this.btsReceiveLocation = baseObject as Microsoft.BizTalk.ExplorerOM.ReceiveLocation;

            btsReceiveLocation.Enable = this.Enable;
            btsReceiveLocation.Address = this.Address;
            btsReceiveLocation.PublicAddress = this.PublicAddress;
            btsReceiveLocation.Description = this.Description;

            return this.btsReceiveLocation as object;
        }

        #endregion
    }
}
