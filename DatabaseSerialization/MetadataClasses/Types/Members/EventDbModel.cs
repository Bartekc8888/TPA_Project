using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    public class EventDbModel : MemberAbstractDbModel
    {
        public EventDbModel(EventModel model) : base(model)
        {
        }
        
        public EventModel ToModel()
        {
            EventModel parameterModel = new EventModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static EventDbModel EmitUniqueType(EventModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new EventDbModel(propertyModel));
        }
    }
}
