using System;
using System.Collections.Generic;
using System.Text;

namespace Model.MetadataClasses.Types.Members
{
    public abstract class MemberAbstract
    {
        public string Name { get; private set; }
        public TypeBasicInfo TypeMetadata { get; private set; }

        public MemberAbstract(string name, TypeBasicInfo typeInfo)
        {
            this.Name = name;
            this.TypeMetadata = typeInfo;
        }
    }
}
