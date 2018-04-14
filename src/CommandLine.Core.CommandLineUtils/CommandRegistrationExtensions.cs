using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace CommandLine.Core.CommandLineUtils
{
    /// <summary>
    /// Provides methods to help register and configure the commands in a CommandLineUtils application.
    /// </summary>
    public static class CommandRegistrationExtensions
    {
        /// <summary>
        /// Defines and registers commands with the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="command">The command to add a child command to.</param>
        /// <param name="name">The name of the command.</param>
        public static CommandLineApplication<TModel> Command<TModel>(this CommandLineApplication command, string name) where TModel : class =>
            command.Command<TModel>(name, _ => { });
    }
}
