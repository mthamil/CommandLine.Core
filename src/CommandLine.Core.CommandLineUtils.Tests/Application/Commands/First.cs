using McMaster.Extensions.CommandLineUtils;

namespace CommandLine.Core.CommandLineUtils.Tests.Application.Commands
{
    public class First
    {
        private readonly IConsole _console;

        public First(IConsole console)
        {
            _console = console;
        }

        public Second Subcommand { get; set; }
    }
}
