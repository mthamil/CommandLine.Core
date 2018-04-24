using CommandLineUtils.Extensions.Utilities;
using Xunit;

namespace CommandLineUtils.Extensions.Tests.Utilities
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("enabled", "Enabled")]
        [InlineData("by-name", "ByName")]
        [InlineData("by_name", "ByName")]
        public void Test_ToPascalCase(string input, string expected)
        {
            // Act.
            var actual = input.ToPascalCase();

            // Assert.
            Assert.Equal(expected, actual);
        }
    }
}
