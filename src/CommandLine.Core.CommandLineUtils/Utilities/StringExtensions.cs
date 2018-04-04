using System.Text.RegularExpressions;

namespace CommandLine.Core.CommandLineUtils.Utilities
{
    static class StringExtensions
    {
        public static string ToPascalCase(this string s) =>
            Regex.Replace(s, "(_|-|^)[a-z]", m => m.Value.TrimStart('-').ToUpperInvariant());
    }
}
