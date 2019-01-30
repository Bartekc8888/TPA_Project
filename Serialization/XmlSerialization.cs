using System;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;
using System.Xml;
using Interfaces;
using Model.MetadataClasses;
using Serialization.MetadataClasses;

namespace Serialization
{
    [Export(typeof(ISerialization))]
    [ExportMetadata("Name", "Xml")]
    public class XmlSerialization : ISerialization
    {
        public void Save(Object context, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be empty");
            }
            
            AssemblySerializationModel assemblySerializationModel = new AssemblySerializationModel((AssemblyModel)context);

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true };
            DataContractSerializer serializer = new DataContractSerializer(assemblySerializationModel.GetType(), null, int.MaxValue, false, true, null, null);
            using (XmlWriter w = XmlWriter.Create(filePath, xmlWriterSettings))
            {
                serializer.WriteObject(w, assemblySerializationModel);
            }
        }

        Object ISerialization.Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be empty");
            }
            
            DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblySerializationModel));
            using (XmlReader xr = XmlReader.Create(filePath))
            {
                AssemblySerializationModel assemblySerializationModel = (AssemblySerializationModel)serializer.ReadObject(xr);
                return assemblySerializationModel.ToModel();
            }
        }
    }
}
