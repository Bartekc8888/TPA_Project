using Model.MetadataClasses;
using System;

namespace Model.ExtractionTools
{
    public static class TypeMetadataFactory
    {
        public static TypeMetadata CreateTypeMetadataClass(Type type)
        {
            return new TypeMetadata(type);
        }
    }
}
