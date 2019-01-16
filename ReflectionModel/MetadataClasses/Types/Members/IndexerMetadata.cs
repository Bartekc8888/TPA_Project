using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReflectionModel.MetadataExtensions;

namespace Model.MetadataClasses.Types.Members
{
    public class IndexerMetadata : MemberAbstractMetadata
    {
        internal static IEnumerable<IndexerMetadata> EmitIndexers(IEnumerable<PropertyInfo> indxs)
        {
            return from indx in indxs where indx.GetIndexParameters().Length!=0
                   select new IndexerMetadata(indx.Name, indx.PropertyType);
        }

        private IndexerMetadata(string indexerName, Type type) : base(indexerName, type.Name)
        {
        }

        public IndexerMetadata() : base() { }

        public IndexerMetadata(IndexerModel model) : base(model)
        {
        }

        public IndexerModel ToModel()
        {
            IndexerModel parameterModel = new IndexerModel();
            FillModel(parameterModel);
            return parameterModel;
        }

        public static IndexerMetadata EmitUniqueType(IndexerModel model)
        {
            return UniqueEmitter.EmitType(model, propertyModel => new IndexerMetadata(propertyModel));
        }
    }
}
