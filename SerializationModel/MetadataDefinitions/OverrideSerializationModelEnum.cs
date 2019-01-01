
using System.Runtime.Serialization;

namespace SerializationModel.MetadataDefinitions
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