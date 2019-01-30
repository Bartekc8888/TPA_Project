
using System.Runtime.Serialization;

namespace Serialization.MetadataDefinitions
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