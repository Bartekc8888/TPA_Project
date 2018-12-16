using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types;

namespace Model.MetadataExtensions
{
    public class ReferenceMap
    {
        public static ObjectIDGenerator IdGenerator = new ObjectIDGenerator();
        public static Dictionary<long, TypeMetadata> LoadedTypes = new Dictionary<long, TypeMetadata>();
    }
}