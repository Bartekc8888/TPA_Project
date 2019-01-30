using System.Data.Entity;

namespace DatabaseLogging
{
    public class LoggingDatabaseContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        public LoggingDatabaseContext(string path) : base (path)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LoggingDatabaseContext>());
        }
    }
}