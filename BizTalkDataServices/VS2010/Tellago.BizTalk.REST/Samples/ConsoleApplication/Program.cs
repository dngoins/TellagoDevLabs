using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.Operations;
using ConsoleApplication1.BTSModel;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // InstancesTest();
            BTSModel.BTSManagementServiceContext ctx = new BTSModel.BTSManagementServiceContext(
                new Uri("http://localhost:8080/Tellago.BizTalk.REST.ServiceHost/BizTalkManagementService.svc/"));

            var hosts = ctx.Hosts.Expand("HostInstances");

            foreach (Host host in hosts)
            {
                foreach (HostInstance instance in host.HostInstances)
                {
                    if (instance.HostType == "In_process" && instance.State == "Stopped")
                    {
                        instance.State = "Started";
                        ctx.UpdateObject(instance);
                        ctx.SaveChanges();
                    }
                }
            }      
        }

        //private static void InstancesTest()
        //{
        //    BizTalkOperations operations = new BizTalkOperations();
        //    IEnumerable instancesEnumerable= operations.GetServiceInstances();
        //    IEnumerator instancesEnum = instancesEnumerable.GetEnumerator();
        //    List<Instance> instances = new List<Instance>();
        //    while (instancesEnum.MoveNext())
        //    {
        //        Instance instance = (Instance)instancesEnum.Current;
        //        instances.Add(instance);
        //    }
        //}
    }
}
