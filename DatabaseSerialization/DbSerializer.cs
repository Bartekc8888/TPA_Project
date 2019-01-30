using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using DatabaseSerialization.MetadataClasses;
using Interfaces;
using Model.MetadataClasses;

namespace DatabaseSerialization
{
    [Export(typeof(ISerialization))]
    public class DbSerializer : ISerialization
    {
        public AssemblyModel Read(string path)
        {
            AssemblyModel assembly;
            if (string.IsNullOrEmpty(path))
            {
                path = GetDbConnectionString();
            }

            using (DatabaseContext ctx = new DatabaseContext(path))
            {
                ctx.LoadData();

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
            if (string.IsNullOrEmpty(path))
            {
                path = GetDbConnectionString();
            }

            using (DatabaseContext ctx = new DatabaseContext(path))
            {
                if (ctx.Database.Exists())
                {
                    ctx.Database.Delete();
                }
                ctx.Database.Create();
                
                AssemblyDbModel serializationModel = new AssemblyDbModel(context);
                ctx.AssemblyModels.Add(serializationModel);
                ctx.SaveChanges();
            }
        }

        private string GetDbConnectionString()
        {
            string dbPath = ConfigurationManager.AppSettings["modelDb"];
            dbPath = Path.GetFullPath(dbPath);
            string path = ConfigurationManager.ConnectionStrings["FileDatabase"].ConnectionString;
            path = $@"{path.Replace("|DataDirectory|", dbPath)}";

            return path;
        }
    }
}
