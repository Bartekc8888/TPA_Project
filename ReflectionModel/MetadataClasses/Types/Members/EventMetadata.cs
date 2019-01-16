using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReflectionModel.MetadataExtensions;

namespace Model.MetadataClasses.Types.Members
{
    public class EventMetadata : MemberAbstractMetadata
    {
        public TypeMetadata TypeMetadata { get; set; }
        internal static IEnumerable<EventMetadata> EmitEvents(IEnumerable<EventInfo> eventsInfo)
        {
            return from info in eventsInfo
                   select new EventMetadata(info.Name, info);
        }

        private EventMetadata(string propertyName, EventInfo info) : base(propertyName, info.Name)
        {
            TypeMetadata = TypeMetadata.EmitReference(info.EventHandlerType);
        }

        public EventMetadata() : base() { }

        public EventMetadata(EventModel model) : base(model)
        {
        }

        public EventModel ToModel()
        {
            EventModel parameterModel = new EventModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static EventMetadata EmitUniqueType(EventModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new EventMetadata(propertyModel));
        }
    }
}
