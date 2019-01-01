using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class ConstructorSerializationModel : MethodSerializationModel
    {
        public ConstructorSerializationModel(ConstructorMetadata metadata) : base(metadata)
        {
        }
        
        public ConstructorMetadata ToModel()
        {
            MethodMetadata methodMetadata = base.ToModel();
            ConstructorMetadata constructorMetadata = new ConstructorMetadata(methodMetadata);
            return constructorMetadata;
        }
    }
}
