using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Services;
using System.ServiceModel;

namespace Tellago.BizTalk.REST.Services
{

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class BTSManagementService: DataService<BTSManagementServiceContext>
    {
        public static void InitializeService(IDataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        }
    }
}
