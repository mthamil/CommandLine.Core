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
}
