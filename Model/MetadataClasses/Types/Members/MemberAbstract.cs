using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    //[XmlRoot]
    [DataContract]
    public abstract class MemberAbstract
    {
        //[XmlElement]
        [DataMember]
        public string Name { get; set; }
        //[XmlElement]
        [DataMember]
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
