using System;
using System.Runtime.Serialization;
using System.Xml;
using Model.MetadataClasses;

namespace Serialization
{
    public class XmlSerialization : ISerialization
    {
        public void saveToFile(AssemblyMetadata context, string filePath)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true };
            DataContractSerializer serializer = new DataContractSerializer(context.GetType(), null, Int32.MaxValue, false, true, null);
            using (XmlWriter w = XmlWriter.Create(filePath, xmlWriterSettings))
            {
                serializer.WriteObject(w, context);
            }
        }

        AssemblyMetadata ISerialization.readFromFile(string filePath)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblyMetadata));
            using (XmlReader xr = XmlReader.Create(filePath))
            {
                return (AssemblyMetadata)serializer.ReadObject(xr);
            }
        }
    }
}
