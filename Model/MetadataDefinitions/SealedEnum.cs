
using System.Runtime.Serialization;

namespace Model.MetadataDefinitions
{
    [DataContract]
    public enum SealedEnum
    {
        [EnumMember]
        Sealed,
        [EnumMember]
        NotSealed
    }
}