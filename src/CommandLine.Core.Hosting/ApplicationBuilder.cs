using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CommandLine.Core.Hosting
{
    class ApplicationBuilder : IApplicationBuilder
    {
        private ApplicationDelegate _app;

        public ApplicationBuilder(IServiceProvider appServices)
        {
            ApplicationServices = appServices ?? throw new ArgumentNullException(nameof(appServices));
        }

        public IServiceProvider ApplicationServices { get; }

        public IApplicationBuilder Use(ApplicationDelegate app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            return this;
        }

        public ApplicationDelegate Build()
        {
            return _app ?? ApplicationServices.GetRequiredService<ApplicationDelegate>();
        }
    }
}
