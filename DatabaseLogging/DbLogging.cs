using Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace DatabaseLogging
{
    [Export(typeof(ILogging))]
    public class DbLogging : ILogging
    {
        public LoggingDatabaseContext DbContext { get; set; }

        private async Task SaveLog(string type, string message)
        {
            await Task.Run(async () =>
             {
                 using (DbContext = new LoggingDatabaseContext())
                 {
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
