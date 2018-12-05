
using System.Runtime.Serialization;

namespace Model.MetadataDefinitions
{
    [DataContract]
    public enum AccessLevelEnum
    {
        [EnumMember]
        Public,
        [EnumMember]
        Protected,
        [EnumMember]
        Internal,
        [EnumMember]
        ProtectedInternal,
        [EnumMember]
        Private
    }
}