using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;

namespace CommandLine.Core.Hosting.CommandLineUtils.Options
{
    public interface ISharedOptions : IEnumerable<CommandOption>
    {
        CommandOption this[string longName] { get; }
    }
}
