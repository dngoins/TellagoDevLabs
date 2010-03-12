using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.BizTalk.Operations;
using Microsoft.BizTalk.Message.Interop;

namespace Tellago.BizTalk.REST.Resources
{
    public class Message
    {
        private string id;
        private string bodyPartName;
        private int partCount;
        private bool isMutable;
        private XElement body;
        private IBaseMessage btsMessage;
        private List<ContextProperty> contextProperties;

        public Message()
        { }

        public Message(IBaseMessage btsmessage)
        {
            btsMessage = btsmessage;

            id = btsmessage.MessageID.ToString();
            partCount = btsmessage.PartCount;
            isMutable = btsmessage.IsMutable;
            bodyPartName = btsmessage.BodyPartName;
            
            if (null != btsmessage.BodyPart && null != btsmessage.BodyPart.Data)
                body= XElement.Load(System.Xml.XmlReader.Create(btsmessage.BodyPart.Data));

            contextProperties= new List<ContextProperty>();
            for(int index= 0; index<= btsmessage.Context.CountProperties - 1; index++)
            {
                string pname, pns;
                var pvalue= btsmessage.Context.ReadAt(index, out pname, out pns);
                bool isPromoted = btsmessage.Context.IsPromoted(pname, pns);
                contextProperties.Add(new ContextProperty(pname, pns, pvalue, isPromoted));
            }
        }

        //properties
        public string ID { get { return id; } }
        public string BodyPartName { get { return bodyPartName; } }
        public int PartCount { get { return partCount; } }
        public bool IsMutable { get { return isMutable; } }
        public XElement Body { get { return body; } }
        //relationships
        public IQueryable<ContextProperty> ContextProperties
        {
            get 
            {
                return contextProperties.AsQueryable<ContextProperty>();
            }
        }
    }
}
