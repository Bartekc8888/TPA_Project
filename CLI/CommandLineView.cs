using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using log4net;
using ViewModel.Logic;

namespace CLI
{
    class CommandLineView
    {
        private static readonly ILog Log = LogManager.GetLogger
            (MethodBase.GetCurrentMethod().DeclaringType);

        private const string ExitCharacter = "0";
        private const string SerializationMode = "W";
        private const string DeserializationMode = "L";

        private TypesTreeViewModel _viewModel;
        private TypeViewAbstract _currentItem;
        private Stack<TypeViewAbstract> _previousTypes;

        private bool _isGoingBack;

        public CommandLineView()
        {
            Log.Info("Creating CommandLineItemViewModel");

            _previousTypes = new Stack<TypeViewAbstract>();
            _viewModel = new TypesTreeViewModel(new CommandLineFileChooser(ExitCharacter));
        }

        public void Run()
        {
            GetFileFromUser();
            
            PrintHeaders(_viewModel.Items);
            while (true)
            {
                TypeViewAbstract temp = _currentItem;
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
                    _previousTypes.Clear();
                    PrintHeaders(_viewModel.Items);
                }
            }
        }

        private void GetFileFromUser()
        {
            do
            {
                _viewModel.ChooseFile();
            } while (!_viewModel.IsPathValid() && !ExitCharacter.Equals(_viewModel.SelectedPath));

            if (ExitCharacter.Equals(_viewModel.SelectedPath))
            {
                Environment.Exit(0);
            }
        }

        private void PrintTypeWithChildren(TypeViewAbstract item)
        {
            Log.Debug("Printing current type with members");

            string itemString = "";
            string name = item.Description;
            int index = 1;

            itemString += item.Description + " " + item.TypeName + " " + item.Name + Environment.NewLine;
            item.IsExpanded = true;
            foreach (TypeViewAbstract tva in item.Children)
            {
                if (tva.Description != name)
                {
                    itemString += Environment.NewLine;
                    name = tva.Description;
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
                
                itemString += "   " + tva.Description + " " + tva.TypeName + " " + tva.Name +
                              Environment.NewLine;
            }

            itemString += GetMainMenuString();
            Console.Clear();
            Console.WriteLine(itemString);
        }

        private string GetMainMenuString()
        {
            string menuString = Environment.NewLine + "Press selected number to expand or 0 to come back";
            menuString += Environment.NewLine + "Press 'w' to start serialization or 'l' to start deserialization";

            return menuString;
        }

        private void PrintHeaders(ObservableCollection<TypeViewAbstract> items)
        {
            string itemString = "Found types" + Environment.NewLine;
            int index = 1;
            
            foreach (TypeViewAbstract itemViewModel in items)
            {
                itemString += index.ToString();
                itemString += "   " + itemViewModel.Description + " " + itemViewModel.TypeName + " " +
                              itemViewModel.Name + Environment.NewLine;

                index++;
            }

            itemString += GetMainMenuString();
            Console.Clear();
            Console.WriteLine(itemString);
        }

        private TypeViewAbstract NewChosen()
        {
            Log.Debug("User chooses new type");

            _isGoingBack = false;
            TypeViewAbstract viewModelItem = null;
            bool didUserChoose = false;

            do
            {
                string chosen = Console.ReadLine();
                bool isNumber = int.TryParse(chosen, out int parsedNumber);

                if (isNumber)
                {
                    if (parsedNumber > 0)
                    {
                        viewModelItem = GetExpandableByIndex(parsedNumber);
                        if (viewModelItem != null)
                        {
                            didUserChoose = true;
                        }
                    }

                    if (parsedNumber == 0)
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
                }
                else if (chosen != null)
                {

                    if (SerializationMode.Equals(chosen.ToUpper()))
                    {
                        HandleSerializationMode();
                        return null;
                    }

                    if (DeserializationMode.Equals(chosen.ToUpper()))
                    {
                        HandleDeserializationMode();
                        return null;
                    }
                }
            } while (!didUserChoose);

            return viewModelItem;
        }

        private TypeViewAbstract GetExpandableByIndex(int index)
        {
            TypeViewAbstract viewModelItem;

            if (_previousTypes.Count > 0)
            {
                bool foundExpandable = false;
                int offset = 0;
                while ((index + offset) <= _currentItem.Children.Count && !foundExpandable)
                {
                    TypeViewAbstract child = _currentItem.Children[offset];
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

        private void HandleSerializationMode()
        {
            Console.Clear();
            Console.WriteLine("Give a path for serialization file: " + Environment.NewLine);
            string givenPath = Console.ReadLine();

            _viewModel.SerializationPath = givenPath;
            _viewModel.SerializeData();
        }

        private void HandleDeserializationMode()
        {
            Console.Clear();
            Console.WriteLine("Give a path for deserialization file: " + Environment.NewLine);
            string givenPath = Console.ReadLine();

            _viewModel.SerializationPath = givenPath;
            _viewModel.DeserializeData();
        }
    }
}