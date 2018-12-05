
using System.Runtime.Serialization;

namespace Model.MetadataDefinitions
{
    [DataContract]
    public enum AbstractEnum
    {
        [EnumMember]
        NotAbstract,
        [EnumMember]
        Abstract
    }
}