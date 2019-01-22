using System.Data.Common;
using System.Data.Entity;
using DatabaseSerialization.MetadataClasses;
using DatabaseSerialization.MetadataClasses.Types;
using DatabaseSerialization.MetadataClasses.Types.Members;

namespace DatabaseSerialization
{
    public class DatabaseContext : DbContext
    {
        public DbSet<NamespaceDbModel> NamespaceModels { get; set; }
        public DbSet<AssemblyDbModel> AssemblyModels { get; set; }
        public DbSet<TypeDbModel> Types { get; set; }
        public DbSet<ConstructorDbModel> ConstructorModels { get; set; }
        public DbSet<EventDbModel> EventModels { get; set; }
        public DbSet<FieldDbModel> FieldModels { get; set; }
        public DbSet<IndexerDbModel> IndexerModels { get; set; }
        public DbSet<AttributeDbModel> AttributeModels { get; set; }
        public DbSet<MethodDbModel> MethodModels { get; set; }
        public DbSet<ParameterDbModel> ParameterModels { get; set; }
        public DbSet<PropertyDbModel> PropertyModels { get; set; }

        public DatabaseContext(string name) : base(name)
        {
        }

        public DatabaseContext(DbConnection connection) : base(connection, true)
        {
        }

        public DatabaseContext() : base(System.Configuration.ConfigurationManager.
                                        ConnectionStrings["FileDatabase"].ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}