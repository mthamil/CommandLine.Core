using System;

namespace CommandLine.Core.Hosting.Abstractions
{
    /// <summary>
    /// Enables configuration of an application.
    /// </summary>
    public interface IApplicationBuilder
    {
        /// <summary>
        /// The service provider for an application.
        /// </summary>
        IServiceProvider ApplicationServices { get; }

        /// <summary>
        /// Uses a delegate to set the core application.
        /// </summary>
        IApplicationBuilder Use(ApplicationDelegate app);

        /// <summary>
        /// Builds an application.
        /// </summary>
        ApplicationDelegate Build();
    }
}
