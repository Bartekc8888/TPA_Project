using System;

namespace CLI
{
    class Program
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger
               (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [STAThread]
        static void Main(string[] args)
        {
            Log.Info("Starting program");

            CommandLineView cm = new CommandLineView();
        }
    }
}
