using CommandLine.Core.CommandLineUtils.Tests.Application;
using System.Threading.Tasks;
using Xunit;

namespace CommandLine.Core.CommandLineUtils.Tests
{
    public class Tests : IClassFixture<AppFixture>
    {
        private readonly AppFixture _fixture;

        public Tests(AppFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Test_Integration()
        {
            // Act.
            var result = await _fixture.CreateApp("first second --value 5")
                                       .RunAsync();

            // Assert.
            Assert.Equal(5, result);
        }
    }
}
