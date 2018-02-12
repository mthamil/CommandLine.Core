namespace CommandLine.Core.Hosting.Abstractions
{
    public interface IHostingEnvironment
    {
        string ApplicationName { get; }

        string EnvironmentName { get; }
    }
}
