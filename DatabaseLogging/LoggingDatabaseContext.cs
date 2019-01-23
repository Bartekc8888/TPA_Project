using System.Data.Entity;

namespace DatabaseLogging
{
    public class LoggingDatabaseContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        public LoggingDatabaseContext() : base (System.Configuration.ConfigurationManager.
                                                ConnectionStrings["FileDatabase"].ConnectionString)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LoggingDatabaseContext>());
        }
    }
}