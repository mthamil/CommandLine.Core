using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;

namespace CommandLine.Core.CommandLineUtils
{
    /// <summary>
    /// A <see cref="CommandLineApplication"/> that is the first command of an application.
    /// </summary>
    public class RootCommandLineApplication : CommandLineApplication
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RootCommandLineApplication"/>.
        /// </summary>
        public RootCommandLineApplication(IHelpTextGenerator helpTextGenerator,
                                          IConsole console,
                                          string workingDirectory,
                                          bool throwOnUnexpectedArg)
            : base(helpTextGenerator, console, workingDirectory, throwOnUnexpectedArg)
        {
        }
    }
}
