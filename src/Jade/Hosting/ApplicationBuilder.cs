// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs;
using Jade.Ecs.Plugins;
using Jade.Ecs.Systems;

namespace Jade.Hosting;

/// <summary>
/// Provides functionality to configure and build an application with ECS plugins, systems, and resources.
/// </summary>
public sealed class ApplicationBuilder
{
    private readonly List<PluginBase> _plugins;
    private readonly World _world;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationBuilder"/> class.
    /// </summary>
    public ApplicationBuilder()
    {
        _plugins = [];
        _world = new World();
    }

    /// <summary>
    /// Adds multiple plugins to the application builder.
    /// </summary>
    /// <param name="plugins">The plugins to add.</param>
    /// <returns>The current instance of <see cref="ApplicationBuilder"/>.</returns>
    public ApplicationBuilder AddPlugins(params IEnumerable<PluginBase> plugins)
    {
        foreach (var plugin in plugins)
            _plugins.Add(plugin);
        return this;
    }

    /// <summary>
    /// Adds a new plugin of type <typeparamref name="T"/> to the application builder.
    /// </summary>
    /// <typeparam name="T">The type of the plugin to add.</typeparam>
    /// <returns>The current instance of <see cref="ApplicationBuilder"/>.</returns>
    public ApplicationBuilder AddPlugin<T>()
        where T : PluginBase, new()
    {
        var plugin = new T();
        _plugins.Add(plugin);
        return this;
    }

    /// <summary>
    /// Adds an existing plugin instance to the application builder.
    /// </summary>
    /// <typeparam name="T">The type of the plugin to add.</typeparam>
    /// <param name="plugin">The plugin instance to add.</param>
    /// <returns>The current instance of <see cref="ApplicationBuilder"/>.</returns>
    public ApplicationBuilder AddPlugin<T>(T plugin)
        where T : PluginBase
    {
        _plugins.Add(plugin);
        return this;
    }

    /// <summary>
    /// Adds multiple systems to a specific stage in the ECS world.
    /// </summary>
    /// <param name="stage">The stage where the systems will be added.</param>
    /// <param name="systems">The systems to add.</param>
    /// <returns>The current instance of <see cref="ApplicationBuilder"/>.</returns>
    public ApplicationBuilder AddSystems(SystemStage stage, params IEnumerable<SystemBase> systems)
    {
        _world.AddSystems(stage, systems);
        return this;
    }

    /// <summary>
    /// Adds a new system of type <typeparamref name="T"/> to a specific stage in the ECS world.
    /// </summary>
    /// <typeparam name="T">The type of the system to add.</typeparam>
    /// <param name="stage">The stage where the system will be added.</param>
    /// <returns>The current instance of <see cref="ApplicationBuilder"/>.</returns>
    public ApplicationBuilder AddSystem<T>(SystemStage stage)
        where T : SystemBase, new()
    {
        _world.AddSystem<T>(stage);
        return this;
    }

    /// <summary>
    /// Adds an existing system instance to a specific stage in the ECS world.
    /// </summary>
    /// <typeparam name="T">The type of the system to add.</typeparam>
    /// <param name="stage">The stage where the system will be added.</param>
    /// <param name="system">The system instance to add.</param>
    /// <returns>The current instance of <see cref="ApplicationBuilder"/>.</returns>
    public ApplicationBuilder AddSystem<T>(SystemStage stage, T system)
        where T : SystemBase
    {
        _world.AddSystem(stage, system);
        return this;
    }

    /// <summary>
    /// Adds a new resource of type <typeparamref name="T"/> to the ECS world.
    /// </summary>
    /// <typeparam name="T">The type of the resource to add.</typeparam>
    /// <returns>The current instance of <see cref="ApplicationBuilder"/>.</returns>
    public ApplicationBuilder AddResource<T>()
        where T : class, new()
    {
        _world.AddResource<T>();
        return this;
    }

    /// <summary>
    /// Adds an existing resource instance to the ECS world.
    /// </summary>
    /// <typeparam name="T">The type of the resource to add.</typeparam>
    /// <param name="resource">The resource instance to add.</param>
    /// <returns>The current instance of <see cref="ApplicationBuilder"/>.</returns>
    public ApplicationBuilder AddResource<T>(T resource)
        where T : class
    {
        _world.AddResource(resource);
        return this;
    }

    /// <summary>
    /// Builds the application by configuring the ECS world with the added plugins.
    /// </summary>
    /// <returns>The configured <see cref="Application"/> instance.</returns>
    public Application Build()
    {
        foreach (var plugin in _plugins)
            plugin.Build(_world);

        return new Application(_world);
    }
}
