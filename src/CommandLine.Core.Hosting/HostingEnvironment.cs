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
            WorkingDirectory = config[HostDefaults.WorkingDirectoryKey] ?? WorkingDirectory;
            WorkingDirectoryFileProvider = new PhysicalFileProvider(WorkingDirectory);
        }

        public string ApplicationName { get; private set; }

        public string EnvironmentName { get; private set; } = "Development";

        public string WorkingDirectory { get; private set; } = Directory.GetCurrentDirectory();

        public IFileProvider WorkingDirectoryFileProvider { get; private set; }
    }
}
