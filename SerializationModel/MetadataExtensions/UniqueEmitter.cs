using System;

namespace SerializationModel.MetadataExtensions
{
    
    public static class UniqueEmitter
    {
        public static T1 EmitType<T1, T2>(T2 metadata, Func<T2, T1> createType)
        {
            bool contains = ReferenceSerializationModelMap.AllSerializedTypes.ContainsKey(metadata);
            if (!contains)
            {
                T1 newTypeMetadata = createType(metadata);
                ReferenceSerializationModelMap.AllSerializedTypes.Add(metadata, newTypeMetadata);
                return newTypeMetadata;
            }
            else
            {
                ReferenceSerializationModelMap.AllSerializedTypes.TryGetValue(metadata, out object newTypeMetadata);
                return (T1)newTypeMetadata;
            }
        }
    }
}