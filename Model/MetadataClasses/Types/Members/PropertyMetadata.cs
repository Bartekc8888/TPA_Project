
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses.Types.Members
{
    public class PropertyMetadata : MemberAbstract
    {
        public MethodInfo[] propertyMethods;
        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {

            return from prop in props where prop.GetIndexParameters().Length == 0
                   select new PropertyMetadata(prop.Name, prop.PropertyType, prop.GetAccessors(true));
        }

        private PropertyMetadata(string propertyName, Type type, MethodInfo[] methods) : base(propertyName, TypeBasicInfo.EmitReference(type))
        {
            propertyMethods = methods;
        }
    }
}