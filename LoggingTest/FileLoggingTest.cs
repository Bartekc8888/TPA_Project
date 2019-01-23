using System;
using System.IO;
using System.Threading.Tasks;
using Interfaces;
using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoggingTest
{
    [TestClass]
    public class FileLoggingTest
    {
        [TestMethod]
        public void CheckIfLoggingIsCorrect()
        {
            FileInfo fileTest = new FileInfo("loggingTest.txt");
            if (fileTest.Exists)
            {
                fileTest.Delete();
            }

            ILogging logger = new FileLogging("loggingTest.txt", "Logging test");
            Task.Run(async () =>
            {
                await logger.Debug("deb");
                await logger.Error("err");
                await logger.Fatal("fat");
                await logger.Info("inf");
                await logger.Warn("war");
            }).GetAwaiter().GetResult();

            fileTest.Refresh();
            Assert.IsTrue(fileTest.Exists);
            Assert.AreEqual("loggingTest.txt", fileTest.Name);
            Assert.IsTrue(fileTest.Length > 30);
        }
    }
}
