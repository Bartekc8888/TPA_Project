using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

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

        public static ConstructorSerializationModel EmitUniqueType(ConstructorMetadata metadata)
        {
            return UniqueEmitter.EmitType(metadata, propertyMetadata => new ConstructorSerializationModel(propertyMetadata));
        }
    }
}
