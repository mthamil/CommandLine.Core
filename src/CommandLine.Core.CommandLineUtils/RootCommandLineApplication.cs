using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;

namespace CommandLine.Core.CommandLineUtils
{
    public class RootCommandLineApplication : CommandLineApplication
    {
        public RootCommandLineApplication(IHelpTextGenerator helpTextGenerator,
                                          IConsole console,
                                          string workingDirectory,
                                          bool throwOnUnexpectedArg)
            : base(helpTextGenerator, console, workingDirectory, throwOnUnexpectedArg)
        {
        }
    }
}
