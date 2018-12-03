using System.Runtime.Serialization;
using System.Xml;
using Model.MetadataClasses;

namespace ViewModel.Logic
{
    public class XmlConventer : IConventer
    {

        public void saveToFile(AssemblyMetadata context, string filePath)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true };
            DataContractSerializer serializer = new DataContractSerializer(context.GetType());
            using (XmlWriter w = XmlWriter.Create(filePath, xmlWriterSettings))
            {
                serializer.WriteObject(w, context);
            }
        }

        AssemblyMetadata IConventer.readFromFile(string filePath)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblyMetadata));
            using (XmlReader xr = XmlReader.Create(filePath))
            {
                return (AssemblyMetadata)serializer.ReadObject(xr);
            }
        }
    }
}
