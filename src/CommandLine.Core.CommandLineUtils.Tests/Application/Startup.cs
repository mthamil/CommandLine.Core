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

            services.AddCommandLineUtils();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCommandLineUtils(c =>
            {
                c.Name = "test";

                c.Options(opts =>
                    opts.Option<int>("--value", inherited: true));

                c.Command<First>("first",
                    f => f.Command<Second>("second"));
            });
        }
    }
}
