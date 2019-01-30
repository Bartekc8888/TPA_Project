using System;

namespace Interfaces
{
    public interface ISerialization
    {
        Object Read(String path);
        void Save(Object context, String path);
    }
}
