
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace Model.MetadataClasses.Types.Members
{
    public class PropertyMetadata : MemberAbstract
    {
        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   select new PropertyMetadata(prop.Name, prop.PropertyType);
        }

        private PropertyMetadata(string propertyName, Type type) : base(propertyName, TypeBasicInfo.EmitReference(type))
        {
        }
    }
}