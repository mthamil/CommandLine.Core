using Microsoft.Extensions.Configuration;
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
        /// Initializes the application's configuration. This may be called multiple times.
        /// </summary>
        ICommandLineHostBuilder ConfigureAppConfiguration(Action<IConfigurationBuilder> configureAppConfiguration);

        /// <summary>
        /// Configures services for the application. This may be called multiple times.
        /// </summary>
        ICommandLineHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);

        /// <summary>
        /// Builds the <see cref="ICommandLineHost"/> that hosts the application.
        /// </summary>
        ICommandLineHost Build();
    }
}
