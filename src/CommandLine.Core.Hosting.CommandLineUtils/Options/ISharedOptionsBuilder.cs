using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;

namespace CommandLine.Core.Hosting.CommandLineUtils.Options
{
    /// <summary>
    /// Provides shared command option configuration.
    /// </summary>
    public interface ISharedOptionsBuilder
    {
        /// <summary>
        /// Sets the source (such as a resource file) from which shared command option descriptions are retrieved. 
        /// This should be a dictionary keyed by option long names.
        /// </summary>
        /// <param name="descriptionProvider">A function providing a mapping between option long names and description strings.</param>
        ISharedOptionsBuilder WithDescriptionsFrom(Func<IServiceProvider, IReadOnlyDictionary<string, string>> descriptionProvider);

        /// <summary>
        /// Registers a new shared command option.
        /// </summary>
        /// <param name="template">The option template string.</param>
        /// <param name="description">The option's description.</param>
        /// <param name="type">The option's type.</param>
        ISharedOptionsBuilder Option(string template, string description = null, CommandOptionType type = CommandOptionType.SingleValue);
    }
}
