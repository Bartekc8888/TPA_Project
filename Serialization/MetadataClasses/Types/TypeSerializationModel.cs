using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types;
using Model.MetadataDefinitions;
using Serialization.MetadataClasses.Types.Members;
using Serialization.MetadataDefinitions;
using Serialization.MetadataExtensions;

namespace Serialization.MetadataClasses.Types
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

        public TypeSerializationModel(TypeModel model)
        {
            TypeEnum = EnumMapper.ConvertEnum<TypeTypesSerializationModelEnum, TypeTypesEnum>(model.TypeEnum);
            TypeName = model.TypeName;
            NamespaceName = model.NamespaceName;

            GenericArguments = model.GenericArguments == null ? null :
                model.GenericArguments.Select(EmitTypeSerializationModel);

            Modifiers = new Tuple<AccessLevelSerializationModelEnum, SealedSerializationModelEnum, AbstractSerializationModelEnum> (
                EnumMapper.ConvertEnum<AccessLevelSerializationModelEnum, AccessLevelEnum>(model.Modifiers.Item1),
                EnumMapper.ConvertEnum<SealedSerializationModelEnum, SealedEnum>(model.Modifiers.Item2),
                EnumMapper.ConvertEnum<AbstractSerializationModelEnum, AbstractEnum>(model.Modifiers.Item3));

            Attributes = model.Attributes.Select(AttributeSerializationModel.EmitUniqueType);
            FullTypeName = model.FullTypeName;
            
            DeclaringType = model.DeclaringType == null ? null : EmitTypeSerializationModel(model.DeclaringType);
            BaseType = model.BaseType == null ? null : EmitTypeSerializationModel(model.BaseType);

            ImplementedInterfaces =
                model.ImplementedInterfaces.Select(EmitTypeSerializationModel);
            Fields =
                model.Fields.Select(FieldSerializationModel.EmitUniqueType);
            Methods =
                model.Methods.Select(MethodSerializationModel.EmitUniqueType);
            Properties =
                model.Properties.Select(PropertySerializationModel.EmitUniqueType);
            Indexers =
                model.Indexers.Select(IndexerSerializationModel.EmitUniqueType);
            Events =
                model.Events.Select(EventSerializationModel.EmitUniqueType);
            Constructors =
                model.Constructors.Select(ConstructorSerializationModel.EmitUniqueType);
            NestedTypes =
                model.NestedTypes.Select(EmitTypeSerializationModel);
        }

        public static TypeSerializationModel EmitTypeSerializationModel(TypeModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new TypeSerializationModel(propertyModel));
        }
        
        public static TypeModel EmitTypeModel(TypeSerializationModel type)
        {
            return UniqueEmitter.EmitType(type, propertyModel => propertyModel.ToModel());
        }
        
        public TypeModel ToModel()
        {
            TypeModel model = new TypeModel();
            model.TypeEnum = EnumMapper.ConvertEnum<TypeTypesEnum, TypeTypesSerializationModelEnum>(TypeEnum);
            model.TypeName = TypeName;
            model.NamespaceName = NamespaceName;
            model.GenericArguments = GenericArguments?.Select(EmitTypeModel);

            model.Modifiers = new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(
                EnumMapper.ConvertEnum<AccessLevelEnum, AccessLevelSerializationModelEnum>(Modifiers.Item1),
                EnumMapper.ConvertEnum<SealedEnum, SealedSerializationModelEnum>(Modifiers.Item2),
                EnumMapper.ConvertEnum<AbstractEnum, AbstractSerializationModelEnum>(Modifiers.Item3));

            model.Attributes = Attributes?.Select(attributeMetadata => attributeMetadata.ToModel());
            model.FullTypeName = FullTypeName;
            model.DeclaringType = DeclaringType == null ? null : EmitTypeModel(DeclaringType);
            model.BaseType = DeclaringType == null ? null : EmitTypeModel(BaseType);
            model.ImplementedInterfaces = ImplementedInterfaces?.Select(EmitTypeModel);
            model.Fields = Fields?.Select(typeModel => typeModel.ToModel());
            model.Methods = Methods?.Select(typeModel => typeModel.ToModel());
            model.Properties = Properties?.Select(typeModel => typeModel.ToModel());
            model.Indexers = Indexers?.Select(typeModel => typeModel.ToModel());
            model.Events = Events?.Select(typeModel => typeModel.ToModel());
            model.Constructors = Constructors?.Select(typeModel => typeModel.ToModel());
            model.NestedTypes = NestedTypes?.Select(EmitTypeModel);

            return model;
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
