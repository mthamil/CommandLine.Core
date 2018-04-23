using CommandLine.Core.Hosting;
using CommandLine.Core.Hosting.Abstractions;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;
using Microsoft.Extensions.Configuration;
using System;

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
                                          IHostingEnvironment environment,
                                          IConfiguration configuration)
            : base(helpTextGenerator,
                   console,
                   environment.WorkingDirectory,
                   !Boolean.Parse(configuration[HostDefaults.AllowUnknownArgumentsKey] ?? Boolean.FalseString))
        {
        }
    }
}
