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

            GenericArguments =
                metadata.GenericArguments.Select(EmitTypeSerializationModel);

            Modifiers = new Tuple<AccessLevelSerializationModelEnum, SealedSerializationModelEnum, AbstractSerializationModelEnum> (
                EnumMapper.ConvertEnum<AccessLevelSerializationModelEnum, AccessLevelEnum>(metadata.Modifiers.Item1),
                EnumMapper.ConvertEnum<SealedSerializationModelEnum, SealedEnum>(metadata.Modifiers.Item2),
                EnumMapper.ConvertEnum<AbstractSerializationModelEnum, AbstractEnum>(metadata.Modifiers.Item3));

            Attributes = metadata.Attributes.Select(attributeMetadata => new AttributeSerializationModel(attributeMetadata));
            FullTypeName = metadata.FullTypeName;
            
            DeclaringType = EmitTypeSerializationModel(metadata.DeclaringType);
            BaseType = EmitTypeSerializationModel(metadata.BaseType);

            ImplementedInterfaces =
                metadata.ImplementedInterfaces.Select(EmitTypeSerializationModel);
            Fields =
                metadata.Fields.Select(typeMetadata => new FieldSerializationModel(typeMetadata));
            Methods =
                metadata.Methods.Select(typeMetadata => new MethodSerializationModel(typeMetadata));
            Properties =
                metadata.Properties.Select(typeMetadata => new PropertySerializationModel(typeMetadata));
            Indexers =
                metadata.Indexers.Select(typeMetadata => new IndexerSerializationModel(typeMetadata));
            Events =
                metadata.Events.Select(typeMetadata => new EventSerializationModel(typeMetadata));
            Constructors =
                metadata.Constructors.Select(typeMetadata => new ConstructorSerializationModel(typeMetadata));
            NestedTypes =
                metadata.NestedTypes.Select(EmitTypeSerializationModel);
        }

        public static TypeSerializationModel EmitTypeSerializationModel(TypeMetadata metadata)
        {
            long id = ReferenceSerializationModelMap.IdGenerator.GetId(metadata, out bool firstTime);
            if (firstTime)
            {
                TypeSerializationModel newTypeMetadata = new TypeSerializationModel(metadata);
                ReferenceSerializationModelMap.SerializedTypes.Add(id, newTypeMetadata);
                return newTypeMetadata;
            }
            else
            {
                ReferenceSerializationModelMap.SerializedTypes.TryGetValue(id, out TypeSerializationModel newTypeMetadata);
                return newTypeMetadata;
            }
        }
        
        public static TypeMetadata EmitTypeMetadata(TypeSerializationModel type)
        {
            long id = ReferenceMap.IdGenerator.GetId(type, out bool firstTime);
            if (firstTime)
            {
                TypeMetadata newTypeMetadata = type.ToModel();
                ReferenceMap.LoadedTypes.Add(id, newTypeMetadata);
                return newTypeMetadata;
            }
            else
            {
                ReferenceMap.LoadedTypes.TryGetValue(id, out TypeMetadata newTypeMetadata);
                return newTypeMetadata;
            }
        }
        
        public TypeMetadata ToModel()
        {
            TypeMetadata metadata = new TypeMetadata
            {
                TypeEnum = EnumMapper.ConvertEnum<TypeTypesEnum, TypeTypesSerializationModelEnum>(TypeEnum),
                TypeName = TypeName,
                NamespaceName = NamespaceName,
                GenericArguments = GenericArguments.Select(EmitTypeMetadata),
                
                Modifiers =
                    new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(
                        EnumMapper.ConvertEnum<AccessLevelEnum, AccessLevelSerializationModelEnum>(Modifiers.Item1),
                        EnumMapper.ConvertEnum<SealedEnum, SealedSerializationModelEnum>(Modifiers.Item2),
                        EnumMapper.ConvertEnum<AbstractEnum, AbstractSerializationModelEnum>(Modifiers.Item3)),
                
                Attributes = Attributes.Select(attributeMetadata => attributeMetadata.ToModel()),
                FullTypeName = FullTypeName,
                DeclaringType = EmitTypeMetadata(DeclaringType),
                BaseType = EmitTypeMetadata(BaseType),
                
                ImplementedInterfaces = ImplementedInterfaces.Select(EmitTypeMetadata),
                Fields = Fields.Select(typeMetadata => typeMetadata.ToModel()),
                Methods = Methods.Select(typeMetadata => typeMetadata.ToModel()),
                Properties = Properties.Select(typeMetadata => typeMetadata.ToModel()),
                Indexers = Indexers.Select(typeMetadata => typeMetadata.ToModel()),
                Events = Events.Select(typeMetadata => typeMetadata.ToModel()),
                Constructors = Constructors.Select(typeMetadata => typeMetadata.ToModel()),
                NestedTypes = NestedTypes.Select(EmitTypeMetadata)
            };

            return metadata;
        }

        protected bool Equals(TypeSerializationModel other)
        {
            return TypeEnum == other.TypeEnum && string.Equals(TypeName, other.TypeName) &&
                   string.Equals(NamespaceName, other.NamespaceName) && string.Equals(FullTypeName, other.FullTypeName);
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
                hashCode = (hashCode * 397) ^ (FullTypeName != null ? FullTypeName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
