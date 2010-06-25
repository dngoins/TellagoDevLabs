using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class Assembly
    {
        private string id;
        private string name;
        private string culture;
        private string displayName;
        private bool isSystem;
        private string publicKeyToken;
        private string version;

        private Application application;

        private BtsAssembly btsAssembly;
        private List<Orchestration> orchestrations;
        private List<Pipeline> pipelines;
        private List<Schema> schemas;
        private List<Transform> transforms;

        public Assembly()
        { }

        public Assembly(BtsAssembly btsassembly)
        {
            btsAssembly = btsassembly;

            id = btsassembly.Name;
            name = btsassembly.Name;
            culture = btsassembly.Culture;
            displayName = btsassembly.DisplayName;
            isSystem = btsassembly.IsSystem;
            publicKeyToken = btsassembly.PublicKeyToken;
            version = btsassembly.Version;
        }
        //properties
        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string Culture { get { return culture; } }
        public string DisplayName { get { return displayName; } }
        public bool IsSystem { get { return isSystem; } }
        public string PublicKeyToken { get { return publicKeyToken; } }
        public string Version { get { return version; } }
        //relationships
        public Application Application
        {
            get { return new Application(btsAssembly.Application); }
        }

        public IQueryable<Orchestration> Orchestrations
        {
            get 
            {
                if (null == orchestrations)
                    orchestrations = BTSContext.Current.GetAssemblyOrchestrations(btsAssembly);
                return orchestrations.AsQueryable<Orchestration>();
            }
        }

        public IQueryable<Pipeline> Pipelines
        {
            get 
            {
                if (null == pipelines)
                    pipelines = BTSContext.Current.GetAssemblyPipelines(btsAssembly);
                return pipelines.AsQueryable<Pipeline>();
            }
        }

        public IQueryable<Transform> Transforms
        {
            get 
            {
                if (null == transforms)
                    transforms = BTSContext.Current.GetAssemblyTransforms(btsAssembly);
                return transforms.AsQueryable<Transform>();
            }
        }


    }
}
