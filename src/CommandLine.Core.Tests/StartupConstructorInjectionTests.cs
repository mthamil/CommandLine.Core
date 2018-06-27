using CommandLine.Core.Hosting;
using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace CommandLine.Core.Tests
{
    public class StartupConstructorInjectionTests
    {
        [Fact]
        public async Task Test()
        {
            // Arrange.
            var app = CommandLineHost.CreateBuilder(new[] { "first", "second", "--value", "5" })
                                     .UseStartup<Startup>()
                                     .Build();

            // Act.
            await app.RunAsync();

            // Assert.
            Assert.NotNull(Startup.Environment);
            Assert.Equal("Development", Startup.Environment.EnvironmentName);
        }

        class Startup : IStartup
        {
            public Startup(IHostingEnvironment environment)
            {
                Environment = environment;
            }

            public static IHostingEnvironment Environment { get; private set; }

            public void ConfigureServices(IServiceCollection services)
            {

            }

            public void Configure(IApplicationBuilder app)
            {
                app.Use(_ => Task.FromResult(1));
            }
        }
    }
}
