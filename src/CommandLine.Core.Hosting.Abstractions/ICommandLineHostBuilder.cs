using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommandLine.Core.Hosting.Abstractions
{
    public interface ICommandLineHostBuilder
    {
        ICommandLineHostBuilder UseSetting(string key, string value);

        ICommandLineHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);

        ICommandLineHost Build();
    }
}
