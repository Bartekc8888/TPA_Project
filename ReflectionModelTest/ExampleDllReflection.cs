using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;

namespace ModelTest
{
    [TestClass]
    public class ExampleDllReflection
    {
        public Assembly assemblyToTest;
        private AssemblyMetadata assembyMetadata;

        [TestInitialize]
        public void Init()
        {
            string dllToReflect = "../../TPA.ApplicationArchitecture.dll";
            assemblyToTest = Assembly.LoadFrom(dllToReflect);
            assembyMetadata = new AssemblyMetadata(assemblyToTest);
        }

        [TestMethod]
        public void CheckIfAssemblyWasLoaded()
        {
            Assert.IsNotNull(assemblyToTest);
            Assert.IsNotNull(assembyMetadata);
        }

        [TestMethod]
        public void AbstractClassTest()
        {
            TypeMetadata abstractClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "AbstractClass");
            Assert.AreEqual(AbstractEnumMetadata.Abstract, abstractClass.Modifiers.Item3);
            Assert.AreEqual(AbstractEnumMetadata.Abstract, abstractClass.Methods.Single(x => x.Name == "AbstractMethod").Modifiers.Item2);
        }

        [TestMethod]
        public void DerivedClassTest()
        {
            TypeMetadata derivedClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "DerivedClass");
            Assert.IsNotNull(derivedClass.BaseType);
        }

        [TestMethod]
        public void InterfaceTest()
        {
            TypeMetadata interfaceClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "IExample");
            Assert.AreEqual<AbstractEnumMetadata>(AbstractEnumMetadata.Abstract, interfaceClass.Modifiers.Item3);
            Assert.AreEqual<AbstractEnumMetadata>(AbstractEnumMetadata.Abstract, interfaceClass.Methods.Single(x => x.Name == "MethodA").Modifiers.Item2);
        }

        [TestMethod]
        public void ClassWithAttributesTest()
        {
            TypeMetadata attributeClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "ClassWithAttribute");
            Assert.AreEqual(1, attributeClass.Attributes.Count());
        }

        [TestMethod]
        public void ImplementedInterfaceTest()
        {
            TypeMetadata _interfaceClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "IExample");
            TypeMetadata implementedInterfaceClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "ImplementationOfIExample");
            Assert.AreEqual<string>("IExample", implementedInterfaceClass.ImplementedInterfaces.Single().TypeName);
            foreach (MethodMetadata method in _interfaceClass.Methods)
                Assert.IsNotNull(implementedInterfaceClass.Methods.SingleOrDefault(x => x.Name == method.Name));
        }

        private class ReflectorTestClass : AssemblyMetadata
        {
            internal static ReflectorTestClass Reflector => m_Reflector.Value;
            internal Dictionary<string, NamespaceMetadata> Namespaces;
            internal NamespaceMetadata MyNamespace { get; private set; }
            internal ReflectorTestClass() : base(Assembly.LoadFrom("../../TPA.ApplicationArchitecture.dll"))
            {
                Namespaces =  base.Namespaces.ToDictionary<NamespaceMetadata, string>(x => x.NamespaceName);
                MyNamespace = Namespaces.ContainsKey(m_NamespaceName) ? Namespaces["TPA.ApplicationArchitecture.Data"] : null;
            }
            internal const string TestAssemblyName = @"Instrumentation\TPA.ApplicationArchitecture.dll";

            #region private
            private const string m_NamespaceName = "TPA.ApplicationArchitecture.Data";
            private static Lazy<ReflectorTestClass> m_Reflector = new Lazy<ReflectorTestClass>(() => new ReflectorTestClass());
            #endregion

        }
    }
}
