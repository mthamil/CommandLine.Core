using CommandLine.Core.Hosting.Abstractions;
using CommandLine.Core.Hosting.CommandLineUtils.Options;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommandLine.Core.Hosting.CommandLineUtils
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds any <see cref="CommandLineApplication"/>s that have been registered as a service to the
        /// root application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configureApp">An optional callback that can be used to configure the root application command.</param>
        public static IApplicationBuilder UseCommands(this IApplicationBuilder app, Action<CommandLineApplication> configureApp = null)
        {
            var rootApp = app.ApplicationServices.GetService<RootCommandLineApplication>();
            using (var rootScope = app.ApplicationServices.CreateScope())
            {
                var commands = rootScope.ServiceProvider.GetServices<CommandLineApplication>();
                foreach (var command in commands)
                {
                    rootApp.Commands.Add(command);
                    command.Parent = rootApp;
                }

                var sharedOptions = app.ApplicationServices.GetService<ISharedOptions>();
                if (sharedOptions != null)
                {
                    rootApp.Options.AddRange(sharedOptions);
                }
            }

            configureApp?.Invoke(rootApp);

            return app;
        }
    }
}
