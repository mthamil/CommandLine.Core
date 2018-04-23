using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;

namespace CommandLineUtils.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="CommandLineApplication"/>s.
    /// </summary>
    public static class CommandExtensions
    {
        /// <summary>
        /// Enumerates a command's parent chain up to the root command.
        /// This includes the starting command.
        /// </summary>
        /// <param name="start">The command to start with.</param>
        public static IEnumerable<CommandLineApplication> CommandChain(this CommandLineApplication start)
        {
            var current = start;
            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }
    }
}
