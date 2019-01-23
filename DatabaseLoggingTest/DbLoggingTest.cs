using System;
using System.IO;
using System.Threading.Tasks;
using DatabaseLogging;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseLoggingTest
{
    [TestClass]
    public class DbLoggingTest
    {
        [TestMethod]
        public void CheckIfLoggingIsCorrect()
        {
            DbLogging logger = new DbLogging();
            Task.Run(async () =>
            {
                await logger.Debug("deb");
                await logger.Error("err");
                await logger.Fatal("fat");
                await logger.Info("inf");
                await logger.Warn("war");
            }).GetAwaiter().GetResult();

            string result1, result2, result3, result4, result5;
            using (logger.DbContext = new LoggingDatabaseContext())
            {
                result1 = logger.DbContext.Set<Log>().Find(1).Message;
                result2 = logger.DbContext.Set<Log>().Find(2).Message;
                result3 = logger.DbContext.Set<Log>().Find(3).Message;
                result4 = logger.DbContext.Set<Log>().Find(4).Message;
                result5 = logger.DbContext.Set<Log>().Find(5).Message;
                logger.DbContext.Database.Delete();
            }
            Assert.AreEqual("deb", result1);
            Assert.AreEqual("err", result2);
            Assert.AreEqual("fat", result3);
            Assert.AreEqual("inf", result4);
            Assert.AreEqual("war", result5);
        }
    }
}
