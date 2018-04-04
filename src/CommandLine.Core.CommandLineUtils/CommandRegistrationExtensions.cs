using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CommandLine.Core.CommandLineUtils
{
    public static class CommandRegistrationExtensions
    {
        /// <summary>
        /// Defines and registers commands with the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add commands to.</param>
        /// <param name="configureCommands">A function that enables defining commands.</param>
        public static IServiceCollection AddCommands(this IServiceCollection services, Action<ICommandServiceCollection> configureCommands)
        {
            var builder = new CommandServiceCollection();
            configureCommands(builder);
            builder.Apply(services);
            return services;
        }

        class CommandServiceCollection : ICommandServiceCollection
        {
            private readonly ICollection<Action<IServiceCollection>> _commandRegistrations = new List<Action<IServiceCollection>>();

            public ICommandServiceCollection Base<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddScoped<CommandLineApplication, TCommand>());

            public ICommandServiceCollection Child<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddScoped<TCommand>());

            public void Apply(IServiceCollection services)
            {
                foreach (var registration in _commandRegistrations)
                    registration(services);
            }

            private ICommandServiceCollection Add(Action<IServiceCollection> add)
            {
                _commandRegistrations.Add(add);
                return this;
            }
        }
    }

    public interface ICommandServiceCollection
    {
        /// <summary>
        /// Adds a root level command.
        /// </summary>
        ICommandServiceCollection Base<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a child command. A child command is any command that is not at the root of an application.
        /// </summary>
        ICommandServiceCollection Child<TCommand>() where TCommand : CommandLineApplication;
    }
}
