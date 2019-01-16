using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses;
using Serialization;
using ViewModel.ExtractionTools;

namespace SerializationTest
{
    [TestClass]
    public class XmlSerializationTest
    {
        [TestMethod]
        public void CheckiIfSerializationWorks()
        {
            ISerialization serializer = new XmlSerialization();
            AssemblyExtractor ae = new AssemblyExtractor("../../TPA.ApplicationArchitecture.dll");
            serializer.Save(ae.AssemblyModel.ToModel(), "serialized.xml");
            AssemblyModel deserialized = serializer.Read("serialized.xml");

            Assert.AreEqual(ae.AssemblyModel.ToModel().Name, deserialized.Name);
            Assert.AreEqual(ae.AssemblyModel.ToModel().Namespaces.Count(), deserialized.Namespaces.Count());
           // Assert.AreEqual(!ae.AssemblyModel.ToModel().Namespaces.Except(deserialized.Namespaces).Any(), !deserialized.Namespaces.Except(ae.AssemblyModel.Namespaces).Any());
        }
    }
}
