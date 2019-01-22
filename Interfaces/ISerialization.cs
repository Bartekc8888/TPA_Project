using System;
using Model.MetadataClasses;

namespace Interfaces
{
    public interface ISerialization
    {
        AssemblyModel Read(String path);
        void Save(AssemblyModel context, String path);
        string GetName();
    }
}
