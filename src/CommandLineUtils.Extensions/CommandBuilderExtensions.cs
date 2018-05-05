using CommandLineUtils.Extensions.Options;
using McMaster.Extensions.CommandLineUtils;
using System;

namespace CommandLineUtils.Extensions
{
    /// <summary>
    /// Provides access to builders that help to configure <see cref="CommandLineApplication"/>s.
    /// </summary>
    public static class CommandBuilderExtensions
    {
        /// <summary>
        /// Builds command options.
        /// </summary>
        public static TCommand Options<TCommand>(this TCommand command, Action<IOptionsBuilder> optionsBuilder) where TCommand : CommandLineApplication
        {
            var builder = new OptionsBuilder(command);
            optionsBuilder(builder);
            builder.Build();
            return command;
        }
    }
}
