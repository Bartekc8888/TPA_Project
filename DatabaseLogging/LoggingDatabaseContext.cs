using System.Data.Entity;

namespace DatabaseLogging
{
    public class LoggingDatabaseContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        public LoggingDatabaseContext(string path) : base ($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={path};Integrated Security=True;Connect Timeout=30;Context Connection=False")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LoggingDatabaseContext>());
        }
    }
}