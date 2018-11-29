
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    //[XmlRoot]
    [DataContract]
    public class PropertyMetadata : MemberAbstract
    {
       // [XmlElement]
       [IgnoreDataMember]
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

        public PropertyMetadata() : base() { }
    }
}