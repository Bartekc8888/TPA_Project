﻿using System;
using System.ComponentModel.Composition;
using System.Runtime.Serialization;
using System.Xml;
using Interfaces;
using Model.MetadataClasses;
using SerializationModel.MetadataClasses;

namespace Serialization
{
    [Export(typeof(ISerialization))]
    [ExportMetadata("Name", "Xml")]
    public class XmlSerialization : ISerialization
    {
        public void Save(AssemblyModel context, string filePath)
        {
            AssemblySerializationModel assemblySerializationModel = new AssemblySerializationModel(context);

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings { Indent = true };
            DataContractSerializer serializer = new DataContractSerializer(assemblySerializationModel.GetType(), null, int.MaxValue, false, true, null, null);
            using (XmlWriter w = XmlWriter.Create(filePath, xmlWriterSettings))
            {
                serializer.WriteObject(w, assemblySerializationModel);
            }
        }

        AssemblyModel ISerialization.Read(string filePath)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblySerializationModel));
            using (XmlReader xr = XmlReader.Create(filePath))
            {
                AssemblySerializationModel assemblySerializationModel = (AssemblySerializationModel)serializer.ReadObject(xr);
                return assemblySerializationModel.ToModel();
            }
        }

        string ISerialization.GetName()
        {
            return "Xml";
        }
    }
}
