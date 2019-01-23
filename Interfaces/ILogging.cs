using System.Threading.Tasks;

namespace Interfaces
{
    public interface ILogging
    {
        Task Debug(string message);
        Task Info(string message);
        Task Warn(string message);
        Task Error(string message);
        Task Fatal(string message);
    }
}
