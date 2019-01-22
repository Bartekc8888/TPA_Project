using System;
using System.IO;
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
            logger.Debug("deb");
            logger.Error("err");
            logger.Fatal("fat");
            logger.Info("inf");
            logger.Warn("war");

            fileTest.Refresh();
            Assert.IsTrue(fileTest.Exists);
            Assert.AreEqual("loggingTest.txt", fileTest.Name);
            Assert.IsTrue(fileTest.Length > 30);
        }
    }
}
