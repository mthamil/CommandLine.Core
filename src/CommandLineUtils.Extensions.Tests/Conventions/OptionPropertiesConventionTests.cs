using CommandLineUtils.Extensions.Conventions;
using McMaster.Extensions.CommandLineUtils;
using Xunit;

namespace CommandLineUtils.Extensions.Tests.Conventions
{
    public class OptionPropertiesConventionTests
    {
        private readonly CommandLineApplication<AppModel> _app;

        public OptionPropertiesConventionTests()
        {
            _app = new CommandLineApplication<AppModel>();
            _app.Option<string>("-n|--name", string.Empty, CommandOptionType.SingleValue);
            _app.Option<int>("-i|--id", string.Empty, CommandOptionType.SingleValue);

            _app.Conventions.UseOptionProperties();
        }

        [Fact]
        public void Test()
        {
            // Act.
            _app.Execute("--name", "test", "--id", "10");

            // Assert.
            Assert.Equal("test", _app.Model.Name);
            Assert.Equal(10, _app.Model.Id);
            Assert.Null(_app.Model.Description);
        }

        public class AppModel
        {
            public string Name { get; set; }

            public int Id { get; set; }

            public string Description { get; set; }

            public string DisplayName => $"{Name}:{Id}";
        }
    }
}
