using CommandLineUtils.Extensions.Utilities;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Conventions;
using System.Linq;
using System.Reflection;

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
        public static IConventionBuilder UseOptionProperties(this IConventionBuilder builder) =>
            builder.AddConvention(new OptionPropertiesConvention());
    }
}
