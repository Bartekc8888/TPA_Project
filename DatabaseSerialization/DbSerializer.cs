using System;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using DatabaseSerialization.MetadataClasses;
using Interfaces;
using Model.MetadataClasses;

namespace DatabaseSerialization
{
    [Export(typeof(ISerialization))]
    [ExportMetadata("Name", "Database")]
    class DbSerializer : ISerialization
    {
        public AssemblyModel Read(string path)
        {
            AssemblyModel assembly;
            
            using(DatabaseContext ctx = new DatabaseContext())
            {
                ctx.AssemblyModels.Load();
                ctx.NamespaceModels.Load();
                ctx.Types.Load();
                ctx.ConstructorModels.Load();
                ctx.EventModels.Load();
                ctx.FieldModels.Load();
                ctx.IndexerModels.Load();
                ctx.AttributeModels.Load();
                ctx.MethodModels.Load();
                ctx.ParameterModels.Load();
                ctx.PropertyModels.Load();

                assembly = ctx.AssemblyModels.FirstOrDefault()?.ToModel();

                if(assembly == null)
                {
                    throw new ArgumentException("Database is empty");
                }
            }
            
            return assembly;
        }

        public void Save(AssemblyModel context, String path)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());
            AssemblyDbModel serializationModel = new AssemblyDbModel(context);
            using (DatabaseContext ctx = new DatabaseContext())
            {
                bool isWorking = ctx.Database.Exists();
                ctx.AssemblyModels.Add(serializationModel);
                ctx.SaveChanges();
            }
        }

        string ISerialization.GetName()
        {
            return "Db";
        }
    }
}
