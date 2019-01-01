using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using SerializationModel.MetadataDefinitions;

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
        public TypeSerializationModel ReturnType { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public bool Extension { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public IEnumerable<ParameterSerializationModel> Parameters { get; set; }
        #endregion

        public MethodSerializationModel(MethodMetadata metadata)
        {
            Name = metadata.Name;
            GenericArguments =
                metadata.GenericArguments.Select(typeMetadata => new TypeSerializationModel(typeMetadata));
            
            Modifiers = new Tuple<AccessLevelSerializationModelEnum, AbstractSerializationModelEnum, StaticSerializationModelEnum,
                VirtualSerializationModelEnum, OverrideSerializationModelEnum> (
                EnumMapper.ConvertEnum<AccessLevelSerializationModelEnum, AccessLevelEnum>(metadata.Modifiers.Item1),
                EnumMapper.ConvertEnum<AbstractSerializationModelEnum, AbstractEnum>(metadata.Modifiers.Item2),
                EnumMapper.ConvertEnum<StaticSerializationModelEnum, StaticEnum>(metadata.Modifiers.Item3),
                EnumMapper.ConvertEnum<VirtualSerializationModelEnum, VirtualEnum>(metadata.Modifiers.Item4),
                EnumMapper.ConvertEnum<OverrideSerializationModelEnum, OverrideEnum>(metadata.Modifiers.Item5)
                );

            ReturnType = new TypeSerializationModel(metadata.ReturnType);
            Extension = metadata.Extension;
            Parameters =
                metadata.Parameters.Select(parameterMetadata => new ParameterSerializationModel(parameterMetadata));
        }

        public MethodMetadata ToModel()
        {
            MethodMetadata methodMetadata = new MethodMetadata();
            methodMetadata.Name = Name;
            
            methodMetadata.GenericArguments =
                GenericArguments.Select(typeMetadata => typeMetadata.ToModel());
            
            methodMetadata.Modifiers = new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum,
                VirtualEnum, OverrideEnum> (
                EnumMapper.ConvertEnum<AccessLevelEnum, AccessLevelSerializationModelEnum>(Modifiers.Item1),
                EnumMapper.ConvertEnum<AbstractEnum, AbstractSerializationModelEnum>(Modifiers.Item2),
                EnumMapper.ConvertEnum<StaticEnum, StaticSerializationModelEnum>(Modifiers.Item3),
                EnumMapper.ConvertEnum<VirtualEnum, VirtualSerializationModelEnum>(Modifiers.Item4),
                EnumMapper.ConvertEnum<OverrideEnum, OverrideSerializationModelEnum>(Modifiers.Item5)
            );

            methodMetadata.ReturnType = ReturnType.ToModel();
            methodMetadata.Extension = Extension;
            methodMetadata.Parameters =
                Parameters.Select(parameterMetadata => parameterMetadata.ToModel());

            return methodMetadata;
        }
        
        protected bool Equals(MethodSerializationModel other)
        {
            return string.Equals(Name, other.Name) && Equals(GenericArguments, other.GenericArguments) &&
                   Equals(Modifiers, other.Modifiers) && Equals(ReturnType, other.ReturnType) &&
                   Equals(Parameters, other.Parameters);
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
                hashCode = (hashCode * 397) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}