using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class Schema
    {
        private string id;
        private string name;
        private string description;
        private bool alwaysTrackAllProperties;
        private string rootName;
        private string targetNamespace;
        private string xmlContent;
        private string[] trackedPropertyNames;
        private Application application;
        private Assembly assembly;

        private Microsoft.BizTalk.ExplorerOM.Schema btsSchema;

        public Schema() { }

        public Schema(Microsoft.BizTalk.ExplorerOM.Schema btsschema)
        {
            btsSchema = btsschema;

            id = btsschema.FullName;
            name = btsschema.FullName;
            description = btsschema.Description;
            //alwaysTrackAllProperties = btsschema.AlwaysTrackAllProperties;
            rootName = btsschema.RootName;
            targetNamespace = btsschema.TargetNameSpace;
            xmlContent = btsschema.XmlContent;
           // List<string> propNames= new List<string>();
            //foreach
            assembly = new Assembly(btsschema.BtsAssembly);
            application = new Application(btsschema.Application);
        }
        //properties
        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public bool AlwaysTrackAllProperties { get { return alwaysTrackAllProperties; } }
        public string RootName { get { return rootName; } }
        public string TargetNamespace { get { return targetNamespace; } }
        public string XmlContent { get { return xmlContent; } }
        //relationships
        public Application Application { get { return application; } }
        public Assembly Assembly { get { return assembly; } }
    }
}
