using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses.Types;
using Model.MetadataDefinitions;

namespace ModelTest.MetadataClasses.Types
{
    internal class TestingInternalClass {
        int testVariable;
        private List<long> listOfLongs;
    }

    public class TestingPublicClass
    {
    }

    [TestClass]
    public class TypeBasicInfoTest
    {
        int testVariable;
        private class TestingPrivateClass { }
        protected class TestingProtectedClass { }
        protected internal class TestingProtectedInternalClass { }
        protected abstract class TestingAbstractClass { }
        protected sealed class TestingSealedClass { }
        protected class TestingGenericClass<TestingProtectedClass, TestingPrivateClass> { }

        [TestMethod]
        public void EmitDeclaringTypeTest()
        {
            TypeBasicInfo basicInfo = TypeBasicInfo.EmitDeclaringType(testVariable.GetType());
            Assert.AreEqual(testVariable.GetType().Name, basicInfo.TypeName);
        }

        [TestMethod]
        public void PublicTest()
        {
            TypeBasicInfo basicInfo = new TypeBasicInfo(typeof(TestingPublicClass));
            Assert.AreEqual(typeof(TestingPublicClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnum.Public, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnum.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnum.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void InternalTest()
        {
            TypeBasicInfo basicInfo = new TypeBasicInfo(typeof(TestingInternalClass));
            Assert.AreEqual(typeof(TestingInternalClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnum.Internal, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnum.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnum.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void ProtectedTest()
        {
            TypeBasicInfo basicInfo = new TypeBasicInfo(typeof(TestingProtectedClass));
            Assert.AreEqual(typeof(TestingProtectedClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnum.Protected, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnum.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnum.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void ProtectedInternalTest()
        {
            TypeBasicInfo basicInfo = new TypeBasicInfo(typeof(TestingProtectedInternalClass));
            Assert.AreEqual(typeof(TestingProtectedInternalClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnum.ProtectedInternal, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnum.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnum.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void PrivateTest()
        {
            TypeBasicInfo basicInfo = new TypeBasicInfo(typeof(TestingPrivateClass));
            Assert.AreEqual(typeof(TestingPrivateClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnum.Private, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnum.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnum.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void AbstractTest()
        {
            TypeBasicInfo basicInfo = new TypeBasicInfo(typeof(TestingAbstractClass));
            Assert.AreEqual(typeof(TestingAbstractClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnum.Protected, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnum.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnum.Abstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void SealedTest()
        {
            TypeBasicInfo basicInfo = new TypeBasicInfo(typeof(TestingSealedClass));
            Assert.AreEqual(typeof(TestingSealedClass).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnum.Protected, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnum.Sealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnum.NotAbstract, basicInfo.Modifiers.Item3);
        }

        [TestMethod]
        public void GenericTest()
        {
            TypeBasicInfo basicInfo = new TypeBasicInfo(typeof(TestingGenericClass<TestingProtectedClass, TestingPrivateClass>));
            Assert.AreEqual(typeof(TestingGenericClass<TestingProtectedClass, TestingPrivateClass>).Name, basicInfo.TypeName);
            Assert.AreEqual(AccessLevelEnum.Protected, basicInfo.Modifiers.Item1);
            Assert.AreEqual(SealedEnum.NotSealed, basicInfo.Modifiers.Item2);
            Assert.AreEqual(AbstractEnum.NotAbstract, basicInfo.Modifiers.Item3);

            IList<TypeBasicInfo> typeList = basicInfo.GenericArguments.ToList();

            Assert.AreEqual(2, typeList.Count);
            Assert.AreEqual(typeof(TestingProtectedClass).Name, typeList[0].TypeName);
            Assert.AreEqual(typeof(TestingPrivateClass).Name, typeList[1].TypeName);
        }
    }
}
