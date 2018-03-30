using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommandLine.Core.Hosting.CommandLineUtils.Options
{
    class SharedOptions : ISharedOptions
    {
        private readonly IReadOnlyList<CommandOption> _options;

        public SharedOptions(IReadOnlyList<CommandOption> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public CommandOption this[string longName] => _options.SingleOrDefault(o => o.LongName == longName);

        public IEnumerator<CommandOption> GetEnumerator() => _options.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
