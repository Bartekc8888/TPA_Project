
using System.Runtime.Serialization;

namespace Serialization.MetadataDefinitions
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