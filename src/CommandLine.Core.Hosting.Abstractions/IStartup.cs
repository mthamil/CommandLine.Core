using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.Core.Hosting.Abstractions
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder app);
    }
}
