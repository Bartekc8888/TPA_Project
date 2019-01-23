using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using DatabaseSerialization;
using DatabaseSerialization.MetadataClasses;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.MetadataClasses;
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
    }
}