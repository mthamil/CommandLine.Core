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
        /// <param name="services">The application service provider.</param>
        public static IOptionsBuilder WithDescriptionsFromResources<TResources>(this IOptionsBuilder builder, IServiceProvider services) =>
            builder.WithDescriptionsFrom(() =>
            {
                var localizerFactory = services.GetService<IStringLocalizerFactory>();
                var localizer = localizerFactory.Create(typeof(TResources));
                return key => localizer[key];
            });
    }
}
