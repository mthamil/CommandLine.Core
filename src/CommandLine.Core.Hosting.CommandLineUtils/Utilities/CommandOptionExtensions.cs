using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLine.Core.Hosting.CommandLineUtils.Utilities
{
    public static class CommandOptionExtensions
    {
        public static bool IsOn(this CommandOption option)
        {
            if (option.OptionType != CommandOptionType.NoValue)
                throw new InvalidOperationException($"Option must have type '{CommandOptionType.NoValue:G}'.");

            return option.Value() == "on";
        }

        public static T Map<T>(this IEnumerable<CommandOption> options) where T : new() =>
            options.Map(new T());

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
                    return option.IsOn();
                case CommandOptionType.MultipleValue:
                    return option.Values;
                default:
                    return option.Value();
            }
        }
    }
}
