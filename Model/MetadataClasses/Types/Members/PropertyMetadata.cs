
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Model.MetadataExtensions;
using System;

namespace Model.MetadataClasses.Types.Members
{
    internal class PropertyMetadata
    {
        private string m_Name;
        private TypeBasicInfo m_TypeMetadata;

        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetadata(prop.Name, prop.PropertyType);
        }

        private PropertyMetadata(string propertyName, Type type)
        {
            m_Name = propertyName;
            m_TypeMetadata = TypeBasicInfo.EmitReference(type);
        }
    }
}