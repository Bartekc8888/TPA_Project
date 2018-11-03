using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses.Types.Members
{
    internal class EventMetadata
    {
        private string m_Name;
        private TypeBasicInfo m_TypeMetadata;

        internal static IEnumerable<EventMetadata> EmitEvents(IEnumerable<EventInfo> eventsInfo)
        {
            return from info in eventsInfo
                   select new EventMetadata(info.Name, info);
        }

        private EventMetadata(string propertyName, EventInfo info)
        {
            m_Name = propertyName;
            // TODO implement event
        }
    }
}
