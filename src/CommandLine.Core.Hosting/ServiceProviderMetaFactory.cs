using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace CommandLine.Core.Hosting
{
    /// <summary>
    /// Creates a dependency injection container-specific implementation of <see cref="IServiceProvider"/>.
    /// </summary>
    /// <remarks>
    /// This provides integration with a custom DI container framework that may have been registered 
    /// using <see cref="IServiceProviderFactory{TContainerBuilder}"/>.
    /// </remarks>
    class ServiceProviderMetaFactory : IServiceProviderFactory
    {
        private readonly IStartup _startup;
        private readonly IServiceProvider _hostingServiceProvider;

        public ServiceProviderMetaFactory(IServiceProvider hostingServiceProvider, IStartup startup)
        {
            _hostingServiceProvider = hostingServiceProvider ?? throw new ArgumentNullException(nameof(hostingServiceProvider));
            _startup = startup ?? throw new ArgumentNullException(nameof(startup));
        }

        public IServiceProvider CreateServiceProvider(IServiceCollection services)
        {
            _startup.ConfigureServices(services);

            var typedStartup = _startup.GetType()
                                       .GetInterfaces()
                                       .SingleOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IStartup<>));

            var containerType = typedStartup?.GetGenericArguments().Single() ?? typeof(IServiceCollection);

            return (IServiceProvider)GetType()
                                    .GetMethod(nameof(CreateServiceProvider), BindingFlags.Instance | BindingFlags.NonPublic)
                                    .MakeGenericMethod(containerType)
                                    .Invoke(this, new object[] { _startup, services });
        }

        private IServiceProvider CreateServiceProvider<TContainerBuilder>(IStartup startup, IServiceCollection services)
        {
            var serviceProviderFactory = _hostingServiceProvider.GetRequiredService<IServiceProviderFactory<TContainerBuilder>>();
            var containerBuilder = serviceProviderFactory.CreateBuilder(services);

            if (startup is IStartup<TContainerBuilder> typedStartup)
                typedStartup.ConfigureContainer(containerBuilder);

            return serviceProviderFactory.CreateServiceProvider(containerBuilder);
        }
    }
}
