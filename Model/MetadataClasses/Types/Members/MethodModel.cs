using System;
using System.Collections.Generic;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types.Members
{
    public class MethodModel
    {
        public string Name { get; set; }
        public IEnumerable<TypeModel> GenericArguments { get; set; }
        public Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum> Modifiers { get; set; }
        public string ReturnType { get; set; }
        public bool Extension { get; set; }
        public IEnumerable<ParameterModel> Parameters { get; set; }

        protected bool Equals(MethodModel other)
        {
            return string.Equals(Name, other.Name) && string.Equals(ReturnType, other.ReturnType) &&
                   Equals(Parameters, other.Parameters);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MethodModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GenericArguments != null ? GenericArguments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Modifiers != null ? Modifiers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ReturnType != null ? ReturnType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Extension.GetHashCode();
                hashCode = (hashCode * 397) ^ (Parameters != null ? Parameters.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}