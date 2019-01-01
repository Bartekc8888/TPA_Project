
using System.Runtime.Serialization;

namespace SerializationModel.MetadataDefinitions
{
    [DataContract]
    public enum StaticSerializationModelEnum
    {
        [EnumMember]
        NotStatic,
        [EnumMember]
        Static
    }
}