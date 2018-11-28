using System;
using System.Reflection;
using Model.MetadataClasses;

namespace ViewModel.ExtractionTools
{
    public class AssemblyExtractor
    {
        public AssemblyExtractor(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
                throw new ArgumentNullException();

            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            AssemblyModel = new AssemblyMetadata(assembly);
        }

        public AssemblyExtractor(Assembly assembly)
        {
            AssemblyModel = new AssemblyMetadata(assembly);
        }

        public AssemblyMetadata AssemblyModel { get; set; }
    }
}