
using System.Runtime.Serialization;

namespace Serialization.MetadataDefinitions
{
    [DataContract]
    public enum VirtualSerializationModelEnum
    {
        [EnumMember]
        NotVirtual,
        [EnumMember]
        Virtual
    }
}