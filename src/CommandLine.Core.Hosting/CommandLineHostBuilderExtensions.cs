using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CommandLine.Core.Hosting
{
    /// <summary>
    /// Provides methods for configuring a commandline application.
    /// </summary>
    public static class CommandLineHostBuilderExtensions
    {
        /// <summary>
        /// Adds a delegate that configures the application.
        /// </summary>
        public static ICommandLineHostBuilder Configure(this ICommandLineHostBuilder builder, Action<IApplicationBuilder> configureApp)
        {
            if (configureApp == null)
                throw new ArgumentNullException(nameof(configureApp));

            return builder.UseSetting(HostDefaults.ApplicationNameKey, configureApp.Method.DeclaringType.Assembly.GetName().Name)
                          .ConfigureServices(services =>
                          {
                              var startupDescriptor = services.SingleOrDefault(s => s.ImplementationType.IsDelegateStartupType());
                              if (startupDescriptor != null)
                                  services.Remove(startupDescriptor);

                              services.AddSingleton<IStartup>(new DelegateStartup<IServiceCollection>
                              {
                                  ConfigureApp = configureApp
                              });
                          });
        }

        /// <summary>
        /// Adds a delegate that configures a custom service container.
        /// </summary>
        public static ICommandLineHostBuilder ConfigureContainer<TContainerBuilder>(this ICommandLineHostBuilder builder, Action<TContainerBuilder> configureContainer)
        {
            if (configureContainer == null)
                throw new ArgumentNullException(nameof(configureContainer));

            return builder.ConfigureServices(services =>
            {
                var startupDescriptor = services.SingleOrDefault(s => s.ImplementationType.IsDelegateStartupType());
                if (startupDescriptor != null)
                    services.Remove(startupDescriptor);

                var startup = startupDescriptor?.ImplementationInstance as IStartup;
                services.AddSingleton<IStartup>(new DelegateStartup<TContainerBuilder>
                {
                    ConfigureApp = b => startup?.Configure(b),
                    ConfigureContainerBuilder = configureContainer
                });
            });
        }

        private static bool IsDelegateStartupType(this Type type) => type != null &&
                                                                     type.IsGenericType &&
                                                                     type.GetGenericTypeDefinition() == typeof(DelegateStartup<>);

        /// <summary>
        /// Adds a delegate that configures application logging.
        /// </summary>
        public static ICommandLineHostBuilder ConfigureLogging(this ICommandLineHostBuilder builder, Action<ILoggingBuilder> configureLogging)
        {
            if (configureLogging == null)
                throw new ArgumentNullException(nameof(configureLogging));

            return builder.ConfigureServices(services => services.AddLogging(configureLogging));
        }

        /// <summary>
        /// Specifies the startup class to use to configure an application.
        /// </summary>
        public static ICommandLineHostBuilder UseStartup<TStartup>(this ICommandLineHostBuilder builder) where TStartup : class, IStartup, new()
        {
            return builder.UseSetting(HostDefaults.ApplicationNameKey, typeof(TStartup).Assembly.GetName().Name)
                          .ConfigureServices(services => services.AddSingleton<IStartup, TStartup>());
        }
    }
}
