using GUI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Logic;

namespace GUIText
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [STAThread]
        static void Main(string[] args)
        {
            log.Info("Starting program");

            string checkIfWpf = Console.ReadLine();

            if(checkIfWpf=="w")
            {
                RunApplication();
            }
            else
            {
                CommandLineViewModel cm = new CommandLineViewModel();
            }
        }

        private static void RunApplication()
        {
            log.Info("Running window application");

            var application = new App();
            application.InitializeComponent();
            application.Run();
        }
    }
}
