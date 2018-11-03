using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using Model.MetadataExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.MetadataClasses
{
    public class TypeMetadata
    {
        #region fields
        private TypeBasicInfo m_TypeBasicInfo;
        private TypeBasicInfo m_DeclaringType;

        private IEnumerable<FieldMetadata> m_fields;
        private IEnumerable<MethodMetadata> m_Methods;
        private IEnumerable<PropertyMetadata> m_Properties;
        private IEnumerable<EventMetadata> m_events;
        // indexers
        // operator
        private IEnumerable<MethodMetadata> m_Constructors;
        // descturctor
        // static_constructor
        private IEnumerable<TypeBasicInfo> m_NestedTypes;
        #endregion

        #region constructors
        public TypeMetadata(Type type)
        {
            m_TypeBasicInfo = new TypeBasicInfo(type);
            m_DeclaringType = TypeBasicInfo.EmitDeclaringType(type.DeclaringType);

            m_fields = FieldMetadata.EmitFields(type.GetFields());
            m_events = EventMetadata.EmitEvents(type.GetEvents());
            m_Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            m_Methods = MethodMetadata.EmitMethods(type.GetMethods());
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_Properties = PropertyMetadata.EmitProperties(type.GetProperties());

        }
        #endregion

        #region methods
        private IEnumerable<TypeBasicInfo> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeBasicInfo(_type);
        }
        #endregion
    }
}
