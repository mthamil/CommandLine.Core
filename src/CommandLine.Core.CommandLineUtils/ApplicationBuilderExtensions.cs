using CommandLine.Core.Hosting.Abstractions;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommandLine.Core.CommandLineUtils
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures the root command of the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configureApp">An optional callback that can be used to configure the root application command.</param>
        public static IApplicationBuilder UseCommands(this IApplicationBuilder app, Action<CommandLineApplication> configureApp = null)
        {
            var rootApp = app.ApplicationServices.GetService<RootCommandLineApplication>();
            configureApp?.Invoke(rootApp);
            return app;
        }
    }
}
