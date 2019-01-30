using System.Runtime.Serialization;
using Model.MetadataClasses.Types.Members;
using Serialization.MetadataExtensions;

namespace Serialization.MetadataClasses.Types.Members
{
    [DataContract(IsReference = true)]
    public class EventSerializationModel : MemberAbstractSerializationModel
    {
        public EventSerializationModel(EventModel model) : base(model)
        {
        }
        
        public EventModel ToModel()
        {
            EventModel parameterModel = new EventModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static EventSerializationModel EmitUniqueType(EventModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new EventSerializationModel(propertyModel));
        }
    }
}
