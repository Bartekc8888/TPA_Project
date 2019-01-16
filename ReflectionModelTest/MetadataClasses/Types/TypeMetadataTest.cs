using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;

namespace ModelTest
{
    [TestClass]
    public class TypeMetadataTest
    {
        internal class TestingInternalClass
        {
            int testVariable;
            private List<long> listOfLongs;
        }

        public class TestingPublicClass
        {
        }

        int testVariable;

        private class TestingPrivateClass
        {
        }

        protected class TestingProtectedClass
        {
        }

        protected internal class TestingProtectedInternalClass
        {
        }

        protected abstract class TestingAbstractClass
        {
        }

        protected sealed class TestingSealedClass
        {
        }

        protected class TestingGenericClass<TestingProtectedClass, TestingPrivateClass>
        {
        }
        
        internal class TestClass
        {
            public float floatField;
            protected double doubleField;
            protected internal long longField;
            private int intField;

            public TestClass()
            {
            }

            ~TestClass()
            {
            }

            public void PublicMethod()
            {
            }

            public virtual void PublicVirtualMethod()
            {
            }

            protected void ProtectedMethod()
            {
            }

            protected internal void ProtectedInternalMethod()
            {
            }

            private int PrivateMethod()
            {
                return 0;
            }

            public class NestedType
            {
            }
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
        public void EmitDeclaringTypeTest()
        {
            TypeMetadata basicInfo = TypeMetadata.EmitDeclaringType(testVariable.GetType());
            Assert.AreEqual(testVariable.GetType().Name, basicInfo.TypeName);
        }

        [TestMethod]
        public void PublicTest()
        {
            TypeMetadata basicInfo = new TypeMetadata(typeof(TestingPublicClass));
            Assert.AreEqual(typeof(TestingPublicClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnumMetadata.Public, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnumMetadata.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnumMetadata.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void InternalTest()
        {
            TypeMetadata basicInfo = new TypeMetadata(typeof(TestingInternalClass));
            Assert.AreEqual(typeof(TestingInternalClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnumMetadata.Internal, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnumMetadata.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnumMetadata.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void ProtectedTest()
        {
            TypeMetadata basicInfo = new TypeMetadata(typeof(TestingProtectedClass));
            Assert.AreEqual(typeof(TestingProtectedClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnumMetadata.Protected, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnumMetadata.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnumMetadata.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void ProtectedInternalTest()
        {
            TypeMetadata basicInfo = new TypeMetadata(typeof(TestingProtectedInternalClass));
            Assert.AreEqual(typeof(TestingProtectedInternalClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnumMetadata.ProtectedInternal, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnumMetadata.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnumMetadata.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void PrivateTest()
        {
            TypeMetadata basicInfo = new TypeMetadata(typeof(TestingPrivateClass));
            Assert.AreEqual(typeof(TestingPrivateClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnumMetadata.Private, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnumMetadata.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnumMetadata.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void AbstractTest()
        {
            TypeMetadata basicInfo = new TypeMetadata(typeof(TestingAbstractClass));
            Assert.AreEqual(typeof(TestingAbstractClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnumMetadata.Protected, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnumMetadata.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnumMetadata.Abstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void SealedTest()
        {
            TypeMetadata basicInfo = new TypeMetadata(typeof(TestingSealedClass));
            Assert.AreEqual(typeof(TestingSealedClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnumMetadata.Protected, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnumMetadata.Sealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnumMetadata.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void GenericTest()
        {
            TypeMetadata basicInfo =
                new TypeMetadata(typeof(TestingGenericClass<TestingProtectedClass, TestingPrivateClass>));
            Assert.AreEqual(typeof(TestingGenericClass<TestingProtectedClass, TestingPrivateClass>).Name,
                basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnumMetadata.Protected, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnumMetadata.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnumMetadata.NotAbstract, basicInfo.Modifiers.Item3);

            IList<TypeMetadata> typeList = basicInfo.GenericArguments.ToList();

            Assert.AreEqual(2, typeList.Count);
            Assert.AreEqual(typeof(TestingProtectedClass).Name, typeList[0].TypeName);
            Assert.AreEqual(typeof(TestingPrivateClass).Name, typeList[1].TypeName);
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
            Assert.AreEqual(typeof(int).Name, methodList[5].ReturnType);
        }

        [TestMethod]
        public void PropertiesTest()
        {
            TypeMetadata metadata = new TypeMetadata(typeof(PropertiesTestClass));
            IList<PropertyMetadata> propertiesList = metadata.Properties.ToList();
            IList<MethodMetadata> methodList = metadata.Methods.ToList();

            Assert.AreEqual(0, methodList.Count());
            Assert.AreEqual(1, propertiesList.Count());
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
            IList<TypeMetadata> nestedTypesList = metadata.NestedTypes.ToList();

            Assert.AreEqual(1, nestedTypesList.Count);
            Assert.AreEqual("NestedType", nestedTypesList[0].TypeName);
        }
    }
}