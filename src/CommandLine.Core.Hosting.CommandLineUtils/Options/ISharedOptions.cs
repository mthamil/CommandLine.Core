using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;

namespace CommandLine.Core.Hosting.CommandLineUtils.Options
{
    /// <summary>
    /// Contains command options shared among multiple commands.
    /// </summary>
    public interface ISharedOptions : IEnumerable<CommandOption>
    {
        /// <summary>
        /// Retrives a shared command option by its long name.
        /// </summary>
        CommandOption this[string longName] { get; }
    }
}
