
using System.Xml.Serialization;

namespace Model.MetadataDefinitions
{
    [XmlRoot]
    public enum SealedEnum
    {
        [XmlEnum("1")]
        Sealed,
        [XmlEnum("2")]
        NotSealed
    }
}