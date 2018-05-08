using CommandLineUtils.Extensions.Conventions;
using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using Xunit;

namespace CommandLineUtils.Extensions.Tests.Conventions
{
    public class OptionPropertiesConventionTests
    {
        private readonly CommandLineApplication<AppModel> _app;

        public OptionPropertiesConventionTests()
        {
            _app = new CommandLineApplication<AppModel>();
            _app.Conventions.SetSubcommandPropertyOnModel();

            _app.Option<string>("-n|--name", string.Empty, CommandOptionType.SingleValue);
            _app.Option<int>("-i|--id", string.Empty, CommandOptionType.SingleValue, true);

            _app.Option<int?>("--int-value", string.Empty, CommandOptionType.SingleValue);
            _app.Option<string>("--string-value", string.Empty, CommandOptionType.SingleValue);

            _app.Command<ChildModel>("child", c =>
            {
                c.Option("--enabled", string.Empty, CommandOptionType.NoValue);
                c.Option("--description", string.Empty, CommandOptionType.SingleOrNoValue);
                c.Option<int>("--item", string.Empty, CommandOptionType.MultipleValue);
                c.Option<string>("--empty-item", string.Empty, CommandOptionType.MultipleValue);
            });
        }

        [Fact]
        public void Test()
        {
            // Arrange.
            _app.Conventions.UseOptionProperties();

            // Act.
            _app.Execute("--name", "test", "--id", "10", "child", "--enabled", "--description=testing...", "--item", "1", "--item", "2", "--item", "3");

            // Assert.
            Assert.Equal("test", _app.Model.Name);
            Assert.Equal(10, _app.Model.Id);
            Assert.Null(_app.Model.Description);
            Assert.True(_app.Model.Subcommand.Enabled);
            Assert.Equal("testing...", _app.Model.Subcommand.Description);
            Assert.Equal(10, _app.Model.Subcommand.Id);
            Assert.Equal(new[] { 1, 2, 3 }, _app.Model.Subcommand.Items);
            Assert.Empty(_app.Model.Subcommand.EmptyItems);
        }

        [Fact]
        public void Test_CustomPropertySuffix()
        {
            // Arrange.
            _app.Conventions.UseOptionProperties(propertySuffix: "Option");

            // Act.
            _app.Execute("--name", "test", "child", "--enabled");

            // Assert.
            Assert.Equal("test", _app.Model.NameOption);
            Assert.Null(_app.Model.Name);
            Assert.True(_app.Model.Subcommand.EnabledOption);
            Assert.False(_app.Model.Subcommand.Enabled);
        }

        [Fact]
        public void Test_NestedOptionsProperty()
        {
            // Arrange.
            _app.Conventions.UseOptionProperties();

            // Act.
            _app.Execute("--name", "test", "--int-value", "2", "--string-value", "value");

            // Assert.
            Assert.Equal("test", _app.Model.Name);
            Assert.Null(_app.Model.SomeOptions);
            Assert.Null(_app.Model.MoreThings);
            Assert.NotNull(_app.Model.MoreOptions);
            Assert.Equal(2, _app.Model.MoreOptions.IntValue);
            Assert.Equal("value", _app.Model.MoreOptions.StringValue);
        }

        [Fact]
        public void Test_NestedOptionsProperty_CustomSuffix()
        {
            // Arrange.
            _app.Conventions.UseOptionProperties(nestedPropertySuffix: "Things");

            // Act.
            _app.Execute("--name", "test", "--int-value", "3", "--string-value", "value");

            // Assert.
            Assert.Equal("test", _app.Model.Name);
            Assert.Null(_app.Model.SomeOptions);
            Assert.Null(_app.Model.MoreOptions);
            Assert.NotNull(_app.Model.MoreThings);
            Assert.Equal(3, _app.Model.MoreThings.IntValue);
            Assert.Equal("value", _app.Model.MoreThings.StringValue);
        }

        [Fact]
        public void Test_NestedOptionsProperty_AlreadyInstantiated()
        {
            // Arrange.
            _app.Conventions.UseOptionProperties();

            // Act.
            _app.Execute("--int-value", "2", "--string-value", "value");

            // Assert.
            Assert.NotNull(_app.Model.PreinstantiatedOptions);
            Assert.Equal(2, _app.Model.PreinstantiatedOptions.IntValue);
            Assert.Equal("value", _app.Model.PreinstantiatedOptions.StringValue);
        }

        public class AppModel
        {
            public string Name { get; set; }

            public string NameOption { get; set; }

            public int Id { get; set; }

            public string Description { get; set; }

            public string DisplayName => $"{Name}:{Id}";

            public NestedWithConstructor SomeOptions { get; set; }

            public NestedWithConstructor PreinstantiatedOptions { get; set; } = new NestedWithConstructor(null, null);

            public Nested MoreOptions { get; set; }

            public Nested MoreThings { get; set; }

            public ChildModel Subcommand { get; set; }
        }

        public class ChildModel
        {
            public bool Enabled { get; set; }

            public bool EnabledOption { get; set; }

            public int Id { get; set; }

            public string Description { get; set; }

            public IEnumerable<int> Items { get; set; }

            public IEnumerable<string> EmptyItems { get; set; }
        }

        public class Nested
        {
            public int? IntValue { get; set; }

            public string StringValue { get; set; }
        }

        public class NestedWithConstructor
        {
            public NestedWithConstructor(int? intValue, string stringValue)
            {
                IntValue = intValue;
                StringValue = stringValue;
            }

            public int? IntValue { get; set; }

            public string StringValue { get; set; }
        }
    }
}
