
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Model.MetadataExtensions;

namespace Model.MetadataClasses
{
    [XmlRoot]
    public class AssemblyMetadata
    {
        [XmlElement]
        public string TypeName { get; set; }
        [XmlElement]
        public string Name { get; set; }
        [XmlIgnore]
        public IEnumerable<NamespaceMetadata> Namespaces { get; set; }
       
        public AssemblyMetadata(Assembly assembly)
        {
            TypeName = assembly.GetType().Name;
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type _type in assembly.GetTypes()
                         where _type.GetVisible()
                         group _type by _type.GetNamespace() into _group
                         orderby _group.Key
                         select new NamespaceMetadata(_group.Key, _group);
        }

        public AssemblyMetadata() { }
    }
}