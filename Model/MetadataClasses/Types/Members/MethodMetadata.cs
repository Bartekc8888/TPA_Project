using System;
using System.Collections.Generic;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types.Members
{
    public class MethodMetadata
    {
        public string name;
        public IEnumerable<TypeMetadata> genericArguments;
        public Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum> modifiers;
        public string returnType;
        public bool extension;
        public IEnumerable<ParameterMetadata> parameters;
    }
}