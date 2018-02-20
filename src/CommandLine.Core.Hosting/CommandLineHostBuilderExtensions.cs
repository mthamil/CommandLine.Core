using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace CommandLine.Core.Hosting
{
    public static class CommandLineHostBuilderExtensions
    {
        public static ICommandLineHostBuilder Configure(this ICommandLineHostBuilder builder, Action<IApplicationBuilder> configureApp)
        {
            if (configureApp == null)
                throw new ArgumentNullException(nameof(configureApp));

            return builder.UseSetting(HostDefaults.ApplicationNameKey, configureApp.GetMethodInfo().DeclaringType.Assembly.GetName().Name)
                          .ConfigureServices(services => services.AddSingleton<IStartup>(new DelegateStartup(configureApp)));
        }

        public static ICommandLineHostBuilder ConfigureLogging(this ICommandLineHostBuilder builder, Action<ILoggingBuilder> configureLogging)
        {
            if (configureLogging == null)
                throw new ArgumentNullException(nameof(configureLogging));

            return builder.ConfigureServices(services => services.AddLogging(configureLogging));
        }

        public static ICommandLineHostBuilder UseStartup<TStartup>(this ICommandLineHostBuilder builder) where TStartup : class, IStartup, new()
        {
            return builder.UseSetting(HostDefaults.ApplicationNameKey, typeof(TStartup).Assembly.GetName().Name)
                          .ConfigureServices(services => services.AddSingleton<IStartup, TStartup>());
        }
    }
}
