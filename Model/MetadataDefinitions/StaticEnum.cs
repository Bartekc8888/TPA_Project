
using System.Runtime.Serialization;

namespace Model.MetadataDefinitions
{
    [DataContract]
    public enum StaticEnum
    {
        [EnumMember]
        NotStatic,
        [EnumMember]
        Static
    }
}