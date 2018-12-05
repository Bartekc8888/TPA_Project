
using System.Runtime.Serialization;

namespace Model.MetadataDefinitions
{
    [DataContract]
    public enum OverrideEnum
    {
        [EnumMember]
        NotOverride,
        [EnumMember]
        Override
    }
}