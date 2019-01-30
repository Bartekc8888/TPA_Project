using System;
using System.IO;
using System.Reflection;
using Model.MetadataClasses;

namespace ReflectionModel
{
    public class Reflector
    {
        public Assembly LoadedAssembly { get; }
        public AssemblyMetadata AssemblyModel { get; set; }

        public Reflector(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
                throw new ArgumentNullException();

            LoadedAssembly = Assembly.ReflectionOnlyLoadFrom(assemblyFile);
           // ModelViewTypeFactory.CurrentAssemblyExtractor = this;
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

        public Reflector(Assembly assembly)
        {
            AssemblyModel = new AssemblyMetadata(assembly);
        }
    }
}
