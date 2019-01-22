using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseSerialization.MetadataClasses.Types.Members;
using DatabaseSerialization.MetadataDefinitions;
using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types;
using Model.MetadataDefinitions;
using SerializationModel.MetadataDefinitions;

namespace DatabaseSerialization.MetadataClasses.Types
{
    public class TypeDbModel
    {
        #region fields
        public TypeTypesDbModelEnum TypeEnum { get; set; }

        public string TypeName { get; set; }
        public string NamespaceName { get; set; }
        public IEnumerable<TypeDbModel> GenericArguments { get; set; }
        public Tuple<AccessLevelDbModelEnum, SealedDbModelEnum, AbstractDbModelEnum> Modifiers { get; set; }
        public IEnumerable<AttributeDbModel> Attributes { get; set; }
        public string FullTypeName { get; set; }
        
        public TypeDbModel DeclaringType { get; set; }

        public TypeDbModel BaseType { get; set; }
        public IEnumerable<TypeDbModel> ImplementedInterfaces { get; set; }

        public IEnumerable<FieldDbModel> Fields { get; set; }
        public IEnumerable<MethodDbModel> Methods { get; set; }
        public IEnumerable<PropertyDbModel> Properties { get; set; }
        public IEnumerable<IndexerDbModel> Indexers { get; set; }
        public IEnumerable<EventDbModel> Events { get; set; }
        public IEnumerable<ConstructorDbModel> Constructors { get; set; }
        public IEnumerable<TypeDbModel> NestedTypes { get; set; }
        #endregion

        public TypeDbModel(TypeModel model)
        {
            TypeEnum = EnumMapper.ConvertEnum<TypeTypesDbModelEnum, TypeTypesEnum>(model.TypeEnum);
            TypeName = model.TypeName;
            NamespaceName = model.NamespaceName;

            GenericArguments = model.GenericArguments == null ? null :
                model.GenericArguments.Select(EmitTypeDbModel);

            Modifiers = new Tuple<AccessLevelDbModelEnum, SealedDbModelEnum, AbstractDbModelEnum> (
                EnumMapper.ConvertEnum<AccessLevelDbModelEnum, AccessLevelEnum>(model.Modifiers.Item1),
                EnumMapper.ConvertEnum<SealedDbModelEnum, SealedEnum>(model.Modifiers.Item2),
                EnumMapper.ConvertEnum<AbstractDbModelEnum, AbstractEnum>(model.Modifiers.Item3));

            Attributes = model.Attributes.Select(AttributeDbModel.EmitUniqueType);
            FullTypeName = model.FullTypeName;
            
            DeclaringType = model.DeclaringType == null ? null : EmitTypeDbModel(model.DeclaringType);
            BaseType = model.BaseType == null ? null : EmitTypeDbModel(model.BaseType);

            ImplementedInterfaces =
                model.ImplementedInterfaces.Select(EmitTypeDbModel);
            Fields =
                model.Fields.Select(FieldDbModel.EmitUniqueType);
            Methods =
                model.Methods.Select(MethodDbModel.EmitUniqueType);
            Properties =
                model.Properties.Select(PropertyDbModel.EmitUniqueType);
            Indexers =
                model.Indexers.Select(IndexerDbModel.EmitUniqueType);
            Events =
                model.Events.Select(EventDbModel.EmitUniqueType);
            Constructors =
                model.Constructors.Select(ConstructorDbModel.EmitUniqueType);
            NestedTypes =
                model.NestedTypes.Select(EmitTypeDbModel);
        }

        public static TypeDbModel EmitTypeDbModel(TypeModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new TypeDbModel(propertyModel));
        }
        
        public static TypeModel EmitTypeModel(TypeDbModel type)
        {
            return UniqueEmitter.EmitType(type, propertyModel => propertyModel.ToModel());
        }
        
        public TypeModel ToModel()
        {
            TypeModel model = new TypeModel();
            model.TypeEnum = EnumMapper.ConvertEnum<TypeTypesEnum, TypeTypesDbModelEnum>(TypeEnum);
            model.TypeName = TypeName;
            model.NamespaceName = NamespaceName;
            model.GenericArguments = GenericArguments?.Select(EmitTypeModel);

            model.Modifiers = new Tuple<AccessLevelEnum, SealedEnum, AbstractEnum>(
                EnumMapper.ConvertEnum<AccessLevelEnum, AccessLevelDbModelEnum>(Modifiers.Item1),
                EnumMapper.ConvertEnum<SealedEnum, SealedDbModelEnum>(Modifiers.Item2),
                EnumMapper.ConvertEnum<AbstractEnum, AbstractDbModelEnum>(Modifiers.Item3));

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

        protected bool Equals(TypeDbModel other)
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
            return Equals((TypeDbModel) obj);
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
