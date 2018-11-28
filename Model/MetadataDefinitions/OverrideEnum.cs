
using System.Xml.Serialization;

namespace Model.MetadataDefinitions
{
    [XmlRoot]
    public enum OverrideEnum
    {
        [XmlEnum("1")]
        NotOverride,
        [XmlEnum("2")]
        Override
    }
}