using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Model.MetadataClasses;

namespace DatabaseSerialization.MetadataClasses
{
//    [Table("Assembly")]
    public class AssemblyDbModel
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public ICollection<NamespaceDbModel> Namespaces { get; set; }

        public AssemblyDbModel(AssemblyModel model)
        {
            TypeName = model.TypeName;
            Name = model.Name;
            Namespaces = model.Namespaces.Select(namespaceModel => new NamespaceDbModel(namespaceModel)).ToList();
        }

        public AssemblyModel ToModel()
        {
            AssemblyModel assemblyModel = new AssemblyModel();
            assemblyModel.TypeName = TypeName;
            assemblyModel.Name = Name;
            assemblyModel.Namespaces = Namespaces.Select(model => model.ToModel());

            return assemblyModel;
        }

        protected bool Equals(AssemblyDbModel other)
        {
            return string.Equals(TypeName, other.TypeName) && string.Equals(Name, other.Name) && Equals(Namespaces, other.Namespaces);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AssemblyDbModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (TypeName != null ? TypeName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Namespaces != null ? Namespaces.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}