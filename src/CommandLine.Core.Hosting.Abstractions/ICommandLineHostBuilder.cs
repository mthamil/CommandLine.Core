using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommandLine.Core.Hosting.Abstractions
{
    /// <summary>
    /// A builder that helps to configure a command line application.
    /// </summary>
    public interface ICommandLineHostBuilder
    {
        /// <summary>
        /// Adds or updates a configuration value.
        /// </summary>
        ICommandLineHostBuilder UseSetting(string key, string value);

        /// <summary>
        /// Adds a function that configures services for the application.
        /// It may be called multiple times.
        /// </summary>
        ICommandLineHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);

        /// <summary>
        /// Builds the <see cref="ICommandLineHost"/> that hosts the application.
        /// </summary>
        ICommandLineHost Build();
    }
}
