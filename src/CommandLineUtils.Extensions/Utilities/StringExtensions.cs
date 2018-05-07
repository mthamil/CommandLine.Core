using Humanizer;

namespace CommandLineUtils.Extensions.Utilities
{
    static class StringExtensions
    {
        /// <summary>
        /// Converts kebab or underscore case to pascal case.
        /// </summary>
        public static string ToPascalCase(this string s) => s.Replace('-', '_').Pascalize();
    }
}
