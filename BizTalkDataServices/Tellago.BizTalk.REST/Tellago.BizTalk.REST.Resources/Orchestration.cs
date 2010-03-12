using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tellago.BizTalk.REST.Resources
{
    public class Orchestration
    {
        private string id;
        private string name;
        private string assemblyQualifiedName;
        private bool autoResumeSuspendedInstances;
        private bool autoSuspendRunningInstances;
        private bool autoTerminateInstances;
        private string description;
        private string status;

        private Application application;
        private Assembly assembly;

        private Microsoft.BizTalk.ExplorerOM.BtsOrchestration btsOrchestration;

        public Orchestration()
        { }

        public Orchestration(Microsoft.BizTalk.ExplorerOM.BtsOrchestration btsorchestration)
        {
            btsOrchestration = btsorchestration;
            id = btsorchestration.FullName;
            name = btsorchestration.FullName;
            assemblyQualifiedName = btsorchestration.AssemblyQualifiedName;
            autoResumeSuspendedInstances = btsorchestration.AutoResumeSuspendedInstances;
            autoSuspendRunningInstances = btsorchestration.AutoSuspendRunningInstances;
            autoTerminateInstances = btsorchestration.AutoTerminateInstances;
            description = btsorchestration.Description;
            status = btsorchestration.Status.ToString();

            application = new Application(btsorchestration.Application);
            assembly = new Assembly(btsorchestration.BtsAssembly);
        }
        //properties
        public string ID{get { return id; }}
        public string Name{get { return name; }}
        public string AssemblyQualifiedName { get { return assemblyQualifiedName; } }
        public bool AutoResumeSuspendedInstances { get { return autoResumeSuspendedInstances; } }
        public bool AutoSuspendRunningInstances { get { return autoResumeSuspendedInstances; } }
        public bool AutoTerminateInstances { get { return autoTerminateInstances; } }
        public string Description { get { return description; } }
        public string Status{get{return description;}}
        //relationships
        public Application Application { get { return application; } }
        public Assembly Assembly { get { return assembly; } }
    }
}
