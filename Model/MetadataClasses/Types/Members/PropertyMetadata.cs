
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [DataContract]
    public class PropertyMetadata : MemberAbstract
    {
        [DataMember]
        public MethodMetadata[] propertyMethods;
        
        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {

            return from prop in props where prop.GetIndexParameters().Length == 0
                   select new PropertyMetadata(prop.Name, prop.PropertyType, prop.GetAccessors(true));
        }

        private PropertyMetadata(string propertyName, Type type, MethodInfo[] methods) : base(propertyName, TypeBasicInfo.EmitReference(type))
        {
            propertyMethods = methods.Select(info => new MethodMetadata(info)).ToArray();
        }

        public PropertyMetadata() : base() { }
    }
}