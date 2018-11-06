using Model.ExtractionTools;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using Model.MetadataExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses
{
    public class TypeMetadata
    {
        #region fields
        public TypeTypesEnum TypeEnum { get; private set; }

        public TypeBasicInfo TypeBasicInfo { get; private set; }
        public TypeBasicInfo DeclaringType { get; private set; }

        public TypeBasicInfo BaseType { get; private set; }
        public IEnumerable<TypeBasicInfo> ImplementedInterfaces { get; private set; }

        public IEnumerable<FieldMetadata> Fields { get; private set; }
        public IEnumerable<MethodMetadata> Methods { get; private set; }
        public IEnumerable<PropertyMetadata> Properties { get; private set; }
        public IEnumerable<EventMetadata> Events { get; private set; }
        public IEnumerable<ConstructorMetadata> Constructors { get; private set; }
        public IEnumerable<TypeBasicInfo> NestedTypes { get; private set; }
        #endregion

        #region constructors
        public TypeMetadata(Type type)
        {
            TypeEnum = TypeEnumFactory.CreateTypeMetadataClass(type);

            TypeBasicInfo = new TypeBasicInfo(type);
            DeclaringType = TypeBasicInfo.EmitDeclaringType(type.DeclaringType);

            BaseType = EmitExtends(type.BaseType);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces());

            BindingFlags flagsToGetAll = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            Fields = FieldMetadata.EmitFields(type.GetFields(flagsToGetAll));
            Methods = MethodMetadata.EmitMethods(type.GetMethods(flagsToGetAll));
            Properties = PropertyMetadata.EmitProperties(type.GetProperties(flagsToGetAll));
            Events = EventMetadata.EmitEvents(type.GetEvents(flagsToGetAll));
            Constructors = ConstructorMetadata.EmitConstructors(type.GetConstructors(flagsToGetAll));
            NestedTypes = EmitNestedTypes(type.GetNestedTypes(flagsToGetAll));
        }
        #endregion

        #region methods
        private IEnumerable<TypeBasicInfo> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeBasicInfo(_type);
        }

        private static TypeBasicInfo EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;

            return TypeBasicInfo.EmitReference(baseType);
        }

        private IEnumerable<TypeBasicInfo> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select TypeBasicInfo.EmitReference(currentInterface);
        }
        #endregion
    }
}
