using Microsoft.Extensions.FileProviders;

namespace CommandLine.Core.Hosting.Abstractions
{
    /// <summary>
    /// Provides information about the environment in which an application is running.
    /// </summary>
    public interface IHostingEnvironment
    {
        /// <summary>
        /// The application's name.
        /// </summary>
        string ApplicationName { get; }

        /// <summary>
        /// The application's environment. This is automatically populated from environment variables.
        /// </summary>
        string EnvironmentName { get; }

        /// <summary>
        /// The application's working directory.
        /// </summary>
        string WorkingDirectory { get; }

        /// <summary>
        /// An <see cref="IFileProvider"/> for the application's <see cref="WorkingDirectory"/>.
        /// </summary>
        IFileProvider WorkingDirectoryFileProvider { get; }
    }
}
