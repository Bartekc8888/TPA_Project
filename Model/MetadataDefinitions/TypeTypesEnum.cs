
using System.Xml.Serialization;

namespace Model.MetadataDefinitions
{
    [XmlRoot]
    public enum TypeTypesEnum
    {
        [XmlEnum("1")]
        Array,
        [XmlEnum("2")]
        Class,
        [XmlEnum("3")]
        Delegate,
        [XmlEnum("4")]
        Interface,
        [XmlEnum("5")]
        Enum,
        [XmlEnum("6")]
        Primitive,
        [XmlEnum("7")]
        Structure,
        [XmlEnum("8")]
        Unknown
    }
}
