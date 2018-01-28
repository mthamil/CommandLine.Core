using System.Threading.Tasks;

namespace CommandLine.Core.Hosting
{
    public delegate Task<int> ApplicationDelegate(string[] args);
}
