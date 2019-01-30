using System;
using System.Linq;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses;
using ReflectionModel;
using Serialization;

namespace SerializationTest
{
    [TestClass]
    public class XmlSerializationTest
    {
        [TestMethod]
        public void CheckiIfSerializationWorks()
        {
            ISerialization serializer = new XmlSerialization();
            Reflector ae = new Reflector("Database/TPA.ApplicationArchitecture.dll");
            AssemblyModel am = ae.AssemblyModel.ToModel();
            serializer.Save(am, "serialized.xml");
            AssemblyModel deserialized = serializer.Read("serialized.xml");
            Assert.AreEqual(am.Name, deserialized.Name);
            Assert.AreEqual(am.Namespaces.Count(), deserialized.Namespaces.Count());
            //Assert.AreEqual(am.Namespaces.Except(deserialized.Namespaces).Any(), !deserialized.Namespaces.Except(ae.AssemblyModel.Namespaces).Any());
        }
    }
}
