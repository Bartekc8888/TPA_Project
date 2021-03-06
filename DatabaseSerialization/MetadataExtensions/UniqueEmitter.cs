using System;
using System.Collections;

namespace DatabaseSerialization.MetadataExtensions
{
    
    public static class UniqueEmitter
    {
        public static T1 EmitType<T1, T2>(T2 model, Func<T2, T1> createType)
        {
            if (model != null)
            {
                bool contains = ReferenceDbModelMap.AllSerializedTypes.ContainsKey(model);
                if (!contains)
                {
                    T1 newTypeModel = createType(model);
                    ReferenceDbModelMap.AllSerializedTypes.Add(model, newTypeModel);
                    if (((IList) typeof(T1).GetInterfaces()).Contains(typeof(ILateBinding)))
                    {
                        ((ILateBinding)newTypeModel).LateBinding(model);
                    }
                    return newTypeModel;
                }
                else
                {
                    ReferenceDbModelMap.AllSerializedTypes.TryGetValue(model, out object newTypeModel);
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