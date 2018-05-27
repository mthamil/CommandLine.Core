using System;
using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.Core.Hosting
{
    class DelegateStartup<TContainerBuilder> : IStartup<TContainerBuilder>
    {
        internal Action<IApplicationBuilder> ConfigureApp { get; set; }

        internal Action<TContainerBuilder> ConfigureContainerBuilder { get; set; }

        public void ConfigureServices(IServiceCollection services) { }

        public void ConfigureContainer(TContainerBuilder builder) => ConfigureContainerBuilder?.Invoke(builder);

        public void Configure(IApplicationBuilder app) => ConfigureApp?.Invoke(app);
    }
}
