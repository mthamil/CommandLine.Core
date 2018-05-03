using CommandLineUtils.Extensions.Utilities;
using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandLineUtils.Extensions.Options
{
    class OptionsBuilder : IOptionsBuilder
    {
        private readonly IList<Func<CommandLineApplication, CommandOption>> _options = new List<Func<CommandLineApplication, CommandOption>>();
        private Lazy<Func<string, string>> _descriptionProvider = new Lazy<Func<string, string>>(() => _ => null);

        public OptionsBuilder(CommandLineApplication command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public IOptionsBuilder WithDescriptionsFrom(Func<Func<string, string>> descriptionProvider)
        {
            _descriptionProvider = new Lazy<Func<string, string>>(descriptionProvider);
            return this;
        }

        public IOptionsBuilder Option(string template, string description, CommandOptionType type, bool inherited)
        {
            _options.Add(app =>
            {
                var option = app.Option(template, string.Empty, type);
                option.Inherited = inherited;
                option.Description = description ?? _descriptionProvider.Value(CreateResourceKey(option.LongName));
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
                option.Description = description ?? _descriptionProvider.Value(CreateResourceKey(option.LongName));
                return option;
            });

            return this;
        }

        public CommandLineApplication Command { get; }

        internal void Build() =>
            _options.Select(f => f(Command))
                    .ToList();

        private static string CreateResourceKey(string longName) => longName.ToPascalCase();
    }
}
