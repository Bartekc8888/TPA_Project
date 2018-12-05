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
        AssemblyMetadata readFromFile(String filePath);
        void saveToFile(AssemblyMetadata context, String filePath);
    }
}
