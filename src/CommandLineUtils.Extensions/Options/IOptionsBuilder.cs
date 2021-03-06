﻿using McMaster.Extensions.CommandLineUtils;
using System;

namespace CommandLineUtils.Extensions.Options
{
    /// <summary>
    /// Provides command option configuration.
    /// </summary>
    public interface IOptionsBuilder
    {
        /// <summary>
        /// Sets the source (such as a resource file) from which shared command option descriptions are retrieved. 
        /// This should be a dictionary keyed by option long names.
        /// </summary>
        /// <param name="descriptionProvider">A function providing a mapping between option long names and description strings.</param>
        IOptionsBuilder WithDescriptionsFrom(Func<Func<string, string>> descriptionProvider);

        /// <summary>
        /// Registers a new shared command option.
        /// </summary>
        /// <param name="template">The option template string.</param>
        /// <param name="description">The option's description.</param>
        /// <param name="type">The option's type.</param>
        /// <param name="inherited">Whether an option should be inherited by child commands.</param>
        IOptionsBuilder Option(string template, string description = null, CommandOptionType type = CommandOptionType.SingleValue, bool inherited = false);

        /// <summary>
        /// Registers a new shared command option.
        /// </summary>
        /// <typeparam name="T">The option's value type.</typeparam>
        /// <param name="template">The option template string.</param>
        /// <param name="description">The option's description.</param>
        /// <param name="type">The option's type.</param>
        /// <param name="inherited">Whether an option should be inherited by child commands.</param>
        IOptionsBuilder Option<T>(string template, string description = null, CommandOptionType type = CommandOptionType.SingleValue, bool inherited = false);

        /// <summary>
        /// The command for which options are being configured.
        /// </summary>
        CommandLineApplication Command { get; }
    }
}
