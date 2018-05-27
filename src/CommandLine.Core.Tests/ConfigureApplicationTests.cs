using CommandLine.Core.Hosting;
using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace CommandLine.Core.Tests
{
    public class ConfigureApplicationTests
    {
        public ConfigureApplicationTests()
        {
            _builder = CommandLineHost.CreateBuilder(new[] { "first", "second", "--value", "5" });
        }

        [Fact]
        public async Task Test_Startup()
        {
            // Arrange.
            var app = _builder.UseStartup<Startup>()
                              .Build();

            // Act.
            var result = await app.RunAsync();

            // Assert.
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Test_NonStartup()
        {
            // Arrange.
            var app = _builder.ConfigureServices(s => s.AddSingleton<IService, Service>())
                              .Configure(a => a.Use(args => a.ApplicationServices.GetService<IService>()
                                                                                 .GetResultAsync()))
                              .Build();

            // Act.
            var result = await app.RunAsync();

            // Assert.
            Assert.Equal(1, result);
        }

        private readonly ICommandLineHostBuilder _builder;

        class Startup : IStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddSingleton<IService, Service>();
            }

            public void Configure(IApplicationBuilder app)
            {
                app.Use(args => app.ApplicationServices.GetService<IService>().GetResultAsync());
            }
        }

        interface IService
        {
            Task<int> GetResultAsync();
        }

        class Service : IService
        {
            public Task<int> GetResultAsync() => Task.FromResult(1);
        }
    }
}
