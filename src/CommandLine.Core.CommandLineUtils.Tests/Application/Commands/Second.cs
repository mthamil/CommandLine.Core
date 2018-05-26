using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandLine.Core.CommandLineUtils.Tests.Application.Commands
{
    public class Second
    {
        private readonly IConsole _console;
        private readonly IEnumerable<CommandOption> _options;

        public Second(IConsole console, IEnumerable<CommandOption> options)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public Task<int> OnExecuteAsync()
        {
            return Task.FromResult(Int32.Parse(_options.Single(o => o.LongName == "value").Value()));
        }
    }
}
