using CommandLine.Core.Hosting.CommandLineUtils.Utilities;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLine.Core.Hosting.CommandLineUtils.Options
{
    class SharedOptionsBuilder : ISharedOptionsBuilder
    {
        private readonly IServiceProvider _services;

        private IList<Func<IReadOnlyDictionary<string, string>, CommandOption>> _options = new List<Func<IReadOnlyDictionary<string, string>, CommandOption>>();
        private Lazy<IReadOnlyDictionary<string, string>> _descriptions;

        public SharedOptionsBuilder(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public ISharedOptionsBuilder WithDescriptionsFrom(Func<IServiceProvider, IReadOnlyDictionary<string, string>> descriptionProvider)
        {
            _descriptions = new Lazy<IReadOnlyDictionary<string, string>>(() => descriptionProvider(_services));
            return this;
        }

        public ISharedOptionsBuilder Option(string template, string description, CommandOptionType type)
        {
            _options.Add(descriptions =>
            {
                var option = new CommandOption(template, type) { Inherited = true };
                option.Description = description ?? descriptions?[CreateResourceKey(option.LongName)];
                return option;
            });
            
            return this;
        }

        public ISharedOptions Build() => new SharedOptions(_options
                                            .Select(f => f(_descriptions.Value))
                                            .ToList());

        private static string CreateResourceKey(string longName) => longName.ToPascalCase();
    }
}
