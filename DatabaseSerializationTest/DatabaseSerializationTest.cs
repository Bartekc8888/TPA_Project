using System;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DatabaseSerialization;
using DatabaseSerialization.MetadataClasses.Types;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using ReflectionModel;

namespace DatabaseSerializationTest
{
    [TestClass]
    [DeploymentItem(@"Database\", @"Database")]
    public class DatabaseSerializationTest
    {
        private static string _connectionString;
        private static AssemblyModel _assemblyModel;
        
        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context)
        {
            string dbRelativePath = @"Database\TpaModelDatabase.mdf";
            string testingWorkingFolder = Environment.CurrentDirectory;
            string dbPath = Path.Combine(testingWorkingFolder, dbRelativePath);
            AppDomain.CurrentDomain.SetData("DataDirectory", dbPath);

            var instance = SqlProviderServices.Instance;
            FileInfo databaseFile = new FileInfo(dbPath);
            Assert.IsTrue(databaseFile.Exists, $"{Environment.CurrentDirectory}");
            _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security = True;Connect Timeout = 30;";
            
            Reflector ae = new Reflector(@"Database\TPA.ApplicationArchitecture.dll");
            _assemblyModel = ae.AssemblyModel.ToModel();
            
            ISerialization serialization = new DbSerializer();
            serialization.Save(_assemblyModel, _connectionString);
        }
        
        [TestMethod]
        public void AssemblyTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                Assert.IsNotNull(databaseContext.Database);
                Assert.IsTrue(databaseContext.Database.Exists());

                Assert.AreEqual(1, databaseContext.AssemblyModels.Count());
            }
        }
        
        [TestMethod]
        public void NamespacesTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                Assert.AreEqual(4, databaseContext.NamespaceModels.ToList().Count);
                Assert.AreEqual("TPA.ApplicationArchitecture.Data.CircularReference",
                    databaseContext.NamespaceModels
                        .FirstOrDefault(x =>
                            x.NamespaceName == "TPA.ApplicationArchitecture.Data.CircularReference")
                        ?.NamespaceName);
                Assert.AreEqual("TPA.ApplicationArchitecture.Data",
                    databaseContext.NamespaceModels
                        .FirstOrDefault(x => x.NamespaceName == "TPA.ApplicationArchitecture.Data")
                        ?.NamespaceName);
                Assert.AreEqual("TPA.ApplicationArchitecture.Presentation",
                    databaseContext.NamespaceModels
                        .FirstOrDefault(x => x.NamespaceName == "TPA.ApplicationArchitecture.Presentation")
                        ?.NamespaceName);
                Assert.AreEqual("TPA.ApplicationArchitecture.BusinessLogic",
                    databaseContext.NamespaceModels
                        .FirstOrDefault(x => x.NamespaceName == "TPA.ApplicationArchitecture.BusinessLogic")
                        ?.NamespaceName);
            }
        }

        [TestMethod]
        public void AbstractClassTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel abstractClass =
                    databaseContext.Types.Single(x => x.TypeName == "AbstractClass").ToModel();
                Assert.AreEqual(AbstractEnum.Abstract, abstractClass.Modifiers.Item3);
                Assert.AreEqual(AbstractEnum.Abstract,
                    abstractClass.Methods.Single(x => x.Name == "AbstractMethod").Modifiers.Item2);
            }
        }

        [TestMethod]
        public void ClassWithAttributesTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel attributeClass = databaseContext.Types.Single(x => x.TypeName == "ClassWithAttribute").ToModel();
                FieldModel fieldWithAttribute = attributeClass.Fields.Single(x => x.Name == "FieldWithAttribute");
                Assert.AreEqual("FieldWithAttribute", fieldWithAttribute.Name);
                Assert.AreEqual(1, attributeClass.Attributes.Count());
            }
        }

        [TestMethod]
        public void DerivedClassTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel derivedClass = databaseContext.Types.Single(x => x.TypeName == "DerivedClass").ToModel();
                Assert.IsNotNull(derivedClass.BaseType);
            }
        }

        [TestMethod]
        public void EnumTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel abstractClass =
                    databaseContext.Types.Single(x => x.TypeName == "AbstractClass").ToModel();
                TypeModel attributeClass = databaseContext.Types.Single(x => x.TypeName == "ClassWithAttribute").ToModel();
                TypeModel derivedClass = databaseContext.Types.Single(x => x.TypeName == "DerivedClass").ToModel();
                TypeModel genericClass = databaseContext.Types.Single(x => x.TypeName == "GenericClass`1").ToModel();
                TypeModel interfaceIn = databaseContext.Types.Single(x => x.TypeName == "IExample").ToModel();
                TypeModel implementedInterfaceClass =
                    databaseContext.Types.Single(x => x.TypeName == "ImplementationOfIExample").ToModel();
                TypeModel outerClass = databaseContext.Types.Single(x => x.TypeName == "OuterClass").ToModel();
                TypeModel staticClass = databaseContext.Types.Single(x => x.TypeName == "StaticClass").ToModel();
                TypeModel structure = databaseContext.Types.Single(x => x.TypeName == "Structure").ToModel();
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
        }

        [TestMethod]
        public void GenericClassTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel genericClass =
                    databaseContext.Types.Single(x => x.TypeName.Contains("GenericClass`1")).ToModel();
                Assert.AreEqual(1, genericClass.GenericArguments.Count());
                Assert.AreEqual("T", genericClass.GenericArguments.Single().TypeName);
                Assert.AreEqual("T",
                    genericClass.Properties.Single(x => x.Name == "GenericProperty").TypeName);
                Assert.AreEqual(1,
                    genericClass.Methods.Single(x => x.Name == "GenericMethod").Parameters.Count());
                Assert.AreEqual("T",
                    genericClass.Methods.Single(x => x.Name == "GenericMethod").Parameters.Single()
                        .TypeName);
                Assert.AreEqual("T",
                    genericClass.Methods.Single(x => x.Name == "GenericMethod").ReturnType);
            }
        }

        [TestMethod]
        public void InterfaceTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel interfaceClass = databaseContext.Types.Single(x => x.TypeName == "IExample").ToModel();
                Assert.AreEqual(AbstractEnum.Abstract, interfaceClass.Modifiers.Item3);
                Assert.AreEqual(AbstractEnum.Abstract,
                    interfaceClass.Methods.Single(x => x.Name == "MethodA").Modifiers.Item2);
            }
        }

        [TestMethod]
        public void ImplementedInterfaceTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel interfaceClass =
                    databaseContext.Types.Single(x => x.TypeName == "IExample").ToModel();
                TypeModel implementedInterfaceClass =
                    databaseContext.Types.Single(x => x.TypeName == "ImplementationOfIExample").ToModel();
                Assert.AreEqual("IExample", implementedInterfaceClass.ImplementedInterfaces.Single().TypeName);
                foreach (MethodModel method in interfaceClass.Methods)
                    Assert.IsNotNull(implementedInterfaceClass.Methods.SingleOrDefault(x => x.Name == method.Name));
            }
        }

        [TestMethod]
        public void NestedClassTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel outerClass = databaseContext.Types.Single(x => x.TypeName == "OuterClass").ToModel();
                Assert.AreEqual(1, outerClass.NestedTypes.ToList().Count);
                Assert.AreEqual("InnerClass", outerClass.NestedTypes.Single(x => x.TypeName == "InnerClass").TypeName);
            }
        }

        [TestMethod]
        public void StaticClassTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel staticClass = databaseContext.Types.Single(x => x.TypeName == "StaticClass").ToModel();
                Assert.AreEqual(StaticEnum.Static.ToString(),
                    staticClass.Methods.Single(x => x.Name == "StaticMethod1").Modifiers.Item3.ToString());
            }
        }

        [TestMethod]
        public void StructureTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();
                TypeModel structure = databaseContext.NamespaceModels
                    .FirstOrDefault(x => x.NamespaceName == "TPA.ApplicationArchitecture.Data")
                    ?.Types.Single(x => x.TypeName == "Structure").ToModel();
                
                Assert.IsNotNull(structure);
                Assert.AreEqual("Structure", structure.TypeEnum.ToString());
            }
        }
    }
}