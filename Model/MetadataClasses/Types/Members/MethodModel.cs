using System;
using System.Collections.Generic;
using Model.MetadataDefinitions;

namespace Model.MetadataClasses.Types.Members
{
    public class MethodModel
    {
        public string Name { get; set; }
        public IEnumerable<TypeModel> GenericArguments { get; set; }
        public Tuple<AccessLevelEnum, AbstractEnum, StaticEnum, VirtualEnum, OverrideEnum> Modifiers { get; set; }
        public string ReturnType { get; set; }
        public bool Extension { get; set; }
        public IEnumerable<ParameterModel> Parameters { get; set; }
    }
}