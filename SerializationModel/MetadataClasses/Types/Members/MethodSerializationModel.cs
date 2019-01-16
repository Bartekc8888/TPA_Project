using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using SerializationModel.MetadataDefinitions;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class MethodSerializationModel
    {
        #region vars
        [DataMember(EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<TypeSerializationModel> GenericArguments { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public Tuple<AccessLevelSerializationModelEnum, AbstractSerializationModelEnum, StaticSerializationModelEnum,
            VirtualSerializationModelEnum, OverrideSerializationModelEnum> Modifiers { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string ReturnType { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool Extension { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<ParameterSerializationModel> Parameters { get; set; }
        #endregion

        public MethodSerializationModel(MethodModel model)
        {
            Name = model.Name;
            GenericArguments = model.GenericArguments == null ? null :
                model.GenericArguments.Select(TypeSerializationModel.EmitTypeSerializationModel);
            
            Modifiers = new Tuple<AccessLevelSerializationModelEnum, AbstractSerializationModelEnum, StaticSerializationModelEnum,
                VirtualSerializationModelEnum, OverrideSerializationModelEnum> (
                EnumMapper.ConvertEnum<AccessLevelSerializationModelEnum, AccessLevelEnum>(model.Modifiers.Item1),
                EnumMapper.ConvertEnum<AbstractSerializationModelEnum, AbstractEnum>(model.Modifiers.Item2),
                EnumMapper.ConvertEnum<StaticSerializationModelEnum, StaticEnum>(model.Modifiers.Item3),
                EnumMapper.ConvertEnum<VirtualSerializationModelEnum, VirtualEnum>(model.Modifiers.Item4),
                EnumMapper.ConvertEnum<OverrideSerializationModelEnum, OverrideEnum>(model.Modifiers.Item5)
                );

            ReturnType = model.ReturnType;
            Extension = model.Extension;
            Parameters =
                model.Parameters.Select(ParameterSerializationModel.EmitUniqueType);
        }

        public MethodModel ToModel()
        {
            MethodModel methodModel = new MethodModel();
            methodModel.Name = Name;

            methodModel.GenericArguments =
                GenericArguments?.Select(typeModel => typeModel.ToModel());

            methodModel.Modifiers = new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum,
                VirtualEnum, OverrideEnum> (
                EnumMapper.ConvertEnum<AccessLevelEnum, AccessLevelSerializationModelEnum>(Modifiers.Item1),
                EnumMapper.ConvertEnum<AbstractEnum, AbstractSerializationModelEnum>(Modifiers.Item2),
                EnumMapper.ConvertEnum<StaticEnum, StaticSerializationModelEnum>(Modifiers.Item3),
                EnumMapper.ConvertEnum<VirtualEnum, VirtualSerializationModelEnum>(Modifiers.Item4),
                EnumMapper.ConvertEnum<OverrideEnum, OverrideSerializationModelEnum>(Modifiers.Item5)
            );

            methodModel.ReturnType = ReturnType;
            methodModel.Extension = Extension;
            methodModel.Parameters =
                Parameters?.Select(parameterModel => parameterModel.ToModel());

            return methodModel;
        }

        protected bool Equals(MethodSerializationModel other)
        {
            return string.Equals(Name, other.Name) && Equals(GenericArguments, other.GenericArguments) &&
                   Equals(Modifiers, other.Modifiers) && Equals(ReturnType, other.ReturnType) &&
                   Extension == other.Extension && Equals(Parameters, other.Parameters);
        }

        public static MethodSerializationModel EmitUniqueType(MethodModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new MethodSerializationModel(propertyModel));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MethodSerializationModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GenericArguments != null ? GenericArguments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Modifiers != null ? Modifiers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Extension.GetHashCode();
                hashCode = (hashCode * 397) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}