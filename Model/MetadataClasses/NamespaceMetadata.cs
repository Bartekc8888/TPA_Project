using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types;

namespace Model.MetadataClasses
{
    [DataContract(IsReference = true)]
    public class NamespaceMetadata
    {
        [DataMember]
        public string NamespaceName { get; set; }
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