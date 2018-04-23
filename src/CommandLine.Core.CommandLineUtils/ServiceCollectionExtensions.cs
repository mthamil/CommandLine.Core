using CommandLine.Core.Hosting;
using CommandLine.Core.Hosting.Abstractions;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommandLine.Core.CommandLineUtils
{
    /// <summary>
    /// Provides methods to help integrate with McMaster.Extensions.CommandLineUtils.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <c>McMaster.Extensions.CommandLineUtils</c> services.
        /// </summary>
        public static IServiceCollection AddCommandLineUtils(this IServiceCollection services) =>
            services.AddCommonServices()
                    .AddRootApplication();

        private static IServiceCollection AddCommonServices(this IServiceCollection services) =>
            services.AddSingleton(PhysicalConsole.Singleton)
                    .AddSingleton<IHelpTextGenerator>(DefaultHelpTextGenerator.Singleton);

        private static IServiceCollection AddRootApplication(this IServiceCollection services) =>
            services.AddSingleton(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                var environment = provider.GetService<IHostingEnvironment>();

                var rootApp = new RootCommandLineApplication(
                    provider.GetService<IHelpTextGenerator>(),
                    provider.GetService<IConsole>(),
                    environment.WorkingDirectory,
                    !Boolean.Parse(config[HostDefaults.AllowUnknownArgumentsKey] ?? Boolean.FalseString));

                rootApp.Conventions.UseDefaultConventionsWithServices(provider);

                return rootApp;
            });
    }
}
