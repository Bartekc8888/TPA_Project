using System;
using System.Collections.Generic;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types
{
    public class TypeMetadata
    {
        public TypeTypesEnum typeEnum;
        public string typeName;
        public string namespaceName;
        public IEnumerable<TypeMetadata> genericArguments;
        public Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> modifiers;
        public IEnumerable<AttributeMetadata> attributes;
        public string fullTypeName;
        public TypeMetadata declaringType;
        public TypeMetadata baseType;
        public IEnumerable<TypeMetadata> implementedInterfaces;
        public IEnumerable<FieldMetadata> fields;
        public IEnumerable<MethodMetadata> methods;
        public IEnumerable<PropertyMetadata> properties;
        public IEnumerable<IndexerMetadata> indexers;
        public IEnumerable<EventMetadata> events;
        public IEnumerable<ConstructorMetadata> constructors;
        public IEnumerable<TypeMetadata> nestedTypes;
    }
}
