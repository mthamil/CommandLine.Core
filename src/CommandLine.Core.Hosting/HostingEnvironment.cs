using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace CommandLine.Core.Hosting
{
    class HostingEnvironment : IHostingEnvironment
    {
        public void Initialize(IConfiguration config)
        {
            ApplicationName = config[HostDefaults.ApplicationNameKey];
            EnvironmentName = config[HostDefaults.EnvironmentNameKey] ?? EnvironmentName;
            WorkingDirectory = config[HostDefaults.WorkingDirectoryKey] ?? Directory.GetCurrentDirectory();
            WorkingDirectoryFileProvider = new PhysicalFileProvider(WorkingDirectory);
        }

        public string ApplicationName { get; set; }

        public string EnvironmentName { get; set; } = "Development";

        public string WorkingDirectory { get; set; }

        public IFileProvider WorkingDirectoryFileProvider { get; set; }
    }
}
