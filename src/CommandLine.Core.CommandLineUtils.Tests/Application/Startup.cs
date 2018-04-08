using CommandLine.Core.CommandLineUtils.Options;
using CommandLine.Core.CommandLineUtils.Tests.Application.Commands;
using CommandLine.Core.Hosting.Abstractions;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CommandLine.Core.CommandLineUtils.Tests.Application
{
    class Startup : IStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Mock.Of<IConsole>());

            services.AddCommands(cmds =>
                cmds.Base<FirstCommand>()
                    .Child<SecondCommand>());

            services.AddCommonOptions(opts =>
                opts.Option("--value"));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCommands(c =>
            {
                c.Name = "test";
            });
        }
    }
}
