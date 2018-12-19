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
        AssemblyMetadata Read(String filePath);
        void Save(AssemblyMetadata context, String filePath);
    }
}
