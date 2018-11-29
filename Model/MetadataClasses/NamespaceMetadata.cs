using System;
using System.Collections.Generic;
using System.Linq;
using Model.MetadataClasses.Types;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Model.MetadataClasses
{
    //[XmlRoot]
    [DataContract]
    public class NamespaceMetadata
    {
        //[XmlElement]
        [DataMember]
        public string NamespaceName { get; set; }
        //[XmlIgnore]
        [DataMember]
        public IEnumerable<TypeMetadata> Types { get; set; }

        public NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            NamespaceName = name;
            Types = from type in types orderby type.Name select new TypeMetadata(type);
        }

        public NamespaceMetadata() { }
    }
}