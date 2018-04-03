using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommandLine.Core.Hosting.CommandLineUtils.Options
{
    public static class SharedOptionsRegistrationExtensions
    {
        /// <summary>
        /// Registers common application options with the specified <see cref="IServiceCollection"/>.
        /// </summary>
        public static IServiceCollection AddCommonOptions(this IServiceCollection services, Action<ISharedOptionsBuilder> optionsBuilder) =>
            services.AddSingleton(provider =>
            {
                var builder = new SharedOptionsBuilder(provider);
                optionsBuilder(builder);
                return builder.Build();
            });
    }
}
