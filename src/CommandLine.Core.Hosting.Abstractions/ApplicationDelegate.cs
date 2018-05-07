using System.Threading.Tasks;

namespace CommandLine.Core.Hosting.Abstractions
{
    /// <summary>
    /// An abstraction for commandline application signatures.
    /// </summary>
    /// <param name="args">The application commandline arguments.</param>
    /// <returns>A return code.</returns>
    public delegate Task<int> ApplicationDelegate(string[] args);
}
