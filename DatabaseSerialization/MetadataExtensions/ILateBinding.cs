using System;

namespace DatabaseSerialization.MetadataExtensions
{
    public interface ILateBinding
    {
        void LateBinding(Object other);
    }
}