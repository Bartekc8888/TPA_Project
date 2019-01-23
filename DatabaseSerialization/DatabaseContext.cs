using System.Data.Common;
using System.Data.Entity;
using DatabaseSerialization.MetadataClasses;
using DatabaseSerialization.MetadataClasses.Types;
using DatabaseSerialization.MetadataClasses.Types.Members;

namespace DatabaseSerialization
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AssemblyDbModel> AssemblyModels { get; set; }
        public DbSet<NamespaceDbModel> NamespaceModels { get; set; }
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
            Configuration.LazyLoadingEnabled = true;
        }

        public DatabaseContext(DbConnection connection) : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DatabaseContext() : base(System.Configuration.ConfigurationManager.
                                        ConnectionStrings["FileDatabase"].ConnectionString)
        {
            Configuration.LazyLoadingEnabled = true;
        }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.GenericArguments).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.Attributes).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.ImplementedInterfaces).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.Fields).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.Methods).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.Properties).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.Indexers).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.Events).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.Constructors).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasMany(t => t.NestedTypes).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasOptional(t => t.DeclaringType).WithMany();
            modelBuilder.Entity<TypeDbModel>().HasOptional(t => t.BaseType).WithMany();
            
            modelBuilder.Entity<MethodDbModel>().HasMany(t => t.GenericArguments).WithMany();
            modelBuilder.Entity<MethodDbModel>().HasMany(t => t.Parameters).WithMany();
            
            modelBuilder.Entity<PropertyDbModel>().HasMany(t => t.propertyMethods).WithMany();
            
            modelBuilder.Entity<NamespaceDbModel>().HasMany(t => t.Types).WithMany();
            
            modelBuilder.Entity<AssemblyDbModel>().HasMany(t => t.Namespaces).WithMany();
        }

        public void ClearDatabase()
        {
            Database.ExecuteSqlCommand("ALTER TABLE AssemblyDbModel NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE NamespaceDbModel NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE TypeDbModel NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE ConstructorDbMode NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE EventDbModel NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE FieldDbModel NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE IndexerDbModel NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE AttributeDbModel NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE MethodDbModel NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE ParameterDbModel NOCHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE PropertyDbModel NOCHECK CONSTRAINT ALL");

            Database.ExecuteSqlCommand("TRUNCATE TABLE AssemblyDbModel");
            Database.ExecuteSqlCommand("TRUNCATE TABLE NamespaceDbModel");
            Database.ExecuteSqlCommand("TRUNCATE TABLE TypeDbModel");
            Database.ExecuteSqlCommand("TRUNCATE TABLE ConstructorDbMode");
            Database.ExecuteSqlCommand("TRUNCATE TABLE EventDbModel");
            Database.ExecuteSqlCommand("TRUNCATE TABLE FieldDbModel");
            Database.ExecuteSqlCommand("TRUNCATE TABLE IndexerDbModel");
            Database.ExecuteSqlCommand("TRUNCATE TABLE AttributeDbModel");
            Database.ExecuteSqlCommand("TRUNCATE TABLE MethodDbModel");
            Database.ExecuteSqlCommand("TRUNCATE TABLE ParameterDbModel");
            Database.ExecuteSqlCommand("TRUNCATE TABLE PropertyDbModel");
            
            Database.ExecuteSqlCommand("ALTER TABLE AssemblyDbModel CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE NamespaceDbModel CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE TypeDbModel CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE ConstructorDbMode CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE EventDbModel CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE FieldDbModel CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE IndexerDbModel CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE AttributeDbModel CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE MethodDbModel CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE ParameterDbModel CHECK CONSTRAINT ALL");
            Database.ExecuteSqlCommand("ALTER TABLE PropertyDbModel CHECK CONSTRAINT ALL");
            
            SaveChanges();
        }
    }
}