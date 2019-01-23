using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
//    [Table("Property")]
    public class PropertyDbModel : MemberAbstractDbModel
    {
        public int Id { get; set; }
        public ICollection<MethodDbModel> propertyMethods { get; set; }
       
        public PropertyDbModel(PropertyModel model) : base(model)
        {
            propertyMethods = model.propertyMethods.Select(methodModel => new MethodDbModel(methodModel)).ToArray();
        }

        public PropertyModel ToModel()
        {
            PropertyModel propertyModel = new PropertyModel();
            FillModel(propertyModel);
            propertyModel.propertyMethods = propertyMethods.Select(model => model.ToModel()).ToArray();

            return propertyModel;
        }
        
        protected bool Equals(PropertyDbModel other)
        {
            return base.Equals(other) && Equals(propertyMethods, other.propertyMethods);
        }

        public static PropertyDbModel EmitUniqueType(PropertyModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new PropertyDbModel(propertyModel));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertyDbModel) obj);
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