using System;
using System.Threading.Tasks;

namespace CommandLine.Core.Hosting.Abstractions
{
    /// <summary>
    /// A command line application host.
    /// </summary>
    public interface ICommandLineHost
    {
        /// <summary>
        /// The <see cref="IServiceProvider"/> for the application.
        /// </summary>
        IServiceProvider Services { get; }

        /// <summary>
        /// Executes the application.
        /// </summary>
        Task<int> RunAsync();
    }
}
