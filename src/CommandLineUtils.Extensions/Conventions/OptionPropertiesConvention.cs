using CommandLineUtils.Extensions.Utilities;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Abstractions;
using McMaster.Extensions.CommandLineUtils.Conventions;
using System.Linq;
using System.Reflection;

namespace CommandLineUtils.Extensions.Conventions
{
    /// <summary>
    /// A convention that sets properties on command models from matching options.
    /// </summary>
    public class OptionPropertiesConvention : IConvention
    {
        public void Apply(ConventionContext context)
        {
            if (context.ModelType == null)
                return;

            var optionsWithProperties = (
                from property in context.ModelType.GetTypeInfo().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                where property.CanRead && property.CanWrite
                join option in context.Application.Options on property.Name equals option.LongName.ToPascalCase()
                select (property, option)
            ).ToList();

            if (optionsWithProperties.Count == 0)
                return;

            context.Application.OnParsingComplete(r =>
            {
                if (r.SelectedCommand is IModelAccessor modelAccessor)
                {
                    foreach (var (property, option) in optionsWithProperties)
                    {
                        var parsedValue = option.GetType().GetProperty(nameof(CommandOption<object>.ParsedValue));
                        if (parsedValue?.PropertyType == property.PropertyType)
                        {
                            property.SetValue(modelAccessor.GetModel(), parsedValue.GetValue(option));
                        }
                    }
                }
            });
        }
    }
}
