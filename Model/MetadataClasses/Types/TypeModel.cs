using System;
using System.Collections.Generic;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types
{
    public class TypeModel
    {
        public TypeTypesEnum TypeEnum { get; set; }
        public string TypeName { get; set; }
        public string NamespaceName { get; set; }
        public IEnumerable<TypeModel> GenericArguments { get; set; }
        public Tuple<AccessLevelEnum, SealedEnum, AbstractEnum> Modifiers { get; set; }
        public IEnumerable<AttributeModel> Attributes { get; set; }
        public string FullTypeName { get; set; }
        public TypeModel DeclaringType { get; set; }
        public TypeModel BaseType { get; set; }
        public IEnumerable<TypeModel> ImplementedInterfaces { get; set; }
        public IEnumerable<FieldModel> Fields { get; set; }
        public IEnumerable<MethodModel> Methods { get; set; }
        public IEnumerable<PropertyModel> Properties { get; set; }
        public IEnumerable<IndexerModel> Indexers { get; set; }
        public IEnumerable<EventModel> Events { get; set; }
        public IEnumerable<ConstructorModel> Constructors { get; set; }
        public IEnumerable<TypeModel> NestedTypes { get; set; }

        protected bool Equals(TypeModel other)
        {
            return string.Equals(TypeName, other.TypeName) && string.Equals(NamespaceName, other.NamespaceName) &&
                   string.Equals(FullTypeName, other.FullTypeName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (TypeName != null ? TypeName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NamespaceName != null ? NamespaceName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FullTypeName != null ? FullTypeName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
