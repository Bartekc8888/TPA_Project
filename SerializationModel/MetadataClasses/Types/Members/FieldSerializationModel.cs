using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using SerializationModel.MetadataExtensions;

namespace SerializationModel.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class FieldSerializationModel : MemberAbstractSerializationModel
    {
        [DataMember(EmitDefaultValue = false)]
        public TypeSerializationModel TypeMetadata { get; set; }
        
        public FieldSerializationModel(FieldMetadata metadata) : base(metadata)
        {
            TypeMetadata = TypeSerializationModel.EmitTypeSerializationModel(metadata.TypeMetadata);
        }
        
        public FieldMetadata ToModel()
        {
            FieldMetadata parameterMetadata = new FieldMetadata();
            parameterMetadata.TypeMetadata = TypeMetadata.ToModel();
            FillModel(parameterMetadata);
            return parameterMetadata;
        }

        public static FieldSerializationModel EmitUniqueType(FieldMetadata metadata)
        {
            return UniqueEmitter.EmitType(metadata, propertyMetadata => new FieldSerializationModel(propertyMetadata));
        }
    }
}
