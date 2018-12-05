
using System.Runtime.Serialization;

namespace Model.MetadataDefinitions
{
    [DataContract]
    public enum VirtualEnum
    {
        [EnumMember]
        NotVirtual,
        [EnumMember]
        Virtual
    }
}