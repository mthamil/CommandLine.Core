using CommandLine.Core.CommandLineUtils.Options;
using McMaster.Extensions.CommandLineUtils;
using System;

namespace CommandLine.Core.CommandLineUtils.Tests.Application.Commands
{
    public class SecondCommand : CommandLineApplication
    {
        private readonly IConsole _console;
        private readonly ISharedOptions _sharedOptions;

        public SecondCommand(IConsole console, ISharedOptions sharedOptions)
        {
            _console = console;
            _sharedOptions = sharedOptions;

            Name = "second";
            OnExecute(() => Int32.Parse(_sharedOptions["value"].Value()));
        }
    }
}
