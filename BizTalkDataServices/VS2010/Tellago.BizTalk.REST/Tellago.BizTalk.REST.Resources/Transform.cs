using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.BizTalk.ExplorerOM;

namespace Tellago.BizTalk.REST.Resources
{
    public class Transform
    {
        private string id;
        private string name;
        private string description;
        private string assemblyQualifiedName;
        private XElement xmlContent;
        private Schema sourceSchema;
        private Schema targetSchema;
        private Assembly assembly;
        private Application application;

        private Microsoft.BizTalk.ExplorerOM.Transform btsTransform;

        public Transform() { }

        public Transform(Microsoft.BizTalk.ExplorerOM.Transform btstransform)
        {
            btsTransform = btstransform;

            id = btstransform.FullName;
            name = btstransform.FullName;
            description = btstransform.Description;
            assemblyQualifiedName = btstransform.AssemblyQualifiedName;
            xmlContent = XElement.Parse(btstransform.XmlContent);
            assembly = new Assembly(btstransform.BtsAssembly);
            application = new Application(btstransform.Application);
            sourceSchema= new Schema(btstransform.SourceSchema);
            targetSchema = new Schema(btstransform.TargetSchema);
        }

        public string ID { get { return id; } }
        public string Name { get { return name; } }
        public string Description { get { return description; } }
        public string AssemblyQualifiedName { get { return assemblyQualifiedName; } }
        public XElement XmlContent { get { return xmlContent; } }
        //relationships
        public Assembly Assembly { get { return assembly; } }
        public Application Application { get { return application; } }
        public Schema SourceSchema { get { return sourceSchema; } }
        public Schema TargetSchema { get { return targetSchema; } }
    }
}
