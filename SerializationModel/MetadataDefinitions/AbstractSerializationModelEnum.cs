
using System.Runtime.Serialization;

namespace SerializationModel.MetadataDefinitions
{
    [DataContract]
    public enum AbstractSerializationModelEnum
    {
        [EnumMember]
        NotAbstract,
        [EnumMember]
        Abstract
    }
}