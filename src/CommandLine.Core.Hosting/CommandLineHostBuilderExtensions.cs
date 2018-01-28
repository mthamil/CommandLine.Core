using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommandLine.Core.Hosting
{
    public static class CommandLineHostBuilderExtensions
    {
        public static ICommandLineHostBuilder Configure(this ICommandLineHostBuilder builder, Action<IApplicationBuilder> appConfiguration)
        {
            return builder.ConfigureServices(services => services.AddSingleton<IStartup>(new DelegateStartup(appConfiguration)));
        }

        public static ICommandLineHostBuilder UseStartup<TStartup>(this ICommandLineHostBuilder builder) where TStartup : class, IStartup, new()
        {
            return builder.ConfigureServices(services => services.AddSingleton<IStartup, TStartup>());
        }

        class DelegateStartup : IStartup
        {
            private readonly Action<IApplicationBuilder> _configure;

            public DelegateStartup(Action<IApplicationBuilder> configure) => _configure = configure;

            public void ConfigureServices(IServiceCollection services) { }

            public void Configure(IApplicationBuilder app) => _configure(app);
        }
    }
}
