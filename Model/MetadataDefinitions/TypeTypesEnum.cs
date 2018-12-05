
using System.Runtime.Serialization;

namespace Model.MetadataDefinitions
{
    [DataContract]
    public enum TypeTypesEnum
    {
        [EnumMember]
        Array,
        [EnumMember]
        Class,
        [EnumMember]
        Delegate,
        [EnumMember]
        Interface,
        [EnumMember]
        Enum,
        [EnumMember]
        Primitive,
        [EnumMember]
        Structure,
        [EnumMember]
        Unknown
    }
}
