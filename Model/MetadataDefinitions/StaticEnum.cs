
using System.Xml.Serialization;

namespace Model.MetadataDefinitions
{
    [XmlRoot]
    public enum StaticEnum
    {
        [XmlEnum("1")]
        NotStatic,
        [XmlEnum("2")]
        Static
    }
}