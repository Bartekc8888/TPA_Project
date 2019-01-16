namespace Model.MetadataClasses.Types.Members
{
    public class PropertyModel : MemberAbstract
    {
        public MethodModel[] propertyMethods { get; set; }

        protected bool Equals(PropertyModel other)
        {
            return Equals(propertyMethods, other.propertyMethods);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PropertyModel) obj);
        }

        public override int GetHashCode()
        {
            return (propertyMethods != null ? propertyMethods.GetHashCode() : 0);
        }
    }
}