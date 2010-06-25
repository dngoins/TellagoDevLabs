using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.Operations;

// Extensions
using Tellago.BizTalk.REST.Resources.Util;

namespace Tellago.BizTalk.REST.Resources
{
    public class Instance : IUpdatableResource
    {
        private string id;
        private string instanceClass;
        private DateTime creationTime;
        private string errorDescription;
        private string instanceStatus;
        private string serviceType;
        private string serviceTypeID;


        private Microsoft.BizTalk.Operations.BizTalkOperations btsOperations;
        protected Microsoft.BizTalk.Operations.Instance btsInstance;

        public Instance()
        { }

        public Instance(Microsoft.BizTalk.Operations.Instance btsinstance, BizTalkOperations operations)
        {
            btsInstance = btsinstance;
            btsOperations = operations;


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
        public string InstanceStatus { get { return instanceStatus; } set { instanceStatus = value; } }
        public string ServiceType{get{return serviceType;}}
        public string ServiceTypeID{get{return serviceTypeID;}}


        #region IUpdatableResource Members

        void IUpdatableResource.UpdateResource(string propertyName, object value)
        {
            ((IUpdatableResource)this).Update(propertyName, value);
        }

        object IUpdatableResource.UpdateUnderlyingObject(object baseObject)
        {
            if (instanceStatus == "Terminated")
                btsOperations.TerminateInstance(new Guid(this.ID));
            else if (instanceStatus == "Suspended")
                btsOperations.SuspendInstance(new Guid(this.ID));
            else if (instanceStatus == "Resume")
                btsOperations.ResumeInstance(new Guid(this.ID));

            return baseObject;
        }
        #endregion
    }
}
