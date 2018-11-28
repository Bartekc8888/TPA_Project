
using System.Xml.Serialization;

namespace Model.MetadataDefinitions
{
    [XmlRoot]
    public enum VirtualEnum
    {
        [XmlEnum("1")]
        NotVirtual,
        [XmlEnum("2")]
        Virtual
    }
}