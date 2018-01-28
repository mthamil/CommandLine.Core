using System;

namespace CommandLine.Core.Hosting
{
    public interface IApplicationBuilder
    {
        IServiceProvider ApplicationServices { get; }
    }
}
