
namespace CLI
{
    class Program
    {
        static CliBootstrapper mefBootstrapper = new CliBootstrapper();

        static void Main(string[] args)
        {
            mefBootstrapper.Run();
        }
    }
}
