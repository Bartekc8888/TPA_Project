using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReflectionModel.MetadataExtensions;

namespace Model.MetadataClasses.Types.Members
{
    public class FieldMetadata : MemberAbstractMetadata
    {
        public TypeMetadata TypeMetadata { get; set; }

        internal static IEnumerable<FieldMetadata> EmitFields(IEnumerable<FieldInfo> fieldsInfo)
        {
            return from info in fieldsInfo
                   select new FieldMetadata(info.Name, info.FieldType);
        }

        private FieldMetadata(string propertyName, Type type) : base(propertyName, type.Name)
        {
            TypeMetadata = TypeMetadata.EmitReference(type);
        }

        public FieldMetadata() :base(){ }

        public FieldMetadata(FieldModel model) : base(model)
        {
            TypeMetadata = TypeMetadata.EmitTypeMetadata(model.TypeModel);
        }

        public FieldModel ToModel()
        {
            FieldModel parameterModel = new FieldModel();
            parameterModel.TypeModel = TypeMetadata.ToModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static FieldMetadata EmitUniqueType(FieldModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new FieldMetadata(propertyModel));
        }
    }
}
