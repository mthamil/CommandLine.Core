using McMaster.Extensions.CommandLineUtils;
using System;

namespace CommandLineUtils.Extensions.Options
{
    /// <summary>
    /// Provides methods to configure the options in an application that are shared by all commands.
    /// </summary>
    public static class OptionsBuilderExtensions
    {
        /// <summary>
        /// Builds application options.
        /// </summary>
        public static TCommand Options<TCommand>(this TCommand command, Action<IOptionsBuilder> optionsBuilder) where TCommand : CommandLineApplication
        {
            var builder = new OptionsBuilder(command);
            optionsBuilder(builder);
            builder.Apply(command);

            return command;
        }
    }
}
