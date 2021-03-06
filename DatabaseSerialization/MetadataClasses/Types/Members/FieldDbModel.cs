﻿using System.ComponentModel.DataAnnotations.Schema;
using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    [Table("Field")]
    public class FieldDbModel : MemberAbstractDbModel, ILateBinding
    {
        public int Id { get; set; }
        public TypeDbModel TypeModel { get; set; }
        
        public FieldDbModel()
        {
            
        }
        
        public FieldDbModel(FieldModel model) : base(model)
        {
            
        }
        
        public FieldModel ToModel()
        {
            FieldModel parameterModel = new FieldModel();
            parameterModel.TypeModel = TypeModel.ToModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static FieldDbModel EmitUniqueType(FieldModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new FieldDbModel(propertyModel));
        }

        public void LateBinding(object other)
        {
            FieldModel model = (FieldModel) other;
            TypeModel = TypeDbModel.EmitTypeDbModel(model.TypeModel);
        }
    }
}
