using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses.Types.Members
{    public class AttributeMetadata : MemberAbstract
    {
        internal static IEnumerable<AttributeMetadata> EmitAttributes(Type type)
        {
            return from attrib in type.GetCustomAttributes()
                   select new AttributeMetadata(attrib.ToString());
        }
        
        public AttributeMetadata(string name) : base(name)
        {
        }
        
        public AttributeMetadata() : base() { }
    }
}
