using System;
using System.IO;
using System.Linq;
using DatabaseSerialization;
using DatabaseSerialization.MetadataClasses;
using DatabaseSerialization.MetadataClasses.Types;
using DatabaseSerialization.MetadataClasses.Types.Members;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses;
using Model.MetadataClasses.Types;
using Model.MetadataClasses.Types.Members;
using Model.MetadataDefinitions;
using ViewModel.ExtractionTools;

namespace DatabaseSerializationTest
{
    [TestClass]
    [DeploymentItem(@"Database\TpaModelDatabase.mdf", @"Database")]
    [DeploymentItem(@"Database\TPA.ApplicationArchitecture.dll", @"Database")]
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

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            FileInfo databaseFile = new FileInfo(dbPath);
            Assert.IsTrue(databaseFile.Exists, $"{Environment.CurrentDirectory}");
            _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security = True; Connect Timeout = 30;";
            
            AssemblyExtractor ae = new AssemblyExtractor(@"Database\TPA.ApplicationArchitecture.dll");
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
        public void NamespaceTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                Assert.IsNotNull(databaseContext.Database);
                Assert.IsTrue(databaseContext.Database.Exists());

                Assert.AreEqual(4, databaseContext.NamespaceModels.Count());
            }
        }
        
        [TestMethod]
        public void AbstractClassTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();

                TypeDbModel abstractClass = databaseContext.Types.Single(x => x.TypeName == "AbstractClass");
                Assert.IsNotNull(abstractClass);
            }
        }

        [TestMethod]
        public void ClassWithAttributesTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();

                TypeDbModel attributeClass =
                    databaseContext.Types.Single(x => x.TypeName == "ClassWithAttribute");
                Assert.AreEqual(1, attributeClass.Attributes.Count());
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
                Assert.AreEqual(AbstractEnum.Abstract, interfaceClass.Methods.Single(x => x.Name == "MethodA").Modifiers.Item2);
            }
        }

        [TestMethod]
        public void ImplementedInterfaceTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();

                TypeModel _interfaceClass =
                    databaseContext.Types.Single(x => x.TypeName == "IExample").ToModel();
                TypeModel implementedInterfaceClass =
                    databaseContext.Types.Single(x => x.TypeName == "ImplementationOfIExample").ToModel();
                Assert.IsNotNull(implementedInterfaceClass);
            }
        }

        [TestMethod]
        public void NestedClassTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                databaseContext.LoadData();

                TypeModel outerClass = databaseContext.Types.Single(x => x.TypeName == "OuterClass").ToModel();
                Assert.IsNotNull(outerClass);
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
    }
}