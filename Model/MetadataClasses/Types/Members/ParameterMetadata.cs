namespace Model.MetadataClasses.Types.Members
{
    public class ParameterMetadata : MemberAbstract
    {
        private string m_Name;
        private TypeBasicInfo m_TypeMetadata;

        public ParameterMetadata(string name, TypeBasicInfo typeMetadata) : base(name, typeMetadata)
        {
        }
    }
}