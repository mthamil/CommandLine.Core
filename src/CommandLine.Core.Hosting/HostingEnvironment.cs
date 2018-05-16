using CommandLine.Core.Hosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace CommandLine.Core.Hosting
{
    class HostingEnvironment : IHostingEnvironment
    {
        public HostingEnvironment(IConfiguration config)
        {
            ApplicationName = config[HostDefaults.ApplicationNameKey];
            EnvironmentName = config[HostDefaults.EnvironmentNameKey] ?? EnvironmentName;
            WorkingDirectory = config[HostDefaults.WorkingDirectoryKey] ?? WorkingDirectory;
            WorkingDirectoryFileProvider = new PhysicalFileProvider(WorkingDirectory);
        }

        public string ApplicationName { get; }

        public string EnvironmentName { get; } = "Development";

        public string WorkingDirectory { get; } = Directory.GetCurrentDirectory();

        public IFileProvider WorkingDirectoryFileProvider { get; }
    }
}
