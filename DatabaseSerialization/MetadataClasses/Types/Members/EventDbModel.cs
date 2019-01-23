using System.ComponentModel.DataAnnotations.Schema;
using DatabaseSerialization.MetadataExtensions;
using Model.MetadataClasses.Types.Members;

namespace DatabaseSerialization.MetadataClasses.Types.Members
{
    [Table("Event")]
    public class EventDbModel : MemberAbstractDbModel
    {
        public int Id { get; set; }
        
        public EventDbModel()
        {
            
        }
        
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
