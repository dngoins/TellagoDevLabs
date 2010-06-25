using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class Pipeline
    {
        private string id;
        private string name;
        private string assemblyQualifiedName;
        private string description;
        private string type;
        private string tracking;

        private Assembly assembly;

        private Microsoft.BizTalk.ExplorerOM.Pipeline btsPipeline;

        public Pipeline()
        { 
        }

        public Pipeline(Microsoft.BizTalk.ExplorerOM.Pipeline btspipeline)
        {
            btsPipeline= btspipeline;

            id = btspipeline.FullName;
            name = btspipeline.FullName;
            assemblyQualifiedName = btspipeline.AssemblyQualifiedName;
            description = btspipeline.Description;
            type = btspipeline.Type.ToString();
            tracking = btspipeline.Tracking.ToString();

            assembly = new Assembly(btspipeline.BtsAssembly);
        }
        //properties
        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string AssemblyQualifiedName { get { return assemblyQualifiedName; } }
        public string Description { get { return description; } }
        public string Type { get { return type; } }
        public string Tracking { get { return tracking; } }
        //relationships
        public Assembly Assembly { get { return assembly; } }
    }
}
