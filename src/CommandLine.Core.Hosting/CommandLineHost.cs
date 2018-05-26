using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CommandLine.Core.Hosting
{
    /// <summary>
    /// Represents a command line application.
    /// </summary>
    public class CommandLineHost : ICommandLineHost
    {
        private readonly IServiceCollection _appServices;
        private readonly Lazy<IServiceProvider> _appServiceProvider;
        private readonly IServiceProvider _hostingServiceProvider;
        private readonly string[] _args;
        private readonly Lazy<IStartup> _startup;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of <see cref="CommandLineHost"/>.
        /// </summary>
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
                _hostingServiceProvider.GetRequiredService<IServiceProviderFactory>()
                                       .CreateServiceProvider(_appServices));
        }

        /// <summary>
        /// The <see cref="IServiceProvider"/> for the application.
        /// </summary>
        public IServiceProvider Services => _appServiceProvider.Value;

        /// <summary>
        /// Executes the application.
        /// </summary>
        public Task<int> RunAsync()
        {
            var appBuilder = new ApplicationBuilder(_appServiceProvider.Value);

            _startup.Value.Configure(appBuilder);

            var app = appBuilder.Build();
            return app(_args);
        }

        /// <summary>
        /// Initializes a new <see cref="CommandLineHostBuilder"/>.
        /// </summary>
        public static ICommandLineHostBuilder CreateBuilder(string[] args) => new CommandLineHostBuilder(args);
    }
}
