using Microsoft.Extensions.FileProviders;

namespace CommandLine.Core.Hosting.Abstractions
{
    public interface IHostingEnvironment
    {
        string ApplicationName { get; }

        string EnvironmentName { get; }

        string WorkingDirectory { get; }

        IFileProvider WorkingDirectoryFileProvider { get; }
    }
}
