using System.Threading.Tasks;

namespace MarkdownParser
{
    public class Program
    {
        private static Task Main(string[] args)
            => new RunAsync().Run();
    }
}
