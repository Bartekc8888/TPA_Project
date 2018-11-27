using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;

namespace ModelTest
{
    [TestClass]
    public class TypeMetadataTest
    {
        internal class TestClass
        {
            public float floatField;
            protected double doubleField;
            protected internal long longField;
            private int intField;

            public TestClass() { }
            ~TestClass() { }

            public void PublicMethod() { }
            public virtual void PublicVirtualMethod() { }
            protected void ProtectedMethod() { }
            protected internal void ProtectedInternalMethod() { }
            private int PrivateMethod() { return 0; }

            public class NestedType { }
        }

        public class PropertiesTestClass
        {
            public long LongFieldProp { get; private set; }
        }

        public class EventsTestClass
        {
            public delegate string TestDelegate(string str);

            event TestDelegate TestEvent;
        }

        [TestMethod]
        public void FieldsTest()
        {
            TypeMetadata metadata = new TypeMetadata(typeof(TestClass));
            IList<FieldMetadata> typeList = metadata.Fields.ToList();

            Assert.AreEqual(4, typeList.Count);
            Assert.AreEqual("doubleField", typeList[1].Name);
            Assert.AreEqual(typeof(double).Name, typeList[1].TypeMetadata.TypeName);
        }

        [TestMethod]
        public void MethodsTest()
        {
            TypeMetadata metadata = new TypeMetadata(typeof(TestClass));
            IList<MethodMetadata> methodList = metadata.Methods.ToList();

            Assert.AreEqual(6, methodList.Count);
            Assert.AreEqual("PublicVirtualMethod", methodList[2].Name);
            Assert.AreEqual(typeof(int).Name, methodList[5].ReturnType.TypeName);
        }

        [TestMethod]
        public void PropertiesTest()
        {
            TypeMetadata metadata = new TypeMetadata(typeof(PropertiesTestClass));
            IList<PropertyMetadata> propertiesList = metadata.Properties.ToList();
            IList<MethodMetadata> methodList = metadata.Methods.ToList();

            Assert.AreEqual(2, methodList.Count);
            Assert.AreEqual(1, propertiesList.Count);
            Assert.AreEqual("LongFieldProp", propertiesList[0].Name);
        }

        [TestMethod]
        public void EventsTest()
        {
            TypeMetadata metadata = new TypeMetadata(typeof(EventsTestClass));
            IList<EventMetadata> eventsList = metadata.Events.ToList();

            Assert.AreEqual(1, eventsList.Count);
            Assert.AreEqual("TestEvent", eventsList[0].Name);
            Assert.AreEqual(typeof(EventsTestClass.TestDelegate).Name, eventsList[0].TypeMetadata.TypeName);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            TypeMetadata metadata = new TypeMetadata(typeof(TestClass));
            IList<ConstructorMetadata> constructorList = metadata.Constructors.ToList();

            Assert.AreEqual(1, constructorList.Count);
            Assert.AreEqual(".ctor", constructorList[0].Name);
        }

        [TestMethod]
        public void NestedTypeTest()
        {
            TypeMetadata metadata = new TypeMetadata(typeof(TestClass));
            IList<TypeBasicInfo> nestedTypesList = metadata.NestedTypes.ToList();

            Assert.AreEqual(1, nestedTypesList.Count);
            Assert.AreEqual("NestedType", nestedTypesList[0].TypeName);
        }
    }
}
