
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Model.MetadataExtensions;
using System;

namespace Model.MetadataClasses.Types.Members
{
    public class PropertyMetadata
    {
        public string Name { get; private set; }
        public TypeBasicInfo TypeMetadata { get; private set; }

        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   select new PropertyMetadata(prop.Name, prop.PropertyType);
        }

        private PropertyMetadata(string propertyName, Type type)
        {
            Name = propertyName;
            TypeMetadata = TypeBasicInfo.EmitReference(type);
        }
    }
}