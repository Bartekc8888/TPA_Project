using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly string dllPath = @"../../TPA.ApplicationArchitecture.dll";



        [TestMethod]
        public void NamespacesTest()
        {
            Assert.AreEqual(4, ReflectorTestClass.Reflector.Namespaces.ToList().Count);
            Assert.AreEqual("TPA.ApplicationArchitecture.Data.CircularReference", ReflectorTestClass.Reflector.Namespaces.FirstOrDefault(x => x.Value.NamespaceName == "TPA.ApplicationArchitecture.Data.CircularReference").Value.NamespaceName);
            Assert.AreEqual("TPA.ApplicationArchitecture.Data", ReflectorTestClass.Reflector.Namespaces.FirstOrDefault(x => x.Value.NamespaceName == "TPA.ApplicationArchitecture.Data").Value.NamespaceName);
            Assert.AreEqual("TPA.ApplicationArchitecture.Presentation", ReflectorTestClass.Reflector.Namespaces.FirstOrDefault(x => x.Value.NamespaceName == "TPA.ApplicationArchitecture.Presentation").Value.NamespaceName);
            Assert.AreEqual("TPA.ApplicationArchitecture.BusinessLogic", ReflectorTestClass.Reflector.Namespaces.FirstOrDefault(x => x.Value.NamespaceName == "TPA.ApplicationArchitecture.BusinessLogic").Value.NamespaceName);
        }

        [TestMethod]
        public void AbstractClassTest()
        {
            TypeMetadata abstractClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "AbstractClass");
            Assert.AreEqual(AbstractEnumMetadata.Abstract, abstractClass.Modifiers.Item3);
            Assert.AreEqual(AbstractEnumMetadata.Abstract, abstractClass.Methods.Single(x => x.Name == "AbstractMethod").Modifiers.Item2);
        }

        [TestMethod]
        public void ClassWithAttributesTest()
        {
            TypeMetadata attributeClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "ClassWithAttribute");
            FieldMetadata fieldWithAttribute = attributeClass.Fields.Single(x => x.Name == "FieldWithAttribute");
            Assert.AreEqual("FieldWithAttribute", fieldWithAttribute.Name);
            Assert.AreEqual(1, attributeClass.Attributes.Count());
        }

        [TestMethod]
        public void DerivedClassTest()
        {
            TypeMetadata derivedClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "DerivedClass");
            Assert.IsNotNull(derivedClass.BaseType);
        }

        [TestMethod]
        public void EnumTest()
        {
            TypeMetadata abstractClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "AbstractClass");
            TypeMetadata attributeClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "ClassWithAttribute");
            TypeMetadata derivedClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "DerivedClass");
            TypeMetadata genericClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "GenericClass`1");
            TypeMetadata interfaceIn = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "IExample");
            TypeMetadata implementedInterfaceClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "ImplementationOfIExample");
            TypeMetadata outerClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "OuterClass");
            TypeMetadata staticClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "StaticClass");
            TypeMetadata structure = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "Structure");
            Assert.AreEqual("Class", abstractClass.TypeEnum.ToString());
            Assert.AreEqual("Class", attributeClass.TypeEnum.ToString());
            Assert.AreEqual("Class", derivedClass.TypeEnum.ToString());
            Assert.AreEqual("Class", genericClass.TypeEnum.ToString());
            Assert.AreEqual("Interface", interfaceIn.TypeEnum.ToString());
            Assert.AreEqual("Class", implementedInterfaceClass.TypeEnum.ToString());
            Assert.AreEqual("Class", outerClass.TypeEnum.ToString());
            Assert.AreEqual("Class", staticClass.TypeEnum.ToString());
            Assert.AreEqual("Structure", structure.TypeEnum.ToString());
        }

        [TestMethod]
        public void GenericClassTest()
        {
            TypeMetadata genericClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName.Contains("GenericClass`1"));
            Assert.AreEqual(1, genericClass.GenericArguments.Count());
            Assert.AreEqual<string>("T", genericClass.GenericArguments.Single().TypeName);
            Assert.AreEqual<string>("T", genericClass.Properties.Single<PropertyMetadata>(x => x.Name == "GenericProperty").TypeName);
            Assert.AreEqual<int>(1, genericClass.Methods.Single<MethodMetadata>(x => x.Name == "GenericMethod").Parameters.Count());
            Assert.AreEqual<string>("T", genericClass.Methods.Single<MethodMetadata>(x => x.Name == "GenericMethod").Parameters.Single().TypeName);
            Assert.AreEqual<string>("T", genericClass.Methods.Single<MethodMetadata>(x => x.Name == "GenericMethod").ReturnType);
        }

        [TestMethod]
        public void InterfaceTest()
        {
            TypeMetadata interfaceClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "IExample");
            Assert.AreEqual<AbstractEnumMetadata>(AbstractEnumMetadata.Abstract, interfaceClass.Modifiers.Item3);
            Assert.AreEqual<AbstractEnumMetadata>(AbstractEnumMetadata.Abstract, interfaceClass.Methods.Single(x => x.Name == "MethodA").Modifiers.Item2);
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

        [TestMethod]
        public void NestedClassTest()
        {
            TypeMetadata outerClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single<TypeMetadata>(x => x.TypeName == "OuterClass");
            Assert.AreEqual(1, outerClass.NestedTypes.ToList().Count);
            Assert.AreEqual("InnerClass", outerClass.NestedTypes.Single(x => x.TypeName == "InnerClass").TypeName);
        }

        [TestMethod]
        public void StaticClassTest()
        {
            TypeMetadata staticClass = ReflectorTestClass.Reflector.MyNamespace.Types.Single(x => x.TypeName == "StaticClass");
            Assert.AreEqual(StaticEnum.Static.ToString(), staticClass.Methods.Single(x => x.Name == "StaticMethod1").Modifiers.Item3.ToString());
        }

        [TestMethod]
        public void StructureTest()
        {
            TypeMetadata structure = ReflectorTestClass.Reflector.Namespaces
                .FirstOrDefault(x => x.Value.NamespaceName == "TPA.ApplicationArchitecture.Data")
                .Value.Types.Single<TypeMetadata>(x => x.TypeName == "Structure");
            Assert.AreEqual("Structure", structure.TypeEnum.ToString());
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
