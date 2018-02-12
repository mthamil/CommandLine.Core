using System;
using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.Core.Hosting
{
    class DelegateStartup : IStartup
    {
        private readonly Action<IApplicationBuilder> _configureApp;

        public DelegateStartup(Action<IApplicationBuilder> configureApp) => _configureApp = configureApp;

        public void ConfigureServices(IServiceCollection services) { }

        public void Configure(IApplicationBuilder app) => _configureApp(app);
    }
}
