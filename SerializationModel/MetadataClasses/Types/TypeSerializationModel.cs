using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types;
using Model.MetadataDefinitions;
using Model.MetadataExtensions;
using SerializationModel.MetadataClasses.Types.Members;
using SerializationModel.MetadataDefinitions;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types
{
    [DataContract(IsReference = true)]
    public class TypeSerializationModel
    {
        #region fields
        [DataMember(EmitDefaultValue = false)]
        public TypeTypesSerializationModelEnum TypeEnum { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string TypeName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string NamespaceName { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<TypeSerializationModel> GenericArguments { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Tuple<AccessLevelSerializationModelEnum, SealedSerializationModelEnum, AbstractSerializationModelEnum> Modifiers { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<AttributeSerializationModel> Attributes { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string FullTypeName { get; set; }
        
        [DataMember(EmitDefaultValue = false)]
        public TypeSerializationModel DeclaringType { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public TypeSerializationModel BaseType { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<TypeSerializationModel> ImplementedInterfaces { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<FieldSerializationModel> Fields { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<MethodSerializationModel> Methods { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<PropertySerializationModel> Properties { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<IndexerSerializationModel> Indexers { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<EventSerializationModel> Events { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<ConstructorSerializationModel> Constructors { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<TypeSerializationModel> NestedTypes { get; set; }
        #endregion

        public TypeSerializationModel(TypeMetadata metadata)
        {
            TypeEnum = EnumMapper.ConvertEnum<TypeTypesSerializationModelEnum, TypeTypesEnum>(metadata.TypeEnum);
            TypeName = metadata.TypeName;
            NamespaceName = metadata.NamespaceName;

            GenericArguments = metadata.GenericArguments == null ? null :
                metadata.GenericArguments.Select(EmitTypeSerializationModel);

            Modifiers = new Tuple<AccessLevelSerializationModelEnum, SealedSerializationModelEnum, AbstractSerializationModelEnum> (
                EnumMapper.ConvertEnum<AccessLevelSerializationModelEnum, AccessLevelEnum>(metadata.Modifiers.Item1),
                EnumMapper.ConvertEnum<SealedSerializationModelEnum, SealedEnum>(metadata.Modifiers.Item2),
                EnumMapper.ConvertEnum<AbstractSerializationModelEnum, AbstractEnum>(metadata.Modifiers.Item3));

            Attributes = metadata.Attributes.Select(AttributeSerializationModel.EmitUniqueType);
            FullTypeName = metadata.FullTypeName;
            
            DeclaringType = metadata.DeclaringType == null ? null : EmitTypeSerializationModel(metadata.DeclaringType);
            BaseType = metadata.BaseType == null ? null : EmitTypeSerializationModel(metadata.BaseType);

            ImplementedInterfaces =
                metadata.ImplementedInterfaces.Select(EmitTypeSerializationModel);
            Fields =
                metadata.Fields.Select(FieldSerializationModel.EmitUniqueType);
            Methods =
                metadata.Methods.Select(MethodSerializationModel.EmitUniqueType);
            Properties =
                metadata.Properties.Select(PropertySerializationModel.EmitUniqueType);
            Indexers =
                metadata.Indexers.Select(IndexerSerializationModel.EmitUniqueType);
            Events =
                metadata.Events.Select(EventSerializationModel.EmitUniqueType);
            Constructors =
                metadata.Constructors.Select(ConstructorSerializationModel.EmitUniqueType);
            NestedTypes =
                metadata.NestedTypes.Select(EmitTypeSerializationModel);
        }

        public static TypeSerializationModel EmitTypeSerializationModel(TypeMetadata metadata)
        {
            return UniqueEmitter.EmitType(metadata, propertyMetadata => new TypeSerializationModel(propertyMetadata));
        }
        
        public static TypeMetadata EmitTypeMetadata(TypeSerializationModel type)
        {
            return UniqueEmitter.EmitType(type, propertyMetadata => propertyMetadata.ToModel());
        }
        
        public TypeMetadata ToModel()
        {
            TypeMetadata metadata = new TypeMetadata();
            metadata.TypeEnum = EnumMapper.ConvertEnum<TypeTypesEnum, TypeTypesSerializationModelEnum>(TypeEnum);
            metadata.TypeName = TypeName;
            metadata.NamespaceName = NamespaceName;
            metadata.GenericArguments = GenericArguments?.Select(EmitTypeMetadata);
            
            metadata.Modifiers = new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(
                EnumMapper.ConvertEnum<AccessLevelEnum, AccessLevelSerializationModelEnum>(Modifiers.Item1),
                EnumMapper.ConvertEnum<SealedEnum, SealedSerializationModelEnum>(Modifiers.Item2),
                EnumMapper.ConvertEnum<AbstractEnum, AbstractSerializationModelEnum>(Modifiers.Item3));
            
            metadata.Attributes = Attributes?.Select(attributeMetadata => attributeMetadata.ToModel());
            metadata.FullTypeName = FullTypeName;
            metadata.DeclaringType = DeclaringType == null ? null : EmitTypeMetadata(DeclaringType);
            metadata.BaseType = DeclaringType == null ? null : EmitTypeMetadata(BaseType);
            metadata.ImplementedInterfaces = ImplementedInterfaces?.Select(EmitTypeMetadata);
            metadata.Fields = Fields?.Select(typeMetadata => typeMetadata.ToModel());
            metadata.Methods = Methods?.Select(typeMetadata => typeMetadata.ToModel());
            metadata.Properties = Properties?.Select(typeMetadata => typeMetadata.ToModel());
            metadata.Indexers = Indexers?.Select(typeMetadata => typeMetadata.ToModel());
            metadata.Events = Events?.Select(typeMetadata => typeMetadata.ToModel());
            metadata.Constructors = Constructors?.Select(typeMetadata => typeMetadata.ToModel());
            metadata.NestedTypes = NestedTypes?.Select(EmitTypeMetadata);

            return metadata;
        }

        protected bool Equals(TypeSerializationModel other)
        {
            return TypeEnum == other.TypeEnum && string.Equals(TypeName, other.TypeName) &&
                   string.Equals(NamespaceName, other.NamespaceName) &&
                   Equals(GenericArguments, other.GenericArguments) && Equals(Modifiers, other.Modifiers) &&
                   Equals(Attributes, other.Attributes) && string.Equals(FullTypeName, other.FullTypeName) &&
                   Equals(ImplementedInterfaces, other.ImplementedInterfaces) && Equals(Fields, other.Fields) &&
                   Equals(Methods, other.Methods) && Equals(Properties, other.Properties) &&
                   Equals(Indexers, other.Indexers) && Equals(Events, other.Events) &&
                   Equals(Constructors, other.Constructors) && Equals(NestedTypes, other.NestedTypes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeSerializationModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) TypeEnum;
                hashCode = (hashCode * 397) ^ (TypeName != null ? TypeName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NamespaceName != null ? NamespaceName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GenericArguments != null ? GenericArguments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Modifiers != null ? Modifiers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Attributes != null ? Attributes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FullTypeName != null ? FullTypeName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ImplementedInterfaces != null ? ImplementedInterfaces.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Fields != null ? Fields.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Methods != null ? Methods.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Properties != null ? Properties.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Indexers != null ? Indexers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Events != null ? Events.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Constructors != null ? Constructors.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NestedTypes != null ? NestedTypes.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
