using Model.MetadataClasses;
using System.Reflection;

namespace Model.ExtractionTools
{
    public class AssemblyExtractor
    {
        public AssemblyExtractor(string assemblyFile)
        {
            if (string.IsNullOrEmpty(assemblyFile))
                throw new System.ArgumentNullException();

            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            AssemblyModel = new AssemblyMetadata(assembly);
        }

        public AssemblyExtractor(Assembly assembly)
        {
            AssemblyModel = new AssemblyMetadata(assembly);
        }

        public AssemblyMetadata AssemblyModel { get; private set; }
    }
}