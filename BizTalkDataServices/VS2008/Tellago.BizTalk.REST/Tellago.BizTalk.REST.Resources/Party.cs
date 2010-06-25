using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class Party
    {
        private string id;
        private string name;
        private List<SendPort> sendPorts;

        private Microsoft.BizTalk.ExplorerOM.Party btsParty;

        public Party()
        { }

        public Party(Microsoft.BizTalk.ExplorerOM.Party btsparty)
        {
            btsParty = btsparty;
            id = btsparty.Name;
            name = btsparty.Name;

        }

        public string ID { get { return id; } }
        public string Name { get { return name; } }

        public IList<SendPort> SendPorts
        {
            get 
            {
                if (null == sendPorts)
                    sendPorts = BTSContext.Current.GetPartySendPorts(this.btsParty);

                return sendPorts;
            }
        }

    }
}
