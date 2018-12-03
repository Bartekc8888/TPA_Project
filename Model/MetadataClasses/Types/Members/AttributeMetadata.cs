using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [DataContract]
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
