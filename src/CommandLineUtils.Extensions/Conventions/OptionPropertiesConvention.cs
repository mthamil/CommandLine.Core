using CommandLineUtils.Extensions.Options;
using CommandLineUtils.Extensions.Utilities;
using Humanizer;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Abstractions;
using McMaster.Extensions.CommandLineUtils.Conventions;
using System;
using System.Collections.Generic;
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
        /// The naming suffix to use for properties of a nested complex type (non-primitive) that option values should be mapped to.
        /// </summary>
        public string NestedPropertySuffix { get; set; } = "Options";

        /// <summary>
        /// The naming suffix for candidate properties that should have option values mapped to them.
        /// </summary>
        public string PropertySuffix { get; set; } = "";

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
                    var optionsWithProperties = GetSimpleProperties(context.Application, context.ModelType).Concat(
                                                GetNestedProperties(context.Application, context.ModelType));

                    foreach (var (parentProperty, property, option) in optionsWithProperties)
                    {
                        var value = option.GetValue(property.PropertyType);
                        if (value != null)
                        {
                            object target = modelAccessor.GetModel();
                            if (parentProperty != null)
                                target = GetNestedInstance(target, parentProperty);

                            if (target != null)
                                property.SetValue(target, value);
                        }
                    }
                }
            });
        }

        private IEnumerable<(PropertyInfo, PropertyInfo, CommandOption)> GetSimpleProperties(CommandLineApplication application, Type type) =>
            from property in type.GetRuntimeProperties()
            where property.CanWrite
            let suffixIndex = String.IsNullOrEmpty(PropertySuffix)? property.Name.Length : property.Name.IndexOf(PropertySuffix)
            where suffixIndex > -1
            join option in application.GetOptions() on FormatPropertyName(property, suffixIndex) equals FormatOptionName(option)
            select ((PropertyInfo)null, property, option);

        private IEnumerable<(PropertyInfo, PropertyInfo, CommandOption)> GetNestedProperties(CommandLineApplication application, Type type) =>
            from property in type.GetRuntimeProperties()
            where property.CanRead && property.CanWrite
            let suffixIndex = String.IsNullOrEmpty(NestedPropertySuffix) ? property.Name.Length : property.Name.IndexOf(NestedPropertySuffix)
            where suffixIndex > -1
            from childProperty in GetSimpleProperties(application, property.PropertyType)
            select (property, childProperty.Item2, childProperty.Item3);

        private static string FormatPropertyName(PropertyInfo property, int suffixIndex) =>
            property.Name.Substring(0, suffixIndex);

        private static string FormatOptionName(CommandOption option) => 
            option.OptionType == CommandOptionType.MultipleValue
                ? option.LongName.ToPascalCase().Pluralize()
                : option.LongName.ToPascalCase();

        private object GetNestedInstance(object parent, PropertyInfo parentProperty)
        {
            var nested = parentProperty.GetValue(parent);
            if (nested == null && parentProperty.PropertyType.GetConstructor(new Type[0]) != null)
            {
                // Instantiate POCO object for holding nested option properties.
                nested = Activator.CreateInstance(parentProperty.PropertyType);
                parentProperty.SetValue(parent, nested);
            }

            return nested;
        }
    }
}
