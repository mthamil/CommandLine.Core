using System.Threading.Tasks;

namespace CommandLine.Core.Hosting.Abstractions
{
    public delegate Task<int> ApplicationDelegate(string[] args);
}
