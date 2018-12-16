using System;
using System.Reflection;
using Model.MetadataClasses;
using ViewModel.ModelRepresentation.Types;

namespace ViewModel.ExtractionTools
{
    public class AssemblyExtractor
    {
        public Assembly LoadedAssembly { get; }
        
        public AssemblyExtractor(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
                throw new ArgumentNullException();

            LoadedAssembly = Assembly.LoadFrom(assemblyFile);
            ModelViewTypeFactory.CurrentAssemblyExtractor = this;
            
            AssemblyModel = new AssemblyMetadata(LoadedAssembly);
        }

        public AssemblyExtractor(Assembly assembly)
        {
            AssemblyModel = new AssemblyMetadata(assembly);
        }

        public AssemblyMetadata AssemblyModel { get; set; }
    }
}