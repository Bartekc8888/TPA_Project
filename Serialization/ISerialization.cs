using Model.MetadataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public interface ISerialization
    {
        AssemblyMetadata Read(String path);
        void Save(AssemblyMetadata context, String path);
    }
}
