﻿using CommandLine.Core.Hosting;
using CommandLine.Core.Hosting.Abstractions;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CommandLine.Core.CommandLineUtils
{
    /// <summary>
    /// Provides methods to help integrate with McMaster.Extensions.CommandLineUtils.
    /// </summary>
    public static class CommandUtilsHostBuilderExtensions
    {
        /// <summary>
        /// Enables integration with McMaster.Extensions.CommandLineUtils.
        /// </summary>
        public static ICommandLineHostBuilder UseCommandLineUtils(this ICommandLineHostBuilder builder) =>
            builder.UseSetting(HostDefaults.WorkingDirectoryKey, Directory.GetCurrentDirectory())
                   .UseSetting(HostDefaults.AllowUnknownArgumentsKey, Boolean.FalseString)
                   .ConfigureServices(services => services.AddCommonServices()
                                                          .AddRootApplication()
                                                          .AddApplicationDelegate());

        private static IServiceCollection AddCommonServices(this IServiceCollection services) =>
            services.AddSingleton(PhysicalConsole.Singleton)
                    .AddSingleton<IHelpTextGenerator>(DefaultHelpTextGenerator.Singleton);

        private static IServiceCollection AddRootApplication(this IServiceCollection services) =>
            services.AddSingleton(provider =>
            {
                var config = provider.GetService<IConfiguration>();

                var rootApp = new RootCommandLineApplication(
                    provider.GetService<IHelpTextGenerator>(),
                    provider.GetService<IConsole>(),
                    config[HostDefaults.WorkingDirectoryKey],
                    !Boolean.Parse(config[HostDefaults.AllowUnknownArgumentsKey]));

                rootApp.Conventions.UseConventions(provider);

                return rootApp;
            });

        private static IServiceCollection AddApplicationDelegate(this IServiceCollection services) =>
            services.AddSingleton(provider =>
            {
                var app = provider.GetRequiredService<RootCommandLineApplication>();
                return new ApplicationDelegate(args => Task.FromResult(app.Execute(args)));
            });

    }
}
