using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses;
using Model.MetadataClasses.Types;
using Model.MetadataDefinitions;

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
            TypeMetadata typeMetadata = new TypeMetadata(typeof(TestClass.TestEnum));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnum.Enum);
        }

        [TestMethod]
        public void PrimitiveTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(long));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnum.Primitive);
        }

        [TestMethod]
        public void StructureTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(TestClass.StructureTest));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnum.Structure);
        }

        [TestMethod]
        public void ArrayTest()
        {
            TestClass testClass = new TestClass();
            TypeMetadata typeMetadata = new TypeMetadata(testClass.ArrayTest.GetType());

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnum.Array);
        }

        [TestMethod]
        public void ClassTest()
        {
            TestClass testClass = new TestClass();
            TypeMetadata typeMetadata = new TypeMetadata(testClass.GetType());

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnum.Class);
        }

        [TestMethod]
        public void DelegateTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(TestClass.TestDelegate));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnum.Delegate);
        }

        [TestMethod]
        public void InterfaceTest()
        {
            TypeMetadata typeMetadata = new TypeMetadata(typeof(ITestInterface));

            Assert.IsTrue(typeMetadata.TypeEnum == TypeTypesEnum.Interface);
        }
    }
}
