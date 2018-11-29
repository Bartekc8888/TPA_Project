using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Model.MetadataClasses.Types.Members
{
    //[XmlRoot]
    [DataContract]
    public class IndexerMetadata : MemberAbstract
    {
        internal static IEnumerable<IndexerMetadata> EmitIndexers(IEnumerable<PropertyInfo> indxs)
        {
            return from indx in indxs where indx.GetIndexParameters().Length!=0
                   select new IndexerMetadata(indx.Name, indx.PropertyType);
        }

        private IndexerMetadata(string indexerName, Type type) : base(indexerName, TypeBasicInfo.EmitReference(type))
        {
        }

        public IndexerMetadata() : base() { }
    }
}
