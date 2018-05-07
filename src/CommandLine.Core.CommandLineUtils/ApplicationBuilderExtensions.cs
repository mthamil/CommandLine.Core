using CommandLine.Core.Hosting.Abstractions;
using CommandLineUtils.Extensions.Conventions;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CommandLine.Core.CommandLineUtils
{
    /// <summary>
    /// Provides methods to help configure a CommandLineUtils application.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures a <c>McMaster.Extensions.CommandLineUtils</c> application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configureApp">An optional callback that can be used to configure the root application command.</param>
        public static IApplicationBuilder UseCommandLineUtils(this IApplicationBuilder app, Action<CommandLineApplication> configureApp = null) =>
            app.Use(args =>
            {
                var rootApp = app.ApplicationServices.GetService<RootCommandLineApplication>();
                rootApp.Conventions.UseDefaultConventionsWithServices(app.ApplicationServices);
                configureApp?.Invoke(rootApp);
                return Task.FromResult(rootApp.Execute(args));
            });
    }
}
