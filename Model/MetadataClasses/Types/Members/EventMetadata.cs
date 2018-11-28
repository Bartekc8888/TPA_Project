using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    [XmlRoot]
    public class EventMetadata : MemberAbstract
    {
        internal static IEnumerable<EventMetadata> EmitEvents(IEnumerable<EventInfo> eventsInfo)
        {
            return from info in eventsInfo
                   select new EventMetadata(info.Name, info);
        }

        private EventMetadata(string propertyName, EventInfo info) : base(propertyName, TypeBasicInfo.EmitReference(info.EventHandlerType))
        {
        }

        public EventMetadata() : base() { }
    }
}
