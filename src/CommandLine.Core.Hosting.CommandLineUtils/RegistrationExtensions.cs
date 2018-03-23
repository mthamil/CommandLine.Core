using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.Core.Hosting.CommandLineUtils
{
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Adds a root level command.
        /// </summary>
        public static IServiceCollection AddBaseCommand<TCommand>(this IServiceCollection services) where TCommand : CommandLineApplication => 
            services.AddScoped<CommandLineApplication, TCommand>();

        /// <summary>
        /// Adds a child command.
        /// </summary>
        public static IServiceCollection AddChildCommand<TCommand>(this IServiceCollection services) where TCommand : CommandLineApplication =>
            services.AddScoped<TCommand>();
    }
}
