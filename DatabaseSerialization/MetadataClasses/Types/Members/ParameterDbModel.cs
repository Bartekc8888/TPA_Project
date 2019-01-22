﻿using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    public class ParameterDbModel : MemberAbstractDbModel
    {
        public ParameterDbModel(ParameterModel model) : base(model)
        {
        }

        public ParameterModel ToModel()
        {
            ParameterModel parameterModel = new ParameterModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static ParameterDbModel EmitUniqueType(ParameterModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new ParameterDbModel(propertyModel));
        }
    }
}