using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses.Types.Members
{
    public class EventMetadata : MemberAbstract
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
    }
}
