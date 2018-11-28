using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [XmlRoot]
    public class AttributeMetadata : MemberAbstract
    {
        internal static IEnumerable<AttributeMetadata> EmitAttributes(Type type)
        {
            return from attrib in type.GetCustomAttributes().Cast<Attribute>()

                   select new AttributeMetadata(attrib.ToString());
        }
        public AttributeMetadata(string name) : base(name)
        {
        }
        public AttributeMetadata() : base() { }
    }
}
