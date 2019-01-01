using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.MetadataClasses.Types;
using SerializationModel.MetadataClasses.Types;

namespace SerializationModel.MetadataExtensions
{
    public static class ReferenceSerializationModelMap
    {
        public static Dictionary<object, object> AllSerializedTypes = new Dictionary<object, object>();
    }
}