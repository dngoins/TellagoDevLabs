using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.BizTalk.Operations;

namespace Tellago.BizTalk.REST.Resources
{
    public class InProcessInstance: Instance
    {
        private string adapter;
        private string currentProcessingServer;
        private string errorCategory;
        private string errorCode;
        private string errorProcessingServer;
        private string pendingOperation;
        private DateTime suspendTime;
        private string messageBoxDB;
        //private Application application;
        private List<Message> messages;

        public InProcessInstance()
            : base()
        { }

        public InProcessInstance(MessageBoxServiceInstance btsinstace, BizTalkOperations operations) : base(btsinstace, operations)
        {
            adapter = btsinstace.Adapter;
            currentProcessingServer = btsinstace.CurrentProcessingServer;
            errorCategory = btsinstace.ErrorCategory.ToString();
            errorCode = btsinstace.ErrorCode;
            errorProcessingServer = btsinstace.ErrorProcessingServer;
            suspendTime = btsinstace.SuspendTime;
            pendingOperation = btsinstace.PendingOperation.ToString();
            messageBoxDB = btsinstace.MessageBox.DBServer + ";" + btsinstace.MessageBox.DBName;
        }
        //properties
        public string Adapter { get { return adapter; } }
        public string CurrentProcessingServer { get { return currentProcessingServer; } }
        public string ErrorCategory { get { return errorCategory; } }
        public string ErrorCode { get { return errorCode; } }
        public string ErrorProcessingServer { get { return errorProcessingServer; } }
        public string PendingOperation { get { return pendingOperation; } }
        public DateTime SuspendTime { get { return suspendTime; } }
        public string MessageBoxDB { get { return messageBoxDB; } }
        //relationships
        public IQueryable<Message> Messages
        {
            get
            {
                if (null == messages)
                    messages = BTSContext.Current.GetMessageBoxServiceInstaceMessages((MessageBoxServiceInstance)btsInstance);
                return messages.AsQueryable<Message>();
            }
        }
    }
}
