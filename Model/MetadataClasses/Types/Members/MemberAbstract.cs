using System.Runtime.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public abstract class MemberAbstract
    {
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)]
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
