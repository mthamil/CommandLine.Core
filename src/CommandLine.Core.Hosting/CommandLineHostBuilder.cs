using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.Core.Hosting
{
    class CommandLineHostBuilder : ICommandLineHostBuilder
    {
        private readonly ICollection<Action<IServiceCollection>> _serviceConfigurations = new List<Action<IServiceCollection>>();
        private readonly ServiceCollection _services;
        private readonly IConfigurationRoot _config;
        private readonly string[] _args;

        public CommandLineHostBuilder(string[] args)
        {
            _services = new ServiceCollection();
            _config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            _args = args;
        }

        public ICommandLineHostBuilder UseSetting(string key, string value)
        {
            _config[key] = value;
            return this;
        }

        public ICommandLineHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            _serviceConfigurations.Add(configureServices);
            return this;
        }

        public ICommandLineHost Build()
        {
            foreach (var serviceConfig in _serviceConfigurations)
                serviceConfig(_services);

            _services.AddSingleton<IConfiguration>(_config);

            var appServices = Copy(_services);
            var hostingServiceProvider = _services.BuildServiceProvider();

            return new CommandLineHost(
                appServices,
                hostingServiceProvider,
                _config,
                _args);
        }

        private static IServiceCollection Copy(IServiceCollection services)
        {
            var copy = new ServiceCollection() as IServiceCollection;
            foreach (var service in services)
                copy.Add(service);
            return copy;
        }
    }
}
