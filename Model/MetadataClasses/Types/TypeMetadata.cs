using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using Model.MetadataExtensions;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace Model.MetadataClasses.Types
{
    //[XmlRoot]
    [DataContract]
    public class TypeMetadata
    {
        #region fields
        //[XmlIgnore]
        [DataMember]
        public TypeTypesEnum TypeEnum { get; set; }

        //[XmlElement]
        [DataMember]
        public TypeBasicInfo TypeBasicInfo { get; set; }
        //[XmlElement]
        [DataMember]
        public TypeBasicInfo DeclaringType { get; set; }

        //[XmlElement]
        [DataMember]
        public TypeBasicInfo BaseType { get; set; }
        //[XmlIgnore]
        [DataMember]
        public IEnumerable<TypeBasicInfo> ImplementedInterfaces { get; set; }

        //[XmlIgnore]
        [DataMember]
        public IEnumerable<FieldMetadata> Fields { get; set; }
        // [XmlIgnore]
        [DataMember]
        public IEnumerable<MethodMetadata> Methods { get; set; }
        // [XmlIgnore]
        [DataMember]
        public IEnumerable<PropertyMetadata> Properties { get; set; }
        //[XmlIgnore]
        [DataMember]
        public IEnumerable<IndexerMetadata> Indexers { get; set; }
        //[XmlIgnore]
        [DataMember]
        public IEnumerable<EventMetadata> Events { get; set; }
        // [XmlIgnore]
        [DataMember]
        public IEnumerable<ConstructorMetadata> Constructors { get; set; }
        //[XmlIgnore]
        [DataMember]
        public IEnumerable<TypeBasicInfo> NestedTypes { get; set; }
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
            Indexers = IndexerMetadata.EmitIndexers(type.GetProperties(flagsToGetAll));
            Events = EventMetadata.EmitEvents(type.GetEvents(flagsToGetAll));
            Constructors = ConstructorMetadata.EmitConstructors(type.GetConstructors(flagsToGetAll));
            NestedTypes = EmitNestedTypes(type.GetNestedTypes(flagsToGetAll));
        }

        public TypeMetadata() { }
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
