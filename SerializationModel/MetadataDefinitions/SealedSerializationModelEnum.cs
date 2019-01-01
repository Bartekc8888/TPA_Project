
using System.Runtime.Serialization;

namespace SerializationModel.MetadataDefinitions
{
    [DataContract]
    public enum SealedSerializationModelEnum
    {
        [EnumMember]
        Sealed,
        [EnumMember]
        NotSealed
    }
}