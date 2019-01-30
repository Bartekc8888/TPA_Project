using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses.Types;
using Model.MetadataDefinitions;

namespace ModelTest.ExtractionTools
{
#pragma warning disable 169
#pragma warning disable 67
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
#pragma warning restore 169
#pragma warning restore 67
        
        public interface ITestInterface
        {

        }

        [TestMethod]
        public void EnumTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(TestClass.TestEnum));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnumMetadata.Enum);
        }

        [TestMethod]
        public void PrimitiveTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(long));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnumMetadata.Primitive);
        }

        [TestMethod]
        public void StructureTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(TestClass.StructureTest));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnumMetadata.Structure);
        }

        [TestMethod]
        public void ArrayTest()
        {
            TestClass testClass = new TestClass();
            TypeMetadata typeMetadata = new TypeMetadata(testClass.ArrayTest.GetType());

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnumMetadata.Array);
        }

        [TestMethod]
        public void ClassTest()
        {
            TestClass testClass = new TestClass();
            TypeMetadata typeMetadata = new TypeMetadata(testClass.GetType());

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnumMetadata.Class);
        }

        [TestMethod]
        public void DelegateTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(TestClass.TestDelegate));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnumMetadata.Delegate);
        }

        [TestMethod]
        public void InterfaceTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(ITestInterface));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnumMetadata.Interface);
        }
    }
}
