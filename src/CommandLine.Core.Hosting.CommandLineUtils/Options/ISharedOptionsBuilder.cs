using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;

namespace CommandLine.Core.Hosting.CommandLineUtils.Options
{
    public interface ISharedOptionsBuilder
    {
        ISharedOptionsBuilder WithDescriptionsFrom(Func<IServiceProvider, IReadOnlyDictionary<string, string>> descriptionProvider);

        ISharedOptionsBuilder Option(string template, string description = null, CommandOptionType optionType = CommandOptionType.SingleValue);
    }
}
