using System;
using System.IO;
using System.Linq;
using DatabaseSerialization;
using DatabaseSerialization.MetadataClasses;
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
            
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                AssemblyDbModel assemblyDbModel = new AssemblyDbModel(_assemblyModel);
                databaseContext.AssemblyModels.Add(assemblyDbModel);
                databaseContext.SaveChanges();
            }
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
                TypeModel abstractClass = databaseContext.Types.Single(x => x.TypeName == "AbstractClass").ToModel();
                Assert.AreEqual(AbstractEnumMetadata.Abstract, abstractClass.Modifiers.Item3);
                Assert.AreEqual(AbstractEnumMetadata.Abstract,
                    abstractClass.Methods.Single(x => x.Name == "AbstractMethod").Modifiers.Item2);
            }
        }

        [TestMethod]
        public void ClassWithAttributesTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                TypeModel attributeClass =
                    databaseContext.Types.Single(x => x.TypeName == "ClassWithAttribute").ToModel();
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
                TypeModel derivedClass =
                    databaseContext.Types.Single(x => x.TypeName == "DerivedClass").ToModel();
                Assert.IsNotNull(derivedClass.BaseType);
            }
        }

        [TestMethod]
        public void StaticClassTest()
        {
            using (DatabaseContext databaseContext = new DatabaseContext(_connectionString))
            {
                TypeModel staticClass =
                    databaseContext.Types.Single(x => x.TypeName == "StaticClass").ToModel();
                Assert.AreEqual(StaticEnum.Static.ToString(),
                    staticClass.Methods.Single(x => x.Name == "StaticMethod1").Modifiers.Item3.ToString());
            }
        }
    }
}