using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.Operations;

namespace Tellago.BizTalk.REST.Resources
{
    public class Instance
    {
        private string id;
        private string instanceClass;
        private DateTime creationTime;
        private string errorDescription;
      //  private string host;
        private string instanceStatus;
        private string serviceType;
        private string serviceTypeID;

        protected Microsoft.BizTalk.Operations.Instance btsInstance;

        public Instance()
        { }

        public Instance(Microsoft.BizTalk.Operations.Instance btsinstance)
        {
            btsInstance = btsinstance;

            id = btsinstance.ID.ToString();
            instanceClass = btsinstance.Class.ToString();
            creationTime = btsinstance.CreationTime;
            errorDescription = btsinstance.ErrorDescription;
            instanceStatus = btsinstance.InstanceStatus.ToString();
            serviceType = btsinstance.ServiceType;
            serviceTypeID = btsinstance.ServiceTypeID.ToString();
        }

        public string ID{get{return id;}}
        public string InstanceClass{get{return instanceClass;}}
        public DateTime CreationTime{get{return creationTime;}}
        public string ErrorDescription{get{return errorDescription;}}
            //  private string host;
        public string InstanceStatus{get{return instanceStatus;}}
        public string ServiceType{get{return serviceType;}}
        public string ServiceTypeID{get{return serviceTypeID;}}
    }
}
