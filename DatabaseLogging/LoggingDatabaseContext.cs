using System.Data.Entity;

namespace DatabaseLogging
{
    public class LoggingDatabaseContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        public LoggingDatabaseContext() : base (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Michal\Desktop\TPA_Project\DatabaseSerializationTests\Database\TpaModelsDatabase.mdf;Integrated Security=True;Connect Timeout=5")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LoggingDatabaseContext>());
        }
    }
}