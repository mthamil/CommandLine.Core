using System;

namespace CommandLine.Core.Hosting
{
    class ApplicationBuilder : IApplicationBuilder
    {
        public ApplicationBuilder(IServiceProvider appServices)
        {
            ApplicationServices = appServices ?? throw new ArgumentNullException(nameof(appServices));
        }

        public IServiceProvider ApplicationServices { get; }
    }
}
