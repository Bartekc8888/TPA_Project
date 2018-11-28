﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading;
using log4net;
using ViewModel.Logic;

namespace CLI
{
    class CommandLineView
    {
        private static readonly ILog Log = LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        private const string ExitCharacter = "0";

        private TypesTreeViewModel _viewModel;
        private TypesTreeItemViewModel _currentItem;
        private Stack<TypesTreeItemViewModel> _previousTypes;

        private bool _isGoingBack;

        public CommandLineView()
        {
            Log.Info("Creating CommandLineItemViewModel");

            _previousTypes = new Stack<TypesTreeItemViewModel>();
            _viewModel = new TypesTreeViewModel();
        }

        public void Run()
        {
            GetFileFromUser();
            
            PrintHeaders(_viewModel.Items);
            while (true)
            {
                TypesTreeItemViewModel temp = _currentItem;
                _currentItem = NewChosen();
                if (_isGoingBack)
                {
                }
                else
                {
                    _previousTypes.Push(temp);
                }

                if (_currentItem != null)
                {
                    PrintTypeWithChildren(_currentItem);
                }
                else
                {
                    PrintHeaders(_viewModel.Items);
                }
            }
        }

        private void GetFileFromUser()
        {
            do
            {
                Console.WriteLine("Give a path to assembly file or type in " + ExitCharacter + " to exit.");
                _viewModel.SelectedPath = Console.ReadLine();
            } while (!_viewModel.IsPathValid() && !ExitCharacter.Equals(_viewModel.SelectedPath));

            if (!ExitCharacter.Equals(_viewModel.SelectedPath))
            {
               _viewModel.ExtractData();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void PrintTypeWithChildren(TypesTreeItemViewModel item)
        {
            Log.Debug("Printing current type with members");

            string itemString = "";
            string name = item.ItemDescription;
            int index = 1;

            itemString += item.ItemDescription + " " + item.ItemType + " " + item.ItemName + Environment.NewLine;
            item.IsExpanded = true;
            foreach (TypesTreeItemViewModel tva in item.Children)
            {
                if (tva.ItemDescription != name)
                {
                    itemString += Environment.NewLine;
                    name = tva.ItemDescription;
                }

                if (tva.CanExpand)
                {
                    itemString += index.ToString();
                    
                    if (index < 10)
                        itemString += " ";
                    index++;
                }
                else
                {
                    itemString += "  ";
                }
                
                itemString += "   " + tva.ItemDescription + " " + tva.ItemType + " " + tva.ItemName +
                              Environment.NewLine;
            }

            itemString += Environment.NewLine + "Press selected number to expand or 0 to come back";
            Console.Clear();
            Console.WriteLine(itemString);
        }

        private void PrintHeaders(ObservableCollection<TypesTreeItemViewModel> items)
        {
            string itemString = "Found types" + Environment.NewLine;
            int index = 1;
            
            foreach (TypesTreeItemViewModel itemViewModel in items)
            {
                itemString += index.ToString();
                itemString += "   " + itemViewModel.ItemDescription + " " + itemViewModel.ItemType + " " +
                              itemViewModel.ItemName + Environment.NewLine;

                index++;
            }

            itemString += Environment.NewLine + "Press selected number to expand or 0 to come back";
            Console.Clear();
            Console.WriteLine(itemString);
        }

        private TypesTreeItemViewModel NewChosen()
        {
            Log.Debug("User chooses new type");

            _isGoingBack = false;
            TypesTreeItemViewModel viewModelItem = null;
            bool didUserChoose = false;

            do
            {
                string chosen = Console.ReadLine();
                bool isNumber = int.TryParse(chosen, out int parsedNumber);

                if (parsedNumber > 0 && isNumber)
                {
                    viewModelItem = GetExpandableByIndex(parsedNumber);
                    if (viewModelItem != null)
                    {
                        didUserChoose = true;
                    }
                }

                if (parsedNumber == 0 && isNumber)
                {
                    _isGoingBack = true;
                    if (_previousTypes.Count != 0)
                    {
                        viewModelItem = _previousTypes.Pop();
                    }
                    else
                    {
                        viewModelItem = _currentItem;
                    }

                    didUserChoose = true;
                }
            } while (!didUserChoose);

            return viewModelItem;
        }

        private TypesTreeItemViewModel GetExpandableByIndex(int index)
        {
            TypesTreeItemViewModel viewModelItem;

            if (_previousTypes.Count > 0)
            {
                bool foundExpandable = false;
                int offset = 0;
                while ((index + offset) <= _currentItem.Children.Count && !foundExpandable)
                {
                    TypesTreeItemViewModel child = _currentItem.Children[offset];
                    if (!child.CanExpand)
                    {
                        offset += 1;
                    }
                    else
                    {
                        foundExpandable = true;
                    }
                }
                
                viewModelItem = _currentItem.Children[index + offset - 1];
            }
            else
            {
                viewModelItem = _viewModel.Items[index - 1];
            }

            return viewModelItem;
        }
    }
}