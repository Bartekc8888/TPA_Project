
using System.Runtime.Serialization;

namespace Serialization.MetadataDefinitions
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