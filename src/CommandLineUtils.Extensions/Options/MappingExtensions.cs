using CommandLineUtils.Extensions.Utilities;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLineUtils.Extensions.Options
{
    /// <summary>
    /// Provides methods for mapping <see cref="CommandOption"/>s to POCO object instances.
    /// </summary>
    public static class MappingExtensions
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
                p.Property.SetValue(instance, p.Option.GetValue(p.Property.PropertyType));

            return instance;
        }

        /// <summary>
        /// Attempts to get the value of a <see cref="CommandOption"/> given the target type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The expected type of the option's value.</typeparam>
        public static T GetValue<T>(this CommandOption option) => (T)option.GetValue(typeof(T));

        /// <summary>
        /// Attempts to get the value of a <see cref="CommandOption"/> given the target <paramref name="type"/>.
        /// </summary>
        public static object GetValue(this CommandOption option, Type type)
        {
            switch (option.OptionType)
            {
                case CommandOptionType.NoValue when type == typeof(bool):
                    return option.HasValue();

                case CommandOptionType.SingleValue:
                case CommandOptionType.SingleOrNoValue:
                    if (!option.HasValue())
                        return null;

                    var parsedValue = option.GetType().GetProperty(nameof(CommandOption<object>.ParsedValue));
                    var value = parsedValue?.GetValue(option) ?? option.Value();
                    return value != null && type.IsAssignableFrom(value.GetType())
                        ? value
                        : null;

                case CommandOptionType.MultipleValue:
                    var parsedValues = option.GetType().GetProperty(nameof(CommandOption<object>.ParsedValues));
                    var values = parsedValues?.GetValue(option) ?? option.Values;
                    return type.IsAssignableFrom(values.GetType())
                        ? values
                        : null;

                default:
                    return null;
            }
        }
    }
}
