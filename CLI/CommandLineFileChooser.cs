using System;
using ViewModel.Logic;

namespace CLI
{
    public class CommandLineFileChooser : IFileChooser
    {
        private string ExitCharacter;

        public CommandLineFileChooser(string exitCharacter)
        {
            ExitCharacter = exitCharacter;
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