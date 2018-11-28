
using System.Xml.Serialization;

namespace Model.MetadataDefinitions
{
    [XmlRoot]
    public enum AbstractEnum
    {
        [XmlEnum("1")]
        NotAbstract,
        [XmlEnum("2")]
        Abstract
    }
}