using CommandLine.Core.CommandLineUtils.Options;
using CommandLine.Core.Hosting;
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
    public static class CommandUtilsHostBuilderExtensions
    {
        /// <summary>
        /// Enables integration with McMaster.Extensions.CommandLineUtils.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
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

                using (var rootScope = provider.CreateScope())
                {
                    foreach (var command in rootScope.ServiceProvider.GetServices<CommandLineApplication>())
                    {
                        rootApp.Commands.Add(command);
                        command.Parent = rootApp;
                    }

                    var sharedOptions = rootScope.ServiceProvider.GetService<ISharedOptions>();
                    if (sharedOptions != null)
                    {
                        rootApp.Options.AddRange(sharedOptions);
                    }
                }

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
