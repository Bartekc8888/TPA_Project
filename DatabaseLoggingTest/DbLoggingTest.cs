using System;
using System.IO;
using System.Threading.Tasks;
using DatabaseLogging;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseLoggingTest
{
    [TestClass]
    [DeploymentItem(@"Database\", @"Database")]
    public class DbLoggingTest
    {
        private static string dbPath;
        private static string _connectionString;

        [ClassInitialize]
        public static void ClassInitializeMethod(TestContext context)
        {
            string dbRelativePath = @"Database\TpaLoggingDatabase.mdf";
            string testingWorkingFolder = Environment.CurrentDirectory;
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            dbPath = Path.Combine(testingWorkingFolder, dbRelativePath);
            _connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security = True;Connect Timeout = 30;";
        }

        [TestMethod]
        public void CheckIfLoggingIsCorrect()
        {
            DbLogging logger = new DbLogging(dbPath);
            Task.Run(async () =>
            {
                await logger.Debug("deb");
                await logger.Error("err");
                await logger.Fatal("fat");
                await logger.Info("inf");
                await logger.Warn("war");
            }).GetAwaiter().GetResult();

            string result1, result2, result3, result4, result5;
            using (LoggingDatabaseContext DbContext = new LoggingDatabaseContext(_connectionString))
            {
                result1 = DbContext.Set<Log>().Find(1).Message;
                result2 = DbContext.Set<Log>().Find(2).Message;
                result3 = DbContext.Set<Log>().Find(3).Message;
                result4 = DbContext.Set<Log>().Find(4).Message;
                result5 = DbContext.Set<Log>().Find(5).Message;
                DbContext.Database.Delete();
            }
            Assert.AreEqual("deb", result1);
            Assert.AreEqual("err", result2);
            Assert.AreEqual("fat", result3);
            Assert.AreEqual("inf", result4);
            Assert.AreEqual("war", result5);
        }
    }
}
