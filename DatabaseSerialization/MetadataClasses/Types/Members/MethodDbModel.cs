using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DatabaseSerialization.MetadataDefinitions;
using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using SerializationModel.MetadataDefinitions;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    [Table("Method")]
    public class MethodDbModel
    {
        public int Id { get; set; }
        #region vars
        public string Name { get; set; }
        public ICollection<TypeDbModel> GenericArguments { get; set; }
        public AccessLevelDbModelEnum Item1 { get; set; }
        public AbstractDbModelEnum Item2 { get; set; }
        public StaticDbModelEnum Item3 { get; set; }
        public VirtualDbModelEnum Item4 { get; set; }
        public OverrideDbModelEnum Item5 { get; set; }
        public string ReturnType { get; set; }
        public bool Extension { get; set; }
        public ICollection<ParameterDbModel> Parameters { get; set; }
        #endregion

        public MethodDbModel()
        {
            
        }
        
        public MethodDbModel(MethodModel model)
        {
            Name = model.Name;
            GenericArguments = model.GenericArguments == null ? null :
                model.GenericArguments.Select(TypeDbModel.EmitTypeDbModel).ToList();
            
            Item1 = EnumMapper.ConvertEnum<AccessLevelDbModelEnum, AccessLevelEnum>(model.Modifiers.Item1);
            Item2 = EnumMapper.ConvertEnum<AbstractDbModelEnum, AbstractEnum>(model.Modifiers.Item2);
            Item3 = EnumMapper.ConvertEnum<StaticDbModelEnum, StaticEnum>(model.Modifiers.Item3);
            Item4 = EnumMapper.ConvertEnum<VirtualDbModelEnum, VirtualEnum>(model.Modifiers.Item4);
            Item5 = EnumMapper.ConvertEnum<OverrideDbModelEnum, OverrideEnum>(model.Modifiers.Item5);

            ReturnType = model.ReturnType;
            Extension = model.Extension;
            Parameters =
                model.Parameters.Select(ParameterDbModel.EmitUniqueType).ToList();
        }

        public MethodModel ToModel()
        {
            MethodModel methodModel = new MethodModel();
            methodModel.Name = Name;

            methodModel.GenericArguments =
                GenericArguments?.Select(typeModel => typeModel.ToModel());

            methodModel.Modifiers = new Tuple<AccessLevelEnum, AbstractEnum, StaticEnum,
                VirtualEnum, OverrideEnum> (
                EnumMapper.ConvertEnum<AccessLevelEnum, AccessLevelDbModelEnum>(Item1),
                EnumMapper.ConvertEnum<AbstractEnum, AbstractDbModelEnum>(Item2),
                EnumMapper.ConvertEnum<StaticEnum, StaticDbModelEnum>(Item3),
                EnumMapper.ConvertEnum<VirtualEnum, VirtualDbModelEnum>(Item4),
                EnumMapper.ConvertEnum<OverrideEnum, OverrideDbModelEnum>(Item5)
            );

            methodModel.ReturnType = ReturnType;
            methodModel.Extension = Extension;
            methodModel.Parameters =
                Parameters?.Select(parameterModel => parameterModel.ToModel());

            return methodModel;
        }

        protected bool Equals(MethodDbModel other)
        {
            return string.Equals(Name, other.Name) && Equals(GenericArguments, other.GenericArguments) &&
                   Equals(ReturnType, other.ReturnType) &&
                   Extension == other.Extension && Equals(Parameters, other.Parameters);
        }

        public static MethodDbModel EmitUniqueType(MethodModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new MethodDbModel(propertyModel));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MethodDbModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GenericArguments != null ? GenericArguments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Extension.GetHashCode();
                hashCode = (hashCode * 397) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}