using System;
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

        public DatabaseContext(string dbPath) : base(dbPath)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
            Configuration.ProxyCreationEnabled = false;
        }

        public DatabaseContext() : base(System.Configuration.ConfigurationManager.
                                        ConnectionStrings["FileDatabase"].ConnectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
            Configuration.ProxyCreationEnabled = false;
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

        public void LoadData()
        {
            AssemblyModels.Include(t => t.Namespaces).Load();
            NamespaceModels.Include(t => t.Types).Load();
            Types.Include(t => t.GenericArguments)
                .Include(t => t.Attributes)
                .Include(t => t.DeclaringType)
                .Include(t => t.BaseType)
                .Include(t => t.ImplementedInterfaces)
                .Include(t => t.Fields)
                .Include(t => t.Methods)
                .Include(t => t.Properties)
                .Include(t => t.Indexers)
                .Include(t => t.Events)
                .Include(t => t.Constructors)
                .Include(t => t.NestedTypes).Load();
            ConstructorModels.Include(t => t.GenericArguments)
                .Include(t => t.Parameters).Load();
            EventModels.Load();
            FieldModels.Include(t => t.TypeModel).Load();
            IndexerModels.Load();
            AttributeModels.Load();
            MethodModels.Include(t => t.GenericArguments)
                .Include(t => t.Parameters).Load();
            ParameterModels.Load();
            PropertyModels.Include(t => t.propertyMethods).Load();
        }
    }
}