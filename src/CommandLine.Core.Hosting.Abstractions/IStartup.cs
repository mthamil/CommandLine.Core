using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.Core.Hosting.Abstractions
{
    /// <summary>
    /// Represents an application's entry point.
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// Configures an application's registered services.
        /// </summary>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Configures an application.
        /// </summary>
        void Configure(IApplicationBuilder app);
    }

    /// <summary>
    /// Represents an application's entry point with service container customization.
    /// </summary>
    public interface IStartup<TContainerBuilder> : IStartup
    {
        /// <summary>
        /// Configures an application's service container.
        /// </summary>
        void ConfigureContainer(TContainerBuilder builder);
    }
}
