
using System.Runtime.Serialization;

namespace Serialization.MetadataDefinitions
{
    [DataContract]
    public enum OverrideSerializationModelEnum
    {
        [EnumMember]
        NotOverride,
        [EnumMember]
        Override
    }
}