using Autofac;
using Autofac.Extensions.DependencyInjection;
using CommandLine.Core.Hosting;
using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace CommandLine.Core.Tests
{
    public class ConfigureContainerBuilderTests
    {
        public ConfigureContainerBuilderTests()
        {
            _builder = CommandLineHost.CreateBuilder(new[] { "first", "second", "--value", "5" });
        }

        [Fact]
        public void Test_Default()
        {
            // Arrange.
            var app = _builder.UseStartup<DefaultStartup>()
                              .Build();

            // Act.
            var services = app.Services.GetServices<IService>();

            // Assert.
            var actual = Assert.Single(services);
            Assert.IsType<Service>(actual);
        }

        [Fact]
        public void Test_Autofac()
        {
            // Arrange.
            var app = _builder.ConfigureServices(s => s.AddAutofac())
                              .UseStartup<AutofacStartup>()
                              .Build();

            // Act.
            var services = app.Services.GetServices<IService>();

            // Assert.
            Assert.Equal(2, services.Count());
            Assert.Equal(new[] { typeof(Service), typeof(Service2) }, services.Select(s => s.GetType()));
        }

        [Fact]
        public void Test_NonStartup()
        {
            // Arrange.
            var app = _builder.ConfigureServices(s => s.AddAutofac())
                              .ConfigureContainer<ContainerBuilder>(c =>
                               {
                                   c.RegisterType<Service>()
                                    .As<IService>()
                                    .SingleInstance();
                               })
                              .Build();

            // Act.
            var services = app.Services.GetServices<IService>();

            // Assert.
            var actual = Assert.Single(services);
            Assert.IsType<Service>(actual);
        }

        private readonly ICommandLineHostBuilder _builder;

        class DefaultStartup : IStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddSingleton<IService, Service>();
            }

            public void Configure(IApplicationBuilder app)
            {
            }
        }

        class AutofacStartup : IStartup<ContainerBuilder>
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddSingleton<IService, Service>();
            }

            public void ConfigureContainer(ContainerBuilder builder)
            {
                builder.RegisterType<Service2>()
                       .As<IService>()
                       .SingleInstance();
            }

            public void Configure(IApplicationBuilder app)
            {
            }
        }

        interface IService { }

        class Service : IService { }

        class Service2 : IService { }
    }
}
