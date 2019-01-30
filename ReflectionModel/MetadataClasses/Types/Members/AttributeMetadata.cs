using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReflectionModel.MetadataExtensions;

namespace Model.MetadataClasses.Types.Members
{    public class AttributeMetadata : MemberAbstractMetadata
    {
        internal static IEnumerable<AttributeMetadata> EmitAttributes(Type type)
        {
            return from attrib in type.GetCustomAttributesData()
                   select new AttributeMetadata(attrib.ToString());
        }
        
        public AttributeMetadata(string name) : base(name)
        {
        }
        
        public AttributeMetadata() : base() { }

        public AttributeMetadata(AttributeModel model) : base(model)
        {
        }

        public AttributeModel ToModel()
        {
            AttributeModel parameterModel = new AttributeModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static AttributeMetadata EmitUniqueType(AttributeModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new AttributeMetadata(propertyModel));
        }
    }

}
