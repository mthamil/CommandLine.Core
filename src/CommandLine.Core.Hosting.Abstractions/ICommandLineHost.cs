using System;
using System.Threading.Tasks;

namespace CommandLine.Core.Hosting.Abstractions
{
    public interface ICommandLineHost
    {
        IServiceProvider Services { get; }

        Task<int> RunAsync();
    }
}
