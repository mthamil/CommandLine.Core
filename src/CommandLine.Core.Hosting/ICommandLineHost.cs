using System;
using System.Threading.Tasks;

namespace CommandLine.Core.Hosting
{
    public interface ICommandLineHost
    {
        IServiceProvider Services { get; }

        Task<int> RunAsync();
    }
}
