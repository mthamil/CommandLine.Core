using CommandLine.Core.Hosting.Abstractions;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CommandLine.Core.Hosting.CommandLineUtils
{
    public static class CommandUtilsHostBuilderExtensions
    {
        public static ICommandLineHostBuilder UseCommandLineUtils(this ICommandLineHostBuilder builder) =>
            builder.UseSetting(HostDefaults.WorkingDirectoryKey, Directory.GetCurrentDirectory())
                   .UseSetting(HostDefaults.AllowUnknownArgumentsKey, Boolean.FalseString)
                   .ConfigureServices(services => services.AddRootApplication()
                                                          .AddApplicationDelegate());

        private static IServiceCollection AddRootApplication(this IServiceCollection services) =>
            services.AddSingleton(provider =>
            {
                var config = provider.GetService<IConfiguration>();

                return new RootCommandLineApplication(
                    provider.GetService<IHelpTextGenerator>() ?? DefaultHelpTextGenerator.Singleton,
                    provider.GetService<IConsole>() ?? PhysicalConsole.Singleton,
                    config[HostDefaults.WorkingDirectoryKey],
                    !Boolean.Parse(config[HostDefaults.AllowUnknownArgumentsKey]));
            });

        private static IServiceCollection AddApplicationDelegate(this IServiceCollection services) =>
            services.AddSingleton(provider =>
            {
                var app = provider.GetRequiredService<RootCommandLineApplication>();
                return new ApplicationDelegate(args => Task.FromResult(app.Execute(args)));
            });
    }
}
