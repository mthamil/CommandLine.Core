using System;

namespace CommandLine.Core.Hosting.Abstractions
{
    public interface IApplicationBuilder
    {
        IServiceProvider ApplicationServices { get; }

        IApplicationBuilder Use(ApplicationDelegate app);

        ApplicationDelegate Build();
    }
}
