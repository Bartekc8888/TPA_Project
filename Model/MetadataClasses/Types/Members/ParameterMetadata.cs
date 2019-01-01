namespace Model.MetadataClasses.Types.Members
{
    public class ParameterMetadata : MemberAbstract
    {

        public ParameterMetadata(string name, TypeMetadata typeMetadata) : base(name, typeMetadata)
        {
        }

        public ParameterMetadata() : base() { }
        
        
    }
}