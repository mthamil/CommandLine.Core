using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CommandLine.Core.Hosting
{
    public static class CommandLineHostBuilderExtensions
    {
        public static ICommandLineHostBuilder Configure(this ICommandLineHostBuilder builder, Action<IApplicationBuilder> configureApp)
        {
            return builder.UseSetting(HostDefaults.ApplicationNameKey, configureApp.GetMethodInfo().DeclaringType.Assembly.GetName().Name)
                          .ConfigureServices(services => services.AddSingleton<IStartup>(new DelegateStartup(configureApp)));
        }

        public static ICommandLineHostBuilder UseStartup<TStartup>(this ICommandLineHostBuilder builder) where TStartup : class, IStartup, new()
        {
            return builder.UseSetting(HostDefaults.ApplicationNameKey, typeof(TStartup).Assembly.GetName().Name)
                          .ConfigureServices(services => services.AddSingleton<IStartup, TStartup>());
        }
    }
}
