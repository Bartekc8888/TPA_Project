﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Input;
using log4net;
using Model.MetadataClasses;
using ViewModel.ExtractionTools;
using ViewModel.View.TypesView;

namespace ViewModel.Logic
{
    public class TypesTreeViewModel : INotifyPropertyChanged
    {
        private static readonly ILog Log = LogManager.GetLogger
              (MethodBase.GetCurrentMethod().DeclaringType);

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => {};
        private AssemblyMetadata _assemblyModel;
        private readonly SynchronizationContext _context;

        private IFileChooser _fileChooser;
        
        private ObservableCollection<TypeViewAbstract> _items;
        public ObservableCollection<TypeViewAbstract> Items
        {
            get => _items;
            set
            {
                _items = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }

        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath;
            set
            {
                _selectedPath = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedPath)));
            }
        }

        private string _serializationPath;
        public string SerializationPath
        {
            get => _serializationPath;
            set
            {
                _serializationPath = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SerializationPath)));
            }
        }

        public ICommand AnalyzeCommand { get; set; }
        public ICommand ChooseAnalyzePathCommand { get; set; }

        public ICommand SerializeCommand { get; set; }
        public ICommand DeserializeCommand { get; set; }

        public TypesTreeViewModel(IFileChooser fileChooser)
        {
            Log.Info("Creating TreeViewModel");
            _context = SynchronizationContext.Current;
            _fileChooser = fileChooser;

            SelectedPath = "";
            SerializationPath = "";
            Items = new ObservableCollection<TypeViewAbstract>();
            AnalyzeCommand = new RelayCommand(ExtractData);
            ChooseAnalyzePathCommand = new RelayCommand(ChooseFile);

            SerializeCommand = new RelayCommand(SerializeData);
            DeserializeCommand = new RelayCommand(DeserializeData);
        }

        public bool IsPathValid()
        {
            string extension = Path.GetExtension(SelectedPath);
            return File.Exists(SelectedPath) && (extension == ".dll" || extension == ".exe");
        }

        public void ChooseFile()
        {
            SelectedPath = _fileChooser.ChooseFilePath();
        }

        public void ExtractData()
        {
            if (!String.IsNullOrEmpty(SelectedPath))
            {
                _assemblyModel = new AssemblyExtractor(SelectedPath).AssemblyModel;
                
                TypeViewAbstract item = ViewTypeFactory.CreateTypeViewClass(_assemblyModel);
                SetNewData(item);
            }
        }

        public void SerializeData()
        {
            if (!String.IsNullOrEmpty(SerializationPath) && _items.Count > 0)
            {
                IConventer xml = new XmlConventer();
                xml.saveToFile(_assemblyModel, SerializationPath);
            }
        }

        public void DeserializeData()
        {
            if (!String.IsNullOrEmpty(SerializationPath))
            {
                IConventer xml = new XmlConventer();
                _assemblyModel = xml.readFromFile(SerializationPath);
                
                TypeViewAbstract item = ViewTypeFactory.CreateTypeViewClass(_assemblyModel);
                SetNewData(item);
            }
        }

        private void SetNewData(TypeViewAbstract item)
        {
            if (_context != null)
            {
                _context.Post(delegate
                {
                    Items.Clear();
                    Items.Add(item);
                }, null);
            }
            else
            {
                Items.Clear();
                Items.Add(item);
            }
        }
    }
}
