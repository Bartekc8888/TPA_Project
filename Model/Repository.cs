using Interfaces;
using Model.MetadataClasses;
using System;
using System.ComponentModel.Composition;

namespace Model
{
    [Export(typeof(Repository))]
    public class Repository
    {
        public AssemblyModel _model { get; set; }

        [Import]
        public ISerialization _serializer;

        public Repository()
        {
        }

        public void Save(string path)
        {
            if (_model == null)
            {
                throw new InvalidOperationException("No data to save");
            }

            _serializer.Save(_model, path);
        }

        public void Read(string path)
        {
            _model = (AssemblyModel)_serializer.Read(path);
        }
    }
}
