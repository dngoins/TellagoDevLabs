using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.Operations;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            InstancesTest();
        }

        private static void InstancesTest()
        {
            BizTalkOperations operations = new BizTalkOperations();
            IEnumerable instancesEnumerable= operations.GetServiceInstances();
            IEnumerator instancesEnum = instancesEnumerable.GetEnumerator();
            List<Instance> instances = new List<Instance>();
            while (instancesEnum.MoveNext())
            {
                Instance instance = (Instance)instancesEnum.Current;
                instances.Add(instance);
            }
        }
    }
}
