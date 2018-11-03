using Model.MetadataClasses.Types;
using Model.MetadataDefinitions;
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

        private Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> m_Modifiers;

        // constant
        // field
        private IEnumerable<MethodMetadata> m_Methods;
        private IEnumerable<PropertyMetadata> m_Properties;
        // events
        // indexers
        // operator
        private IEnumerable<MethodMetadata> m_Constructors;
        // descturctor
        // static_constructor
        private IEnumerable<TypeMetadata> m_NestedTypes;

        private IEnumerable<Attribute> m_Attributes;
        #endregion

        #region constructors
        public TypeMetadata(Type type)
        {
            m_TypeBasicInfo = new TypeBasicInfo(type);
            m_DeclaringType = TypeBasicInfo.EmitDeclaringType(type.DeclaringType);
            m_Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            m_Methods = MethodMetadata.EmitMethods(type.GetMethods());
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_Modifiers = EmitModifiers(type);
            m_Properties = PropertyMetadata.EmitProperties(type.GetProperties());
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();
        }
        #endregion

        #region methods
        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeMetadata(_type);
        }

        static Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            //set defaults 
            AccessLevelEnum _access = AccessLevelEnum.Private;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            SealedEnum _sealed = SealedEnum.NotSealed;

            // check if not default 
            if (type.IsPublic || type.IsNestedPublic)
                _access = AccessLevelEnum.Public;
            else if (type.IsNotPublic || type.IsNestedPrivate)
                _access = AccessLevelEnum.Private;
            else if (type.IsNestedFamily)
                _access = AccessLevelEnum.Protected;
            else if (type.IsNestedAssembly)
                _access = AccessLevelEnum.Internal;
            else if (type.IsNestedFamORAssem || type.IsNestedFamORAssem)
                _access = AccessLevelEnum.ProtectedInternal;

            if (type.IsSealed)
                _sealed = SealedEnum.Sealed;

            if (type.IsAbstract)
                _abstract = AbstractEnum.Abstract;

            return new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
        }
        #endregion
    }
}
