using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DatabaseSerialization.MetadataClasses.Types;
using Model.MetadataClasses;

namespace DatabaseSerialization.MetadataClasses
{
    [Table("Namespace")]
    public class NamespaceDbModel
    {
        public int Id { get; set; }
        public string NamespaceName { get; set; }
        public ICollection<TypeDbModel> Types { get; set; }

        public NamespaceDbModel()
        {
            
        }
        
        public NamespaceDbModel(NamespaceModel model)
        {
            NamespaceName = model.NamespaceName;
            Types = model.Types.Select(TypeDbModel.EmitTypeDbModel).ToList();
        }
        
        public NamespaceModel ToModel()
        {
            NamespaceModel namespaceModel = new NamespaceModel();
            namespaceModel.NamespaceName = NamespaceName;
            namespaceModel.Types = Types.Select(model => model.ToModel());
            return namespaceModel;
        }

        protected bool Equals(NamespaceDbModel other)
        {
            return string.Equals(NamespaceName, other.NamespaceName) && Equals(Types, other.Types);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NamespaceDbModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((NamespaceName != null ? NamespaceName.GetHashCode() : 0) * 397) ^ (Types != null ? Types.GetHashCode() : 0);
            }
        }
    }
}