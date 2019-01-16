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
        AssemblyModel Read(String path);
        void Save(AssemblyModel context, String path);
    }
}
