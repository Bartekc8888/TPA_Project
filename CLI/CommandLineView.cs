using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using ViewModel.Logic;

namespace CLI
{
    [Export()]
    class CommandLineView
    {

        private const string ExitCharacter = "0";
        private const string SerializationMode = "W";
        private const string DeserializationMode = "L";

        private TypesTreeViewModel _viewModel;
        private TypeViewModelAbstract _currentItem;
        private Stack<TypeViewModelAbstract> _previousTypes;

        private bool _isGoingBack;

        public CommandLineView()
        {

            _previousTypes = new Stack<TypeViewModelAbstract>();
            _viewModel = new TypesTreeViewModel();
        }

        public void Run()
        {
            GetFileFromUser();
            
            PrintHeaders(_viewModel.Items);
            while (true)
            {
                TypeViewModelAbstract temp = _currentItem;
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

        private void PrintTypeWithChildren(TypeViewModelAbstract item)
        {

            string itemString = "";
            string name = item.Description;
            int index = 1;

            itemString += item.Description + " " + item.TypeName + " " + item.Name + Environment.NewLine;
            item.IsExpanded = true;
            foreach (TypeViewModelAbstract tva in item.Children)
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

        private void PrintHeaders(ObservableCollection<TypeViewModelAbstract> items)
        {
            string itemString = "Found types" + Environment.NewLine;
            int index = 1;
            
            foreach (TypeViewModelAbstract itemViewModel in items)
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

        private TypeViewModelAbstract NewChosen()
        {

            _isGoingBack = false;
            TypeViewModelAbstract viewModelItem = null;
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

        private TypeViewModelAbstract GetExpandableByIndex(int index)
        {
            TypeViewModelAbstract viewModelItem;

            if (_previousTypes.Count > 0)
            {
                bool foundExpandable = false;
                int offset = 0;
                while ((index + offset) <= _currentItem.Children.Count && !foundExpandable)
                {
                    TypeViewModelAbstract child = _currentItem.Children[offset];
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