﻿using ViewModel.View.TypesView;
using Model.MetadataDefinitions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace ViewModel.Logic
{
    public class TypesTreeItemViewModel : INotifyPropertyChanged
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
              (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => {};

        public TypeViewAbstract CurrentType { get; }
        public string TypeIcon => CurrentType.IconPath;
        public string ItemName => CurrentType.Name;
        public string ItemType => CurrentType.TypeName;
        public string ItemDescription => CurrentType.Description;

        private ObservableCollection<TypesTreeItemViewModel> mChildren;
        public ObservableCollection<TypesTreeItemViewModel> Children
        {
            get { return mChildren; }
            private set
            {
                mChildren = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Children)));
            }
        }

        public ICommand ExpandCommand { get; set; }
        public bool CanExpand => HasChildren();

        private bool _isExpanded;

        public bool IsExpanded
        {

            get
            {
                return _isExpanded;
            }
            set
            {
                if (_isExpanded != value)
                {
                    if (value)
                    {
                        Expand();
                    }
                    else
                    {
                        Collapse();
                    }

                    _isExpanded = value;
                }
            }
        }

        public TypesTreeItemViewModel(TypeViewAbstract type)
        {
            Log.Info("Creating TypesTreeItemViewModel");

            CurrentType = type;

            ExpandCommand = new RelayCommand(Expand);
            if (CanExpand)
            {
                Children = new ObservableCollection<TypesTreeItemViewModel>();
                Children.Add(null);
            }
        }

        private void Collapse()
        {
            
        }

        private void Expand()
        {
            Log.Info("Set members of current type");

            Children = new ObservableCollection<TypesTreeItemViewModel>(CurrentType.CreateChildren().Select(child => new TypesTreeItemViewModel(child)));
        }

        private bool HasChildren()
        {
            Log.Info("Set members of current type");

            return CurrentType.HaveChildren;
        }
    }
}