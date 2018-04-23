using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Conventions;
using System;

namespace CommandLine.Core.CommandLineUtils
{
    static class CommandConventionExtensions
    {
        public static IConventionBuilder UseDefaultConventionsWithServices(this IConventionBuilder builder, IServiceProvider serviceProvider) =>
            builder.UseAttributes()
                   .SetAppNameFromEntryAssembly()
                   .SetRemainingArgsPropertyOnModel()
                   .SetSubcommandPropertyOnModel()
                   .SetParentPropertyOnModel()
                   .UseOnExecuteMethodFromModel()
                   .UseOnValidateMethodFromModel()
                   .UseOnValidationErrorMethodFromModel()
                   .UseConstructorInjection(serviceProvider);
    }
}
