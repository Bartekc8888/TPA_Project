using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types;
using SerializationModel.MetadataClasses.Types;

namespace SerializationModel.MetadataExtensions
{
    public class ReferenceSerializationModelMap
    {
        public static ObjectIDGenerator IdGenerator = new ObjectIDGenerator();
        public static Dictionary<long, TypeSerializationModel> SerializedTypes = new Dictionary<long, TypeSerializationModel>();
        public static Dictionary<long, TypeMetadata> LoadedTypes = new Dictionary<long, TypeMetadata>();
    }
}