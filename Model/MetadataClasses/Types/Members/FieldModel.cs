namespace Model.MetadataClasses.Types.Members
{
    public class FieldModel : MemberAbstract
    {
        public TypeModel TypeModel { get; set; }

        protected bool Equals(FieldModel other)
        {
            return Equals(TypeModel, other.TypeModel);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FieldModel) obj);
        }

        public override int GetHashCode()
        {
            return (TypeModel != null ? TypeModel.GetHashCode() : 0);
        }
    }
}
