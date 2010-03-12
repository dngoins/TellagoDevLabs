﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class SendPort
    {
        private string id;
        private string name;
        private string description;
        private string filter;
        private bool isDynamic;
        private bool isTwoWay;
        private bool orderedDelivery;
        private string primaryTransport;
        private int priority;
        private bool routeFailedMessages;
        private string secondaryTransport;
        private string status;
        private bool stopSendingOnFailure;
        private string tracking;
        private List<Transform> inboundTransforms;
        private List<Transform> outboundTransforms;
        private Pipeline receivePipeline;
        private Pipeline sendPipeline;
        private Application application;

        private Microsoft.BizTalk.ExplorerOM.SendPort btsSendPort;

        public SendPort()
        { }

        public SendPort(Microsoft.BizTalk.ExplorerOM.SendPort btssendport)
        {
            id = btssendport.Name;
            name = btssendport.Name;
            description = btssendport.Description;
            filter = btssendport.Filter;
            isDynamic = btssendport.IsDynamic;
            isTwoWay = btssendport.IsTwoWay;
            orderedDelivery = btssendport.OrderedDelivery;
            primaryTransport = btssendport.PrimaryTransport.TransportType.Name;
            priority = btssendport.Priority;
            routeFailedMessages = btssendport.RouteFailedMessage;
            if (null != btssendport.SecondaryTransport.TransportType)
             secondaryTransport = btssendport.SecondaryTransport.TransportType.Name;
            status = btssendport.Status.ToString();
            stopSendingOnFailure = btssendport.StopSendingOnFailure;
            tracking = btssendport.Tracking.ToString();
            application = new Application(btssendport.Application);
            inboundTransforms = new List<Transform>();
            if (null != btssendport.InboundTransforms && btssendport.InboundTransforms.Count > 0)
            {
                
                foreach (Microsoft.BizTalk.ExplorerOM.Transform t in btssendport.InboundTransforms)
                    inboundTransforms.Add(new Transform(t));
            }
            outboundTransforms = new List<Transform>();
            if (null != btssendport.OutboundTransforms && btssendport.OutboundTransforms.Count > 0)
            {
                
                foreach (Microsoft.BizTalk.ExplorerOM.Transform t in btssendport.OutboundTransforms)
                    outboundTransforms.Add(new Transform(t));
            }
            if (null != btssendport.ReceivePipeline)
                receivePipeline = new Pipeline(btssendport.ReceivePipeline);
            if (null != btssendport.SendPipeline)
                sendPipeline = new Pipeline(btssendport.SendPipeline);

        }
        //properties
        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public string Filter { get { return filter; } }
        public bool IsDynamic { get { return isDynamic; } }
        public bool IsTwoWay { get { return isTwoWay; } }
        public bool OrderedDelivery { get { return orderedDelivery; } }
        public string PrimaryTransport { get { return primaryTransport; } }
        public int Priority { get { return priority; } }
        public bool RouteFailedMessages { get { return routeFailedMessages; } }
        public string SecondaryTransport { get { return secondaryTransport; } }
        public string Status { get { return status; } }
        public bool StopSendingOnFailure { get { return stopSendingOnFailure; } }
        public string Tracking { get { return tracking; } }
        //relationships
        public Application Application { get { return application; } }
        public IQueryable<Transform> InboundTransforms { get { return inboundTransforms.AsQueryable<Transform>(); } }
        public IQueryable<Transform> OutboundTransforms { get { return outboundTransforms.AsQueryable<Transform>(); } }
        public Pipeline ReceivePipeline { get { return receivePipeline; } }
        public Pipeline SendPipeline { get { return sendPipeline; } }

    }
}
