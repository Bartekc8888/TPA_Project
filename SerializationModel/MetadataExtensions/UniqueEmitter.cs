using System;

namespace SerializationModel.MetadataExtensions
{
    
    public static class UniqueEmitter
    {
        public static T1 EmitType<T1, T2>(T2 model, Func<T2, T1> createType)
        {
            if (model != null)
            {
                bool contains = ReferenceSerializationModelMap.AllSerializedTypes.ContainsKey(model);
                if (!contains)
                {
                    T1 newTypeModel = createType(model);
                    ReferenceSerializationModelMap.AllSerializedTypes.Add(model, newTypeModel);
                    return newTypeModel;
                }
                else
                {
                    ReferenceSerializationModelMap.AllSerializedTypes.TryGetValue(model, out object newTypeModel);
                    return (T1) newTypeModel;
                }
            }
            else
            {
                return default(T1);
            }
        }
    }
}