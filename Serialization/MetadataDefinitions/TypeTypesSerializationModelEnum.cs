
using System.Runtime.Serialization;

namespace Serialization.MetadataDefinitions
{
    [DataContract]
    public enum TypeTypesSerializationModelEnum
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
