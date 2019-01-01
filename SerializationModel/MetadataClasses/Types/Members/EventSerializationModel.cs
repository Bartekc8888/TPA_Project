using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class EventSerializationModel : MemberAbstractSerializationModel
    {
        public EventSerializationModel(EventMetadata metadata) : base(metadata)
        {
        }
        
        public EventMetadata ToModel()
        {
            EventMetadata parameterMetadata = new EventMetadata();
            FillModel(parameterMetadata);
            return parameterMetadata;
        }

        public static EventSerializationModel EmitUniqueType(EventMetadata metadata)
        {
            return UniqueEmitter.EmitType(metadata, propertyMetadata => new EventSerializationModel(propertyMetadata));
        }
    }
}
