using System;
using System.Reflection;
using log4net;

namespace CLI
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger
               (MethodBase.GetCurrentMethod().DeclaringType);

        [STAThread]
        static void Main(string[] args)
        {
            Log.Info("Starting program");

            CommandLineView commandLine = new CommandLineView();
            commandLine.Run();
        }
    }
}
