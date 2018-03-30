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
        public static ICommandServiceCollection Commands(this IServiceCollection services) => new CommandServiceCollection(services);

        class CommandServiceCollection : ICommandServiceCollection
        {
            private readonly IServiceCollection _services;

            public CommandServiceCollection(IServiceCollection services)
            {
                _services = services ?? throw new ArgumentNullException(nameof(services));
            }

            public ICommandServiceCollection AddSingletonBaseCommand<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddSingleton<CommandLineApplication, TCommand>());

            public ICommandServiceCollection AddScopedBaseCommand<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddScoped<CommandLineApplication, TCommand>());

            public ICommandServiceCollection AddTransientBaseCommand<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddTransient<CommandLineApplication, TCommand>());

            public ICommandServiceCollection AddSingletonChildCommand<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddSingleton<TCommand>());

            public ICommandServiceCollection AddScopedChildCommand<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddScoped<TCommand>());

            public ICommandServiceCollection AddTransientChildCommand<TCommand>() where TCommand : CommandLineApplication =>
                Add(s => s.AddTransient<TCommand>());

            private ICommandServiceCollection Add(Action<IServiceCollection> add)
            {
                add(_services);
                return this;
            }

            public ICollection<ServiceDescriptor> Services => _services;
        }
    }

    public interface ICommandServiceCollection
    {
        /// <summary>
        /// Adds a root level command.
        /// </summary>
        ICommandServiceCollection AddSingletonBaseCommand<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a root level command.
        /// </summary>
        ICommandServiceCollection AddScopedBaseCommand<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a root level command.
        /// </summary>
        ICommandServiceCollection AddTransientBaseCommand<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a child command.
        /// </summary>
        ICommandServiceCollection AddSingletonChildCommand<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a child command.
        /// </summary>
        ICommandServiceCollection AddScopedChildCommand<TCommand>() where TCommand : CommandLineApplication;

        /// <summary>
        /// Adds a child command.
        /// </summary>
        ICommandServiceCollection AddTransientChildCommand<TCommand>() where TCommand : CommandLineApplication;

        ICollection<ServiceDescriptor> Services { get; }
    }
}
