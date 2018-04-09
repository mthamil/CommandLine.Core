namespace CommandLine.Core.Hosting
{
    /// <summary>
    /// Contains common application configuration keys.
    /// </summary>
    public static class HostDefaults
    {
        /// <summary>
        /// The application name configuration key.
        /// </summary>
        public static string ApplicationNameKey = "ApplicationName";

        /// <summary>
        /// The environment name configuration key.
        /// </summary>
        public static string EnvironmentNameKey = "EnvironmentName";

        /// <summary>
        /// The current working directory configuration key.
        /// </summary>
        public static string WorkingDirectoryKey = "WorkingDirectory";

        /// <summary>
        /// The configuration key for whether to allow unknown arguments.
        /// </summary>
        public static string AllowUnknownArgumentsKey = "AllowUnknownArguments";
    }
}
