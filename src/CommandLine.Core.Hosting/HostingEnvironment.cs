using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.Configuration;

namespace CommandLine.Core.Hosting
{
    class HostingEnvironment : IHostingEnvironment
    {
        public void Initialize(IConfiguration config)
        {
            ApplicationName = config[HostDefaults.ApplicationNameKey];
            EnvironmentName = config[HostDefaults.EnvironmentNameKey] ?? EnvironmentName;
        }

        public string ApplicationName { get; set; }

        public string EnvironmentName { get; set; } = "Development";
    }
}
