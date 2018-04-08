using CommandLine.Core.Hosting;
using CommandLine.Core.Hosting.Abstractions;
using System;

namespace CommandLine.Core.CommandLineUtils.Tests.Application
{
    public class AppFixture
    {
        public ICommandLineHost CreateApp(string args) =>
            CommandLineHost.CreateBuilder(args.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                           .UseCommandLineUtils()
                           .UseStartup<Startup>()
                           .Build();
    }
}
