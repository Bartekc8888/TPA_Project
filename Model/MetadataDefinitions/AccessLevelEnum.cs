
using System.Xml.Serialization;

namespace Model.MetadataDefinitions
{
    [XmlRoot]
    public enum AccessLevelEnum
    {
        [XmlEnum("1")]
        Public,
        [XmlEnum("2")]
        Protected,
        [XmlEnum("3")]
        Internal,
        [XmlEnum("4")]
        ProtectedInternal,
        [XmlEnum("5")]
        Private
    }
}