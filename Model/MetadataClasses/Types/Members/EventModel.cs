namespace Model.MetadataClasses.Types.Members
{
    public class EventModel : MemberAbstract
    {
        public TypeModel TypeModel { get; set; }

        protected bool Equals(EventModel other)
        {
            return base.Equals(other) && Equals(TypeModel, other.TypeModel);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EventModel) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (TypeModel != null ? TypeModel.GetHashCode() : 0);
            }
        }
    }
}
