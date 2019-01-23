using System;
using System.ComponentModel.Composition;
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
    [ExportMetadata("Name", "Database")]
    public class DbSerializer : ISerialization
    {
        public AssemblyModel Read(string path)
        {
            AssemblyModel assembly;
            if (string.IsNullOrEmpty(path))
            {
                path = System.Configuration.ConfigurationManager.ConnectionStrings["FileDatabase"].ConnectionString;
                path = $@"{path.Replace("|DataDirectory|", (string)AppDomain.CurrentDomain.GetData("DataDirectory"))}";
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
                path = System.Configuration.ConfigurationManager.ConnectionStrings["FileDatabase"].ConnectionString;
                path = $@"{path.Replace("|DataDirectory|", (string)AppDomain.CurrentDomain.GetData("DataDirectory"))}";
            }

            using (DatabaseContext ctx = new DatabaseContext(path))
            {
                AssemblyDbModel serializationModel = new AssemblyDbModel(context);
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
