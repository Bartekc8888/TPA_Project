using System.Runtime.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata : MemberAbstract
    {

        public ParameterMetadata(string name, TypeBasicInfo typeMetadata) : base(name, typeMetadata)
        {
        }

        public ParameterMetadata() : base() { }
    }
}