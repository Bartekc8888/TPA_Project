using GUI.View.TypesView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Logic
{
    class CommandLineItemViewModel
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TypeViewAbstract CurrentType { get; set; }
        public Stack<TypeViewAbstract> PreviousTypes { get; set; }

        public List<TypeViewAbstract> pairs;

        private int i = 1;
        private bool isBacking = false;

        public ObservableCollection<TypeViewAbstract> children;
  
        public CommandLineItemViewModel(TypeViewAbstract type)
        {
            log.Info("Creating CommandLineItemViewModel");

            pairs = new List<TypeViewAbstract>();
            PreviousTypes = new Stack<TypeViewAbstract>();
            children = new ObservableCollection<TypeViewAbstract>();
            CurrentType = type;

            SetChildrenAndPairs(CurrentType);
            printCurrentTypeWithChildren();
            while(CurrentType.HaveChildren)
            {
                i = 1;
                TypeViewAbstract temp = CurrentType;
                CurrentType = NewChosen();
                if (isBacking)
                {

                }
                else
                {
                    PreviousTypes.Push(temp);
                }
                SetChildrenAndPairs(CurrentType);
                printCurrentTypeWithChildren();
            }
        }

        public void printCurrentTypeWithChildren()
        {
            log.Debug("Printing current type with members");

            string ItemString = "";
            string name= CurrentType.Description;

            ItemString += CurrentType.Description + " " + CurrentType.TypeName + " " + CurrentType.Name + Environment.NewLine;
            foreach(TypeViewAbstract tva in children)
            {
                if(tva.Description != name)
                {
                    ItemString += Environment.NewLine;
                    name = tva.Description;
                }
                if(tva.HaveChildren)
                {
                    ItemString += i.ToString();
                    if(i>=10)
                        ItemString += "   " + tva.Description + " " + tva.TypeName + " " + tva.Name + Environment.NewLine;
                    else
                        ItemString += "    " + tva.Description + " " + tva.TypeName + " " + tva.Name + Environment.NewLine;
                    i++;
                }
                else
                {
                    ItemString += "     " + tva.Description + " " + tva.TypeName + " " + tva.Name + Environment.NewLine;
                }

            }
            ItemString += Environment.NewLine + "Press selected number to expand or 0 to come back";
            Console.Clear();
            Console.WriteLine(ItemString);
        }

       private void SetChildrenAndPairs(TypeViewAbstract currentType)
        {
            log.Debug("Set members of current type and check if member has own members");

            children.Clear();
            pairs.Clear();
            if (currentType.HaveChildren)
            {
                foreach (TypeViewAbstract tva in currentType.CreateChildren())
                {
                    children.Add(tva);
                    if(tva.HaveChildren)
                    {
                        pairs.Add(tva);
                    }
                }
            }
        }

        private TypeViewAbstract NewChosen()
        {
            log.Debug("User chooses new type");

            isBacking = false;
            TypeViewAbstract tva=null;
            string chosen = "";
            int n;
            bool isNumber;
            do
            {
                chosen = Console.ReadLine();
                isNumber = int.TryParse(chosen, out n);

                if (n>0 && n < pairs.Count+1 && isNumber)
                    tva = pairs[Int32.Parse(chosen) - 1];
                if (n == 0 && isNumber)
                {
                    isBacking = true;
                    if (PreviousTypes.Count != 0)
                        tva = PreviousTypes.Pop();
                    else
                        tva = CurrentType;
                       
                }
            }
            while (!(n>=0 && n<=pairs.Count+1 && isNumber));

            return tva;
        }
    }
}
