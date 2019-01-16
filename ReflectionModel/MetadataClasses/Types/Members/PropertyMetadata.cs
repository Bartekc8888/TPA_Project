using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReflectionModel.MetadataExtensions;

namespace Model.MetadataClasses.Types.Members
{
    public class PropertyMetadata : MemberAbstractMetadata
    {
        public MethodMetadata[] propertyMethods { get; set; }
        
        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {

            return from prop in props where prop.GetIndexParameters().Length == 0
                   select new PropertyMetadata(prop.Name, prop.PropertyType, prop.GetAccessors(true));
        }

        private PropertyMetadata(string propertyName, Type type, MethodInfo[] methods) : base(propertyName, type.Name)
        {
            propertyMethods = methods.Select(info => new MethodMetadata(info)).ToArray();
        }

        public PropertyMetadata() : base() { }

        public PropertyMetadata(PropertyModel model) : base(model)
        {
            propertyMethods = model.propertyMethods.Select(methodModel => new MethodMetadata(methodModel)).ToArray();
        }

        public PropertyModel ToModel()
        {
            PropertyModel propertyModel = new PropertyModel();
            FillModel(propertyModel);
            propertyModel.propertyMethods = propertyMethods.Select(model => model.ToModel()).ToArray();

            return propertyModel;
        }

        public static PropertyMetadata EmitUniqueType(PropertyModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new PropertyMetadata(propertyModel));
        }

        protected bool Equals(PropertyMetadata other)
        {
            return base.Equals(other) && Equals(propertyMethods, other.propertyMethods);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertyMetadata) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (propertyMethods != null ? propertyMethods.GetHashCode() : 0);
            }
        }
    }
}