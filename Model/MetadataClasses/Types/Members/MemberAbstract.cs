using System.Runtime.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [DataContract]
    public abstract class MemberAbstract
    {
        [DataMember]
        public string Name { get; set; }
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
