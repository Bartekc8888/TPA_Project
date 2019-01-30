using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using Interfaces;

namespace Logging
{
    [Export(typeof(ILogging))]
    public class FileLogging : ILogging
    {
        private TraceListener listener;

        public FileLogging()
        {
            listener = new TextWriterTraceListener("logs.txt", "TpaLogs");
        }
        public FileLogging(string path, string instance)
        {
            listener = new TextWriterTraceListener(path, instance);
        }

        public Task Debug(string message)
        {
            Task task = Task.Run(() =>
            {
                listener.WriteLine("Debug :: " + message);
                listener.Flush();
            });
            return task;
        }

        public Task Error(string message)
        {
            Task task = Task.Run(() =>
            {
                listener.WriteLine("Error :: " + message);
                listener.Flush();
            });
            return task;
        }

        public Task Fatal(string message)
        {
            Task task = Task.Run(() =>
            {
                listener.WriteLine("Fatal :: " + message);
                listener.Flush();
            });
            return task;
        }

        public Task Info(string message)
        {
            Task task = Task.Run(() =>
            {
                listener.WriteLine("Info :: " + message);
                listener.Flush();
            });
            return task;
        }

        public Task Warn(string message)
        {
            Task task = Task.Run(() =>
            {
                listener.WriteLine("Warn :: " + message);
                listener.Flush();
            });
            return task;
        }
    }
}
