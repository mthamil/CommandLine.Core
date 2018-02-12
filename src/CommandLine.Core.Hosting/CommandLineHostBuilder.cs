using System;
using System.Collections.Generic;
using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.Core.Hosting
{
    class CommandLineHostBuilder : ICommandLineHostBuilder
    {
        private readonly ICollection<Action<IServiceCollection>> _serviceConfigurations = new List<Action<IServiceCollection>>();
        private readonly IConfigurationRoot _config;
        private readonly HostingEnvironment _hostingEnvironment;
        private readonly string[] _args;
        private bool _built;

        public CommandLineHostBuilder(string[] args)
        {
            _hostingEnvironment = new HostingEnvironment();
            _config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            // Fall back to the ASPNETCORE environment name.
            if (String.IsNullOrEmpty(_config[HostDefaults.EnvironmentNameKey]))
                UseSetting(HostDefaults.EnvironmentNameKey, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

            _args = args;
        }

        public ICommandLineHostBuilder UseSetting(string key, string value)
        {
            _config[key] = value;
            return this;
        }

        public ICommandLineHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            _serviceConfigurations.Add(configureServices ?? throw new ArgumentNullException(nameof(configureServices)));
            return this;
        }

        public ICommandLineHost Build()
        {
            if (_built)
                throw new InvalidOperationException("Host has already been built.");

            _hostingEnvironment.Initialize(_config);

            var services = new ServiceCollection()
                .AddSingleton<IConfiguration>(_config)
                .AddSingleton<IHostingEnvironment>(_hostingEnvironment);

            foreach (var serviceConfig in _serviceConfigurations)
                serviceConfig(services);

            var appServices = CopyServices(services);
            var hostingServiceProvider = services.BuildServiceProvider();

            _built = true;
            return new CommandLineHost(
                appServices,
                hostingServiceProvider,
                _config,
                _args);
        }

        private static IServiceCollection CopyServices(IServiceCollection services)
        {
            var copy = new ServiceCollection() as IServiceCollection;
            foreach (var service in services)
                copy.Add(service);
            return copy;
        }
    }
}
