using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.ExtractionTools;
using Model.MetadataClasses;
using Model.MetadataClasses.Types.ReferenceTypes;
using Model.MetadataClasses.Types.ValueTypes;

namespace ModelTest.ExtractionTools
{
    [TestClass]
    public class TypeMetadataFactoryTest
    {
        public class TestClass
        {
            public enum TestEnum { TestValue, TestValue2 }
            public struct StructureTest { int testValue; };
            public int[] ArrayTest = new int[32];

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

            public long LongFieldProp { get; private set; }

            public delegate string TestDelegate(string str);
            event TestDelegate TestEvent;
        }

        public interface ITestInterface
        {

        }

        [TestMethod]
        public void EnumTest()
        {
            TypeMetadata typeMetadata = TypeMetadataFactory.CreateTypeMetadataClass(typeof(TestClass.TestEnum));

            Assert.IsTrue(typeMetadata is EnumMetadata);
        }

        [TestMethod]
        public void PrimitiveTest()
        {
            TypeMetadata typeMetadata = TypeMetadataFactory.CreateTypeMetadataClass(typeof(long));

            Assert.IsTrue(typeMetadata is PrimitiveMetadata);
        }

        [TestMethod]
        public void StructureTest()
        {
            TypeMetadata typeMetadata = TypeMetadataFactory.CreateTypeMetadataClass(typeof(TestClass.StructureTest));

            Assert.IsTrue(typeMetadata is StructureMetadata);
        }

        [TestMethod]
        public void ArrayTest()
        {
            TestClass testClass = new TestClass();
            TypeMetadata typeMetadata = TypeMetadataFactory.CreateTypeMetadataClass(testClass.ArrayTest.GetType());

            Assert.IsTrue(typeMetadata is ArrayMetadata);
        }

        [TestMethod]
        public void ClassTest()
        {
            TestClass testClass = new TestClass();
            TypeMetadata typeMetadata = TypeMetadataFactory.CreateTypeMetadataClass(testClass.GetType());

            Assert.IsTrue(typeMetadata is ClassMetadata);
        }

        [TestMethod]
        public void DelegateTest()
        {
            TypeMetadata typeMetadata = TypeMetadataFactory.CreateTypeMetadataClass(typeof(TestClass.TestDelegate));

            Assert.IsTrue(typeMetadata is DelegateMetadata);
        }

        [TestMethod]
        public void InterfaceTest()
        {
            TypeMetadata typeMetadata = TypeMetadataFactory.CreateTypeMetadataClass(typeof(ITestInterface));

            Assert.IsTrue(typeMetadata is InterfaceMetadata);
        }
    }
}
