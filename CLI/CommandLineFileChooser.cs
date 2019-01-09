using System;
using System.ComponentModel.Composition;
using ViewModel.Logic;

namespace CLI
{
    [Export(typeof(IFileChooser))]
    public class CommandLineFileChooser : IFileChooser
    {
        private string ExitCharacter = "0";

        public CommandLineFileChooser()
        {
        }

        public string ChooseFilePath()
        {
            string chosenPath = "";
            
            Console.WriteLine("Give a path to assembly file or type in " + ExitCharacter + " to go back.");
            chosenPath = Console.ReadLine();

            return chosenPath;
        }
    }
}