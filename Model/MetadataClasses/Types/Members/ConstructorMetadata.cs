using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model.MetadataClasses.Types.Members
{
    public class ConstructorMetadata : MethodMetadata
    {
        public ConstructorMetadata(MethodBase method) : base(method)
        {
        }
        
        public ConstructorMetadata(MethodMetadata method) : base(method)
        {
        }

        public ConstructorMetadata() : base() { }

        internal static IEnumerable<ConstructorMetadata> EmitConstructors(IEnumerable<MethodBase> methods)
        {
            return from MethodBase currentMethod in methods
                   select new ConstructorMetadata(currentMethod);
        }
    }
}
