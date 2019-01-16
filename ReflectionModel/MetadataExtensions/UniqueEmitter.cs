using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionModel.MetadataExtensions
{
    class UniqueEmitter
    {
        public static T1 EmitType<T1, T2>(T2 model, Func<T2, T1> createType)
        {
            if (model != null)
            {
                bool contains = ReferenceMetadataMap.AllSerializedTypes.ContainsKey(model);
                if (!contains)
                {
                    T1 newTypeModel = createType(model);
                    ReferenceMetadataMap.AllSerializedTypes.Add(model, newTypeModel);
                    return newTypeModel;
                }
                else
                {
                    ReferenceMetadataMap.AllSerializedTypes.TryGetValue(model, out object newTypeModel);
                    return (T1)newTypeModel;
                }
            }
            else
            {
                return default(T1);
            }
        }
    }
}
