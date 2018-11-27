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

        private TypesTreeViewModel _viewModel;
        private TypesTreeItemViewModel _currentItem;
        private Stack<TypesTreeItemViewModel> _previousTypes;

        public List<TypesTreeItemViewModel> pairs;

        private int i = 1;
        private bool isGoingBack;

        public CommandLineView()
        {
            Log.Info("Creating CommandLineItemViewModel");

            _previousTypes = new Stack<TypesTreeItemViewModel>();
            _viewModel = new TypesTreeViewModel();

            PrintHeaders(_viewModel.Items);
            while (true)
            {
                i = 1;
                TypesTreeItemViewModel temp = _currentItem;
                _currentItem = NewChosen();
                if (isGoingBack)
                {
                }
                else
                {
                    _previousTypes.Push(temp);
                }

                printTypeWithChildren(_currentItem);
            }
        }

        public void printTypeWithChildren(TypesTreeItemViewModel item)
        {
            Log.Debug("Printing current type with members");

            string itemString = "";
            string name = item.ItemDescription;

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
                    itemString += i.ToString();
                    if (i >= 10)
                        itemString += "   " + tva.ItemDescription + " " + tva.ItemType + " " + tva.ItemName +
                                      Environment.NewLine;
                    else
                        itemString += "    " + tva.ItemDescription + " " + tva.ItemType + " " + tva.ItemName +
                                      Environment.NewLine;
                    i++;
                }
                else
                {
                    itemString += "     " + tva.ItemDescription + " " + tva.ItemType + " " + tva.ItemName +
                                  Environment.NewLine;
                }
            }

            itemString += Environment.NewLine + "Press selected number to expand or 0 to come back";
            Console.Clear();
            Console.WriteLine(itemString);
        }

        private void PrintHeaders(ObservableCollection<TypesTreeItemViewModel> items)
        {
            string itemString = "Found types" + Environment.NewLine;
            foreach (TypesTreeItemViewModel itemViewModel in items)
            {
                itemString += i.ToString();
                itemString += "   " + itemViewModel.ItemDescription + " " + itemViewModel.ItemType + " " +
                              itemViewModel.ItemName + Environment.NewLine;

                i++;
            }

            itemString += Environment.NewLine + "Press selected number to expand or 0 to come back";
            Console.Clear();
            Console.WriteLine(itemString);
        }

        private TypesTreeItemViewModel NewChosen()
        {
            Log.Debug("User chooses new type");

            isGoingBack = false;
            TypesTreeItemViewModel tva = null;
            int n;
            bool isNumber;
            bool didUserChoose = false;

            do
            {
                string chosen = Console.ReadLine();
                isNumber = int.TryParse(chosen, out n);

                if (n > 0 && isNumber)
                {
                    tva = GetExpandableByIndex(n);
                    if (tva != null)
                    {
                        didUserChoose = true;
                    }
                }

                if (n == 0 && isNumber)
                {
                    isGoingBack = true;
                    if (_previousTypes.Count != 0)
                    {
                        tva = _previousTypes.Pop();
                    }
                    else
                    {
                        tva = _currentItem;
                    }

                    didUserChoose = true;
                }
            } while (!didUserChoose);

            return tva;
        }

        private TypesTreeItemViewModel GetExpandableByIndex(int index)
        {
            TypesTreeItemViewModel tva = null;

            if (_previousTypes.Count > 0)
            {
                bool foundExpandable = false;
                int offset = 0;
                while ((index + offset) <= _currentItem.Children.Count && !foundExpandable)
                {
                    var child = _currentItem.Children[offset];
                    if (!child.CanExpand)
                    {
                        offset += 1;
                    }
                    else
                    {
                        foundExpandable = true;
                    }
                }
                
                tva = _currentItem.Children[index + offset - 1];
            }
            else
            {
                tva = _viewModel.Items[index - 1];
            }

            return tva;
        }
    }
}