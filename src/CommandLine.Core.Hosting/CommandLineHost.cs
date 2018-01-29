using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CommandLine.Core.Hosting
{
    public class CommandLineHost : ICommandLineHost
    {
        private readonly IServiceCollection _appServices;
        private readonly Lazy<IServiceProvider> _appServiceProvider;
        private readonly IServiceProvider _hostingServiceProvider;
        private readonly string[] _args;
        private readonly Lazy<IStartup> _startup;
        private readonly IConfiguration _config;

        public CommandLineHost(IServiceCollection appServices,
                               IServiceProvider hostingServiceProvider,
                               IConfiguration config,
                               string[] args)
        {
            _appServices = appServices ?? throw new ArgumentNullException(nameof(appServices));
            _hostingServiceProvider = hostingServiceProvider ?? throw new ArgumentNullException(nameof(hostingServiceProvider));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _args = args ?? throw new ArgumentNullException(nameof(args));

            _startup = new Lazy<IStartup>(() => _hostingServiceProvider.GetRequiredService<IStartup>());
            _appServiceProvider = new Lazy<IServiceProvider>(() =>
            {
                _startup.Value.ConfigureServices(_appServices);
                return _appServices.BuildServiceProvider();
            });
        }

        public IServiceProvider Services => _appServiceProvider.Value;

        public Task<int> RunAsync()
        {
            var appBuilder = new ApplicationBuilder(_appServiceProvider.Value);

            _startup.Value.Configure(appBuilder);

            var app = appBuilder.Build();
            return app(_args);
        }

        public static ICommandLineHostBuilder CreateBuilder(string[] args) => new CommandLineHostBuilder(args);
    }
}
