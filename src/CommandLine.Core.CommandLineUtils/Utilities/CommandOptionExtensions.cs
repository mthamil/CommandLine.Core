using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLine.Core.CommandLineUtils.Utilities
{
    public static class CommandOptionExtensions
    {
        /// <summary>
        /// Maps the values of a collection of options to a new instance of a class.
        /// </summary>
        public static T Map<T>(this IEnumerable<CommandOption> options) where T : new() =>
            options.Map(new T());

        /// <summary>
        /// Maps the values of a collection of options to an instance of a class.
        /// </summary>
        public static T Map<T>(this IEnumerable<CommandOption> options, T instance)
        {
            var properties =
                options.Join(typeof(T).GetRuntimeProperties().Where(p => p.CanWrite),
                             o => o.LongName.ToPascalCase(),
                             p => p.Name,
                             (o, p) => new { Option = o, Property = p });

            foreach (var p in properties)
                p.Property.SetValue(instance, GetValue(p.Option));

            return instance;
        }

        private static object GetValue(CommandOption option)
        {
            switch (option.OptionType)
            {
                case CommandOptionType.NoValue:
                    return option.HasValue();
                case CommandOptionType.MultipleValue:
                    return option.Values;
                default:
                    return option.Value();
            }
        }
    }
}
