using CommandLineUtils.Extensions.Options;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;

namespace CommandLine.Core.CommandLineUtils
{
    /// <summary>
    /// Provides extension methods for <see cref="IOptionsBuilder"/>.
    /// </summary>
    public static class OptionsBuilderExtensions
    {
        /// <summary>
        /// Specifies that a <see cref="CommandOption"/> descriptions should come from resource files.
        /// </summary>
        /// <typeparam name="TResources">The resource type.</typeparam>
        /// <param name="builder">The builder.</param>
        public static IOptionsBuilder WithDescriptionsFromResources<TResources>(this IOptionsBuilder builder)
        {
            var localizerFactory = ((IServiceProvider)builder.Command).GetService<IStringLocalizerFactory>();
            var localizer = localizerFactory.Create(typeof(TResources));
            return builder.WithDescriptionsFromResources(localizer);
        }

        /// <summary>
        /// Specifies that a <see cref="CommandOption"/> descriptions should come from a <see cref="IStringLocalizer"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="localizer">Service that provides string resources.</param>
        public static IOptionsBuilder WithDescriptionsFromResources(this IOptionsBuilder builder, IStringLocalizer localizer) =>
            builder.WithDescriptionsFrom(() => key => localizer[key]);
    }
}
