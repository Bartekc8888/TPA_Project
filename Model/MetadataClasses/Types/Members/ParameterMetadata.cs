using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    //[XmlRoot]
    [DataContract]
    public class ParameterMetadata : MemberAbstract
    {

        public ParameterMetadata(string name, TypeBasicInfo typeMetadata) : base(name, typeMetadata)
        {
        }

        public ParameterMetadata() : base() { }
    }
}