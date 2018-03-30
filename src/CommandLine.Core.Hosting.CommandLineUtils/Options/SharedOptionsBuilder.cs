using CommandLine.Core.Hosting.CommandLineUtils.Utilities;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;

namespace CommandLine.Core.Hosting.CommandLineUtils.Options
{
    class SharedOptionsBuilder : ISharedOptionsBuilder
    {
        private readonly List<CommandOption> _options = new List<CommandOption>();
        private readonly IServiceProvider _services;

        private IReadOnlyDictionary<string, string> _descriptions;

        public SharedOptionsBuilder(IServiceProvider services)
        {
            _services = services;
        }

        public ISharedOptionsBuilder WithDescriptionsFrom(Func<IServiceProvider, IReadOnlyDictionary<string, string>> descriptionProvider)
        {
            _descriptions = descriptionProvider(_services);
            return this;
        }

        public ISharedOptionsBuilder Option(string template, string description, CommandOptionType type)
        {
            var option = new CommandOption(template, type) { Inherited = true };
            option.Description = description ?? _descriptions?[CreateResourceKey(option.LongName)];
            _options.Add(option);
            return this;
        }

        public ISharedOptions Build() => new SharedOptions(_options);

        private static string CreateResourceKey(string longName) => longName.ToPascalCase();
    }
}
