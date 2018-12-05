using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class FileLogging : ILogging
    {
        private TraceListener listener;

        public FileLogging(string fileName, string instanceName)
        {
            listener = new TextWriterTraceListener(fileName, instanceName);
        }

        public void Debug(string message)
        {
            listener.WriteLine("Debug :: " + message);
            listener.Flush();
        }

        public void Error(string message)
        {
            listener.WriteLine("Error :: " + message);
            listener.Flush();
        }

        public void Fatal(string message)
        {
            listener.WriteLine("Fatal :: " + message);
            listener.Flush();
        }

        public void Info(string message)
        {
            listener.WriteLine("Info :: " + message);
            listener.Flush();
        }

        public void Warn(string message)
        {
            listener.WriteLine("Warn :: " + message);
            listener.Flush();
        }
    }
}
