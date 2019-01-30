using Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace DatabaseLogging
{
    [Export(typeof(ILogging))]
    public class DbLogging : ILogging
    {
        private string connectionString;

        public DbLogging(string path)
        {
            string dbPath;
            if (string.IsNullOrEmpty(path))
            {
                dbPath = ConfigurationManager.AppSettings["modelDb"];
                dbPath = Path.GetFullPath(dbPath);
            }
            else
            {
                dbPath = path;
            }
            
            path = ConfigurationManager.ConnectionStrings["FileDatabase"].ConnectionString;
            connectionString = $@"{path.Replace("|DataDirectory|", dbPath)}";
        }

        public DbLogging()
        {
            string dbPath, path;
            dbPath = ConfigurationManager.AppSettings["modelDb"];
            dbPath = Path.GetFullPath(dbPath);
            path = ConfigurationManager.ConnectionStrings["FileDatabase"].ConnectionString;
            connectionString = $@"{path.Replace("|DataDirectory|", dbPath)}";

        }
        
        private async Task SaveLog(string type, string message)
        {
            await Task.Run(async () =>
             {
                 using (LoggingDatabaseContext DbContext = new LoggingDatabaseContext(connectionString))
                 {
                     DbContext.Database.CreateIfNotExists();
                     Log log = new Log
                     {
                         Time = DateTime.Now,
                         Type = type,
                         Message = message,
                     };

                     DbContext.Logs.Add(log);
                     await DbContext.SaveChangesAsync();
                 }
             });
        }

        public async Task Debug(string message)
        {
            await SaveLog("Debug", message);
        }

        public async Task Error(string message)
        {
            await SaveLog("Error", message);
        }

        public async Task Fatal(string message)
        {
            await SaveLog("Fatal", message);
        }

        public async Task Info(string message)
        {
            await SaveLog("Info", message);
        }

        public async Task Warn(string message)
        {
            await SaveLog("Warn", message);
        }
    }
}
