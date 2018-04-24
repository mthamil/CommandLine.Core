using CommandLineUtils.Extensions.Utilities;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Abstractions;
using McMaster.Extensions.CommandLineUtils.Conventions;
using System.Linq;
using System.Reflection;

namespace CommandLineUtils.Extensions.Conventions
{
    /// <summary>
    /// A convention that sets properties on command models using the values from matching options.
    /// </summary>
    public class OptionPropertiesConvention : IConvention
    {
        /// <summary>
        /// Applies the convention.
        /// </summary>
        /// <param name="context">The context in which the convention is applied.</param>
        public void Apply(ConventionContext context)
        {
            if (context.ModelType == null || !context.ModelType.GetRuntimeProperties().Any())
                return;

            context.Application.OnParsingComplete(r =>
            {
                var command = r.SelectedCommand
                    .CommandChain()
                    .FirstOrDefault(c => ReferenceEquals(c, context.Application));

                if (command is IModelAccessor modelAccessor)
                {
                    var optionsWithProperties =
                        from property in context.ModelType.GetRuntimeProperties()
                        where property.CanRead && property.CanWrite
                        join option in context.Application.Options on property.Name equals option.LongName.ToPascalCase()
                        select (property, option);

                    foreach (var (property, option) in optionsWithProperties)
                    {
                        var value = GetValue(property, option);
                        if (value != null)
                        {
                            property.SetValue(modelAccessor.GetModel(), value);
                        }
                    }
                }
            });
        }

        private static object GetValue(PropertyInfo property, CommandOption option)
        {
            if (option.OptionType == CommandOptionType.NoValue && property.PropertyType == typeof(bool))
            {
                return option.HasValue();
            }

            var parsedValue = option.GetType().GetProperty(nameof(CommandOption<object>.ParsedValue));
            if (parsedValue?.PropertyType == property.PropertyType)
            {
                return parsedValue.GetValue(option);
            }

            return null;
        }
    }
}
