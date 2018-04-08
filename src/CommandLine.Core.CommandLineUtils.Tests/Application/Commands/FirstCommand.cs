using McMaster.Extensions.CommandLineUtils;

namespace CommandLine.Core.CommandLineUtils.Tests.Application.Commands
{
    public class FirstCommand : CommandLineApplication
    {
        private readonly IConsole _console;

        public FirstCommand(IConsole console, SecondCommand second)
        {
            _console = console;

            Name = "first";
            Commands.Add(second);
            second.Parent = this;
        }
    }
}
