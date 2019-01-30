using System;
using System.IO;
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

            LoadedAssembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
            ModelViewTypeFactory.CurrentAssemblyExtractor = this;
            foreach (AssemblyName assemblyName in LoadedAssembly.GetReferencedAssemblies())
            {
                try
                {
                    Assembly.ReflectionOnlyLoad(assemblyName.FullName);
                }
                catch
                {
                    Assembly.ReflectionOnlyLoadFrom(Path.Combine(Path.GetDirectoryName(assemblyFile), assemblyName.Name + ".dll"));
                }
            }
            AssemblyModel = new AssemblyMetadata(LoadedAssembly);
        }

        public AssemblyExtractor(Assembly assembly)
        {
            AssemblyModel = new AssemblyMetadata(assembly);
        }

        public AssemblyMetadata AssemblyModel { get; set; }
    }
}