using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tellago.BizTalk.REST.Tests.BTSModel;
using System.Data.Services.Client;

namespace Tellago.BizTalk.REST.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ModelFixture
    {
        public ModelFixture()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        BTSModel.BTSManagementServiceContext ctx;

        public BTSModel.BTSManagementServiceContext Context
        {
            get
            {
                if (ctx == null)
                {
                    ctx = new BTSModel.BTSManagementServiceContext(
                                      new Uri("http://localhost:8080/Tellago.BizTalk.REST.ServiceHost/BizTalkManagementService.svc/"));
                }
                return ctx;
            }
        }

        [TestMethod]
        public void ShouldStartHostInstances()
        {
            var hosts = Context.Hosts.Expand("HostInstances");

            foreach (Host host in hosts)
            {
                foreach (HostInstance instance in host.HostInstances)
                {
                    if (instance.HostType == "In_process" && instance.State == "Stopped")
                    {
                        instance.State = "Started";
                        Context.UpdateObject(instance);
                        Context.SaveChanges();
                    }
                }
            }
        }

        [TestMethod]
        public void ShouldStartApplication()
        {
            var app = Context.Applications.Where(a => a.Name.Equals("Test")).FirstOrDefault();

            if (app != null)
            {

                app.Status = "Start";
                Context.UpdateObject(app);
                Context.SaveChanges();

            }
        }

        [TestMethod]
        public void ShouldEnableReceiveLocation()
        {
            var app = Context.Applications.Expand("ReceiveLocations").Where(a => a.Name.Equals("Test")).FirstOrDefault();

            if (app != null)
            {
                foreach (ReceiveLocation rl in app.ReceiveLocations)
                {
                    rl.Name = rl.Name;
                    rl.Enable = true;
                    rl.Address = rl.Address + ".txt";
                    rl.Description = "lalalala";
                    Context.UpdateObject(rl);
                    Context.SaveChanges();
                }
            }
        }

        [TestMethod]
        public void ShouldStartSendPort()
        {

            var app = Context.Applications.Where(a => a.Name.Equals("Test")).FirstOrDefault();

            if (app != null)
            {

                app.Status = "Start";
                Context.UpdateObject(app);
                Context.SaveChanges();

            }
        }

        [TestMethod]
        public void ShouldStartOrchestrations()
        {

            var app = Context.Applications.Expand("Orchestrations").Where(a => a.Name.Equals("Test")).FirstOrDefault();

            if (app != null)
            {
                foreach (Orchestration orch in app.Orchestrations)
                {
                    app.Status = "Unenlisted";
                    Context.UpdateObject(orch);
                    Context.SaveChanges();
                }
            }
        }

        [TestMethod]
        public void ShouldStartAllOrchestrations()
        {
            var app = Context.Orchestrations;

            foreach (Orchestration orch in Context.Orchestrations)
            {
                orch.Status = "Started";
                Context.UpdateObject(orch);
                Console.WriteLine(orch.Name);
            }
            Context.SaveChanges(SaveChangesOptions.Batch);
        }

        [TestMethod]
        public void ShouldResumeSuspendedInstances()
        {
            var instances = Context.InProcessInstances;

            foreach (InProcessInstance instance in instances)
            {
                if (instance.InstanceStatus == "Suspended")
                    instance.InstanceStatus = "Resume";

                Context.UpdateObject(instance);
                Console.WriteLine(instance.ID);
            }
            Context.SaveChanges(SaveChangesOptions.Batch);
        }

        [TestMethod]
        public void ShouldGetApplicationReceiveLocations()
        {
            var app = Context.Applications.Expand("ReceivePorts").Expand("ReceiveLocations").Where(a => a.Name.Equals("Test")).FirstOrDefault();

            if (app != null)
            {
                foreach (ReceiveLocation rl in app.ReceiveLocations)
                {
                    Console.Write(string.Format("{0} -> {1}d", rl.Name, rl.Enable.ToString()));
                }
            }
        }
    }
}
