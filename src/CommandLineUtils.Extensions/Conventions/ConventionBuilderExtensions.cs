using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Conventions;
using System;

namespace CommandLineUtils.Extensions.Conventions
{
    /// <summary>
    /// Provides additional conventions.
    /// </summary>
    public static class ConventionBuilderExtensions
    {
        /// <summary>
        /// Enables setting properties on a command model from <see cref="CommandOption"/>s.
        /// </summary>
        /// <param name="builder">The convention builder.</param>
        /// <param name="propertySuffix">The naming suffix for candidate properties that should have option values mapped to them.</param>
        /// <param name="nestedPropertySuffix">The naming suffix to use for properties of a complex type (non-primitive) that option values should be mapped to.</param>
        public static IConventionBuilder UseOptionProperties(this IConventionBuilder builder, string propertySuffix = "", string nestedPropertySuffix = "Options") =>
            builder.AddConvention(new OptionPropertiesConvention
            {
                NestedPropertySuffix = nestedPropertySuffix,
                PropertySuffix = propertySuffix
            });

        /// <summary>
        /// Provides the same conventions as using <see cref="McMaster.Extensions.CommandLineUtils.ConventionBuilderExtensions.UseDefaultConventions"/>, 
        /// but with an additional external service provider.
        /// </summary>
        /// <param name="builder">The convention builder.</param>
        /// <param name="additionalServices">Additional services that should be passed to the service provider.</param>
        public static IConventionBuilder UseDefaultConventionsWithServices(this IConventionBuilder builder, IServiceProvider additionalServices) =>
            builder.UseAttributes()
                   .SetAppNameFromEntryAssembly()
                   .SetRemainingArgsPropertyOnModel()
                   .SetSubcommandPropertyOnModel()
                   .SetParentPropertyOnModel()
                   .UseOnExecuteMethodFromModel()
                   .UseOnValidateMethodFromModel()
                   .UseOnValidationErrorMethodFromModel()
                   .UseConstructorInjection(additionalServices);
    }
}
