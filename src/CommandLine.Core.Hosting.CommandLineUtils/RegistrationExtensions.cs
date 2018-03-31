using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace CommandLine.Core.Hosting.CommandLineUtils
{
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Provides service registration for commands.
        /// </summary>
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

            public ICommandServiceCollection SingletonBase<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddSingleton<CommandLineApplication, TCommand>());

            public ICommandServiceCollection ScopedBase<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddScoped<CommandLineApplication, TCommand>());

            public ICommandServiceCollection TransientBase<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddTransient<CommandLineApplication, TCommand>());

            public ICommandServiceCollection SingletonChild<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddSingleton<TCommand>());

            public ICommandServiceCollection ScopedChild<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddScoped<TCommand>());

            public ICommandServiceCollection TransientChild<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddTransient<TCommand>());

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
        ICommandServiceCollection SingletonBase<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a root level command.
        /// </summary>
        ICommandServiceCollection ScopedBase<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a root level command.
        /// </summary>
        ICommandServiceCollection TransientBase<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a child command.
        /// </summary>
        ICommandServiceCollection SingletonChild<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a child command.
        /// </summary>
        ICommandServiceCollection ScopedChild<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a child command.
        /// </summary>
        ICommandServiceCollection TransientChild<TCommand>() where TCommand : CommandLineApplication;
    }
}
