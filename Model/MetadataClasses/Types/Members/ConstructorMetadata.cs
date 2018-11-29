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
    public class ConstructorMetadata : MethodMetadata
    {
        public ConstructorMetadata(MethodBase method) : base(method)
        {
        }

        public ConstructorMetadata() : base() { }

        internal static IEnumerable<ConstructorMetadata> EmitConstructors(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   select new ConstructorMetadata(_currentMethod);
        }
    }
}
