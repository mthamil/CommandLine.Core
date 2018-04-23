using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.HelpText;
using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.Core.CommandLineUtils
{
    /// <summary>
    /// Provides methods to help integrate with McMaster.Extensions.CommandLineUtils.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds <c>McMaster.Extensions.CommandLineUtils</c> services.
        /// </summary>
        public static IServiceCollection AddCommandLineUtils(this IServiceCollection services) =>
            services.AddCommonServices()
                    .AddRootApplication();

        private static IServiceCollection AddCommonServices(this IServiceCollection services) =>
            services.AddSingleton(PhysicalConsole.Singleton)
                    .AddSingleton<IHelpTextGenerator>(DefaultHelpTextGenerator.Singleton);

        private static IServiceCollection AddRootApplication(this IServiceCollection services) =>
            services.AddSingleton<RootCommandLineApplication>();
    }
}
