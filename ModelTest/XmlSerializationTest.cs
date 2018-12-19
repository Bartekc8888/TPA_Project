using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses;
using Serialization;
using ViewModel.ExtractionTools;

namespace ModelTest
{
    [TestClass]
    public class XmlSerializationTest
    {
        [TestMethod]
        public void CheckiIfSerializationWorks()
        {
            ISerialization serializer = new XmlSerialization();
            AssemblyExtractor ae = new AssemblyExtractor("log4net.dll");
            serializer.Save(ae.AssemblyModel, "serialized.xml");
            AssemblyMetadata deserialized = serializer.Read("serialized.xml");

            Assert.AreEqual(ae.AssemblyModel.Name, deserialized.Name);
            Assert.AreEqual(ae.AssemblyModel.Namespaces.Count(), deserialized.Namespaces.Count());
            Assert.AreEqual(!ae.AssemblyModel.Namespaces.Except(deserialized.Namespaces).Any(), !deserialized.Namespaces.Except(ae.AssemblyModel.Namespaces).Any());
        }
    }
}
