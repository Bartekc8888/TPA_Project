using System.ComponentModel.Composition;
using System.Diagnostics;

namespace Logging
{
    [Export(typeof(ILogging))]
    public class FileLogging : ILogging
    {
        private TraceListener listener;

        public FileLogging(string fileName, string instanceName)
        {
            listener = new TextWriterTraceListener(fileName, instanceName);
        }        
        
        [ImportingConstructor]
        public FileLogging(FileLoggingSettings loggingSettings)
        {
            listener = new TextWriterTraceListener(loggingSettings.FileName, loggingSettings.InstanceName);
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
