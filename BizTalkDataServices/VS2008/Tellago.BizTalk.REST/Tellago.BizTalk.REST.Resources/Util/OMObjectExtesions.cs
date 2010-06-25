using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources.Util
{
    public static class OMObjectExtesions
    {
        public static List<Microsoft.BizTalk.ExplorerOM.ReceivePort> ToList(this Microsoft.BizTalk.ExplorerOM.ReceivePortCollection receivePortCollection)
        {
            List<Microsoft.BizTalk.ExplorerOM.ReceivePort> receivePorts = new List<Microsoft.BizTalk.ExplorerOM.ReceivePort>();

            var r = from Microsoft.BizTalk.ExplorerOM.ReceiveLocation rl in receivePortCollection
                    select rl;


            foreach (Microsoft.BizTalk.ExplorerOM.ReceivePort rp in receivePortCollection)
                receivePorts.Add(rp);

            return receivePorts;
        }

        public static List<Microsoft.BizTalk.ExplorerOM.ReceiveLocation> ToList(this Microsoft.BizTalk.ExplorerOM.ReceiveLocationCollection receiveLocationCollection)
        {
            List<Microsoft.BizTalk.ExplorerOM.ReceiveLocation> receiveLocations = new List<Microsoft.BizTalk.ExplorerOM.ReceiveLocation>();

            foreach (Microsoft.BizTalk.ExplorerOM.ReceiveLocation rl in receiveLocationCollection)
                receiveLocations.Add(rl);

            return receiveLocations;
        }
    }
}
