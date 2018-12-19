using System;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;
using System.Xml;
using Model.MetadataClasses;

namespace Serialization
{
    [Export(typeof(ISerialization))]
    public class XmlSerialization : ISerialization
    {
        public void Save(AssemblyMetadata context, string filePath)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true };
            DataContractSerializer serializer = new DataContractSerializer(context.GetType(), null, Int32.MaxValue, false, true, null);
            using (XmlWriter w = XmlWriter.Create(filePath, xmlWriterSettings))
            {
                serializer.WriteObject(w, context);
            }
        }

        AssemblyMetadata ISerialization.Read(string filePath)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblyMetadata));
            using (XmlReader xr = XmlReader.Create(filePath))
            {
                return (AssemblyMetadata)serializer.ReadObject(xr);
            }
        }
    }
}
