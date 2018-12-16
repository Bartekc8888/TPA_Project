using System.ComponentModel.Composition;

namespace Logging
{
    [Export(typeof(FileLoggingSettings))]
    public class FileLoggingSettings
    {
        public string FileName { get; set; }
        public string InstanceName { get; set; }
    }
}