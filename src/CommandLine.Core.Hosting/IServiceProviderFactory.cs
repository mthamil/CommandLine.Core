using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommandLine.Core.Hosting
{
    /// <summary>
    /// Creates <see cref="IServiceProvider"/>s.
    /// </summary>
    public interface IServiceProviderFactory
    {
        /// <summary>
        /// Creates an <see cref="IServiceProvider"/>.
        /// </summary>
        /// <param name="services">The collection of service registrations for the provider.</param>
        /// <returns></returns>
        IServiceProvider CreateServiceProvider(IServiceCollection services);
    }
}
