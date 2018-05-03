using CommandLineUtils.Extensions.Utilities;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLineUtils.Extensions.Options
{
    class OptionsBuilder : IOptionsBuilder
    {
        private readonly IServiceProvider _services;

        private IList<Func<CommandLineApplication, CommandOption>> _options = new List<Func<CommandLineApplication, CommandOption>>();
        private Func<string, string> _descriptions = _ => null;

        public OptionsBuilder(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IOptionsBuilder WithDescriptionsFrom(Func<IServiceProvider, Func<string, string>> descriptionProvider)
        {
            _descriptions = key => descriptionProvider(_services)(key);
            return this;
        }

        public IOptionsBuilder Option(string template, string description, CommandOptionType type, bool inherited)
        {
            _options.Add(app =>
            {
                var option = app.Option(template, string.Empty, type);
                option.Inherited = inherited;
                option.Description = description ?? _descriptions(CreateResourceKey(option.LongName));
                return option;
            });

            return this;
        }

        public IOptionsBuilder Option<T>(string template, string description, CommandOptionType type, bool inherited)
        {
            _options.Add(app =>
            {
                var option = app.Option<T>(template, string.Empty, type);
                option.Inherited = inherited;
                option.Description = description ?? _descriptions(CreateResourceKey(option.LongName));
                return option;
            });

            return this;
        }

        internal void Apply(CommandLineApplication app) =>
            _options.Select(f => f(app))
                    .ToList();

        private static string CreateResourceKey(string longName) => longName.ToPascalCase();
    }
}
