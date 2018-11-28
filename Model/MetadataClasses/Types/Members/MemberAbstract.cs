using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [XmlRoot]
    public abstract class MemberAbstract
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public TypeBasicInfo TypeMetadata { get; set; }

        public MemberAbstract(string name, TypeBasicInfo typeInfo)
        {
            this.Name = name;
            this.TypeMetadata = typeInfo;
        }

        public MemberAbstract(string name)
        {
            this.Name = name;
            this.TypeMetadata = null;
        }

        public MemberAbstract() { }
    }
}
