
using System.Runtime.Serialization;

namespace SerializationModel.MetadataDefinitions
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