using System;
using Model.MetadataClasses;

namespace ViewModel.Logic
{
    public interface IConventer
    {
        AssemblyMetadata readFromFile(String filePath);
        void saveToFile(AssemblyMetadata context, String filePath);

    }
}
