
namespace Model.MetadataClasses
{
    internal class ParameterMetadata
    {
        private string m_Name;
        private TypeMetadata m_TypeMetadata;

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }
    }
}